namespace LudoGame.LudoObjects;

using System.Dynamic;
using LudoGame.GameObject;
using LudoGame.Utility;

public class LudoDice
{
    private int _diceValue;

    // public int GetLastRoll(){
    //     return 1;
    // }
    
    public int Roll(){
        Random rnd = new Random();
        _diceValue = rnd.Next(1, 7);
        return _diceValue;
    }
}
