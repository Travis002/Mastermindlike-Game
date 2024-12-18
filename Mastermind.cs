/**
 * Name: Travis Bode
 * Date: 12/18/2024
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
 * This game can be played alone or with another human player.
 * 
 * If playing alone: the computer creates a random password that the player must try
 * to guess.
 * 
 * If playing with someone else: one player gets to choose the password that must be
 * guessed, and the other player must try to guess that password.
 * 
 * This app supports integration with an Arduino microcontroller for using LED lights
 * to indicate misplaced and correct guesses. Misplaced guesses are indicated with
 * yellow LEDs, and correct guesses are indicated with green LEDs. After the game is
 * won, a short LED light show is displayed using the yellow and green LEDs.
 * 
 * For more information about Arduino integration, refer to the GitHub page for this app.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;

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

        // these store the current guess's correct and misplaced colors
        private bool[] correctColors;
        private bool[] misplacedColors;

        private bool isGameOver;

        // each player's guess slot is named this by default
        // this marks a slot that the player has not put a guess into
        public const string EMPTY_GUESS_SLOT = "Empty Slot";

        // this is the serial port used by the Arduino.
        private SerialPort serialPort { get; set; }

        // this contains whether the serial port for the Arduino is being used during the game
        // the Arduino must be plugged in before starting the game for it to be used
        private bool isSerialPortBeingUsed = false;

        // these codes are used to tell the Arduino various things about the game
        // this is used to tell the Arduino to turn off (reset) all LEDs
        private const string ARDUINO_LED_RESET_CODE = "R";

        // this is used to tell the Arduino the game has been won, and to display the LED light show
        private const string ARDUINO_WIN_CODE = "W";

        // this is used to tell the Arduino to turn on (activate) the red LED status light
        private const string ARDUINO_ACTIVATED_CODE = "A";

        // this is used to tell the Arduino to turn off (deactivate) the red LED status light
        private const string ARDUINO_DEACTIVATED_CODE = "D";

        // the 4 green LEDs' codes (used to tell the Arduino which to turn on)
        private const int GREEN0_CODE = 4;
        private const int GREEN1_CODE = 5;
        private const int GREEN2_CODE = 6;
        private const int GREEN3_CODE = 7;

        private int[] greens;

        // the 4 yellow LEDs' codes (used to tell the Arduino which to turn on)
        private const int YELLOW0_CODE = 0;
        private const int YELLOW1_CODE = 1;
        private const int YELLOW2_CODE = 2;
        private const int YELLOW3_CODE = 3;

        private int[] yellows;

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
            // create the serial port for the Arduino to use
            this.serialPort = new SerialPort()
            {
                // the specific USB (serial) port the Arduino is plugged in to
                // open Device Manager to see which port your Arduino is using
                PortName = "COM4",
                BaudRate = 9600
            };

            // open the serial port used by the Arduino
            bool didOpenSerialPortWork = this.ArduinoSerialPortConnection("Open");

            // set the variable that contains whether the Arduino is being used
            if (didOpenSerialPortWork)
            {
                this.isSerialPortBeingUsed = true;
            }
            else
            {
                this.isSerialPortBeingUsed = false;
            }

            this.yellows = new int[] { Mastermind.YELLOW0_CODE, Mastermind.YELLOW1_CODE, Mastermind.YELLOW2_CODE, Mastermind.YELLOW3_CODE };
            this.greens = new int[] { Mastermind.GREEN0_CODE, Mastermind.GREEN1_CODE, Mastermind.GREEN2_CODE, Mastermind.GREEN3_CODE };

            this.isGameOver = false;

            // this array keeps track of whether a color in a guess slot is correct or incorrect
            // misplaced colors are not tracked here
            this.correctColors = new bool[Mastermind.GUESS_LENGTH] { false, false, false, false };

            // this array keeps track of whether a color in a given slot is misplaced
            this.misplacedColors = new bool[Mastermind.GUESS_LENGTH] { false, false, false, false };

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
                            this.opponentGuess[i] = "Black";
                            break;
                        case 2:
                            this.opponentGuess[i] = "Blue";
                            break;
                        case 3:
                            this.opponentGuess[i] = "Brown";
                            break;
                        case 4:
                            this.opponentGuess[i] = "Orange";
                            break;
                        case 5:
                            this.opponentGuess[i] = "Purple";
                            break;
                    }
                }
            }

            // This outputs the opponent's guess to the console
            // uncomment if desired
            //foreach (string slot in this.opponentGuess) { Console.WriteLine("Opponent guess: " + slot); }

            // get rid of any guesses from a previous game
            // this removes all guesses from the GUI
            this.flowLayoutPanelGuesses.Controls.Clear();

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
            this.flowLayoutPanelGuesses.Controls.AddRange(this.guessList.ToArray());
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
                                    

            // reset the correct and misplaced colors arrays
            for (int i = 0; i < Mastermind.GUESS_LENGTH; i++)
            {
                this.correctColors[i] = false;
                this.misplacedColors[i] = false;
            }

            // check each guess slot for correctness (NOT misplaced colors)
            for (int i = 0; i < Mastermind.GUESS_LENGTH; i++)
            {
                if (guess[i].Name == this.opponentGuess[i])
                {
                    this.correctColors[i] = true;
                }

                // if a slot is empty, display an error dialog box then cancel the check
                if (guess[i].Name == Mastermind.EMPTY_GUESS_SLOT)
                {
                    MessageBox.Show(text: "All guess slots must be filled.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                    return;
                }
            }

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
                        if (guess[playerGuessIndex].Name == this.opponentGuess[computerGuessIndex] && this.correctColors[computerGuessIndex] == false)
                        {
                            this.misplacedColors[playerGuessIndex] = true;
                        }
                    }


                }
            }

            // only do Arduino tasks if it is connected
            if (this.isSerialPortBeingUsed)
            {
                // turn off (reset) all Arduino LEDs
                this.ArduinoLEDReset();
            }

            // add up the number of correct and misplaced guesses
            int totalCorrect = 0;
            int totalMisplaced = 0;
            for (int i = 0; i < Mastermind.GUESS_LENGTH; i++)
            {
                if (this.correctColors[i])
                {
                    totalCorrect++;

                    // only do Arduino tasks if it is connected
                    if (this.isSerialPortBeingUsed)
                    {
                        // tell the Arduino to indicate which color was correct
                        this.ArduinoLEDGuess();
                    }
                }

                if (this.misplacedColors[i])
                {
                    totalMisplaced++;

                    // only do Arduino tasks if it is connected
                    if (this.isSerialPortBeingUsed)
                    {
                        // tell the Arduino to indicate which color was misplaced
                        this.ArduinoLEDGuess();
                    }
                }
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
                this.flowLayoutPanelGuesses.ScrollControlIntoView(this.guessList[this.guessList.Count - 1]);

                // only do Arduino tasks if it is connected
                if (this.isSerialPortBeingUsed)
                {
                    // turn off (reset) all Arduino LEDs
                    this.ArduinoLEDReset();

                    // do a LED light show on the Arduino
                    this.ArduinoWinLEDShow();

                    // close the serial port used by the Arduino
                    this.ArduinoSerialPortConnection("Close");
                }
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
            // only do Arduino tasks if it is connected
            if (this.isSerialPortBeingUsed)
            {
                // close the serial port used by the Arduino
                this.ArduinoSerialPortConnection("Close");
            }
            Application.Exit();
        }

        private void Mastermind_Load(object sender, EventArgs e)
        {
            this.ResizeControls();
        }

        /**
         * Resize the controls whenever the form changes size
         */
        private void ResizeControls()
        {
            this.buttonNewGame.Location = new Point(this.buttonNewGame.Location.X, this.Height - 50 - this.buttonNewGame.Height);
            this.buttonSubmitGuess.Location = new Point(this.buttonSubmitGuess.Location.X, this.Height - 50 - this.buttonSubmitGuess.Height);

            // when resizing the guesses panel, the controls inside it (the guess slots and feedback box) are still their same size
            // which can cause the controls to be misaligned
        }

        /**
         * Causes the controls to be resized whenever the form changes size
         */
        private void Mastermind_ClientSizeChanged(object sender, EventArgs e)
        {
            this.ResizeControls();
        }

        /**
         * Open or Close the serial port used by the Arduino
         * Returns true if the action succeeded
         * Returns false if the action didn't succeed or if the command wasn't recognized
         */
        private bool ArduinoSerialPortConnection(string state)
        {
            state = state.ToUpper();

            if (state == "OPEN")
            {
                try
                {
                    // open the connection with the serial port used by the Arduino if not already open
                    if (!this.serialPort.IsOpen)
                    {
                        this.serialPort.Open();
                    }

                    // turn on the red LED to indicate the Arduino is being used by Mastermind
                    this.serialPort.Write(Mastermind.ARDUINO_ACTIVATED_CODE);
                    Console.WriteLine($"Successfully wrote command \"{Mastermind.ARDUINO_ACTIVATED_CODE}\" to activate LED for power status.");
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to write command \"{Mastermind.ARDUINO_ACTIVATED_CODE}\" to activate LED for power status.");
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            else if (state == "CLOSE")
            {
                try
                {
                    // turn off the red LED to indicate the Arduino is no longer being used by Mastermind
                    this.serialPort.Write(Mastermind.ARDUINO_DEACTIVATED_CODE);

                    Console.WriteLine($"Successfully wrote command \"{Mastermind.ARDUINO_DEACTIVATED_CODE}\" to deactivate LED for power status.");

                    // close the connection with the serial port used by the Arduino if not already closed
                    if (this.serialPort.IsOpen)
                    {
                        this.serialPort.Close();
                    }

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to write command \"{Mastermind.ARDUINO_DEACTIVATED_CODE}\" to deactivate LED for power status.");
                    Console.WriteLine(e.Message);
                    return false;
                }
            }

            /**
             * If the command was not either "Open" or "Close", then it was not a valid command.
             * In that case, this method did not work correctly, so return false.
             */
            else
            {
                return false;
            }
        }

        /**
         * This is used to turn off (reset) all the LEDs used by the Arduino for Mastermind.
         */
        private void ArduinoLEDReset()
        {
            try
            {
                // reset the Arduino's LEDs
                this.serialPort.Write(Mastermind.ARDUINO_LED_RESET_CODE);
                Console.WriteLine($"Successfully wrote command \"{Mastermind.ARDUINO_LED_RESET_CODE}\" to reset all Arduino LEDs.");
            }
            catch
            {
                Console.WriteLine($"Failed to write command \"{Mastermind.ARDUINO_LED_RESET_CODE}\" to reset all Arduino LEDs.");
            }
        }

        /**
         * This is used to turn on the corresponding LEDs when a user's guess is checked
         */
        private void ArduinoLEDGuess()
        {
            for (int i = 0; i < Mastermind.GUESS_LENGTH; i++)
            {
                if (this.correctColors[i])
                {
                    try
                    {
                        // turn on the green LED that corresponds to the correct guess slot
                        this.serialPort.Write(this.greens[i] + "");
                        Console.WriteLine($"Successfully turned on the green LED {i} for a correct guess.");
                    }
                    catch
                    {
                        Console.WriteLine($"Failed to turn on the green LED {i} for a correct guess.");
                    }
                }

                if (this.misplacedColors[i])
                {
                    try
                    {
                        // turn on the yellow LED that corresponds to the misplaced guess slot
                        this.serialPort.Write(this.yellows[i] + "");
                        Console.WriteLine($"Successfully turned on the yellow LED {i} for a misplaced guess.");
                    }
                    catch
                    {
                        Console.WriteLine($"Failed to turn on the yellow LED {i} for a misplaced guess.");
                    }
                }
            }
        }

        /**
         * This performs a LED light show on the Arduino after the game is won
         */
        private void ArduinoWinLEDShow()
        {
            try
            {
                this.serialPort.Write(Mastermind.ARDUINO_WIN_CODE);
            }
            catch
            {
                Console.WriteLine($"Failed to write command \"{Mastermind.ARDUINO_WIN_CODE}\" to perform LED light show on Arduino.");
            }
        }
    }
}
