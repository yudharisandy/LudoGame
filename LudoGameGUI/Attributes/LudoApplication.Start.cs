using System.Reflection.Metadata.Ecma335;

namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.GameObject;
using LudoGame.LudoObjects;

public partial class LudoApplication
{
    private Button _startButton;
    private Label _startLabel;
    private bool _gameStatus; // Status to start and end the game

    private void CreateStartButton()
    {
        // Add a button for confirming the number of players
        this._startButton = new Button();
        this._startButton.Text = "Start The Game";
        this._startButton.Size = new Size(123, 70);
        this._startButton.Location = new Point(150, 690); // Position the button below the text box
        this.Controls.Add(this._startButton);
        this._startButton.Click += StartButton_Click;
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
        _startLabel.Text += $"Status: {_gameStatus}";

        Play();
    }

    private void CreateTotemHomePosition(){
        foreach(var totemList in _ludoGameScene.ludoContext._playerTotems){
            Color color = SetTotemColor(totemList.Key);
            foreach(var totem in totemList.Value){
                AddTotem(totem.HomePosition.x, totem.HomePosition.y, color, totem);            
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

