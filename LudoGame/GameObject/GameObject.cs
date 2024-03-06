namespace LudoGame.GameObject;

public interface IPlayer
{
    // <<interface>>
    // + ID : readonly
}
public interface IContextManager<T>
{
    // <<interface>>
    // + T GetContext()
}
public interface IPlayerWithAction : IPlayer
{
    // <<interface>>
    // + IActionable GetActionable()
}
public interface IActionable
{
    // <<interface>>
    // + Step()    
}