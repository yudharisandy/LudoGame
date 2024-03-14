namespace LudoGame.Game;

using LudoGame.GameFramework;
using LudoGame.GameObject;
using LudoGame.LudoObjects;
using LudoGame.Interface;
using LudoGame.Enums;

public interface IContextManager
{
    // Ludo Context
}

public class LudoActionable : IActionable
{
    // private int _power;
}

public class LudoTotemMoveTogether : LudoActionable
{
    // - Totem
    public void Bind(ITotem a, ITotem b){}
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

