namespace LudoGame.LudoObjects;

using LudoGame.Interface;
using LudoGame.Enums;

public class LudoPlayer : ILudoPlayer
{
    public int ID {get; set;}
    public PlayerTotemHome PlayersTotemHome {get; set;} 

    public LudoPlayer(int id){
        ID = id;
        PlayersTotemHome = PlayerTotemHome.StillHaveInHome;
    }
}
