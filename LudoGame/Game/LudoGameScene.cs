namespace LudoGame.Game;

using LudoGame.GameFramework;
using LudoGame.GameObject;
using LudoGame.LudoObjects;
using LudoGame.Interface;
using LudoGame.Enums;

public class LudoGameScene : IScene, IContextManager
{
    protected ISceneManager? _sceneManager;
    public LudoContext ludoContext;
    private bool collisionStatus;
    private Dictionary<IPlayer, Totem> _totemToBeKicked;

    public LudoGameScene(){
        ludoContext = new LudoContext();

        _totemToBeKicked = new Dictionary<IPlayer, Totem>();
    }

    public void Update(){}

    public bool GetCollisionStatus(){
        return collisionStatus;
    }
    public Totem GetTotemToBeKicked(){
        Totem result = new(100); // Just random Totem
        // There is only one Totem to be kicked!
        foreach(var i in _totemToBeKicked){
            result = i.Value;
        }
        return result;
    }
    public IPlayer GetPlayerToBeKicked(){
        IPlayer result = new LudoPlayer(100);
        // There is only one Totem to be kicked!
        foreach(var i in _totemToBeKicked){
            result = i.Key;
        }
        return result;
    }

    public bool GetTotemReachFinalCellStatus(Totem totem){
        int index = GetWorkingCellIndex(totem, BeforeAfterMoveCell.After);
        var cell = ludoContext.board.Cells?[index];

        if (cell?.Type == CellType.Final){
            // this method runs every 1 totem reach the final cell
            totem.totemStatus = TotemStatus.OnFinal; // Change OnPlay -> OnFinal
            return true; // Totem reach final cell
        }
        return false; // Totem doesn't reach final cell
    }

    public bool GetGameStatus(IPlayer player, Totem totem){
        
        // If the finalCell.Count == TotemList.Count -> game should stop, return false
        int index = GetWorkingCellIndex(totem, BeforeAfterMoveCell.After);
        var cell = ludoContext.board.Cells?[index];

        // this method run each 1 totem reach the final cell
        if (cell?.Type == CellType.Final){
            var totemListToCheck = cell.GetListTotemOccupants(player);

            // If the final cell contains totem as many as totems registered -> stop the game
            // The winner: player
            int totalNumTotemsEachPlayer = ludoContext.GetTotalNumberTotemsEachPlayer();
            if (totemListToCheck.Count == totalNumTotemsEachPlayer){ 
                return false; // Game stop
            }
        }
        return true; // Game run forward
    }

    public void NextTurn(IPlayer player, List<Totem> totemList, int diceValue, int userinputTotemID){
        if (diceValue == 6){
            GotSixInDice(player, totemList, diceValue, userinputTotemID);
            UpdateTotemBasedOnCellConditionBeforeMove(player, totemList[userinputTotemID]);
            UpdateTotemBasedOnCellConditionAfterMove(player, totemList[userinputTotemID]);
            // System.Console.WriteLine("Dice 6");
        }
        else{
            // Move available Totem (if totemStatus is OnPlay)
            if (totemList[userinputTotemID].totemStatus == TotemStatus.OnPlay){
                UpdateTotemPosition(player, totemList[userinputTotemID], diceValue);
                UpdateTotemBasedOnCellConditionBeforeMove(player, totemList[userinputTotemID]);
                UpdateTotemBasedOnCellConditionAfterMove(player, totemList[userinputTotemID]);
                // System.Console.WriteLine("Dice not 6 & totem OnPlay");
            }
            else if (totemList[userinputTotemID].totemStatus == TotemStatus.OnHome){
                totemList[userinputTotemID].Position.X =  totemList[userinputTotemID].HomePosition.X;
                totemList[userinputTotemID].Position.Y =  totemList[userinputTotemID].HomePosition.Y;
                // System.Console.WriteLine("Dice not 6 & totem OnHome");
            }
        }
    }

    private void UpdateTotemBasedOnCellConditionBeforeMove(IPlayer player, Totem totem){
        // Call when the totem move to another cell
        int index = GetWorkingCellIndex(totem, BeforeAfterMoveCell.Before);
        var cell = ludoContext.board.Cells?[index];

        // [WARNING] Possibility to kick all available totem in a cell (from the same player)
        // While only one totem move forward.
        cell?.KickTotem(player);
    }

