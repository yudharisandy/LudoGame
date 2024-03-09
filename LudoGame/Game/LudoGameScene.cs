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

    public void NextTurn(IPlayer player, List<Totem> totemList, int diceValue, int userinputTotemID, UserChoiceSixInDice userInputChoice){
        if (diceValue == 6){
            GotSixInDice(player, totemList, diceValue, userinputTotemID, userInputChoice);
        }
        else{
            // Move available Totem (if totemStatus is OnPlay)
            if (totemList[userinputTotemID].totemStatus == TotemStatus.OnPlay){
                UpdateTotemPosition(player, totemList[userinputTotemID], diceValue);
            }
        }
    }

    private void GotSixInDice(IPlayer player, List<Totem> totemList, int diceValue, int userinputTotemID, UserChoiceSixInDice userInputChoice){
        if (userInputChoice == UserChoiceSixInDice.GetOutHome){
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

    public bool CheckTotemStatus(IPlayer player, List<Totem> totemList){
        foreach(var totem in totemList){
            if (totem.totemStatus == TotemStatus.OnPlay){
                return true;
            }
        }
        return false;
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
}

public enum UserChoiceSixInDice
{
    GetOutHome,
    MoveTotemForward
}