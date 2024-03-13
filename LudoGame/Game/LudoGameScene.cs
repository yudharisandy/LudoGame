namespace LudoGame.Game;

using LudoGame.GameFramework;
using LudoGame.GameObject;
using LudoGame.LudoObjects;

public class LudoGameScene : IScene, IContextManager
{
    protected ISceneManager _sceneManager;
    public LudoContext ludoContext;
    private bool collisionStatus;
    public LudoGameScene(){
        ludoContext = new LudoContext();
    }

    public void Update(){}

    public bool GetCollisionStatus(){
        return collisionStatus;
    }

    public bool GetTotemReachFinalCellStatus(Totem totem){
        int index = GetWorkingCellIndex(totem, BeforeAfterMoveCell.After);
        var cell = ludoContext.board.Cells[index];

        if (cell.Type == CellType.Final){
            // this method runs every 1 totem reach the final cell
            totem.totemStatus = TotemStatus.OnFinal; // Change OnPlay -> OnFinal
            return true; // Totem reach final cell
        }
        return false; // Totem doesn't reach final cell
    }

    public bool GetGameStatus(IPlayer player, Totem totem){
        
        // If the finalCell.Count == TotemList.Count -> game should stop, return false
        int index = GetWorkingCellIndex(totem, BeforeAfterMoveCell.After);
        var cell = ludoContext.board.Cells[index];

        // this method run each 1 totem reach the final cell
        if (cell.Type == CellType.Final){
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
                totemList[userinputTotemID].Position.x =  totemList[userinputTotemID].HomePosition.x;
                totemList[userinputTotemID].Position.y =  totemList[userinputTotemID].HomePosition.y;
                // System.Console.WriteLine("Dice not 6 & totem OnHome");
            }
        }
        
    }

    private void UpdateTotemBasedOnCellConditionBeforeMove(IPlayer player, Totem totem){
        // Call when the totem move to another cell
        int index = GetWorkingCellIndex(totem, BeforeAfterMoveCell.Before);
        var cell = ludoContext.board.Cells[index];

        // [WARNING] Possibility to kick all available totem in a cell (from the same player)
        // While only one totem move forward.
        cell.KickTotem(player);
    }

    private void UpdateTotemBasedOnCellConditionAfterMove(IPlayer player, Totem totem){
        // Collision rule
        // totem to be checked: totemList[userinputTotemID]

        // Take certain cell (from ludoContext.board.Cells) with the same x, y as totemList[userinputTotemID].Position.x, y
        int index = GetWorkingCellIndex(totem, BeforeAfterMoveCell.After); 
        var cell = ludoContext.board.Cells[index]; // After-move cell

        // Current occupant totem list of the same player in working cell
        // var totemList = ludoContext.board.Cells[index].GetListTotemOccupants(player);

        if (cell.Occupants.Count == 0 || cell.Type == CellType.Safe){
            cell.AddTotem(player, totem);
            collisionStatus = false; // To state that there is no collision
        }
        else if (cell.Occupants.Count != 0 || cell.Type == CellType.Normal){
            foreach(var playerTotem in cell.Occupants){
                if(player == playerTotem.Key){
                    cell.AddTotem(player, totem);
                    collisionStatus = false; // To state that there is no collision
                }
                else{
                    // change all totems position to HomePosition 
                    // Set status to OnHome
                    foreach(var totemToKick in playerTotem.Value){
                        totemToKick.Position.x = totemToKick.HomePosition.x;
                        totemToKick.Position.y = totemToKick.HomePosition.y; 
                        totemToKick.totemStatus = TotemStatus.OnHome;
                        totemToKick.pathStatus = 0; // Reset the path/route history
                    }
                    collisionStatus = true; // To state that there is collision
                    cell.KickTotem(playerTotem.Key);
                }
            }
            
        }
    }

    private int GetWorkingCellIndex(Totem totem, BeforeAfterMoveCell type){
        // got index of working cell
        int index = 0;

        if (type == BeforeAfterMoveCell.After){
            for(index = 0; index < ludoContext.board.Cells.Count; index++){
                if (totem.Position.x == ludoContext.board.Cells[index].Position.x 
                    && totem.Position.y == ludoContext.board.Cells[index].Position.y){
                    return index;
                }
            }
            return 0;
        }
        else{ // Before-move cell
            for(index = 0; index < ludoContext.board.Cells.Count; index++){
                if (totem.PreviousPosition.x == ludoContext.board.Cells[index].Position.x 
                    && totem.PreviousPosition.y == ludoContext.board.Cells[index].Position.y){
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
        totem.PreviousPosition.x =  totem.Position.x;
        totem.PreviousPosition.y =  totem.Position.y;
        if(player.ID == 0){    
            totem.Position.x = ludoContext.board.Paths.pathPlayer1[totem.pathStatus + diceValue].x;
            totem.Position.y = ludoContext.board.Paths.pathPlayer1[totem.pathStatus + diceValue].y;
            totem.pathStatus += diceValue;
        }
        else if(player.ID == 1){
            totem.Position.x = ludoContext.board.Paths.pathPlayer2[totem.pathStatus + diceValue].x;
            totem.Position.y = ludoContext.board.Paths.pathPlayer2[totem.pathStatus + diceValue].y;
            totem.pathStatus += diceValue;
        }
        else if(player.ID == 2){
            totem.Position.x = ludoContext.board.Paths.pathPlayer3[totem.pathStatus + diceValue].x;
            totem.Position.y = ludoContext.board.Paths.pathPlayer3[totem.pathStatus + diceValue].y;
            totem.pathStatus += diceValue;
        }
        else{
            totem.Position.x = ludoContext.board.Paths.pathPlayer4[totem.pathStatus + diceValue].x;
            totem.Position.y = ludoContext.board.Paths.pathPlayer4[totem.pathStatus + diceValue].y;
            totem.pathStatus += diceValue;
        }
    }

    public void UpdateOutHomePosition(IPlayer player, Totem totem){
        // Move out of HomePosition (pathPlayer[0] == Initial totem position on board)
        if (player.ID == 0){
            totem.Position.x = ludoContext.board.Paths.pathPlayer1[0].x;
            totem.Position.y = ludoContext.board.Paths.pathPlayer1[0].y;
        }
        else if (player.ID == 1){
            totem.Position.x = ludoContext.board.Paths.pathPlayer2[0].x;
            totem.Position.y = ludoContext.board.Paths.pathPlayer2[0].y;
        }
        else if (player.ID == 2){
            totem.Position.x = ludoContext.board.Paths.pathPlayer3[0].x;
            totem.Position.y = ludoContext.board.Paths.pathPlayer3[0].y;
        }
        else{
            totem.Position.x = ludoContext.board.Paths.pathPlayer4[0].x;
            totem.Position.y = ludoContext.board.Paths.pathPlayer4[0].y;
        }
    }

    // public bool CheckTotemStatus(IPlayer player, List<Totem> totemList){
    //     foreach(var totem in totemList){
    //         if (totem.totemStatus == TotemStatus.OnPlay){
    //             return true;
    //         }
    //     }
    //     return false;
    // }
}

public enum BeforeAfterMoveCell
{
    Before,
    After
}