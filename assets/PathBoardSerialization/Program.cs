using System.Text.Json;
using LudoGame;
using LudoGame.Enums;
using LudoGame.Game;
using LudoGame.Interface;
using LudoGame.LudoObjects;
using LudoGame.Utility;

// Only public property that can be serialized (by default)
// Can add conditional serialization 

class Program 
{
	static void Main() 
	{
        // [Serialize]
		// PathBoard path = new PathBoard();

		// string json = JsonSerializer.Serialize(path);

		// using(StreamWriter sw = new("PathBoardCoordinate.json")) 
		// {
		// 	sw.Write(json);
		// }

        // [Deserialize]
		string result;
		// using(StreamReader sr = new("pathBoard.json")) 
		// {
		// 	result = sr.ReadToEnd();
		// }
		// PathBoard path = JsonSerializer.Deserialize<PathBoard>(result);

        // foreach(var i in path.pathPlayer4){
        //     System.Console.Write(i.x + ", " + i.y);
        //     System.Console.WriteLine();
        // }

        // Board board = new();

        // foreach(var i in board.Paths.pathPlayer1){
        //     System.Console.WriteLine(i.X + " " + i.Y);
        // }
        // for (int i = 0; i < board.Paths.pathPlayer1.Count; i++){
        //     System.Console.WriteLine(board.Paths.pathPlayer1[i]);
        //     System.Console.WriteLine(board.Paths.pathPlayer1[i].X + " " + board.Paths.pathPlayer1[i].Y);
        // }
	}
}

