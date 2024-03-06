using LudoGame.Game;
using LudoGame.GameObject;
using LudoGame.LudoObjects;

public class Program{
    static void Main(){
        // Create player
        IPlayerWithAction player1 = new LudoPlayer();
        IPlayerWithAction player2 = new LudoPlayer();

        // Create ludo objects
        Totem totem1Player1 = new();
        Totem totem1Player2 = new();

        Cell cell = new();
        LudoDice ludoDice = new();
        Board board = new();
    }
}
