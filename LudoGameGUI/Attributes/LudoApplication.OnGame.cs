namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication
{
    private Label _playerTurnLabel;
    private TaskCompletionSource<bool> chooseTotemToMove;
    private bool _getTotemReachFinalCellStatus;
    private bool _getCollisionStatus;
    private int userInputTotemID;
    
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
                    int numberTotemOnPlay = CheckTotemStatus(player.Value);

                    // If there is no Totem OnPlay and dice != 6, continue to next player directly
                    if (diceValue == 6 || numberTotemOnPlay >= 1){ 
                        
                        // If there is only 1 OnPlay totem, user doesn't need to choose anymore.
                        if (numberTotemOnPlay == 1 && diceValue != 6){
                            // method to get that one totem ID
                            // When get dice 6, user can choose themselves
                            userInputTotemID = GetTheOnlyOnPlayTotemID(player.Value);
                            await Task.Delay(300);
                        }
                        else{
                            // User(s) chooses by themselves
                            chooseTotemToMove = new TaskCompletionSource<bool>();
                            await chooseTotemToMove.Task; // Wait player to choose one totem to change "userinputTotemID" (0-3)
                        }
                        
                        // Method to update totem position
                        _ludoGameScene.NextTurn(player.Key, player.Value, diceValue, userInputTotemID);

                        // Check whether there is collision or not
                        _getCollisionStatus = _ludoGameScene.GetCollisionStatus();
                        // method to update scene due to collision - Only 1 totem each time
                        CollisionSceneUpdate();

                        // Update GUI sccene
                        RemoveTotem(player.Value[userInputTotemID].HomePosition.X, player.Value[userInputTotemID].HomePosition.Y);
                        RemoveTotem(player.Value[userInputTotemID].PreviousPosition.X, player.Value[userInputTotemID].PreviousPosition.Y);
                        Color color = SetTotemColor(player.Key);
                        MoveTotem(player.Value[userInputTotemID].Position.X, 
                                    player.Value[userInputTotemID].Position.Y, 
                                    player.Value[userInputTotemID], 
                                    color);

                        // Check whether the totem reach the final cell or not
                        // If true: the same player holds.
                        _getTotemReachFinalCellStatus = _ludoGameScene.GetTotemReachFinalCellStatus(player.Value[userInputTotemID]);

                        // Method to check the winner (to stop the game)
                        // Default: _gameStatus = true (when just started)
                        _gameStatus = _ludoGameScene.GetGameStatus(player.Key, player.Value[userInputTotemID]);
                        if (_gameStatus == false){
                            _playerTurnLabel.Text = $"Player {player.Key.ID + 1} Win!";
                            _startLabel.Text += $"Status: {_gameStatus}";

                            await Task.Delay(100000); // Stop the game;
                        }

                    }
                    else{
                        _getTotemReachFinalCellStatus = false; // Player doesn't have any OnPlay totems -> continue to next player
                    }

                    // To refresh the object rendering process
                    RefreshRendering(); // Run only when "Re-Render" button is pushed.

                    await Task.Delay(500);
                    
                    // Just to mark the diceButton to be available
                    diceButton.BackColor = Color.Gold;

                } while(diceValue == 6 || _getTotemReachFinalCellStatus == true || _getCollisionStatus == true);
            }
        }
    }

    private void CollisionSceneUpdate(){
        if (_getCollisionStatus == true){
            // Get totemToBeKicked
            var totemToBeKicked = _ludoGameScene.GetTotemToBeKicked();
            var playerToBeKicked = _ludoGameScene.GetPlayerToBeKicked();
            // RemoveTotem from working cell
            RemoveTotem(totemToBeKicked.PreviousPosition.X, totemToBeKicked.PreviousPosition.Y);
            // MoveTotem to Home Position
            Color colorToBeKicked = SetTotemColor(playerToBeKicked);
            MoveTotem(totemToBeKicked.HomePosition.X, 
                    totemToBeKicked.HomePosition.Y, 
                    totemToBeKicked, 
                    colorToBeKicked);
        }
    }

    private void RefreshRendering(){
        refreshRenderingButtonTask = new TaskCompletionSource<bool>();
        if (_refreshRenderingStatus == true){
            // .. rendering method
            // Get all totem and players from _ludoGameScene.ludoContext._playerTotems
            
            // Remove All Totems from the Board
            for (int x = 0; x < 15; x++){
                for (int y = 0; y < 15; y++){
                    RemoveTotem(x, y);
                }
            }   
            foreach(var playerRender in _ludoGameScene.ludoContext._playerTotems){
                // MoveTotem to Home Position
                Color colorToBeRefresh = SetTotemColor(playerRender.Key);
                foreach(var totems in playerRender.Value){
                    MoveTotem(totems.Position.X, 
                                totems.Position.Y, 
                                totems, 
                                colorToBeRefresh);
                }
            }
            _refreshRenderingStatus = false; // Reset the status
        }
    }

    private int CheckTotemStatus(List<Totem> totemLists){
        // A method to check how many OnPlay totems are there
        int index = 0;
        foreach(var totem in totemLists){
            if(totem.totemStatus == TotemStatus.OnPlay){
                index++;
            }
        }
        return index;
    }

    private int GetTheOnlyOnPlayTotemID(List<Totem> totemLists){
        // A method to get the only one OnPlay Totem ID
        // Called only when there is one OnPlay Totem
        int id = 0;
        foreach(var totem in totemLists){
            if(totem.totemStatus == TotemStatus.OnPlay){
                id = totem.ID;
            }
        }
        return id;
    }

    private void MoveTotem(int x, int y, Totem totem, Color color)
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
        Control? control = this.tableLayoutPanel.GetControlFromPosition(x, y);

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

