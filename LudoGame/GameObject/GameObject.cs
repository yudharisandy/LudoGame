namespace LudoGame.GameObject;

using System.Dynamic;
using LudoGame.Game;
using LudoGame.Enums;

public interface IPlayer
{
    public int ID {get;}
    public PlayerTotemHome PlayersTotemHome {get; set;}
}

public interface IContextManager<T>
{
    // public T GetContext(){}
}

public interface IPlayerWithAction : IPlayer
{
    // public IActionable GetActionable(){}
}

public interface IActionable
{
    public void Step(){}
}