    private void UpdateTotemBasedOnCellConditionAfterMove(IPlayer player, Totem totem){
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
                            totemToKick.totemStatus = TotemStatus.OnHome;
                            totemToKick.pathStatus = 0; // Reset the path/route history

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

    private int GetWorkingCellIndex(Totem totem, BeforeAfterMoveCell type){
        // got index of working cell
        int index = 0;

        if (type == BeforeAfterMoveCell.After){
            for (index = 0; index < ludoContext.board.Cells?.Count; index++){
                if (totem.Position.X == ludoContext.board.Cells[index].Position?.X 
                    && totem.Position.Y == ludoContext.board.Cells[index].Position?.Y){
                    return index;
                }
            }

            return 0;
        }
        else{ // Before-move cell
            for(index = 0; index < ludoContext.board.Cells?.Count; index++){
                if (totem.PreviousPosition.X == ludoContext.board.Cells[index].Position?.X 
                    && totem.PreviousPosition.Y == ludoContext.board.Cells[index].Position?.Y){
                    return index;
                }
            }
            return 0; 
        }
    }
    
    private void GotSixInDice(IPlayer player, List<Totem> totemList, int diceValue, int userinputTotemID){
        if (totemList[userinputTotemID].totemStatus == TotemStatus.OnHome){
            // Options: Change Totem OnHome -> OnPlay
            totemList[userinputTotemID].totemStatus = TotemStatus.OnPlay;
            UpdateOutHomePosition(player, totemList[userinputTotemID]);
        }
        else if(totemList[userinputTotemID].totemStatus == TotemStatus.OnPlay){
            // Move available Totem
            UpdateTotemPosition(player, totemList[userinputTotemID], diceValue);
            // cell.KickTotem(this from the previous cell)
        }
    }

    public void UpdateTotemPosition(IPlayer player, Totem totem, int diceValue){
        totem.PreviousPosition.X =  totem.Position.X;
        totem.PreviousPosition.Y =  totem.Position.Y;
        
        if(player.ID == 0){
            // if the diceValue > total remaining path, totem.Position is not changed.
            if(diceValue < ludoContext.board.Paths?.pathPlayer1?.Count - totem.pathStatus){
                totem.Position.X = ludoContext.board.Paths.pathPlayer1[totem.pathStatus + diceValue].X;
                totem.Position.Y = ludoContext.board.Paths.pathPlayer1[totem.pathStatus + diceValue].Y;
                totem.pathStatus += diceValue;
            }
        }
        else if(player.ID == 1){
            if(diceValue < ludoContext.board.Paths?.pathPlayer2?.Count - totem.pathStatus){
                totem.Position.X = ludoContext.board.Paths.pathPlayer2[totem.pathStatus + diceValue].X;
                totem.Position.Y = ludoContext.board.Paths.pathPlayer2[totem.pathStatus + diceValue].Y;
                totem.pathStatus += diceValue;
            }
        }
        else if(player.ID == 2){
            if(diceValue < ludoContext.board.Paths?.pathPlayer3?.Count - totem.pathStatus){
                totem.Position.X = ludoContext.board.Paths.pathPlayer3[totem.pathStatus + diceValue].X;
                totem.Position.Y = ludoContext.board.Paths.pathPlayer3[totem.pathStatus + diceValue].Y;
                totem.pathStatus += diceValue;
            }
        }
        else{
            if(diceValue < ludoContext.board.Paths?.pathPlayer4?.Count - totem.pathStatus){
                totem.Position.X = ludoContext.board.Paths.pathPlayer4[totem.pathStatus + diceValue].X;
                totem.Position.Y = ludoContext.board.Paths.pathPlayer4[totem.pathStatus + diceValue].Y;
                totem.pathStatus += diceValue;
            }
        }
        
    }

    public void UpdateOutHomePosition(IPlayer player, Totem totem){
        // Move out of HomePosition (pathPlayer[0] == Initial totem position on board)
        if (player.ID == 0){
            System.Console.WriteLine("--Check------");
            totem.Position.X = ludoContext.board.Paths.pathPlayer1[0].X;
            totem.Position.Y = ludoContext.board.Paths.pathPlayer1[0].Y;
            System.Console.WriteLine("--Check------");
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

