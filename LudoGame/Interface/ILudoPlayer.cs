namespace LudoGame.Interface;

using LudoGame.Enums;

/// <summary>
/// Represent an interface for Ludo Game Player (user).
/// </summary>
public interface ILudoPlayer : IPlayer
{
    /// <summary>
    /// Represent an id of a player.
    /// </summary>
    public int ID {get; set;}
}