namespace LudoGame.LudoObjects;

using LudoGame.Utility;
using LudoGame.Interface;
using LudoGame.Enums;

/// <summary>
/// Represent a totem or playing piece in a Ludo Game.
/// </summary>
public class Totem : ITotem
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

    /// <summary>
    /// Initializes a new instance of the LudoGame.LudoObjects.Totem class using an id,
    /// and initialize other properties needed.
    /// </summary>
    /// <param name="id">Totem id.</param>
    public Totem(int id){
        ID = id;
        TotemStatusInfo = (int)TotemStatus.OnHome;
        PathStatus = 0;
        Position = new MathVector();
        HomePosition = new MathVector();
        PreviousPosition = new MathVector();
    }
}


