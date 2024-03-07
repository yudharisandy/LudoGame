namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication
{
    private TextBox _inputTotemTextBox;
    private Button _inputTotemButton;
    private Label _totemsLabel;
    private Totem _totem;

    private void CreateAddTotemButton()
    {
        // Add a button for confirming the number of players
        this._inputTotemButton = new Button();
        this._inputTotemButton.Text = "Add Totem";
        this._inputTotemButton.Size = new Size(123, 50);
        this._inputTotemButton.Location = new Point(40, 450); // Position the button below the text box
        this.Controls.Add(this._inputTotemButton);
        this._inputTotemButton.Click += TotemInputButton_Click;
    }
    private void CreateInputTotemTextBox()
    {
        // Create a text box for inputting the number of players
        this._inputTotemTextBox = new TextBox();
        this._inputTotemTextBox.Location = new System.Drawing.Point(50, 420);
        this._inputTotemTextBox.Size = new System.Drawing.Size(100, 20);
        this.Controls.Add(this._inputTotemTextBox);
    }
    private void CreateTotemsLabel()
    {
        this._totemsLabel = new Label();
        this._totemsLabel.Text = "";
        this._totemsLabel.AutoSize = true;
        this._totemsLabel.Location = new System.Drawing.Point(20, 510);
        this.Controls.Add(this._totemsLabel);
    }
    private void TotemInputButton_Click(object sender, EventArgs e)
    {
        // Get the input number of players
        if (int.TryParse(_inputTotemTextBox.Text, out int numberOfTotems))
        {
            // Clear previous player names
            _totemsLabel.Text = "";

            // Display player names
            for (int i = 1; i <= numberOfTotems; i++)
            {
                Totem _totem = new(i);
                // bool status = _ludoContext.RegisterPlayers(_ludoPlayers);
                _totemsLabel.Text += $"Totem {i}\n";
            }
        }
        else
        {
            // Display error message if input is not a valid number
            MessageBox.Show("Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

