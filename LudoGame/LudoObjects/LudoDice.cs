namespace LudoGame.LudoObjects;

using LudoGame.Interface;

public class LudoDice : ILudoDice
{    
    public int Roll(){
        Random _rnd = new();
        int _diceValue = _rnd.Next(1, 7);
        return _diceValue;
    }
    // public int GetLastRoll(){
    //     return 1;
    // }
}
