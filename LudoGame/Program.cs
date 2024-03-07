namespace  LudoGame;

using LudoGame.Game;
using LudoGame.GameObject;
using LudoGame.LudoObjects;

public class Program{
    static void Main(){
        // // Create ludo objects
        // Cell cell = new();
        // LudoDice ludoDice = new();
        // Board board = new();

        // // Create player instances
        // IPlayerWithAction player1 = new LudoPlayer(1);        
        // IPlayerWithAction player2 = new LudoPlayer(2);

        // // Create totme instances
        // Totem totem1Player1 = new(1);
        // // Totem totem2Player1 = new(2);
        // // Totem totem3Player1 = new(3);
        // Totem totem1Player2 = new(1);

        // // Create ludo context instance (to store all ludo live objects)
        // LudoContext ludoContext = new();

        // // Assign players to ludo context
        // // ludoContext._players.Add(player1);
        // // ludoContext.players.Add(player2);
        // // System.Console.WriteLine(ludoContext.players);

        // // Assign totems to dict of player-totems in ludo context
        // List<Totem> totemsPlayer1 = new();
        // List<Totem> totemsPlayer2 = new();
        // totemsPlayer1.Add(totem1Player1);
        // // totemsPlayer1.Add(totem2Player1);
        // // totemsPlayer1.Add(totem3Player1);
        // totemsPlayer2.Add(totem1Player2);
        // ludoContext.RegisterTotems(player1, totemsPlayer1);
        // ludoContext.RegisterTotems(player2, totemsPlayer2);

        // // Check totems that were already registered
        // var totemplayersatu = ludoContext.GetTotems(player1);
        // foreach (var i in totemplayersatu){
        //     System.Console.WriteLine(i);
        // }

        // // This stage: Already have dict Players and List<Totem>

        // // Start the game

        // // Get all available board coordinates
        // var boardCoordinate = board.GetBoardCoordinate();
        // System.Console.WriteLine($"boardCoordinate count: {boardCoordinate.Count}");
        // foreach (var i in boardCoordinate){
        //     System.Console.WriteLine(i);
        // }

        
    }
}
