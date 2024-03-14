namespace LudoGameGUI;

using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;
using LudoGame.Interface;

public partial class LudoApplication
{
    private TextBox _inputTotemTextBox;
    private Button _inputTotemButton;
    private Label _totemsLabel;
    private ITotem _totem;

    private void CreateAddTotemButton()
    {
        // Add a button for confirming the number of players
        this._inputTotemButton = new Button();
        this._inputTotemButton.Text = "Add Totem";
        this._inputTotemButton.Size = new Size(123, 50);
        this._inputTotemButton.Location = new Point(90, 500); // Position the button below the text box
        this.Controls.Add(this._inputTotemButton);
        this._inputTotemButton.Click += TotemInputButton_Click;
    }
    private void CreateInputTotemTextBox()
    {
        // Create a text box for inputting the number of players
        this._inputTotemTextBox = new TextBox();
        this._inputTotemTextBox.Location = new System.Drawing.Point(100, 470);
        this._inputTotemTextBox.Size = new System.Drawing.Size(100, 20);
        this.Controls.Add(this._inputTotemTextBox);
    }
    private void CreateTotemsLabel()
    {
        this._totemsLabel = new Label();
        this._totemsLabel.Text = "";
        this._totemsLabel.AutoSize = true;
        this._totemsLabel.Location = new System.Drawing.Point(70, 555);
        this.Controls.Add(this._totemsLabel);
    }
    private void TotemInputButton_Click(object sender, EventArgs e)
    {
        // Get the input number of players
        if (int.TryParse(_inputTotemTextBox.Text, out int numberOfTotems))
        {
            foreach(var player in _ludoGameScene.ludoContext._players){
                List<ITotem> totemsList = new();
                // Clear previous player names
                _totemsLabel.Text = "";

                // Display player names
                for (int i = 0; i < numberOfTotems; i++)
                {
                    ITotem _totem = new Totem(i);
                    totemsList.Add(_totem);
                    _totemsLabel.Text += $"Totem {i}, Status: False\n";
                }
                bool status = _ludoGameScene.ludoContext.RegisterTotems(player, totemsList);
            }
        }
        else
        {
            // Display error message if input is not a valid number
            MessageBox.Show("Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

