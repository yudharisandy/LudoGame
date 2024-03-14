namespace LudoGame.GameObject;

public interface IContextManager<T>
{
    // public T GetContext(){}
}

public interface IActionable
{
    public void Step(){}
}