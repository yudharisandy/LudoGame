namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication
{
    private Label _playerTurnLabel;
    private int userInputTotemID;
    private TaskCompletionSource<bool> chooseTotemToMove;
    private bool _getTotemReachFinalCellStatus;
    private bool _getCollisionStatus;
    
    private async void Play(){
        while(true){
            // Loop for every player
            foreach(var player in _ludoGameScene.ludoContext._playerTotems){
                
                // Logic for dice-6 rule (the same player holds)
                do {

                    _playerTurnLabel.Text = $"Turn: Player {player.Key.ID + 1}";
                    
                    rollDiceClickedTask = new TaskCompletionSource<bool>();
                    await rollDiceClickedTask.Task; // // Wait player push "Roll Dice" to change the "diceValue" (1-6)
                    
                    // To check whether the totem OnPlay exist(s) or not.
                    bool statusOnPlay = CheckTotemStatus(player.Value);

                    // If there is no Totem OnPlay and dice != 6, continue to next player directly
                    if (diceValue == 6 || statusOnPlay == true){ 
                        chooseTotemToMove = new TaskCompletionSource<bool>();
                        await chooseTotemToMove.Task; // Wait player to choose one totem to change "userinputTotemID" (0-3)
                        
                        _ludoGameScene.NextTurn(player.Key, player.Value, diceValue, userInputTotemID);

                        RemoveTotem(player.Value[userInputTotemID].HomePosition.x, player.Value[userInputTotemID].HomePosition.y);
                        RemoveTotem(player.Value[userInputTotemID].PreviousPosition.x, player.Value[userInputTotemID].PreviousPosition.y);
                        
                        Color color = SetTotemColor(player.Key);
                        MoveTotem(player.Value[userInputTotemID].Position.x, player.Value[userInputTotemID].Position.y, color, player.Value[userInputTotemID]);

                        // Method to check the winner (to stop the game)
                        // Default: _gameStatus = true (when just started)
                        _gameStatus = _ludoGameScene.GetGameStatus(player.Key, player.Value[userInputTotemID]);
                        if (_gameStatus == false){
                            _playerTurnLabel.Text = $"Player {player.Key.ID + 1} Win!";
                            _startLabel.Text += $"Status: {_gameStatus}";

                            await Task.Delay(100000); // Stop the game;
                        }

                        // Check whether the totem reach the final cell or not
                        // If true: the same player holds.
                        _getTotemReachFinalCellStatus = _ludoGameScene.GetTotemReachFinalCellStatus(player.Value[userInputTotemID]);

                        // Check whether there is collision or not
                        _getCollisionStatus = _ludoGameScene.GetCollisionStatus();

                        // Next: method to update scene due to collision (Priority: Minor)
                    }
                    else{
                        _getTotemReachFinalCellStatus = false; // Player doesn't have any OnPlay totems -> continue to next player
                    }

                    await Task.Delay(500);

                } while(diceValue == 6 || _getTotemReachFinalCellStatus == true || _getCollisionStatus == true);
            }
        }
    }

    private bool CheckTotemStatus(List<Totem> totemLists){
        foreach(var totem in totemLists){
            if(totem.totemStatus == TotemStatus.OnPlay){
                return true;
            }
        }
        return false;
    }
    private void MoveTotem(int x, int y, Color color, Totem totem)
    {  
        // Create a circle panel
        Panel totemPanel = new Panel();
        totemPanel.BackColor = color; // Change color as needed
        totemPanel.Width = 20; // Adjust size as needed
        totemPanel.Height = 20; // Adjust size as needed
        totemPanel.BorderStyle = BorderStyle.FixedSingle; // Add border if needed
        totemPanel.Dock = DockStyle.Fill;

        // Add label with the specified letter
        Label label = new Label();
        label.Text = $"T{totem.ID}"; // T: Totem
        label.TextAlign = ContentAlignment.MiddleCenter;
        label.Dock = DockStyle.Fill;
        label.ForeColor = Color.White; // Change text color as needed
        totemPanel.Controls.Add(label);

        // Add circle panel to the specific cell
        this.tableLayoutPanel.Controls.Add(totemPanel, x, y);
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

    private void AddTotem(int x, int y, Color color, Totem totem)
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
        label.Text = $"T{totem.ID}"; // T: Totem
        label.TextAlign = ContentAlignment.MiddleCenter;
        label.Dock = DockStyle.Fill;
        label.ForeColor = Color.White; // Change text color as needed
        circlePanel.Controls.Add(label);

        // Add circle panel to the specific cell
        this.tableLayoutPanel.Controls.Add(circlePanel, x, y);
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
}

