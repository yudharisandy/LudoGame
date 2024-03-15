namespace LudoGame.LudoObjects;

using LudoGame.Utility;
using LudoGame.Enums;
using LudoGame.Interface;


/// <summary>
/// Represent a cell or playing block on the board of Ludo Game.
/// </summary>
public class Cell : ICell
{
    /// <summary>
    /// Represent a type of a cell (normal, safe, final).
    /// For all cell type information, please refer to the "Board Coordinate Scheme" in the original repo.
    /// </summary>
    public CellType Type { get; set; }

    /// <summary>
    /// Represent a dictionary that contains all current player-totem exist in a certain cell.
    /// </summary>
    public Dictionary<IPlayer, List<ITotem>>? Occupants { get; set; }

    /// <summary>
    /// Represent a cell positition relative to board. Contains X and Y value.
    /// For all cell coordinates, please refer to the "Board Coordinate Scheme" in the original repo.
    /// </summary>
    public MathVector? Position {get; set;}

    /// <summary>
    /// Initializes a new instance of the LudoGame.Cell class using a default x, y, and type.
    /// </summary>
    /// <param name="x">Horizontal position.</param>
    /// <param name="y">Vertical position.</param>
    /// <param name="type">Cell type (normal, safe, final).</param>
    public Cell(int x, int y, CellType type){
        Occupants = new Dictionary<IPlayer, List<ITotem>>();
        
        Type = new CellType();
        Type = type;

        Position = new MathVector();
        Position.X = x;
        Position.Y = y;
    }

    /// <summary>
    /// Add IPlayer and totem to the existing Occupants.
    /// </summary>
    /// <param name="player">Current player.</param>
    /// <param name="totem">Current totem.</param>
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

    /// <summary>
    /// Remove/kick IPlayer and totem from the existing Occupants. 
    /// </summary>
    /// <param name="player">Player to be removed.</param>
    /// <param name="totem">Totem to be removed.</param>
    /// <returns>True: Remove sucessfully, False: Fail to remove.</returns>
    public bool KickTotem(IPlayer player, ITotem totem){
        var totemLists = new List<ITotem>();
        if(Occupants is not null){
            foreach(var playerTotem in Occupants){
                if(playerTotem.Key.ID == player.ID){
                    totemLists = playerTotem.Value;
                    foreach(var totemToRemove in playerTotem.Value){
                        if(totemToRemove.ID == totem.ID){
                            totemLists.Remove(totemToRemove);
                            Occupants[player] = totemLists;
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Gett all ITotem(s) exist in the current cell of a certain player.
    /// </summary>
    /// <param name="player">Current player.</param>
    /// <returns>Totems list</returns>
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


