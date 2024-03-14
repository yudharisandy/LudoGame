namespace LudoGame.Interface;

using LudoGame.LudoObjects;
using LudoGame.Utility;

public interface IBoard
{
    public List<ICell>? Cells { get; set; }
    public PathBoard? Paths { get; set; }
}
