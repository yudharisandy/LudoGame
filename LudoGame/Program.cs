namespace LudoGame;

using LudoGame.Game;
using LudoGame.GameObject;
using LudoGame.LudoObjects;

public class Program
{
    static void Main()
    {
        // Instanciate LudoGameScene
        var _ludoGameScene = new LudoGameScene();

        // Register player
        int numberOfPlayers = 4;
        for (int i = 0; i < numberOfPlayers; i++)
        {
            LudoPlayer _ludoPlayer = new(i);
            bool status = _ludoGameScene.ludoContext.RegisterPlayers(_ludoPlayer);
        }

        // Register Totem
        int numberOfTotems = 4;
        foreach (var player in _ludoGameScene.ludoContext._players)
        {
            List<Totem> totemsList = new();

            for (int i = 0; i < numberOfTotems; i++)
            {
                Totem _totem = new(i);
                totemsList.Add(_totem);
            }
            bool status = _ludoGameScene.ludoContext.RegisterTotems(player, totemsList);
        }

        // Roll the dice
        // int diceValue = _ludoGameScene.ludoContext.dice.Roll();
        int diceValue = 0;
        int userInputTotemID;

        // Run the game
        while (true)
        {
            // Loop for every player
            foreach (var player in _ludoGameScene.ludoContext._playerTotems)
            {
                // Player turn
                System.Console.WriteLine($"Turn: Player {player.Key.ID + 1}");

                // Roll dice
                System.Console.Write("Input dice: ");
                string diceString = Console.ReadLine();
                int.TryParse(diceString, out diceValue);
                // System.Console.WriteLine(diceValue);

                // Choose totem to be moved
                System.Console.Write("Totem to be moved: ");
                string userInputTotemIDString = Console.ReadLine();
                int.TryParse(userInputTotemIDString, out userInputTotemID);
                // System.Console.WriteLine(userInputTotemID);

                _ludoGameScene.NextTurn(player.Key, player.Value, diceValue, userInputTotemID);

                // ... method to update each totems position in your interface

                System.Console.WriteLine("--------------------------");
            }
        }

    }
}
