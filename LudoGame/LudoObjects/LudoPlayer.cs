namespace LudoGame.LudoObjects;

using LudoGame.Interface;

/// <summary>
/// Represent a Ludo Player (user).
/// </summary>
public class LudoPlayer : ILudoPlayer
{
    /// <summary>
    /// Represent an id of a player.
    /// </summary>
    public int ID {get; set;}

    /// <summary>
    /// Initializes a new instance of the LudoGame.LudoObjects.LudoPlayer class using an id.
    /// </summary>
    /// <param name="id">Identity number.</param>
    public LudoPlayer(int id){
        ID = id;
    }
}
