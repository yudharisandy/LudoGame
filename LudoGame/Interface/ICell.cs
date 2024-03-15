namespace LudoGame.Interface;

using LudoGame.Enums;
using LudoGame.Utility;

/// <summary>
/// Represent an interface of cell or playing block on the board of Ludo Game.
/// </summary>
public interface ICell
{
    /// <summary>
    /// Represent a type of a cell (normal, safe, final).
    /// For all cell type information, please refer to the "Board Coordinate Scheme" in the original repo.
    /// </summary>v
    public CellType Type { get; set; }

    /// <summary>
    /// Represent a dictionary that contains all current player-totem exist in a certain cell.
    /// </summary>
    public Dictionary<IPlayer, List<ITotem>>? Occupants { get; set; }

    /// <summary>
    /// Represent a cell positition relative to board. Contains X and Y value.
    /// For all cell coordinates, please refer to the "Board Coordinate Scheme" in the original repo.
    /// </summary>
    public MathVector? Position { get; set; }

    /// <summary>
    /// Add IPlayer and totem to the existing Occupants.
    /// </summary>
    /// <param name="player">Current player.</param>
    /// <param name="totem">Current totem.</param>
    public void AddTotem(IPlayer player, ITotem totem);

    /// <summary>
    /// Remove/kick IPlayer and totem from the existing Occupants. 
    /// </summary>
    /// <param name="player">Player to be removed.</param>
    /// <param name="totem">Totem to be removed.</param>
    /// <returns>True: Remove sucessfully, False: Fail to remove.</returns>
    public bool KickTotem(IPlayer player, ITotem totem);

    /// <summary>
    /// Gett all ITotem(s) exist in the current cell of a certain player.
    /// </summary>
    /// <param name="player">Current player.</param>
    /// <returns>Totems list.</returns>
    public List<ITotem> GetListTotemOccupants(IPlayer player);

}