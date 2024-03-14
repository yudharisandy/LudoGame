namespace LudoGame.LudoObjects;

using LudoGame.Utility;
using LudoGame.Interface;
using LudoGame.Enums;

public class Totem : ITotem
{
    public int ID {get; set;}
    public MathVector Position {get;set;}
    public MathVector HomePosition {get;set;}
    public MathVector PreviousPosition {get; set;}
    public TotemStatus TotemStatusInfo {get; set;}
    public int PathStatus {get; set;}

    public Totem(int id){
        ID = id;
        TotemStatusInfo = (int)TotemStatus.OnHome;
        PathStatus = 0;
        Position = new MathVector();
        HomePosition = new MathVector();
        PreviousPosition = new MathVector();
    }
    public void AdvanceOnce(){}
    public void GoHome(){}
}


