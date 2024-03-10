namespace LudoGame.LudoObjects;

using System.Dynamic;
using LudoGame.GameObject;
using LudoGame.Utility;

public class Cell
{
    public CellType Type {get; private set;}
    private List<Totem> Occupants {get;set;}
    public MathVector Position {get; set;}
    public Cell(int x, int y, CellType type){
        Occupants = new List<Totem>();
        
        Type = new CellType();
        Type = type;

        Position = new MathVector();
        Position.x = x;
        Position.y = y;
    }
    public void AddTotem(Totem totem){
        Occupants.Add(totem);
    }
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
