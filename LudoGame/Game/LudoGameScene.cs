namespace LudoGame.Game;

using LudoGame.LudoObjects;
using LudoGame.Interface;
using LudoGame.Enums;

using Microsoft.Extensions.Logging;

/// <summary>
/// A class used to control the game in general. Bring the specific ludo rule.
/// </summary>
public class LudoGameScene
{
    /// <summary>
    /// Represent a basic need of the game (board, list of totem from each player, and so on).
    /// </summary>
    public LudoContext ludoContext;

    /// <summary>
    /// Represent a state that informs whether a collision happens or not.
    /// </summary>
    private bool _collisionStatus;

    /// <summary>
    /// Represent a state that informs whether a merging process happens or not in the next cell goal.
    /// </summary>
    private bool _mergeTotemAfterMoveStatus;

    /// <summary>
    /// Represent a state that informs whether a merging process happens or not in the left cell.
    /// </summary>
    private bool _mergeTotemBeforeMoveStatus;

    /// <summary>
    /// Represent a dictionary that contains totem(s) to be kicked as the collision consequences.
    /// </summary>
    private Dictionary<IPlayer, ITotem> _totemToBeKicked;

    /// <summary>
    /// Represent a logging variable to store the log messages.
    /// </summary>
    private ILogger<LudoGameScene>? _log;

    /// <summary>
    /// The constructor.
    /// </summary>
    public LudoGameScene(ILogger<LudoGameScene>? logger = null){
        ludoContext = new LudoContext();
        _totemToBeKicked = new Dictionary<IPlayer, ITotem>();

        _log = logger;
        _log?.LogInformation("Ludo Game Scene was created.");
    }

    /// <summary>
    /// A method to get the status of collisions.
    /// </summary>
    /// <returns></returns>
    public bool GetCollisionStatus(){
        _log?.LogInformation("Get collision status of {status}.", _collisionStatus);
        return _collisionStatus;
    }

    /// <summary>
    /// A method to get Merge Totem Status in the after-move cell (the movement objective cell).
    /// True: After-cell is merged, False: Not.
    /// </summary>
    /// <returns>The merge status </returns>
    public bool GetMergeTotemAfterMoveStatus(){
        _log?.LogInformation("Get merge-totem-after-move status of {status}.", _mergeTotemAfterMoveStatus);
        return _mergeTotemAfterMoveStatus;
    }

    /// <summary>
    /// A method to get Merge Totem Status in the before-move cell (the left cell).
    /// True: Before-cell is merged, False: Not.
    /// </summary>
    /// <returns>The merge status </returns>
    public bool GetMergeTotemBeforeMoveStatus(){
        _log?.LogInformation("Get merge-totem-before-move status of {status}.", _mergeTotemBeforeMoveStatus);
        return _mergeTotemBeforeMoveStatus;
    }

    /// <summary>
    /// A method to set Merge Totem Status in the before-move cell (the left cell).
    /// True: Before-cell is merged, False: Not.
    /// </summary>
    /// <param name="status">The merge status</param>
    public void SetMergeTotemBeforeMoveStatus(bool status){
        _mergeTotemBeforeMoveStatus = status;
        _log?.LogInformation("Set merge-totem-before-move status to be {status}.", status);
    }

    /// <summary>
    /// A method to get a totem to be kicked.
    /// </summary>
    /// <returns>A totem to be kicked</returns>
    public ITotem GetTotemToBeKicked(){
        ITotem result = new Totem(100); // Just random Totem
        
        // There is only one Totem to be kicked!
        foreach(var i in _totemToBeKicked){
            result = i.Value;
        }
        
        _log?.LogInformation("Get Totem {totem ID} to be kicked.", result.ID);
        return result;
    }

    /// <summary>
    /// A method to get a player to be kicked.
    /// </summary>
    /// <returns>Player to be kicked</returns>
    public IPlayer GetPlayerToBeKicked(){
        IPlayer result = new LudoPlayer(100);

        // There is only one Totem to be kicked!
        foreach(var i in _totemToBeKicked){
            result = i.Key;
        }

        _log?.LogInformation("Get Player {player} to be kicked.", result.ID);
        return result;
    }

    /// <summary>
    /// A method to state a totem reach a final cell or not. 
    /// If true, will also change the TotemStatusInfo to OnFinal.
    /// </summary>
    /// <param name="totem"></param>
    /// <returns>True, if totem reachs final cell. False, if not.</returns>
    public bool GetTotemReachFinalCellStatus(ITotem totem){
        var cell = GetWorkingCell(totem, BeforeAfterMoveCell.After);

        if (cell?.Type == CellType.Final){
            // this method runs every 1 totem reach the final cell
            totem.TotemStatusInfo = TotemStatus.OnFinal; // Change OnPlay -> OnFinal
            _log?.LogInformation("Get totem-reach-final-cell status of {status}", true);
            return true;
        }
        _log?.LogInformation("Get totem-reach-final-cell status of {status}", false);
        return false;
    }

