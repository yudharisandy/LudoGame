namespace LudoGame.Game;

using LudoGame.GameFramework;
using LudoGame.GameObject;
using LudoGame.LudoObjects;
using LudoGame.Interface;
using LudoGame.Enums;

/// <summary>
/// A class used to control the game in general. Bring the specific ludo rule.
/// </summary>
public class LudoGameScene : IScene, IContextManager
{
    protected ISceneManager? _sceneManager;
    public LudoContext ludoContext;
    private bool collisionStatus;
    private Dictionary<IPlayer, ITotem> _totemToBeKicked;

    public LudoGameScene(){
        ludoContext = new LudoContext();
        _totemToBeKicked = new Dictionary<IPlayer, ITotem>();
    }

    public void Update(){}

    /// <summary>
    /// A method to get the status of collisions.
    /// </summary>
    /// <returns></returns>
    public bool GetCollisionStatus(){
        return collisionStatus;
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
        return result;
    }

    /// <summary>
    /// A method to state a totem reach a final cell or not. 
    /// If true, will also change the TotemStatusInfo to OnFinal.
    /// </summary>
    /// <param name="totem"></param>
    /// <returns>True, if totem reachs final cell. False, if not.</returns>
    public bool GetTotemReachFinalCellStatus(ITotem totem){
        int index = GetWorkingCellIndex(totem, BeforeAfterMoveCell.After);
        var cell = ludoContext.board.Cells?[index];

        if (cell?.Type == CellType.Final){
            // this method runs every 1 totem reach the final cell
            totem.TotemStatusInfo = TotemStatus.OnFinal; // Change OnPlay -> OnFinal
            return true;
        }
        return false;
    }

    /// <summary>
    /// A method to check the game status. 
    /// Comparing the number of OnFinal totems of current player to the total totems.
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="totem"></param>
    /// <returns>False, if all totems of a player has been reach the final cell. True, if not.</returns>
    public bool GetGameStatus(IPlayer player, ITotem totem){
        int index = GetWorkingCellIndex(totem, BeforeAfterMoveCell.After);
        var cell = ludoContext.board.Cells?[index];

        if (cell?.Type == CellType.Final){
            var totemListToCheck = cell.GetListTotemOccupants(player);
            int totalNumTotemsEachPlayer = ludoContext.GetTotalNumberTotemsEachPlayer();
            if (totemListToCheck.Count == totalNumTotemsEachPlayer){ 
                return false;
            }
        }
        return true;
    }


    public void NextTurn(IPlayer player, List<ITotem> totemList, int diceValue, int userinputTotemID){
        if (diceValue == 6){
            GotSixInDice(player, totemList[userinputTotemID], diceValue);
            UpdateTotemBasedOnCellConditionBeforeMove(player, totemList[userinputTotemID]);
            UpdateTotemBasedOnCellConditionAfterMove(player, totemList[userinputTotemID]);
            // System.Console.WriteLine("Dice 6");
        }
        else{
            // Move available Totem (if totemStatus is OnPlay)
            if (totemList[userinputTotemID].TotemStatusInfo == TotemStatus.OnPlay){
                UpdateTotemPosition(player, totemList[userinputTotemID], diceValue);
                UpdateTotemBasedOnCellConditionBeforeMove(player, totemList[userinputTotemID]);
                UpdateTotemBasedOnCellConditionAfterMove(player, totemList[userinputTotemID]);
                // System.Console.WriteLine("Dice not 6 & totem OnPlay");
            }
            else if (totemList[userinputTotemID].TotemStatusInfo == TotemStatus.OnHome){
                totemList[userinputTotemID].Position.X =  totemList[userinputTotemID].HomePosition.X;
                totemList[userinputTotemID].Position.Y =  totemList[userinputTotemID].HomePosition.Y;
                // System.Console.WriteLine("Dice not 6 & totem OnHome");
            }
        }
    }

