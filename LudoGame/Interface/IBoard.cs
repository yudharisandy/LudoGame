namespace LudoGame.Interface;

using LudoGame.Utility;

/// <summary>
/// An interface of Board.
/// </summary>
public interface IBoard
{
    public List<ICell>? Cells { get; set; }
    public PathBoard? Paths { get; set; }
}
