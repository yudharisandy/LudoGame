namespace LudoGame.Game;

using LudoGame.GameFramework;
using LudoGame.GameObject;
using LudoGame.LudoObjects;
using LudoGame.Interface;

public class LudoRule
{
    // private IContextManager<LudoContext>? _contextManager;
    // private Func<Board,bool>? _ruleSet;

    public bool Check(IActionable action){
        return true; // example
    }

    public bool statusCheck(){
        return true; // example
    }
    
    public void RegisterRule(Func<IBoard,bool> func){

    }
}
