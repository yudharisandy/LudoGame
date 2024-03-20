namespace LudoGame.Interface;

using LudoGame.Utility;
using LudoGame.LudoObjects;

/// <summary>
/// Represent an interface of playing board in Ludo Game.
/// </summary>
public interface IBoard
{
    /// <summary>
    /// Represent a container cell of all available cells in the game.
    /// </summary>
    public List<ICell>? Cells { get; set; }
    public List<Cell>? CellsToBeSerialized { get; set; }
    public List<Cell>? CellsToBeDeserialized { get; set; }

    /// <summary>
    /// Represent an object that contains all players' totem path.
    /// </summary>
    public PathBoard? Paths { get; set; }
}

 