namespace LudoGame.Interface;

using LudoGame.Enums;
using LudoGame.LudoObjects;
using LudoGame.Utility;

/// <summary>
/// An interface for Cell.
/// </summary>
public interface ICell
{
    public CellType Type { get; set; }
    public Dictionary<IPlayer, List<ITotem>>? Occupants { get; set; }
    public MathVector? Position { get; set; }

    public void AddTotem(IPlayer player, ITotem totem);
    public bool KickTotem(IPlayer player);
    public List<ITotem> GetListTotemOccupants(IPlayer player);

}