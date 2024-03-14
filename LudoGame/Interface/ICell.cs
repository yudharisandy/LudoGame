namespace LudoGame.Interface;

using LudoGame.Enums;
using LudoGame.LudoObjects;
using LudoGame.GameObject;

public interface ICell
{
    public CellType Type { get; set; }
    public Dictionary<IPlayer, List<Totem>>? Occupants { get; set; }
    public MathVector? Position { get; set; }

}