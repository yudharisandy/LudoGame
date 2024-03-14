namespace LudoGame.LudoObjects;

using System.Dynamic;
using LudoGame.Game;
using LudoGame.Utility;
using LudoGame.Enums;
using LudoGame.Interface;

// Create interface -> ICell (have at least)
// Create all Interface -> 
// Create a folder consist of every enums

public class Cell : ICell
{
    public CellType Type { get; set; }
    public Dictionary<IPlayer, List<ITotem>>? Occupants { get; set; }
    public MathVector? Position {get; set;}

    public Cell(int x, int y, CellType type){
        Occupants = new Dictionary<IPlayer, List<ITotem>>();
        
        Type = new CellType();
        Type = type;

        Position = new MathVector();
        Position.X = x;
        Position.Y = y;
    }
    public void AddTotem(IPlayer player, ITotem totem){
        var totemList = GetListTotemOccupants(player);
        totemList.Add(totem);
        if (Occupants is not null){ // Just to avoid warning
            if (Occupants.TryGetValue(player, out _)){ // if the key exist
                Occupants[player] = totemList; // Change the value
            }
            else{ // if the key doesn't exist
                Occupants.Add(player, totemList); // Assign the value
            }
        }
        
    }

    public bool KickTotem(IPlayer player){
        if(Occupants is not null){
            Occupants.Remove(player);
        }
        return true; // example
    }

    public List<ITotem> GetListTotemOccupants(IPlayer player){
        if(Occupants is not null){ // Just to avoid warning
            if(Occupants.Count != 0){ // Make sure the dictionary is not null
                foreach(var occupant in Occupants){
                    if (occupant.Key == player){
                        return occupant.Value;
                    }
                }
            }
        }
        return new List<ITotem>();
    }
}


