namespace LudoGame.Interface;

/// <summary>
/// Represent an interface of Ludo Game dice.
/// </summary>
public interface ILudoDice
{
    /// <summary>
    /// Randomly get the dice value, by using a pseudo-random number generator.
    /// </summary>
    /// <returns>Dice value.</returns>
    public int Roll();
}