using System;
using System.Windows.Forms;
using LudoGame; // Import the namespace from the NumPlayer project

namespace LudoGameGUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tableLayoutPanel;
        private Button diceButton;
        private TextBox inputTextBox;
        private Button inputButton;
        private Label playersLabel;
        private Label diceResultLabel; // New label to display the dice result

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 800); // Increased width to accommodate additional space
            this.Text = "LudoApp";

            CreateGrid();
            CreateDiceButton();
            CreateInputTextBox();
            CreateInputButton();
            CreatePlayersLabel();
            CreateDiceResultLabel(); // Create the label to display the dice result
        }
        #endregion
    }
}
