namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;

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
        this._inputPlayerButton.Location = new Point(40, 200); // Position the button below the text box
        this.Controls.Add(this._inputPlayerButton);
        this._inputPlayerButton.Click += PlayerInputButton_Click;
    }
    private void CreateInputPlayerTextBox()
    {
        // Create a text box for inputting the number of players
        this._inputPlayerTextBox = new TextBox();
        this._inputPlayerTextBox.Location = new System.Drawing.Point(50, 170);
        this._inputPlayerTextBox.Size = new System.Drawing.Size(100, 20);
        this.Controls.Add(this._inputPlayerTextBox);
    }
    private void CreatePlayersLabel()
    {
        // Add a label to display player names
        this._playersLabel = new Label();
        this._playersLabel.Text = "";
        this._playersLabel.AutoSize = true;
        this._playersLabel.Location = new System.Drawing.Point(20, 260);
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
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                LudoPlayer _ludoPlayers = new(i);
                bool status = _ludoContext.RegisterPlayers(_ludoPlayers);
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