    /// <summary>
    /// A method to check the game status. 
    /// Comparing the number of OnFinal totems of current player to the total totems.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="totem"></param>
    /// <returns>False, if all totems of a player has been reach the final cell. True, if not.</returns>
    public bool GetGameStatus(IPlayer player, ITotem totem){
        var cell = GetWorkingCell(totem, BeforeAfterMoveCell.After);
        if (cell?.Type == CellType.Final){
            var totemListToCheck = cell.GetListTotemOccupants(player);
            int totalNumTotemsEachPlayer = ludoContext.GetTotalNumberTotemsEachPlayer();
            if (totemListToCheck.Count == totalNumTotemsEachPlayer){ 
                _log?.LogInformation("Get game status of {status}.", false);
                return false;
            }
        }
        _log?.LogInformation("Get game status of {status}.", true);
        return true;
    }


    /// <summary>
    /// A method to control the update process of totem based on the dice value.
    /// Functionized as controller-like method.
    /// </summary>
    /// <param name="player">Current player</param>
    /// <param name="totem">Current totem</param>
    /// <param name="diceValue">Current dice value</param>
    public void NextTurn(IPlayer player, ITotem totem, int diceValue){
        _log?.LogInformation("Next turn: Player {player id}, Totem {totem id}, dice value is {dice value}.\n", player.ID, totem.ID, diceValue);
        if (diceValue == 6){
            GotSixInDice(player, totem, diceValue);
            UpdateTotemBasedOnCellConditionBeforeMove(player, totem);
            UpdateTotemBasedOnCellConditionAfterMove(player, totem);
            System.Console.WriteLine(_mergeTotemAfterMoveStatus);
            _log?.LogInformation("Run the logic of dice value {dice}", diceValue);
        }
        else{
            if (totem.TotemStatusInfo == TotemStatus.OnPlay){
                UpdateTotemPosition(player, totem, diceValue);
                UpdateTotemBasedOnCellConditionBeforeMove(player, totem);
                UpdateTotemBasedOnCellConditionAfterMove(player, totem);
                _log?.LogInformation("Run the non-six dice logic, Totem status is {totem status}", TotemStatus.OnPlay);
            }
            else if (totem.TotemStatusInfo == TotemStatus.OnHome){
                totem.Position.X =  totem.HomePosition.X;
                totem.Position.Y =  totem.HomePosition.Y;
                _log?.LogInformation("Run the non-six dice logic, Totem status is {totem status}", TotemStatus.OnHome);
            }
        }        
    }

    /// <summary>
    /// A method to remove player-totem in the cell Occupants dictinoary.
    /// When a totem move forward (after), the previous cell (before) should delete the totem from the cell Occupants dictionary.
    /// </summary>
    /// <param name="player">Current player</param>
    /// <param name="totem">Current totem</param>
    private void UpdateTotemBasedOnCellConditionBeforeMove(IPlayer player, ITotem totem){
        if(totem.PathStatus != 0 ){
            System.Console.WriteLine("PathStatus != 0");
            // Call when the totem move to another cell
            var cell = GetWorkingCell(totem, BeforeAfterMoveCell.Before);
            cell.KickTotem(player, totem);

            int remainingTotems = 0;
            foreach(var i in cell.Occupants){
                if(i.Value.Count != 0){
                    remainingTotems = i.Value.Count;
                }
            }
            System.Console.WriteLine($"Before: remaining totems: {remainingTotems}");

            if(remainingTotems> 0){ // The totem left other totems in previous cell
                _mergeTotemBeforeMoveStatus = true;
            }
            else{ // There is no totem left when a totem move forward
                _mergeTotemBeforeMoveStatus = false;
            }
            System.Console.WriteLine($"_mergeTotemBeforeMoveStatus: {_mergeTotemBeforeMoveStatus}");
        }
        // [Potential BUG] -> for the second player, who just go out the totem -> got true if previous player is true (can't be modified)
    }

