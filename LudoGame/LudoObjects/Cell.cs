namespace LudoGame.LudoObjects;

using System.Dynamic;
using LudoGame.GameObject;
using LudoGame.Utility;

public class Cell
{
    public CellType Type {get; private set;}
    private List<Totem> Occupants {get;set;}
    public MathVector Position {get; set;}
    public Cell(){
        Occupants = new List<Totem>();
        Type = new CellType();
        Position = new MathVector();
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
