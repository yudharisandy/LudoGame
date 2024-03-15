namespace LudoGameGUI;

using System.Windows.Forms;
using LudoGame;
using LudoGame.Enums;
using LudoGame.Game;
using LudoGame.Interface;
using LudoGame.LudoObjects;
using LudoGame.Utility;

public partial class LudoApplication
{
    private Button _totem1PlayerButton;
    private Button _totem2PlayerButton;
    private Button _totem3PlayerButton;
    private Button _totem4PlayerButton;
    private Label _totemPlayersLabel;

    
    private void CreateTotemPlayerButton(){
        CreateTotemPlayer1Button();
        CreateTotemPlayerLabel();
        
        // Temporary
        CreateRefreshRenderingButton();
        // CreateChooseToMoveForwardButton();
        CreateInputDiceTextBox();
    }
    private void CreateTotemPlayerLabel()
    {
        // Add a label to display player names
        _totemPlayersLabel = new Label();
        _totemPlayersLabel.Text = "Totem to be run:";
        _totemPlayersLabel.AutoSize = true;
        _totemPlayersLabel.Location = new Point(85, 145);
        Controls.Add(this._totemPlayersLabel);
    }
    private void CreateTotemPlayer1Button()
    {
        int space = 60;
        int initialPositionX = 40;
        int initialPositionY = 175;

        // Add a button for confirming the number of players
        _totem1PlayerButton = new Button();
        _totem1PlayerButton.Text = "A";
        _totem1PlayerButton.Size = new Size(50, 50);
        _totem1PlayerButton.BackColor = Color.Gainsboro;
        _totem1PlayerButton.ForeColor = Color.White;
        _totem1PlayerButton.Location = new Point(initialPositionX, initialPositionY);
        Controls.Add(_totem1PlayerButton);
        _totem1PlayerButton.Click += Totem1PlayerInputButton_Click;

        // Add a button for confirming the number of players
        _totem2PlayerButton = new Button();
        _totem2PlayerButton.Text = "B";
        _totem2PlayerButton.Size = new Size(50, 50);
        _totem2PlayerButton.BackColor = Color.Gainsboro;
        _totem2PlayerButton.ForeColor = Color.White;
        _totem2PlayerButton.Location = new Point(initialPositionX + 1*space, initialPositionY);
        Controls.Add(_totem2PlayerButton);
        _totem2PlayerButton.Click += Totem2PlayerInputButton_Click;

        // Add a button for confirming the number of players
        _totem3PlayerButton = new Button();
        _totem3PlayerButton.Text = "C";
        _totem3PlayerButton.Size = new Size(50, 50);
        _totem3PlayerButton.BackColor = Color.Gainsboro;
        _totem3PlayerButton.ForeColor = Color.White;
        _totem3PlayerButton.Location = new Point(initialPositionX + 2*space, initialPositionY);
        Controls.Add(_totem3PlayerButton);
        _totem3PlayerButton.Click += Totem3PlayerInputButton_Click;

        // Add a button for confirming the number of players
        _totem4PlayerButton = new Button();
        _totem4PlayerButton.Text = "D";
        _totem4PlayerButton.Size = new Size(50, 50);
        _totem4PlayerButton.BackColor = Color.Gainsboro;
        _totem4PlayerButton.ForeColor = Color.White;
        _totem4PlayerButton.Location = new Point(initialPositionX + 3*space, initialPositionY);
        Controls.Add(_totem4PlayerButton);
        _totem4PlayerButton.Click += Totem4PlayerInputButton_Click;
    }
    private void Totem1PlayerInputButton_Click(object sender, EventArgs e)
    {
        userInputTotemID = 0;
        chooseTotemToMove.SetResult(true);
    }
    private void Totem2PlayerInputButton_Click(object sender, EventArgs e)
    {
        userInputTotemID = 1;
        chooseTotemToMove.SetResult(true);
    }
    private void Totem3PlayerInputButton_Click(object sender, EventArgs e)
    {
        userInputTotemID = 2;
        chooseTotemToMove.SetResult(true);
    }
    private void Totem4PlayerInputButton_Click(object sender, EventArgs e)
    {
        userInputTotemID = 3;
        chooseTotemToMove.SetResult(true);
    }
}

