using LudoGame.GameObject;

namespace LudoGame.GameFramework;

public class GameEngine
{
    // # Stack~IScene~ _sceneStack
    // # Queue~SceneManagementCommand~ sceneCommand
    // # Queue~IScene~ stagedScene
    public void Run(){}
    public void Setup(){}
    protected void Loop(){}
    protected void Render(){}
}
public class ConsoleGameEngine
{

}
public class RenderSystem
{
    // input: ConsoleRenderable

}

public class RenderSystem<T>
{
    // <<static>>
    public List<T> Renderables;
    public void RegisterRenderable(T something){}
}
public enum SceneManagementCommand
{
    INSERT,
    EXIT
}
public interface ISceneManager
{
    public void StageSceneExit();
    public void StageSceneInsert(IScene s);
    public void CommitScene();
    public void GetCurrentScene();
}
public class ConsoleRenderable : IRenderable
{
    public ConsoleRenderable(){}
}
public interface IScene
{
    public void Update();
}
public interface IRenderable
{
    // <<interface>>
    // + void Draw()
}
