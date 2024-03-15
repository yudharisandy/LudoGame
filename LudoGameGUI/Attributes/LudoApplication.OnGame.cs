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
    private Label _playerTurnLabel;
    private TaskCompletionSource<bool> chooseTotemToMove;
    private bool _getTotemReachFinalCellStatus;
    private bool _getCollisionStatus;
    private bool _mergeTotemAfterMoveStatus;
    private bool _mergeTotemBeforeMoveStatus;
    private int userInputTotemID;
    private string _totemNameOnPanel;
    private Label totemLabel;
    private int numberTotemOnPlay;
    
    private async void Play(){
        while(true){
            // Loop for every player
            foreach(var player in _ludoGameScene.ludoContext._playerTotems){
                Color color = SetTotemColor(player.Key);
                // Logic for dice-6 rule (the same player holds)
                do {

                    _playerTurnLabel.Text = $"Turn: Player {player.Key.ID + 1}";
                    
                    // Specify color for each player
                    SpecifyColorUIAttribute(color);
                    
                    rollDiceClickedTask = new TaskCompletionSource<bool>();
                    await rollDiceClickedTask.Task; // // Wait player push "Roll Dice" to change the "diceValue" (1-6)
                    
                    // To check whether the totem OnPlay exist(s) or not.
                    numberTotemOnPlay = CheckTotemStatus(player.Value);

                    // If there is no Totem OnPlay and dice != 6, continue to next player directly
                    if (diceValue == 6 || numberTotemOnPlay >= 1){ 
                        
                        // If there is only 1 OnPlay totem, user doesn't need to choose anymore.
                        // If not, user will choose by using totem button.
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
                        _ludoGameScene.NextTurn(player.Key, player.Value[userInputTotemID], diceValue);

                        // Check whether there is collision or not
                        _getCollisionStatus = _ludoGameScene.GetCollisionStatus();
                        CollisionSceneUpdate(); // method to update scene due to collision - Only 1 totem each time

                        // Update GUI sccene
                        RemoveTotem(player.Value[userInputTotemID].HomePosition.X, player.Value[userInputTotemID].HomePosition.Y);                   
                        RemoveTotem(player.Value[userInputTotemID].PreviousPosition.X, player.Value[userInputTotemID].PreviousPosition.Y);

                        // Check totem merging process (after-move)
                        _mergeTotemAfterMoveStatus = _ludoGameScene.GetMergeTotemAfterMoveStatus();
                        var cell2 = _ludoGameScene.GetWorkingCell(player.Value[userInputTotemID], BeforeAfterMoveCell.After);
                        MergeTotemAfterMoveSceneUpdate(cell2);
                        
                        // Move totem to new coordinate
                        MoveTotemAfter(player.Key, player.Value[userInputTotemID].Position.X, player.Value[userInputTotemID].Position.Y, player.Value[userInputTotemID], color);

                        // Check totem merging process (before-move)
                        _mergeTotemBeforeMoveStatus = _ludoGameScene.GetMergeTotemBeforeMoveStatus();
                        var cell1 = _ludoGameScene.GetWorkingCell(player.Value[userInputTotemID], BeforeAfterMoveCell.Before);
                        MergeTotemBeforeMoveSceneUpdate(cell1, color);
                        // the above line works while it doesn't suppose to.

                        // Check whether the totem reach the final cell or not
                        // If true: the same player holds.
                        _getTotemReachFinalCellStatus = _ludoGameScene.GetTotemReachFinalCellStatus(player.Value[userInputTotemID]);

                        // Method to check the winner (to stop the game)
                        // Default: _gameStatus = true (when just started)
                        _gameStatus = _ludoGameScene.GetGameStatus(player.Key, player.Value[userInputTotemID]);
                        if (_gameStatus == false){
                            _playerTurnLabel.Text = $"Player {player.Key.ID + 1} Win!";
                            _startLabel.Text = $"Status: {_gameStatus}";

                            await Task.Delay(100000); // Stop the game;
                        }
                    }
                    else{
                        _getTotemReachFinalCellStatus = false; // Player doesn't have any OnPlay totems -> continue to next player
                    }

                    // To refresh the object rendering process
                    // RefreshRendering(); // Run only when "Re-Render" button is pushed.

                    await Task.Delay(500);

                } while(diceValue == 6 || _getTotemReachFinalCellStatus == true || _getCollisionStatus == true);
            }
        }
    }

    private void SpecifyColorUIAttribute(Color color){
        _playerTurnLabel.BackColor = color; 
        _totem1PlayerButton.BackColor = color;
        _totem2PlayerButton.BackColor = color;
        _totem3PlayerButton.BackColor = color;
        _totem4PlayerButton.BackColor = color;
        diceButton.BackColor = color;
    }

    private void MergeTotemBeforeMoveSceneUpdate(ICell cell, Color color){
        // assumption: there is no control anymore in the before-move cell (need to be confirmed)
        IPlayer playerKey = new LudoPlayer(1000);
        ITotem totemValue = new Totem(1000);

        if(_mergeTotemBeforeMoveStatus == true){
            _totemNameOnPanel = "";
            foreach(var player in cell.Occupants){
                foreach(var totem in player.Value){    
                    _totemNameOnPanel += $"{(TotemName)totem.ID}{player.Key.ID + 1},"; // Update the _totemNameOnPanel = All totem name
                    totemValue = totem;
                    playerKey = player.Key;
                }
            }
            // [Potential BUG]
            MoveTotemBefore(playerKey, totemValue.Position.X, totemValue.Position.Y, totemValue, color);
            _mergeTotemBeforeMoveStatus = false; // Are not updated in the library // To make sure every next player start from False
            _ludoGameScene.SetMergeTotemBeforeMoveStatus(false); // Update in the library

        }
        
    }

    private void MergeTotemAfterMoveSceneUpdate(ICell cell){
        System.Console.WriteLine(_mergeTotemAfterMoveStatus);
        if(_mergeTotemAfterMoveStatus == true){
            _totemNameOnPanel = "";
            foreach(var player in cell.Occupants){
                // [Potential BUG: cell.Occupants already contains current totem]
                foreach(var totem in player.Value){    
                    _totemNameOnPanel += $"{(TotemName)totem.ID}{player.Key.ID + 1},"; // Update the _totemNameOnPanel = All totem name
                    RemoveTotem(totem.Position.X, totem.Position.Y); // Remove all existing control at the working cell
                }
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
            MoveTotemCollision(playerToBeKicked,
                    totemToBeKicked.HomePosition.X, 
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
                    MoveTotemAfter(playerRender.Key,
                                totems.Position.X, 
                                totems.Position.Y, 
                                totems, 
                                colorToBeRefresh);
                }
            }
            _refreshRenderingStatus = false; // Reset the status
        }
    }

    private int CheckTotemStatus(List<ITotem> totemLists){
        // A method to check how many OnPlay totems are there
        int index = 0;
        foreach(var totem in totemLists){
            if(totem.TotemStatusInfo == TotemStatus.OnPlay){
                index++;
            }
        }
        return index;
    }

    private int GetTheOnlyOnPlayTotemID(List<ITotem> totemLists){
        // A method to get the only one OnPlay Totem ID
        // Called only when there is one OnPlay Totem
        int id = 0;
        foreach(var totem in totemLists){
            if(totem.TotemStatusInfo == TotemStatus.OnPlay){
                id = totem.ID;
            }
        }
        return id;
    }

    private void MoveTotemBefore(IPlayer player, int x, int y, ITotem totem, Color color)
    {  
        // Create a circle panel
        Panel totemPanel = new Panel();
        totemPanel.BackColor = color; // Change color as needed
        totemPanel.Width = 20; // Adjust size as needed
        totemPanel.Height = 20; // Adjust size as needed
        totemPanel.BorderStyle = BorderStyle.FixedSingle; // Add border if needed
        totemPanel.Dock = DockStyle.Fill;

        // Add label with the specified letter
        totemLabel = new Label();

        if( _mergeTotemBeforeMoveStatus == false){ // A single totem, No merging process
            _totemNameOnPanel = $"{(TotemName)totem.ID}{player.ID + 1}";
            totemLabel.Font = new Font("Arial", 8, FontStyle.Regular);
        }
        else{
            totemLabel.Font = new Font("Arial", 6, FontStyle.Regular);
        }

        totemLabel.Text = _totemNameOnPanel;
        totemLabel.TextAlign = ContentAlignment.MiddleCenter;
        totemLabel.Dock = DockStyle.Fill;
        totemLabel.ForeColor = Color.White; // Change text color as needed
        totemPanel.Controls.Add(totemLabel);

        // Add circle panel to the specific cell
        this.tableLayoutPanel.Controls.Add(totemPanel, x, y);
    }

