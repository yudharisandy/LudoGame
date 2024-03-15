namespace LudoGame.LudoObjects;

using LudoGame.Interface;

/// <summary>
/// Represent a dice of Ludo Game.
/// </summary>
public class LudoDice : ILudoDice
{    
    /// <summary>
    /// Randomly get the dice value, by using a pseudo-random number generator.
    /// </summary>
    /// <returns>Dice value.</returns>
    public int Roll(){
        Random _rnd = new();
        int _diceValue = _rnd.Next(1, 7);
        return _diceValue;
    }
}
