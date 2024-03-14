namespace LudoGame.Interface;

using LudoGame.Utility;
using LudoGame.Enums;

/// <summary>
/// An interface for Totem.
/// </summary>
public interface ITotem
{
    public int ID {get; set;}
    public MathVector Position {get;set;}
    public MathVector HomePosition {get;set;}
    public MathVector PreviousPosition {get; set;}
    public TotemStatus TotemStatusInfo {get; set;}
    public int PathStatus {get; set;}

    public void AdvanceOnce();
    public void GoHome();
}
