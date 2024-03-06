using LudoGame.GameFramework;
using LudoGame.GameObject;
using LudoGame.LudoObjects;

namespace LudoGame.Game;

public class LudoGameScene : IScene, IContextManager
{
    protected ISceneManager _sceneManager;
    protected LudoContext _context;
    public void Update(){}
    protected void NextTurn(){}
}
public class LudoContext
{
    public List<IPlayerWithAction> players;
    public Board board;
    public LudoDice dice;
    private Dictionary<IPlayer, List<Totem>> _playerTotem;
    
    // public List<Totem> GetTotems(IPlayer player){}
    

}
public class LudoPlayer : IPlayerWithAction
{
    public int ID {get; set;}
    private IContextManager _contextManager;
}
public class LudoRule
{
    private IContextManager<LudoContext> _contextManager;
    private Func<Board,bool> _ruleSet;
    public bool Check(IActionable action){
        return true; // example
    }
    public bool statusCheck(){
        return true; // example
    }
    public void RegisterRule(Func<Board,bool> func){}
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