    /// <summary>
    /// A method to consider the collision rule.
    /// Collision rule: When reach a normal cell, a totem from different player could kick another player's totem that is in the cell Occupants before.
    /// This method will update the collisionStatus, false: No collision, true: Collision.
    /// When collision happens, the will-be-kicked totem position will set to it's home, and reset it's attributes as initial stage.
    /// </summary>
    /// <param name="player">Current player</param>
    /// <param name="totem">Current totem</param>
    private void UpdateTotemBasedOnCellConditionAfterMove(IPlayer player, ITotem totem){
        // Collision rule

        // Get the working cell
        var cell = GetWorkingCell(totem, BeforeAfterMoveCell.After); // After-move cell

        int remainingTotems = 0;
        if(cell.Occupants is not null){
            foreach(var i in cell.Occupants){
                if(i.Value.Count != 0){
                    remainingTotems = i.Value.Count;
                }
            }
        }
        System.Console.WriteLine($"After: remaining totems: {remainingTotems}");

        // Current occupant totem list of the same player in working cell
        if (cell is not null && cell.Occupants is not null){ // Just for avoiding warning
            if (remainingTotems == 0){
                cell.AddTotem(player, totem);
                _collisionStatus = false; // To state that there is no collision
                _mergeTotemAfterMoveStatus = false; // To state that there is no merging process
            }
            else if(remainingTotems > 0 && cell?.Type == CellType.Safe){
                cell.AddTotem(player, totem);
                _mergeTotemAfterMoveStatus = true; // To state that there is merging process
                System.Console.WriteLine($"cell.Occupants.Count: {remainingTotems}");
                _collisionStatus = false; // To state that there is no collision
            }
            else if(remainingTotems > 0 && cell?.Type == CellType.Final){
                cell.AddTotem(player, totem);
                _mergeTotemAfterMoveStatus = true; // To state that there is merging process
                System.Console.WriteLine($"cell.Occupants.Count: {remainingTotems}");
                _collisionStatus = false; // To state that there is no collision
            }
            else if (remainingTotems > 0 && cell.Type == CellType.Normal){
                Dictionary<IPlayer, List<ITotem>> temp1 = new Dictionary<IPlayer, List<ITotem>>(cell.Occupants);
                foreach (var playerTotem in temp1){
                    if(playerTotem.Key == player){
                        cell.AddTotem(player, totem);
                        _collisionStatus = false; // To state that there is no collision
                        _mergeTotemAfterMoveStatus = true; // To state that there is merging process
                    }
                    else{
                        // Collision happens
                        // change all totems position to HomePosition 
                        List<ITotem> temp = new List<ITotem>(playerTotem.Value);
                        // temp = playerTotem.Value.Select(x => x.Clone()).ToList();

                        foreach(var totemToKick in temp){
                            totemToKick.PreviousPosition.X = totemToKick.Position.X; // To kick the totem to
                            totemToKick.PreviousPosition.Y = totemToKick.Position.Y;
                            totemToKick.Position.X = totemToKick.HomePosition.X;
                            totemToKick.Position.Y = totemToKick.HomePosition.Y; 
                            totemToKick.TotemStatusInfo = TotemStatus.OnHome;
                            totemToKick.PathStatus = 0; // Reset the path/route history

                            // Save the totem to be kicked
                            _totemToBeKicked.Clear(); // Clear before using it
                            _totemToBeKicked.Add(playerTotem.Key, totemToKick);
                            cell.KickTotem(playerTotem.Key, totemToKick);
                        }
                        _mergeTotemAfterMoveStatus = false;
                        _collisionStatus = true; // To state that there is collision
                        // cell.AddTotem(player, totem);
                    }
                }
            }
            System.Console.WriteLine($"_collisionStatus: {_collisionStatus}");
            System.Console.WriteLine($"_mergeTotemAfterMoveStatus: {_mergeTotemAfterMoveStatus}");
        }
    }

    /// <summary>
    /// A method to get the current cell that has been reached out by the moved totem.
    /// Before: The cell that was left by the totem after it moves.
    /// After: The objective cell to move.
    /// </summary>
    /// <param name="totem">Current totem</param>
    /// <param name="type">Type: Before or After</param>
    /// <returns>Working cell</returns>
    public ICell GetWorkingCell(ITotem totem, BeforeAfterMoveCell type){
        int index = 0;
        if (type == BeforeAfterMoveCell.After){
            for (index = 0; index < ludoContext.board.Cells.Count; index++){
                if (totem.Position.X == ludoContext.board.Cells[index].Position.X 
                    && totem.Position.Y == ludoContext.board.Cells[index].Position.Y){
                    System.Console.WriteLine("return good cell"); // Good
                    return ludoContext.board.Cells[index]; // After-move cell
                }
            } 
            System.Console.WriteLine($"{totem.Position.X}, {totem.Position.Y}");
            System.Console.WriteLine("return null cell");
            return null;
        }
        else{
            for(index = 0; index < ludoContext.board.Cells.Count; index++){
                if (totem.PreviousPosition.X == ludoContext.board.Cells[index].Position.X 
                    && totem.PreviousPosition.Y == ludoContext.board.Cells[index].Position.Y){
                    return ludoContext.board.Cells[index];
                }
                
            } 
            System.Console.WriteLine($"{totem.PreviousPosition.X}, {totem.PreviousPosition.Y}"); // BUG: 0,0
            System.Console.WriteLine("return null " + $"{ludoContext.board.Cells.Count}"); // Works well
            return null; // return this value -> Bug
        }
    }
    