private void MoveTotemCollision(IPlayer player, int x, int y, ITotem totem, Color color)
    {  
        // Create a circle panel
        Panel totemPanel = new Panel();
        totemPanel.BackColor = color; // Change color as needed
        totemPanel.Width = 20; // Adjust size as needed
        totemPanel.Height = 20; // Adjust size as needed
        totemPanel.BorderStyle = BorderStyle.FixedSingle; // Add border if needed
        totemPanel.Dock = DockStyle.Fill;

        // Add label with the specified letter
        totemLabel = new Label();

        if(_getCollisionStatus == true){ // Collusion happen
            _totemNameOnPanel = $"{(TotemName)totem.ID}{player.ID + 1}";
            totemLabel.Font = new Font("Arial", 8, FontStyle.Regular);
        }
        else{
            totemLabel.Font = new Font("Arial", 6, FontStyle.Regular);
        }

        totemLabel.Text = _totemNameOnPanel;
        totemLabel.TextAlign = ContentAlignment.MiddleCenter;
        totemLabel.Dock = DockStyle.Fill;
        totemLabel.ForeColor = Color.White; // Change text color as needed
        totemPanel.Controls.Add(totemLabel);

        // Add circle panel to the specific cell
        this.tableLayoutPanel.Controls.Add(totemPanel, x, y);
    }

    private void MoveTotemAfter(IPlayer player, int x, int y, ITotem totem, Color color)
    {  
        // Create a circle panel
        Panel totemPanel = new Panel();
        totemPanel.BackColor = color; // Change color as needed
        totemPanel.Width = 20; // Adjust size as needed
        totemPanel.Height = 20; // Adjust size as needed
        totemPanel.BorderStyle = BorderStyle.FixedSingle; // Add border if needed
        totemPanel.Dock = DockStyle.Fill;

        // Add label with the specified letter
        totemLabel = new Label();

        if(_mergeTotemAfterMoveStatus == false){ // A single totem, No merging process
            _totemNameOnPanel = $"{(TotemName)totem.ID}{player.ID + 1}";
            totemLabel.Font = new Font("Arial", 8, FontStyle.Regular);
        }
        else{
            totemLabel.Font = new Font("Arial", 6, FontStyle.Regular);
        }

        totemLabel.Text = _totemNameOnPanel;
        totemLabel.TextAlign = ContentAlignment.MiddleCenter;
        totemLabel.Dock = DockStyle.Fill;
        totemLabel.ForeColor = Color.White; // Change text color as needed
        totemPanel.Controls.Add(totemLabel);

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

    private void AddTotem(IPlayer player, int x, int y, Color color, ITotem totem)
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
        _totemNameOnPanel = $"{(TotemName)totem.ID}{player.ID + 1}";
        label.Text = _totemNameOnPanel;
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
        _playerTurnLabel = new Label();
        _playerTurnLabel.Text = "Turn: -";
        _playerTurnLabel.BackColor = Color.Gainsboro;
        _playerTurnLabel.ForeColor = Color.White;
        _playerTurnLabel.Font = new Font("Arial", 10, FontStyle.Bold);
        _playerTurnLabel.AutoSize = true;
        _playerTurnLabel.Location = new Point(80, 110);
        Controls.Add(_playerTurnLabel);
    }
}

