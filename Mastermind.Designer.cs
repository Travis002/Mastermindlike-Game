
namespace CSMastermindProject
{
    partial class Mastermind
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
            this.GroupBoxColors = new System.Windows.Forms.GroupBox();
            this.pictureBoxBlack = new System.Windows.Forms.PictureBox();
            this.pictureBoxBlue = new System.Windows.Forms.PictureBox();
            this.pictureBoxPurple = new System.Windows.Forms.PictureBox();
            this.pictureBoxBrown = new System.Windows.Forms.PictureBox();
            this.pictureBoxOrange = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanelGuesses = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelBlackColor = new System.Windows.Forms.Label();
            this.labelBlueColor = new System.Windows.Forms.Label();
            this.labelBrownColor = new System.Windows.Forms.Label();
            this.labelOrangeColor = new System.Windows.Forms.Label();
            this.labelPurpleColor = new System.Windows.Forms.Label();
            this.panelColorLabels = new System.Windows.Forms.Panel();
            this.buttonSubmitGuess = new System.Windows.Forms.Button();
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.GroupBoxColors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPurple)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBrown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOrange)).BeginInit();
            this.panelColorLabels.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxColors
            // 
            this.GroupBoxColors.Controls.Add(this.pictureBoxBlack);
            this.GroupBoxColors.Controls.Add(this.pictureBoxBlue);
            this.GroupBoxColors.Controls.Add(this.pictureBoxPurple);
            this.GroupBoxColors.Controls.Add(this.pictureBoxBrown);
            this.GroupBoxColors.Controls.Add(this.pictureBoxOrange);
            this.GroupBoxColors.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBoxColors.Location = new System.Drawing.Point(123, 63);
            this.GroupBoxColors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GroupBoxColors.Name = "GroupBoxColors";
            this.GroupBoxColors.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GroupBoxColors.Size = new System.Drawing.Size(197, 415);
            this.GroupBoxColors.TabIndex = 13;
            this.GroupBoxColors.TabStop = false;
            this.GroupBoxColors.Text = "Colors";
            // 
            // pictureBoxBlack
            // 
            this.pictureBoxBlack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxBlack.Image = global::CSMastermindProject.Properties.Resources.Black;
            this.pictureBoxBlack.Location = new System.Drawing.Point(29, 53);
            this.pictureBoxBlack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxBlack.Name = "pictureBoxBlack";
            this.pictureBoxBlack.Size = new System.Drawing.Size(133, 61);
            this.pictureBoxBlack.TabIndex = 0;
            this.pictureBoxBlack.TabStop = false;
            this.pictureBoxBlack.Click += new System.EventHandler(this.PictureBoxBlack_Click);
            // 
            // pictureBoxBlue
            // 
            this.pictureBoxBlue.Image = global::CSMastermindProject.Properties.Resources.Blue;
            this.pictureBoxBlue.Location = new System.Drawing.Point(29, 122);
            this.pictureBoxBlue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxBlue.Name = "pictureBoxBlue";
            this.pictureBoxBlue.Size = new System.Drawing.Size(133, 62);
            this.pictureBoxBlue.TabIndex = 1;
            this.pictureBoxBlue.TabStop = false;
            this.pictureBoxBlue.Click += new System.EventHandler(this.PictureBoxBlue_Click);
            // 
            // pictureBoxPurple
            // 
            this.pictureBoxPurple.Image = global::CSMastermindProject.Properties.Resources.Purple;
            this.pictureBoxPurple.Location = new System.Drawing.Point(29, 329);
            this.pictureBoxPurple.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxPurple.Name = "pictureBoxPurple";
            this.pictureBoxPurple.Size = new System.Drawing.Size(133, 62);
            this.pictureBoxPurple.TabIndex = 5;
            this.pictureBoxPurple.TabStop = false;
            this.pictureBoxPurple.Click += new System.EventHandler(this.PictureBoxPurple_Click);
            // 
            // pictureBoxBrown
            // 
            this.pictureBoxBrown.Image = global::CSMastermindProject.Properties.Resources.Brown;
            this.pictureBoxBrown.Location = new System.Drawing.Point(29, 191);
            this.pictureBoxBrown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxBrown.Name = "pictureBoxBrown";
            this.pictureBoxBrown.Size = new System.Drawing.Size(133, 62);
            this.pictureBoxBrown.TabIndex = 2;
            this.pictureBoxBrown.TabStop = false;
            this.pictureBoxBrown.Click += new System.EventHandler(this.PictureBoxBrown_Click);
            // 
            // pictureBoxOrange
            // 
            this.pictureBoxOrange.Image = global::CSMastermindProject.Properties.Resources.Orange;
            this.pictureBoxOrange.Location = new System.Drawing.Point(29, 260);
            this.pictureBoxOrange.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxOrange.Name = "pictureBoxOrange";
            this.pictureBoxOrange.Size = new System.Drawing.Size(133, 62);
            this.pictureBoxOrange.TabIndex = 4;
            this.pictureBoxOrange.TabStop = false;
            this.pictureBoxOrange.Click += new System.EventHandler(this.PictureBoxOrange_Click);
            // 
            // flowLayoutPanelGuesses
            // 
            this.flowLayoutPanelGuesses.AutoScroll = true;
            this.flowLayoutPanelGuesses.Location = new System.Drawing.Point(352, 133);
            this.flowLayoutPanelGuesses.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanelGuesses.Name = "flowLayoutPanelGuesses";
            this.flowLayoutPanelGuesses.Size = new System.Drawing.Size(1088, 484);
            this.flowLayoutPanelGuesses.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(352, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(994, 58);
            this.label1.TabIndex = 15;
            this.label1.Text = "I am thinking of a 4-color password. Can you guess it? You have unlimited attempt" +
    "s to guess.\r\nSelect a color by clicking it on the left.";
            // 
            // labelBlackColor
            // 
            this.labelBlackColor.AutoSize = true;
            this.labelBlackColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBlackColor.Location = new System.Drawing.Point(21, 28);
            this.labelBlackColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBlackColor.Name = "labelBlackColor";
            this.labelBlackColor.Size = new System.Drawing.Size(66, 29);
            this.labelBlackColor.TabIndex = 16;
            this.labelBlackColor.Text = "label";
            // 
            // labelBlueColor
            // 
            this.labelBlueColor.AutoSize = true;
            this.labelBlueColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBlueColor.Location = new System.Drawing.Point(21, 98);
            this.labelBlueColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBlueColor.Name = "labelBlueColor";
            this.labelBlueColor.Size = new System.Drawing.Size(66, 29);
            this.labelBlueColor.TabIndex = 17;
            this.labelBlueColor.Text = "label";
            // 
            // labelBrownColor
            // 
            this.labelBrownColor.AutoSize = true;
            this.labelBrownColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBrownColor.Location = new System.Drawing.Point(21, 171);
            this.labelBrownColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBrownColor.Name = "labelBrownColor";
            this.labelBrownColor.Size = new System.Drawing.Size(66, 29);
            this.labelBrownColor.TabIndex = 18;
            this.labelBrownColor.Text = "label";
            // 
            // labelOrangeColor
            // 
            this.labelOrangeColor.AutoSize = true;
            this.labelOrangeColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOrangeColor.Location = new System.Drawing.Point(21, 239);
            this.labelOrangeColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOrangeColor.Name = "labelOrangeColor";
            this.labelOrangeColor.Size = new System.Drawing.Size(66, 29);
            this.labelOrangeColor.TabIndex = 19;
            this.labelOrangeColor.Text = "label";
            // 
            // labelPurpleColor
            // 
            this.labelPurpleColor.AutoSize = true;
            this.labelPurpleColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPurpleColor.Location = new System.Drawing.Point(21, 309);
            this.labelPurpleColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPurpleColor.Name = "labelPurpleColor";
            this.labelPurpleColor.Size = new System.Drawing.Size(66, 29);
            this.labelPurpleColor.TabIndex = 20;
            this.labelPurpleColor.Text = "label";
            // 
            // panelColorLabels
            // 
            this.panelColorLabels.Controls.Add(this.labelPurpleColor);
            this.panelColorLabels.Controls.Add(this.labelOrangeColor);
            this.panelColorLabels.Controls.Add(this.labelBrownColor);
            this.panelColorLabels.Controls.Add(this.labelBlueColor);
            this.panelColorLabels.Controls.Add(this.labelBlackColor);
            this.panelColorLabels.Location = new System.Drawing.Point(11, 101);
            this.panelColorLabels.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelColorLabels.Name = "panelColorLabels";
            this.panelColorLabels.Size = new System.Drawing.Size(92, 375);
            this.panelColorLabels.TabIndex = 21;
            // 
            // buttonSubmitGuess
            // 
            this.buttonSubmitGuess.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSubmitGuess.Location = new System.Drawing.Point(187, 533);
            this.buttonSubmitGuess.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSubmitGuess.Name = "buttonSubmitGuess";
            this.buttonSubmitGuess.Size = new System.Drawing.Size(133, 84);
            this.buttonSubmitGuess.TabIndex = 22;
            this.buttonSubmitGuess.Text = "Submit Guess";
            this.buttonSubmitGuess.UseVisualStyleBackColor = true;
            this.buttonSubmitGuess.Click += new System.EventHandler(this.ButtonSubmitGuess_Click);
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.Enabled = false;
            this.buttonNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewGame.Location = new System.Drawing.Point(37, 533);
            this.buttonNewGame.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(133, 84);
            this.buttonNewGame.TabIndex = 23;
            this.buttonNewGame.Text = "New Game";
            this.buttonNewGame.UseVisualStyleBackColor = true;
            this.buttonNewGame.Click += new System.EventHandler(this.ButtonNewGame_Click);
            // 
            // Mastermind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1496, 663);
            this.Controls.Add(this.buttonNewGame);
            this.Controls.Add(this.buttonSubmitGuess);
            this.Controls.Add(this.panelColorLabels);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanelGuesses);
            this.Controls.Add(this.GroupBoxColors);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Mastermind";
            this.Text = "Mastermind";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mastermind_FormClosing);
            this.Load += new System.EventHandler(this.Mastermind_Load);
            this.ClientSizeChanged += new System.EventHandler(this.Mastermind_ClientSizeChanged);
            this.GroupBoxColors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPurple)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBrown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOrange)).EndInit();
            this.panelColorLabels.ResumeLayout(false);
            this.panelColorLabels.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBlack;
        private System.Windows.Forms.PictureBox pictureBoxBlue;
        private System.Windows.Forms.PictureBox pictureBoxBrown;
        private System.Windows.Forms.PictureBox pictureBoxOrange;
        private System.Windows.Forms.PictureBox pictureBoxPurple;
        private System.Windows.Forms.GroupBox GroupBoxColors;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelGuesses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelBlackColor;
        private System.Windows.Forms.Label labelBlueColor;
        private System.Windows.Forms.Label labelBrownColor;
        private System.Windows.Forms.Label labelOrangeColor;
        private System.Windows.Forms.Label labelPurpleColor;
        private System.Windows.Forms.Panel panelColorLabels;
        private System.Windows.Forms.Button buttonSubmitGuess;
        private System.Windows.Forms.Button buttonNewGame;
    }
}

