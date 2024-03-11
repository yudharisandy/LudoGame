namespace LudoGame.LudoObjects;

using System.Dynamic;
using LudoGame.Game;
using LudoGame.GameObject;
using LudoGame.Utility;

public class Cell
{
    public CellType Type {get; private set;}
    public Dictionary<IPlayer, Totem> Occupants {get;set;}
    public MathVector Position {get; set;}
    public Cell(int x, int y, CellType type){
        Occupants = new Dictionary<IPlayer, Totem>();
        
        Type = new CellType();
        Type = type;

        Position = new MathVector();
        Position.x = x;
        Position.y = y;

        Occupants = new Dictionary<IPlayer, Totem>();
    }
    public void AddTotem(IPlayer player, Totem totem){
        Occupants.Add(player, totem);
    }
    public bool KickTotem(IPlayer player){
        Occupants.Remove(player);
        return true; // example
    }
    // public IPlayer GetOwnership(){}
}
public enum CellType
{
    Normal,
    Safe
}
