/**
 * Name: Travis Bode
 * Date: 7/10/2024
 * 
 * 
 * This is my C# project.
 * 
 * It is a Mastermind-like game that contains a GUI for playing the game.
 * 
 * The player has 5 colors to choose from, which they use to guess a password that
 * contains 4 colors. They have unlimited attempts to guess the password. Feedback
 * is given for incorrect and misplaced colors.
 * 
 * This game can be played with 1 or 2 human players.
 * 
 * If playing alone: the computer creates a random password that the player must try to guess.
 * 
 * If playing with someone else: one player gets to choose the password that must be guessed,
 * and the other player must try to guess that password.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CSMastermindProject
{
    public partial class Mastermind : Form
    {
        // the image of the current selected color
        private Image selectedColorImage;

        // the name of the current selected color
        private string selectedColorName;

        // the list of guesses that the player has made
        // these are displayed to the player
        private List<Control> guessList;

        // the amount of colors that the player must put into a guess
        private const int GUESS_LENGTH = 4;

        // the guess that the opponent (computer or friend) makes before the game starts
        private string[] opponentGuess = new string[Mastermind.GUESS_LENGTH];

        // this is used to prevent the computer's random password from overriding the player's custom password
        private bool isComputerPlaying = true;

        // the current guess count
        private int guess;

        private bool isGameOver;

        // each player's guess slot is named this by default
        // this marks a slot that the player has not put a guess into
        public const string EMPTY_GUESS_SLOT = "Empty Slot";

        public Mastermind()
        {
            InitializeComponent();
            this.StartGame();
        }

        /**
         * Set the opponent's guess (the password to guess) to the player's custom password
         * Used when playing against a friend
         */
        public Mastermind(string[] playerPassword)
        {
            // mark that the computer is NOT playing
            this.isComputerPlaying = false;

            // Set the opponent's guess to the player's custom password
            for (int i = 0; i < Mastermind.GUESS_LENGTH; i++)
            {
                this.opponentGuess[i] = playerPassword[i];
            }
            InitializeComponent();
            this.StartGame();
        }

        /**
         * Sets up the game to be ready to be played.
         */
        private void StartGame()
        {
            this.isGameOver = false;

            // generate a guess for the computer if playing against it
            if (isComputerPlaying)
            {
                this.opponentGuess = new string[Mastermind.GUESS_LENGTH];
                Random rnd = new Random();
                for (int i = 0; i < Mastermind.GUESS_LENGTH; i++)
                {
                    // generate a random number between 1 and 5 (inclusive)
                    // then use that number to pick a color
                    int number = rnd.Next(1, 6);
                    switch (number)
                    {
                        case 1:
                            opponentGuess[i] = "Black";
                            break;
                        case 2:
                            opponentGuess[i] = "Blue";
                            break;
                        case 3:
                            opponentGuess[i] = "Brown";
                            break;
                        case 4:
                            opponentGuess[i] = "Orange";
                            break;
                        case 5:
                            opponentGuess[i] = "Purple";
                            break;
                    }
                }
            }

            // This outputs the opponent's guess to the console
            // uncomment if desired
            //foreach (string slot in this.opponentGuess) { Console.WriteLine("Opponent guess: " + slot); }

            // get rid of any guesses from a previous game
            // this removes all guesses from the GUI
            this.FlowLayoutPanelGuesses.Controls.Clear();

            // this removes all guesses from the internal guess list
            this.guessList = new List<Control>();
            this.guess = 0;
            this.AddNewGuessRow();

            // these labels are used to indicate which color is currently selected
            // they have text in the form editor to make moving them around easier
            // but once the game starts, that default text should be erased
            foreach (Label label in this.panelColorLabels.Controls)
            {
                label.Text = "";
            }

            // make Black the default selected color
            this.ChangeSelectedColor("Black");

            this.buttonSubmitGuess.Enabled = true;
            this.buttonNewGame.Enabled = false;
        }
        
        /**
         * Adds a new password guess row for the player to enter their next guess
         */
        private void AddNewGuessRow()
        {
            this.guess++;

            // disable the previous guess row if this is not the first guess
            if (this.guess > 1)
            {
                // disable each PictureBox in the previous guess row
                // this way, the player cannot modify a previous guess
                // a guess row contains 4 PictureBoxes and 1 feedback box
                for (int i = this.guessList.Count - 2; i >= this.guessList.Count - 5; i--)
                {
                    PictureBox pb = (PictureBox)this.guessList[i];
                    pb.Enabled = false;
                }
            }

            // put PictureBoxes in each guess slot
            for (int i = 0; i < Mastermind.GUESS_LENGTH; i++)
            {

                PictureBox pb = new PictureBox()
                {
                    Name = Mastermind.EMPTY_GUESS_SLOT,
                    Image = Properties.Resources.EmptySlot3,
                    Size = new Size(100, 70),
                    Padding = new Padding(0, 10, 0, 0), // set the top padding to 10
                    SizeMode = PictureBoxSizeMode.Zoom // this makes the colors appear better
                };
                
                // Make each PictureBox respond to clicks
                pb.MouseClick += Pb_MouseClick;
                this.guessList.Add(pb);


            }

            // this is the feedback area of each guess
            RichTextBox rtb = new RichTextBox() { Text = this.guess + ": ", Size = new Size(350, 45), Margin = new Padding(3, 15, 3, 15),
                ReadOnly = true, Font = new Font(FontFamily.GenericSansSerif, 14.25f)};
            this.guessList.Add(rtb);

            // add all of the controls in the guessList to the flow panel
            this.FlowLayoutPanelGuesses.Controls.AddRange(this.guessList.ToArray());
        }

        /**
         * Reacts to a guess PictureBox being clicked.
         * Sets the color (image) of the PictureBox to the currently selected color.
         */
        private void Pb_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            pb.Name = this.selectedColorName;
            pb.Image = this.selectedColorImage;
        }

        /**
         * Changes the currently selected color (image) to one that the player wants.
         */
        void ChangeSelectedColor(string color)
        {

            // clear the text of all the color labels
            foreach (Label label in this.panelColorLabels.Controls)
            {
                label.Text = "";
            }

            // then add an arrow in front of the newly selected color
            switch (color)
            {
                case "Black":
                    this.selectedColorImage = Properties.Resources.Black;
                    this.selectedColorName = "Black";
                    this.labelBlackColor.Text = "-->";
                    break;
                case "Blue":
                    this.selectedColorImage = Properties.Resources.Blue;
                    this.selectedColorName = "Blue";
                    this.labelBlueColor.Text = "-->";
                    break;
                case "Brown":
                    this.selectedColorImage = Properties.Resources.Brown;
                    this.selectedColorName = "Brown";
                    this.labelBrownColor.Text = "-->";
                    break;
                case "Orange":
                    this.selectedColorImage = Properties.Resources.Orange;
                    this.selectedColorName = "Orange";
                    this.labelOrangeColor.Text = "-->";
                    break;
                case "Purple":
                    this.selectedColorImage = Properties.Resources.Purple;
                    this.selectedColorName = "Purple";
                    this.labelPurpleColor.Text = "-->";
                    break;

            }

        }

        /**
         * Checks if the player's guess is correct.
         */
        private void CheckGuess()
        {
            // get the current guess row that the player is on
            // (each guess row has 4 guess slots and a text box.
            // Get the 2nd to last element, all the way to the
            // 5th to last, because there are 4 guess slots).
            PictureBox[] guess = new PictureBox[Mastermind.GUESS_LENGTH]
                                    {(PictureBox)this.guessList[this.guessList.Count - 5],
                                    (PictureBox)this.guessList[this.guessList.Count - 4],
                                    (PictureBox)this.guessList[this.guessList.Count - 3],
                                    (PictureBox)this.guessList[this.guessList.Count - 2]};
                                    

            // this array keeps track of whether a color in a guess slot is correct or incorrect
            // misplaced colors are not tracked here
            bool[] correctColors = new bool[Mastermind.GUESS_LENGTH] { false, false, false, false };

            // check each guess slot for correctness (NOT misplaced colors)
            for (int i = 0; i < Mastermind.GUESS_LENGTH; i++)
            {
                if (guess[i].Name == this.opponentGuess[i])
                {
                    correctColors[i] = true;
                }

                // if a slot is empty, display an error dialog box then cancel the check
                if (guess[i].Name == Mastermind.EMPTY_GUESS_SLOT)
                {
                    MessageBox.Show(text: "All guess slots must be filled.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                    return;
                }
            }

            // this array keeps track of whether a color in a given slot is misplaced
            bool[] misplacedColors = new bool[Mastermind.GUESS_LENGTH] { false, false, false, false };

            // NOW check for misplaced colors
            for (int playerGuessIndex = 0; playerGuessIndex < Mastermind.GUESS_LENGTH; playerGuessIndex++)
            {
                // if the guess is incorrect, examine it further
                if (guess[playerGuessIndex].Name != this.opponentGuess[playerGuessIndex])
                {
                    // if this color is in the code but NOT in this slot,
                    // AND that color is not guessed correctly somewhere else
                    // in the code, then it is misplaced
                    for (int computerGuessIndex = 0; computerGuessIndex < Mastermind.GUESS_LENGTH; computerGuessIndex++)
                    {
                        if (guess[playerGuessIndex].Name == this.opponentGuess[computerGuessIndex] && correctColors[computerGuessIndex] == false)
                        {
                            misplacedColors[playerGuessIndex] = true;
                        }
                    }


                }
            }

            // add up the number of correct and misplaced guesses
            int totalCorrect = 0;
            int totalMisplaced = 0;
            for (int i = 0; i < Mastermind.GUESS_LENGTH; i++)
            {
                if (correctColors[i]) totalCorrect++;
                if (misplacedColors[i]) totalMisplaced++;
            }

            // give the player feedback on their guess
            RichTextBox feedbackBox = (RichTextBox)this.guessList[this.guessList.Count - 1];

            // if the player correctly guessed the password
            if (totalCorrect == Mastermind.GUESS_LENGTH)
            {
                feedbackBox.Text += "You guessed the password!";
                feedbackBox.BackColor = Color.LawnGreen;
                this.isGameOver = true;
                this.buttonSubmitGuess.Enabled = false;
                this.buttonNewGame.Enabled = true;
                this.FlowLayoutPanelGuesses.ScrollControlIntoView(this.guessList[this.guessList.Count - 1]);
            }

            // if the player got no colors correct, and possibly misplaced others
            else if (totalCorrect == 0)
            {
                feedbackBox.Text += $"Correct: {totalCorrect}    Misplaced: {totalMisplaced}";
                feedbackBox.BackColor = Color.Red;
            }

            // if the player got some colors correct but misplaced others
            else
            {
                feedbackBox.Text += $"Correct: {totalCorrect}    Misplaced: {totalMisplaced}";
                feedbackBox.BackColor = Color.Orange;
            }

            // add a new guess row if the game is not over yet
            if (!isGameOver)
            {
                this.AddNewGuessRow();
            }
        }

        /**
         * These event handlers react to a color being selected on the left side of the Form.
         */
        private void PictureBoxBlack_Click(object sender, EventArgs e)
        {
            this.ChangeSelectedColor("Black");
        }

        private void PictureBoxBlue_Click(object sender, EventArgs e)
        {
            this.ChangeSelectedColor("Blue");
        }

        private void PictureBoxBrown_Click(object sender, EventArgs e)
        {
            this.ChangeSelectedColor("Brown");
        }

        private void PictureBoxOrange_Click(object sender, EventArgs e)
        {
            this.ChangeSelectedColor("Orange");
        }

        private void PictureBoxPurple_Click(object sender, EventArgs e)
        {
            this.ChangeSelectedColor("Purple");
        }

        /**
         * These event handlers react to the buttons on the GUI
         */
        private void ButtonSubmitGuess_Click(object sender, EventArgs e)
        {
            this.CheckGuess();
        }

        private void ButtonNewGame_Click(object sender, EventArgs e)
        {
            MainMenu menu = new MainMenu();
            menu.Show();
            this.Hide();
        }

        /**
         * Close the app when the user closes the form
         */
        private void Mastermind_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
