/**
 * Name: Travis Bode
 * Date: 6/30/2024
 * 
 * This is where the user will set the password
 * for another person to guess.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CSMastermindProject
{
    public partial class SetPassword : Form
    {
        // the image of the current selected color
        private Image selectedColorImage;

        // the name of the current selected color
        private string selectedColorName;

        // these are the controls that allow a player to input a password
        private List<Control> playerPasswordGUI = new List<Control>();

        // this is the actual password that the player inputs
        private string[] playerPassword = new string[SetPassword.GUESS_LENGTH];

        // the amount of colors that the player must put into a guess
        private const int GUESS_LENGTH = 4;

        // each player's guess slot is named this by default
        // this marks a slot that the player has not put a guess into
        public const string EMPTY_GUESS_SLOT = "Empty Slot";

        public SetPassword()
        {
            InitializeComponent();
            this.Setup();
        }

        /**
         * Sets up the game to be ready to be played.
         */
        private void Setup()
        {

            // these labels are used to indicate which color is currently selected
            // they have text in the form editor to make moving them around easier
            // but once the game starts, that default text should be erased
            foreach (Label label in this.panelColorLabels.Controls)
            {
                label.Text = "";
            }

            // make Black the default selected color
            this.ChangeSelectedColor("Black");

            this.btnSetPassword.Enabled = true;

            // put PictureBoxes in each guess slot
            this.playerPasswordGUI = new List<Control>();

            for (int i = 0; i < SetPassword.GUESS_LENGTH; i++)
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
                this.playerPasswordGUI.Add(pb);


            }

            // add all of the controls in the guessList to the flow panel
            this.FlowLayoutPanelGuesses.Controls.AddRange(this.playerPasswordGUI.ToArray());
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

        private void ButtonNewGame_Click(object sender, EventArgs e)
        {
            this.Setup();
        }

        /**
         * Close the app when the user closes the form
         */
        private void Mastermind_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /**
         * Launch the main game with the user's custom password
         */
        private void BtnSetPassword_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < SetPassword.GUESS_LENGTH; i++)
            {
                this.playerPassword[i] = this.FlowLayoutPanelGuesses.Controls[i].Name;
                Console.WriteLine("Player input: " + this.playerPassword[i]);
                Console.ReadLine();
            }

            Mastermind game = new Mastermind(this.playerPassword);
            game.Show();
            this.Hide();
        }
    }
}
