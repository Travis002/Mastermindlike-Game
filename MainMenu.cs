/**
 * Name: Travis Bode
 * Date: 6/30/2024
 * 
 * This main menu currently allows the user to play
 * against the computer or against a friend.
 * 
 */

using System;
using System.Windows.Forms;

namespace CSMastermindProject
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        /**
         * Play against the computer
         */
        private void BtnPlayAgainstComputer_Click(object sender, EventArgs e)
        {
            Mastermind game = new Mastermind();
            game.Show();
            this.Hide();
        }

        
        /**
         * Play against a friend
         */
        private void BtnPlayAgainstAFriend_Click(object sender, EventArgs e)
        {
            SetPassword setPassword = new SetPassword();
            setPassword.Show();
            this.Hide();
        }


        /**
         * Close the app when the user closes the window
         */
        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
