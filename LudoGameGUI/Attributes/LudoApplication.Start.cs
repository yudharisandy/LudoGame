using System.Reflection.Metadata.Ecma335;

namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication
{
    private Button _startButton;
    private Label _startLabel;
    private void CreateStartButton()
    {
        // Add a button for confirming the number of players
        this._startButton = new Button();
        this._startButton.Text = "Start The Game";
        this._startButton.Size = new Size(123, 70);
        this._startButton.Location = new Point(90, 670); // Position the button below the text box
        this.Controls.Add(this._startButton);
        this._startButton.Click += StartButton_Click;
    }
    private void CreateStartLabel()
    {
        // Add a label to display player names
        this._startLabel = new Label();
        this._startLabel.Text = "Status: False";
        this._startLabel.AutoSize = true;
        this._startLabel.Location = new System.Drawing.Point(96, 760);
        this.Controls.Add(this._startLabel);
    }
    private void StartButton_Click(object sender, EventArgs e)
    {
        // Clear previous player names
        _startLabel.Text = "";
        bool status = _ludoContext.StartGame();
        _startLabel.Text += $"Status: {status}\n";
        
        _ludoContext.AssignTotemHomePosition();
        CreateTotemHomePosition();

    }
    private void CreateTotemHomePosition(){
        foreach(var totemList in _ludoContext._playerTotems){
            Color color = SetTotemColor(totemList);
            foreach(var totem in totemList.Value){
                AddCircle(totem.HomePosition.x, totem.HomePosition.y, color);            
            } 
        }
    }
    private Color SetTotemColor(KeyValuePair<LudoGame.GameObject.IPlayer, List<Totem>> totemList)
    {
        if (totemList.Key.ID == 1){ // Player 1
            return Color.DarkBlue;
        }
        else if (totemList.Key.ID == 2){ // Player 2
            return Color.DarkGreen;
        }
        else if (totemList.Key.ID == 3){ // Player 3
            return Color.Yellow;
        }
        return Color.DarkRed; // Player 4
    }
}

