namespace LudoGame.LudoObjects;

using System.Dynamic;
using LudoGame.Game;
using LudoGame.GameObject;
using LudoGame.Utility;
using LudoGame.Enums;
using LudoGame.Interface;

// Create interface -> ICell (have at least)
// Create all Interface -> 
// Create a folder consist of every enums

public class Cell : ICell
{
    public CellType Type { get; set; }
    public Dictionary<IPlayer, List<Totem>>? Occupants { get; set; }
    public MathVector? Position {get; set;}


    public void AddTotem(IPlayer player, Totem totem){
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

    public List<Totem> GetListTotemOccupants(IPlayer player){
        if(Occupants is not null){ // Just to avoid warning
            if(Occupants.Count != 0){ // Make sure the dictionary is not null
                foreach(var occupant in Occupants){
                    if (occupant.Key == player){
                        return occupant.Value;
                    }
                }
            }
        }
        return new List<Totem>();
    }
    // public IPlayer GetOwnership(){}
}


