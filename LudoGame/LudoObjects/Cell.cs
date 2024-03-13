namespace LudoGame.LudoObjects;

using System.Dynamic;
using LudoGame.Game;
using LudoGame.GameObject;
using LudoGame.Utility;

public class Cell
{
    public CellType Type {get; private set;}
    public Dictionary<IPlayer, List<Totem>> Occupants {get;set;}
    public MathVector Position {get; set;}
    public Cell(int x, int y, CellType type){
        Occupants = new Dictionary<IPlayer, List<Totem>>();
        
        Type = new CellType();
        Type = type;

        Position = new MathVector();
        Position.x = x;
        Position.y = y;
    }
    public void AddTotem(IPlayer player, Totem totem){
        var totemList = GetListTotemOccupants(player);
        totemList.Add(totem);

        if(Occupants.TryGetValue(player, out List<Totem> val)){ // if the key exist
            Occupants[player] = totemList; // Change the value
        }
        else{ // if the key doesn't exist
            Occupants.Add(player, totemList); // Assign the value
        }
        
    }
    public bool KickTotem(IPlayer player){
        Occupants.Remove(player);
        return true; // example
    }

    public List<Totem> GetListTotemOccupants(IPlayer player){
        if(Occupants.Count != 0){ // Make sure the dictionary is not null
            foreach(var occupant in Occupants){
                if (occupant.Key == player){
                    return occupant.Value;
                }
            }
        }
        return new List<Totem>();
    }
    // public IPlayer GetOwnership(){}
}
public enum CellType
{
    Normal,
    Safe,
    Final
}
