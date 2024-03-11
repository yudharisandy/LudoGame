namespace LudoGame.Game;

using LudoGame.GameFramework;
using LudoGame.GameObject;
using LudoGame.LudoObjects;

public class LudoGameScene : IScene, IContextManager
{
    protected ISceneManager _sceneManager;
    public LudoContext ludoContext;
    public LudoGameScene(){
        ludoContext = new LudoContext();
    }
    public void Update(){}
    public void NextTurn(IPlayer player, List<Totem> totemList, int diceValue, int userinputTotemID){
        if (diceValue == 6){
            GotSixInDice(player, totemList, diceValue, userinputTotemID);
            UpdateTotemBasedOnCellCondition(player, totemList[userinputTotemID]);
        }
        else{
            // Move available Totem (if totemStatus is OnPlay)
            if (totemList[userinputTotemID].totemStatus == TotemStatus.OnPlay){
                UpdateTotemPosition(player, totemList[userinputTotemID], diceValue);
                UpdateTotemBasedOnCellCondition(player, totemList[userinputTotemID]);
            }
            else{
                totemList[userinputTotemID].Position.x =  totemList[userinputTotemID].HomePosition.x;
                totemList[userinputTotemID].Position.y =  totemList[userinputTotemID].HomePosition.y;
            }
        }
        
    }
    private void UpdateTotemBasedOnCellCondition(IPlayer player, Totem totem){
        // totem to be checked: totemList[userinputTotemID]
        // Take certain cell (from ludoContext.board.Cells) with the same x, y as totemList[userinputTotemID].Position.x, y
        int index = 0;
        for(index = 0; index < ludoContext.board.Cells.Count; index++){
            if (totem.Position.x == ludoContext.board.Cells[index].Position.x 
                && totem.Position.y == ludoContext.board.Cells[index].Position.y){
                // return index;
                break;
            }
        } // got index of working cell
        var cell = ludoContext.board.Cells[index]; // bug: if cell is null -> OutOfRange

        if (cell.Occupants is null || cell.Type == CellType.Safe){
            cell.AddTotem(player, totem);
        }
        else{
            foreach(var playerTotem in cell.Occupants){
                if(player == playerTotem.Key){
                    cell.AddTotem(player, totem);
                }
                else{
                    // change totem position to HomePosition 
                    // Set status to OnHome
                    playerTotem.Value.Position.x = playerTotem.Value.HomePosition.x;
                    playerTotem.Value.Position.y = playerTotem.Value.HomePosition.y; 
                    playerTotem.Value.totemStatus = TotemStatus.OnHome;
                    playerTotem.Value.pathStatus = 0;
                    cell.KickTotem(playerTotem.Key);
                }
            }
            
        }
        // Check ludoContext.board.Cells.Occupants, if there is another totem from different player -> Kick
            // Kick: the totem Position = HomePosition, remove from occupantas
            // if the player is same -> Cells.AddTotem(player, totem)
            // if Occupant is null -> Cells.AddTotem(player, totem)
    }

    private void GotSixInDice(IPlayer player, List<Totem> totemList, int diceValue, int userinputTotemID){
        if (totemList[userinputTotemID].totemStatus == TotemStatus.OnHome){
            // Options: Change Totem OnHome -> OnPlay
            totemList[userinputTotemID].totemStatus = TotemStatus.OnPlay;
            UpdateOutHomePosition(player, totemList[userinputTotemID]);
        }
        else{
            // Move available Totem
            UpdateTotemPosition(player, totemList[userinputTotemID], diceValue);
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