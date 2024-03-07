using System;
using System.Windows.Forms;
using LudoGame; // Import the namespace from the NumPlayer project

namespace LudoGameGUI
{
    partial class Form1
    {
        private void CreateGrid()
        {
            // Create TableLayoutPanel for the grid
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Right; // Dock to the right side
            this.tableLayoutPanel.Size = new System.Drawing.Size(700, 800); // Adjust the size to fit the form
            this.tableLayoutPanel.Location = new System.Drawing.Point(200, 0); // Adjusted the starting position from the left edge
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 15;
            this.tableLayoutPanel.ColumnCount = 15;
            for (int i = 0; i < 15; i++)
            {
                this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F / 15F));
                this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F / 15F));
            }
            this.Controls.Add(this.tableLayoutPanel);
        }

        private void CreateDiceButton()
        {
            // Add a button for rolling dice
            this.diceButton = new System.Windows.Forms.Button();
            this.diceButton.Text = "Roll Dice";
            this.diceButton.Size = new System.Drawing.Size(100, 50);
            this.diceButton.Location = new System.Drawing.Point(50, 10); // Position the button on the left side
            this.Controls.Add(this.diceButton);
            this.diceButton.Click += DiceButton_Click;
        }

        private void CreateInputTextBox()
        {
            // Create a text box for inputting the number of players
            this.inputTextBox = new TextBox();
            this.inputTextBox.Location = new System.Drawing.Point(50, 370);
            this.inputTextBox.Size = new System.Drawing.Size(100, 20);
            this.Controls.Add(this.inputTextBox);
        }

        private void CreateInputButton()
        {
            // Add a button for confirming the number of players
            this.inputButton = new Button();
            this.inputButton.Text = "Confirm";
            this.inputButton.Size = new Size(100, 30);
            this.inputButton.Location = new Point(50, 400); // Position the button below the text box
            this.Controls.Add(this.inputButton);
            this.inputButton.Click += InputButton_Click;
        }

        private void CreatePlayersLabel()
        {
            // Add a label to display player names
            this.playersLabel = new Label();
            this.playersLabel.Text = "";
            this.playersLabel.AutoSize = true;
            this.playersLabel.Location = new System.Drawing.Point(50, 440); // Position the label below the button
            this.Controls.Add(this.playersLabel);
        }

        private void CreateDiceResultLabel()
        {
            // Add a label to display the dice result
            this.diceResultLabel = new Label();
            this.diceResultLabel.Text = "";
            this.diceResultLabel.AutoSize = true;
            this.diceResultLabel.Location = new System.Drawing.Point(80, 100); // Position the label below the player label
            this.Controls.Add(this.diceResultLabel);
        }     

        private void DiceButton_Click(object sender, EventArgs e)
        {
            // Generate a random number from 1 to 6 and display it
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 7);
            diceResultLabel.Text = $"{randomNumber}"; // Update the label with the dice result
        }

        private void InputButton_Click(object sender, EventArgs e)
        {
            // Get the input number of players from NumPlayer project
            int numberOfPlayers = LudoGame.Program.ReturnGetNumberOfPlayers();

            // Clear previous player names
            playersLabel.Text = "";

            // Display player names
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                playersLabel.Text += $"Player {i}\n";
            }
        }
    }
}
