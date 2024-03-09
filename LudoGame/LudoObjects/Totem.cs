namespace LudoGame.LudoObjects;

using System.Dynamic;
using LudoGame.GameObject;
using LudoGame.Utility;

public class Totem
{
    public int ID {get; set;}
    public MathVector Position {get;set;}
    public MathVector HomePosition {get;set;}
    public MathVector PreviousPosition {get; set;}
    private List<int> path;
    public IPlayer Owner {get; private set;}
    public TotemStatus totemStatus;
    public int pathStatus;
    // Constructor
    public Totem(int id){
        ID = id;
        totemStatus = (int)TotemStatus.OnHome;
        pathStatus = 0;
        Position = new MathVector();
        HomePosition = new MathVector();
        PreviousPosition = new MathVector();
    }
    public void AdvanceOnce(){}
    public void GoHome(){}
}
public class MathVector(){
    public int x;
    public int y;
}
public enum TotemStatus
{
    OnHome,
    OnPlay
}