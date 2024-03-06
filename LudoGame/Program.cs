using LudoGame.GameFramework;
using LudoGame.GameObject;

class Program{
    static void Main(){

    }
}
public class LudoGameScene : IScene, IContextManager
{
    // # ISceneManager _sceneManager
    // # LudoContext _context
    // # void NextTurn()
}
public class LudoContext
{
    // + List~IPlayerWithAction~ players
    // + Board board
    // + LudoDice dice
    // - Dictionary~ IPlayer,List ~Totem~ ~ _playerTotem
    // + List~Totem~ GetTotems(IPlayer)
}
public class LudoPlayer : IPlayerWithAction
{
    // - IContextManager _contextManager
}
public class LudoRule
{
    // - IContextManager~LudoContext~ _contextManager
    // - Func~Board,bool~ _ruleSet
    // + bool Check(IActionable)
    // + bool statusCheck()
    // + void RegisterRule(Func~Board,bool~)
}
public interface IContextManager
{
    // Ludo Context
}
public class LudoActionable : IActionable
{
    // - int power
}
public class LudoTotemMoveTogether : LudoActionable
{
    // - Totem
    // + void Bind(Totem, Totem)
}
public class SingleTotemActionable : LudoActionable
{
    // - Totem bindedTotem
    // * void Bind(totem)
}
public class LudoTotemStart : SingleTotemActionable
{

}
public class LudoTotemMove : SingleTotemActionable
{

}
