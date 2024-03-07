using System.Dynamic;
using LudoGame.GameObject;
using LudoGame.Utility;

namespace LudoGame.LudoObjects;

public class Totem
{
    public int ID {get; set;}
    public MathVector Position {get;}
    public MathVector HomePosition {get;}
    private List<int> path;
    public IPlayer Owner {get; private set;}
    // Constructor
    public Totem(int id){
        ID = id;
    }
    public void AdvanceOnce(){}
    public void GoHome(){}
}
public class MathVector(){
    public int x;
    public int y;
}
public class Cell
{
    public CellType Type {get; private set;}
    private List<Totem> Occupants {get;set;}
    public void AddTotem(Totem totem){}
    public bool KickTotem(Totem totem){
        return true; // example
    }
    // public IPlayer GetOwnership(){}
}
public enum CellType
{
    Normal,
    Safe
}
public class LudoDice
{
    private int _diceValue;
    // public int GetLastRoll(){
    //     return 1;
    // }
    public int Roll(){
        Random rnd = new Random();
        _diceValue = rnd.Next(1, 7);
        return _diceValue;
    }
}
