namespace LudoGame.LudoObjects;

using System.Dynamic;
using LudoGame.GameObject;
using LudoGame.Utility;

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
