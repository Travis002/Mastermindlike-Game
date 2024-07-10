
namespace CSMastermindProject
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPlayAgainstComputer = new System.Windows.Forms.Button();
            this.btnPlayAgainstAFriend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPlayAgainstComputer
            // 
            this.btnPlayAgainstComputer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayAgainstComputer.Location = new System.Drawing.Point(207, 126);
            this.btnPlayAgainstComputer.Name = "btnPlayAgainstComputer";
            this.btnPlayAgainstComputer.Size = new System.Drawing.Size(358, 55);
            this.btnPlayAgainstComputer.TabIndex = 0;
            this.btnPlayAgainstComputer.Text = "Play Against The Computer";
            this.btnPlayAgainstComputer.UseVisualStyleBackColor = true;
            this.btnPlayAgainstComputer.Click += new System.EventHandler(this.BtnPlayAgainstComputer_Click);
            // 
            // btnPlayAgainstAFriend
            // 
            this.btnPlayAgainstAFriend.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayAgainstAFriend.Location = new System.Drawing.Point(207, 242);
            this.btnPlayAgainstAFriend.Name = "btnPlayAgainstAFriend";
            this.btnPlayAgainstAFriend.Size = new System.Drawing.Size(358, 55);
            this.btnPlayAgainstAFriend.TabIndex = 1;
            this.btnPlayAgainstAFriend.Text = "Play Against A Friend";
            this.btnPlayAgainstAFriend.UseVisualStyleBackColor = true;
            this.btnPlayAgainstAFriend.Click += new System.EventHandler(this.BtnPlayAgainstAFriend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(298, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Game Mode";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPlayAgainstAFriend);
            this.Controls.Add(this.btnPlayAgainstComputer);
            this.Name = "MainMenu";
            this.Text = "Main Menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenu_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPlayAgainstComputer;
        private System.Windows.Forms.Button btnPlayAgainstAFriend;
        private System.Windows.Forms.Label label1;
    }
}