    /// <summary>
    /// A method to update totem position when a player get dice value of six.
    /// If the chosen totem is OnHome, will be changed to OnPlay, and call UpdateOutHomePosition() method to update the position.
    /// If the chosen totem is already OnPlay, the position will be updated directly.
    /// </summary>
    /// <param name="player">Current player</param>
    /// <param name="totem">Current totem</param>
    /// <param name="diceValue">Dice value</param>
    private void GotSixInDice(IPlayer player, ITotem totem, int diceValue){
        if (totem.TotemStatusInfo == TotemStatus.OnHome){
            totem.TotemStatusInfo = TotemStatus.OnPlay;
            UpdateOutHomePosition(player, totem);
        }
        else if(totem.TotemStatusInfo == TotemStatus.OnPlay){
            UpdateTotemPosition(player, totem, diceValue);
            // cell.KickTotem(this from the previous cell)
        }
    }

    /// <summary>
    /// A method to update the totem position when a totem is already OnPlay.
    /// Set the previous position to the current position (before move).
    /// There will be a checking of dice value and maximum path route.
    /// </summary>
    /// <param name="player">Current player</param>
    /// <param name="totem">Current totem</param>
    /// <param name="diceValue">Dice value</param>
    public void UpdateTotemPosition(IPlayer player, ITotem totem, int diceValue){
        totem.PreviousPosition.X =  totem.Position.X;
        totem.PreviousPosition.Y =  totem.Position.Y;
        
        if(player.ID == 0){
            // if the diceValue > total remaining path, totem.Position is not changed.
            if(diceValue < ludoContext.board.Paths?.pathPlayer1?.Count - totem.PathStatus){
                totem.Position.X = ludoContext.board.Paths.pathPlayer1[totem.PathStatus + diceValue].X;
                totem.Position.Y = ludoContext.board.Paths.pathPlayer1[totem.PathStatus + diceValue].Y;
                totem.PathStatus += diceValue;
            }
        }
        else if(player.ID == 1){
            if(diceValue < ludoContext.board.Paths?.pathPlayer2?.Count - totem.PathStatus){
                totem.Position.X = ludoContext.board.Paths.pathPlayer2[totem.PathStatus + diceValue].X;
                totem.Position.Y = ludoContext.board.Paths.pathPlayer2[totem.PathStatus + diceValue].Y;
                totem.PathStatus += diceValue;
            }
        }
        else if(player.ID == 2){
            if(diceValue < ludoContext.board.Paths?.pathPlayer3?.Count - totem.PathStatus){
                totem.Position.X = ludoContext.board.Paths.pathPlayer3[totem.PathStatus + diceValue].X;
                totem.Position.Y = ludoContext.board.Paths.pathPlayer3[totem.PathStatus + diceValue].Y;
                totem.PathStatus += diceValue;
            }
        }
        else{
            if(diceValue < ludoContext.board.Paths?.pathPlayer4?.Count - totem.PathStatus){
                totem.Position.X = ludoContext.board.Paths.pathPlayer4[totem.PathStatus + diceValue].X;
                totem.Position.Y = ludoContext.board.Paths.pathPlayer4[totem.PathStatus + diceValue].Y;
                totem.PathStatus += diceValue;
            }
        }
        
    }

    /// <summary>
    /// A method to update totem position to it's first path route.
    /// </summary>
    /// <param name="player">Current player</param>
    /// <param name="totem">Current totem</param>
    public void UpdateOutHomePosition(IPlayer player, ITotem totem){
        // Move out of HomePosition (pathPlayer[0] == Initial totem position on board)
        
        if (player.ID == 0){
            totem.Position.X = ludoContext.board.Paths.pathPlayer1[0].X;
            totem.Position.Y = ludoContext.board.Paths.pathPlayer1[0].Y;
        }
        else if (player.ID == 1){
            totem.Position.X = ludoContext.board.Paths.pathPlayer2[0].X;
            totem.Position.Y = ludoContext.board.Paths.pathPlayer2[0].Y;
        }
        else if (player.ID == 2){
            totem.Position.X = ludoContext.board.Paths.pathPlayer3[0].X;
            totem.Position.Y = ludoContext.board.Paths.pathPlayer3[0].Y;
        }
        else{
            totem.Position.X = ludoContext.board.Paths.pathPlayer4[0].X;
            totem.Position.Y = ludoContext.board.Paths.pathPlayer4[0].Y;
        }
        totem.PreviousPosition.X = totem.Position.X;
        totem.PreviousPosition.Y = totem.Position.Y;
    }
}

