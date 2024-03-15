namespace LudoGame.Interface;

using LudoGame.Utility;
using LudoGame.Enums;

/// <summary>
/// Represent an interface of a totem or playing piece in a Ludo Game.
/// </summary>
public interface ITotem
{
    /// <summary>
    /// Represent an id of a totem.
    /// </summary>
    public int ID {get; set;}

    /// <summary>
    /// Represent a totem positition relative to board. Contains X and Y value.
    /// For all position coordinates, please refer to the "Board Coordinate Scheme" in the original repo.
    /// </summary>  
    public MathVector Position {get;set;}

    /// <summary>
    /// Represent a home base position (default position) assigned in the beginning of the game,
    /// or when a totem is kicked out by another totem.
    /// For all home position coordinates, please refer to the "Board Coordinate Scheme" in the original repo.
    /// </summary>
    public MathVector HomePosition {get;set;}

    /// <summary>
    /// Represent a position before a totem move forward to a new coordinate.
    /// Will be beneficial in the UI development.
    /// </summary>
    public MathVector PreviousPosition {get; set;}

    /// <summary>
    /// Represent a position status of a totem, whether it is on home position (OnHome),
    /// on playground (OnPlay), or on the final cell (OnFinal).
    /// </summary>
    public TotemStatus TotemStatusInfo {get; set;}

    /// <summary>
    /// Represent an index for getting the current position from a correct path coordinate list of a totem.
    /// </summary>
    public int PathStatus {get; set;}
}
