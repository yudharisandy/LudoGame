using System.Dynamic;

namespace LudoGame.GameObject;

public interface IPlayer
{
    public int ID {get;}
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