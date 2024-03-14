namespace LudoGame.LudoObjects;

using System.Dynamic;
using LudoGame.GameObject;
using LudoGame.Utility;

// Create interface -> ILudoDice

public class LudoDice
{

    public LudoDice(){
        // To highlight the randomness -> instanciate the Random object once only. 
    }
    
    // public int GetLastRoll(){
    //     return 1;
    // }

    public int Roll(){
        Random _rnd = new();
        int _diceValue = _rnd.Next(1, 7);
        return _diceValue;
    }
}
