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
    private Button _startButton;
    private Label _startLabel;
    private bool _gameStatus; // Status to start and end the game

    private void CreateStartButton()
    {
        // Add a button for confirming the number of players
        _startButton = new Button();
        _startButton.Text = "Start The Game";
        _startButton.Size = new Size(123, 70);
        _startButton.Location = new Point(150, 690); // Position the button below the text box
        Controls.Add(_startButton);
        _startButton.Click += StartButton_Click;
    }

    private void CreateStartLabel()
    {
        // Add a label to display player names
        this._startLabel = new Label();
        this._startLabel.Text = "Status: False";
        this._startLabel.AutoSize = true;
        this._startLabel.Location = new System.Drawing.Point(158, 760);
        this.Controls.Add(this._startLabel);
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
        _ludoGameScene.ludoContext.AssignTotemHomePosition();
        CreateTotemHomePosition();

        // Clear previous player names
        _startLabel.Text = "";
        _gameStatus = _ludoGameScene.ludoContext.StartGame();
        _getTotemReachFinalCellStatus = false;
        _getCollisionStatus = false;
        _refreshRenderingStatus = false;
        _startLabel.Text += $"Status: {_gameStatus}";
        _startButton.BackColor = Color.Gainsboro;

        Play();
    }

    private void CreateTotemHomePosition(){
        foreach(var totemList in _ludoGameScene.ludoContext._playerTotems){
            Color color = SetTotemColor(totemList.Key);
            foreach(var totem in totemList.Value){
                AddTotem(totemList.Key, totem.HomePosition.X, totem.HomePosition.Y, color, totem);            
            } 
        }
    }

    private Color SetTotemColor(IPlayer player)
    {
        if (player.ID == 0){ // Player 1
            return Color.DarkBlue;
        }
        else if (player.ID == 1){ // Player 2
            return Color.DarkGreen;
        }
        else if (player.ID == 2){ // Player 3
            return Color.YellowGreen;
        }
        return Color.DarkRed; // Player 4
    }
}

