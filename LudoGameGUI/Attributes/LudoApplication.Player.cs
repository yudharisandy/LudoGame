namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Enums;
using LudoGame.Game;
using LudoGame.GameObject;
using LudoGame.Interface;
using LudoGame.LudoObjects;
using LudoGame.Utility;

public partial class LudoApplication
{
    private TextBox _inputPlayerTextBox;
    private Button _inputPlayerButton;
    private Label _playersLabel;
    
    private void CreateAddPlayerButton()
    {
        // Add a button for confirming the number of players
        this._inputPlayerButton = new Button();
        this._inputPlayerButton.Text = "Add Player";
        this._inputPlayerButton.Size = new Size(123, 50);
        this._inputPlayerButton.Location = new Point(90, 285);
        this.Controls.Add(this._inputPlayerButton);
        this._inputPlayerButton.Click += PlayerInputButton_Click;
    }

    private void CreateInputPlayerTextBox()
    {
        // Create a text box for inputting the number of players
        this._inputPlayerTextBox = new TextBox();
        this._inputPlayerTextBox.Location = new System.Drawing.Point(100, 255);
        this._inputPlayerTextBox.Size = new System.Drawing.Size(100, 20);
        this.Controls.Add(this._inputPlayerTextBox);
    }

    private void CreatePlayersLabel()
    {
        // Add a label to display player names
        this._playersLabel = new Label();
        this._playersLabel.Text = "";
        this._playersLabel.AutoSize = true;
        this._playersLabel.Location = new System.Drawing.Point(70, 345);
        this.Controls.Add(this._playersLabel);
    }
    
    private void PlayerInputButton_Click(object sender, EventArgs e)
    {
        // Get the input number of players
        if (int.TryParse(_inputPlayerTextBox.Text, out int numberOfPlayers))
        {
            // Clear previous player names
            _playersLabel.Text = "";

            // Display player names
            for (int i = 0; i < numberOfPlayers; i++)
            {
                LudoPlayer _ludoPlayer = new(i);
                bool status = _ludoGameScene.ludoContext.RegisterPlayers(_ludoPlayer);
                _playersLabel.Text += $"Player {i}, Status: {status}\n";
            }
        }
        else
        {
            // Display error message if input is not a valid number
            MessageBox.Show("Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

