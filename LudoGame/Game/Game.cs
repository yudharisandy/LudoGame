namespace LudoGame.Game;

using LudoGame.GameFramework;
using LudoGame.GameObject;
using LudoGame.LudoObjects;

public class LudoPlayer : IPlayerWithAction
{
    public int ID {get; set;}
    private IContextManager _contextManager;
    public LudoPlayer(int id){
        ID = id;
    }
}

public interface IContextManager
{
    // Ludo Context
}
public class LudoActionable : IActionable
{
    private int _power;
}
public class LudoTotemMoveTogether : LudoActionable
{
    // - Totem
    public void Bind(Totem a, Totem b){}
}
public class SingleTotemActionable : LudoActionable
{
    // - Totem bindedTotem
    // * void Bind(totem)
    public void Bind(){}
}
public class LudoTotemStart : SingleTotemActionable
{

}
public class LudoTotemMove : SingleTotemActionable
{

}