    private void UpdateTotemBasedOnCellConditionBeforeMove(IPlayer player, ITotem totem){
        // Call when the totem move to another cell
        int index = GetWorkingCellIndex(totem, BeforeAfterMoveCell.Before);
        var cell = ludoContext.board.Cells?[index];

        // [WARNING] Possibility to kick all available totem in a cell (from the same player)
        // While only one totem move forward.
        cell?.KickTotem(player);
    }

    private void UpdateTotemBasedOnCellConditionAfterMove(IPlayer player, ITotem totem){
        // Collision rule
        // totem to be checked: totemList[userinputTotemID]

        // Take certain cell (from ludoContext.board.Cells) with the same x, y as totemList[userinputTotemID].Position.x, y
        int index = GetWorkingCellIndex(totem, BeforeAfterMoveCell.After); 
        var cell = ludoContext.board.Cells?[index]; // After-move cell

        // Current occupant totem list of the same player in working cell
        // var totemList = ludoContext.board.Cells[index].GetListTotemOccupants(player);
        if (cell is not null && cell.Occupants is not null){ // Just to avoid warning
            if (cell.Occupants.Count == 0 || cell?.Type == CellType.Safe){
                cell.AddTotem(player, totem);
                collisionStatus = false; // To state that there is no collision
            }
            else if (cell?.Occupants.Count != 0 || cell.Type == CellType.Normal){
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                foreach (var playerTotem in cell.Occupants){
                    if(playerTotem.Key == player){
                        cell.AddTotem(player, totem);
                        collisionStatus = false; // To state that there is no collision
                    }
                    else{
                        // change all totems position to HomePosition 
                        // Set status to OnHome
                        foreach(var totemToKick in playerTotem.Value){
                            totemToKick.PreviousPosition.X = totemToKick.Position.X;
                            totemToKick.PreviousPosition.Y = totemToKick.Position.Y;
                            totemToKick.Position.X = totemToKick.HomePosition.X;
                            totemToKick.Position.Y = totemToKick.HomePosition.Y; 
                            totemToKick.TotemStatusInfo = TotemStatus.OnHome;
                            totemToKick.PathStatus = 0; // Reset the path/route history

                            // Save the totem to be kicked
                            _totemToBeKicked.Clear(); // Clear before using it
                            _totemToBeKicked.Add(playerTotem.Key, totemToKick);
                        }
                        // To state that there is collision
                        collisionStatus = true; 

                        cell.KickTotem(playerTotem.Key);
                    }
                }
                #pragma warning restore CS8602 // Dereference of a possibly null reference.


            }
        }
    }

    /// <summary>
    /// A method to get the current cell that has been reached out by the moved totem.
    /// </summary>
    /// <param name="totem">Totem</param>
    /// <param name="type">Type: Before or After</param>
    /// <returns>Index of working cell</returns>
    private int GetWorkingCellIndex(ITotem totem, BeforeAfterMoveCell type){
        int index = 0;
        if (type == BeforeAfterMoveCell.After){
            for (index = 0; index < ludoContext.board.Cells?.Count; index++){
                if (totem.Position.X == ludoContext.board.Cells[index].Position?.X 
                    && totem.Position.Y == ludoContext.board.Cells[index].Position?.Y){
                    return index;
                }
            } return 0;
        }
        else{
            for(index = 0; index < ludoContext.board.Cells?.Count; index++){
                if (totem.PreviousPosition.X == ludoContext.board.Cells[index].Position?.X 
                    && totem.PreviousPosition.Y == ludoContext.board.Cells[index].Position?.Y){
                    return index;
                }
            } return 0; 
        }
    }
    
    /// <summary>
    /// A method to update totem position when a player get dice value of six.
    /// If the chosen totem is OnHome, will be changed to OnPlay, and call UpdateOutHomePosition() method to update the position.
    /// If the chosen totem is already OnPlay, the position will be updated directly.
    /// </summary>
    /// <param name="player">Current player</param>
    /// <param name="totem">Totem to be updated</param>
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
    /// <param name="player">Player</param>
    /// <param name="totem">Totem</param>
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
    /// <param name="player">Player</param>
    /// <param name="totem">Totem</param>
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
    }
}

