namespace LudoGame.Interface;

using LudoGame.Enums;

/// <summary>
/// An interface for LudoPlayer.
/// </summary>
public interface ILudoPlayer : IPlayer
{
    public int ID {get; set;}
    public PlayerTotemHome PlayersTotemHome {get; set;}
}