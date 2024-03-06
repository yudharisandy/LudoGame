using System.Dynamic;
using LudoGame.GameObject;
using LudoGame.Utility;

namespace LudoGame.LudoObjects;

public class Totem
{
    public MathVector Position {get;}
    public MathVector HomePosition {get;}
    private List<int> path;
    public IPlayer Owner {get; private set;}
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
    public int GetLastRoll(){
        return 1;
    }
    public int Roll(){
        return 1;
    }
}
public class Board
{
    public List<Cell> Cells {get; private set;}
    public List<PathBoard> Paths {get; private set;}
}