namespace LudoGame.LudoObjects;

using System.Dynamic;
using LudoGame.GameObject;
using LudoGame.Utility;

public class LudoDice
{
    private int _diceValue;
    private Random _rnd; 

    public LudoDice(){
        // To highlight the randomness -> instanciate the Random object once only.
        _rnd = new Random(); 
    }
    
    // public int GetLastRoll(){
    //     return 1;
    // }

    public int Roll(){
        _diceValue = _rnd.Next(1, 7);
        return _diceValue;
    }
}
