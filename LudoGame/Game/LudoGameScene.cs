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
    public Totem MoveOutHomePosition(KeyValuePair<IPlayer, List<Totem>> player, int diceValue){
        // Move out of HomePosition
        if (player.Key.ID == 1 && diceValue == 6){
            player.Value[0].Position.x = ludoContext.board.Paths.pathPlayer1[0].x;
            player.Value[0].Position.y = ludoContext.board.Paths.pathPlayer1[0].y;
            return player.Value[0];
        }
        else if (player.Key.ID == 2 && diceValue == 6){
            player.Value[0].Position.x = ludoContext.board.Paths.pathPlayer2[0].x;
            player.Value[0].Position.y = ludoContext.board.Paths.pathPlayer2[0].y;
            return player.Value[0];
        }
        else if (player.Key.ID == 3 && diceValue == 6){
            player.Value[0].Position.x = ludoContext.board.Paths.pathPlayer3[0].x;
            player.Value[0].Position.y = ludoContext.board.Paths.pathPlayer3[0].y;
            return player.Value[0];
        }
        else if (player.Key.ID == 4 && diceValue == 6){
            player.Value[0].Position.x = ludoContext.board.Paths.pathPlayer4[0].x;
            player.Value[0].Position.y = ludoContext.board.Paths.pathPlayer4[0].y;
            return player.Value[0];
        }
        return player.Value[0];
    }
}