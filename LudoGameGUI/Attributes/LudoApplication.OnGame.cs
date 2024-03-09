namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication
{
    private Label _playerTurnLabel;
    private async void Play(){
        while(true){
            // Loop for every player
            foreach(var player in _ludoGameScene.ludoContext._playerTotems){
                // Wait player to push the dice button
                _playerTurnLabel.Text = $"Turn: Player {player.Key.ID}";
                diceResultLabel.Text = "?";
                

                rollDiceClickedTask = new TaskCompletionSource<bool>();
                await rollDiceClickedTask.Task; // Change the "diceValue"
                var totem = _ludoGameScene.MoveOutHomePosition(player, diceValue);

                Color color = SetTotemColor(player);
                
                // only for initial
                if (diceValue == 6){
                    RemoveTotem(totem.HomePosition.x, totem.HomePosition.y);
                    MoveTotem(totem.Position.x, totem.Position.y, color);   
                }

                await Task.Delay(1000);
            }
        }
    }
    private void CreatePlayerTurnLabel()
    {
        // Add a label to display player names
        this._playerTurnLabel = new Label();
        this._playerTurnLabel.Text = "Turn: -";
        this._playerTurnLabel.ForeColor = Color.DarkBlue;
        this._playerTurnLabel.Font = new Font("Arial", 10, FontStyle.Bold);
        this._playerTurnLabel.AutoSize = true;
        this._playerTurnLabel.Location = new Point(80, 110);
        this.Controls.Add(this._playerTurnLabel);
    }

    private void MoveTotem(int x, int y, Color color)
    {
        // Create a circle panel
        Panel circlePanel = new Panel();
        circlePanel.BackColor = color; // Change color as needed
        circlePanel.Width = 20; // Adjust size as needed
        circlePanel.Height = 20; // Adjust size as needed
        circlePanel.BorderStyle = BorderStyle.FixedSingle; // Add border if needed
        circlePanel.Dock = DockStyle.Fill;

        // Add label with the specified letter
        Label label = new Label();
        label.Text = "O"; // T: Totem
        label.TextAlign = ContentAlignment.MiddleCenter;
        label.Dock = DockStyle.Fill;
        label.ForeColor = Color.White; // Change text color as needed
        circlePanel.Controls.Add(label);

        // Add circle panel to the specific cell
        this.tableLayoutPanel.Controls.Add(circlePanel, x, y);
    }
    private void AddTotem(int x, int y, Color color)
    {
        // Create a circle panel
        Panel circlePanel = new Panel();
        circlePanel.BackColor = color; // Change color as needed
        circlePanel.Width = 20; // Adjust size as needed
        circlePanel.Height = 20; // Adjust size as needed
        circlePanel.BorderStyle = BorderStyle.FixedSingle; // Add border if needed
        circlePanel.Dock = DockStyle.Fill;

        // Add label with the specified letter
        Label label = new Label();
        label.Text = "O"; // T: Totem
        label.TextAlign = ContentAlignment.MiddleCenter;
        label.Dock = DockStyle.Fill;
        label.ForeColor = Color.White; // Change text color as needed
        circlePanel.Controls.Add(label);

        // Add circle panel to the specific cell
        this.tableLayoutPanel.Controls.Add(circlePanel, x, y);
    }
    private void RemoveTotem(int x, int y)
    {
        // Retrieve the control at the specified cell
        Control control = this.tableLayoutPanel.GetControlFromPosition(x, y);

        // If a control is found, remove it from the tableLayoutPanel
        if (control != null)
        {
            this.tableLayoutPanel.Controls.Remove(control);
            control.Dispose(); // Dispose of the control to release resources
        }
    }
}

