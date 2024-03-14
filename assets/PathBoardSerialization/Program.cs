// using System.Text.Json;

// // Only public property that can be serialized (by default)
// // Can add contional serialization 

// class Program 
// {
// 	static void Main() 
// 	{
//         // [Serialize]
// 		PathBoard path = new PathBoard();

// 		string json = JsonSerializer.Serialize(path);

// 		using(StreamWriter sw = new("pathBoard.json")) 
// 		{
// 			sw.WriteLine(json);
// 		}

//         // [Deserialize]
// 		// string result;
// 		// using(StreamReader sr = new("pathBoard.json")) 
// 		// {
// 		// 	result = sr.ReadToEnd();
// 		// }
// 		// PathBoard path = JsonSerializer.Deserialize<PathBoard>(result);

//         // foreach(var i in path.pathPlayer4){
//         //     System.Console.Write(i.x + ", " + i.y);
//         //     System.Console.WriteLine();
//         // }

//         // Board board = new();

//         // foreach(var i in board.Paths.pathPlayer1){
//         //     System.Console.WriteLine(i.X + " " + i.Y);
//         // }
//         // for (int i = 0; i < board.Paths.pathPlayer1.Count; i++){
//         //     System.Console.WriteLine(board.Paths.pathPlayer1[i]);
//         //     System.Console.WriteLine(board.Paths.pathPlayer1[i].X + " " + board.Paths.pathPlayer1[i].Y);
//         // }
// 	}
// }

// // public class Board
// // {
// //     public PathBoard Paths { get; set; }
// //     public Board(){
// //         string result;
// // 		using(StreamReader sr = new("../LudoGame/Utility/pathBoard.json")) 
// // 		{
// // 			result = sr.ReadToEnd();
// // 		}
// // 		Paths = JsonSerializer.Deserialize<PathBoard>(result);
// //     }
// // }

// public class PathBoard
// {
//     public List<MathVector>? pathPlayer1 {get; set;}
//     public List<MathVector>? pathPlayer2 {get; set;}
//     public List<MathVector>? pathPlayer3 {get; set;}
//     public List<MathVector>? pathPlayer4 {get; set;}

//     private List<(int, int)>? _player1PathCoordinate {get; set;}
//     private List<(int, int)>? _player2PathCoordinate {get; set;}
//     private List<(int, int)>? _player3PathCoordinate {get; set;}
//     private List<(int, int)>? _player4PathCoordinate {get; set;}

//     public PathBoard(){
//         pathPlayer1 = new List<MathVector>();
//         pathPlayer2 = new List<MathVector>();
//         pathPlayer3 = new List<MathVector>();
//         pathPlayer4 = new List<MathVector>();
//         SetCoordinate();
//         RegisterPath();
//     }
    
//     private void RegisterPath()
//     {
//         AssignToList(_player1PathCoordinate, pathPlayer1);
//         AssignToList(_player2PathCoordinate, pathPlayer2);
//         AssignToList(_player3PathCoordinate, pathPlayer3);
//         AssignToList(_player4PathCoordinate, pathPlayer4);
//     }

//     private void AssignToList(List<(int, int)> playerPathCoordinate, List<MathVector> pathPlayer){
//         for (int index = 0; index < 57; index++)
//         { // 57 points
//             MathVector vector = new();
//             vector.X = playerPathCoordinate[index].Item1;
//             vector.Y = playerPathCoordinate[index].Item2;
//             pathPlayer.Add(vector);
//         }
//     }

//     private void SetCoordinate()
//     {
//         _player1PathCoordinate = new List<(int, int)> {
//             (6, 13), (6, 12), (6, 11), (6, 10), (6, 9),
//             (5, 8), (4, 8), (3, 8), (2, 8), (1, 8), (0, 8), (0, 7),
//             (0, 6), (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
//             (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0),
//             (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
//             (9, 6), (10, 6), (11, 6), (12, 6), (13, 6), (14, 6), (14, 7),
//             (14, 8), (13, 8), (12, 8), (11, 8), (10, 8), (9, 8),
//             (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (7, 14),
//             (7, 13), (7, 12), (7, 11), (7, 10), (7, 9), (7, 8)
//         };

//         _player2PathCoordinate = new List<(int, int)> {
//             (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
//             (9, 6), (10, 6), (11, 6), (12, 6), (13, 6), (14, 6), (14, 7),
//             (14, 8), (13, 8), (12, 8), (11, 8), (10, 8), (9, 8),
//             (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (7, 14),
//             (6, 14), (6, 13), (6, 12), (6, 11), (6, 10), (6, 9),
//             (5, 8), (4, 8), (3, 8), (2, 8), (1, 8), (0, 8), (0, 7),
//             (0, 6), (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
//             (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0),
//             (7, 1), (7, 2), (7, 3), (7, 4), (7, 5), (7, 6)
//          };

//         _player3PathCoordinate = new List<(int, int)> {
//             (13, 8), (12, 8), (11, 8), (10, 8), (9, 8),
//             (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (7, 14),
//             (6, 14), (6, 13), (6, 12), (6, 11), (6, 10), (6, 9),
//             (5, 8), (4, 8), (3, 8), (2, 8), (1, 8), (0, 8), (0, 7),
//             (0, 6), (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
//             (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0),
//             (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
//             (9, 6), (10, 6), (11, 6), (12, 6), (13, 6), (14, 6), (14, 7),
//             (13, 7), (12, 7), (11, 7), (10, 7), (9, 7), (8, 7)
//         };

//         _player4PathCoordinate = new List<(int, int)> {
//             (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
//             (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0),
//             (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
//             (9, 6), (10, 6), (11, 6), (12, 6), (13, 6), (14, 6), (14, 7),
//             (14, 8), (13, 8), (12, 8), (11, 8), (10, 8), (9, 8),
//             (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (7, 14),
//             (6, 14), (6, 13), (6, 12), (6, 11), (6, 10), (6, 9),
//             (5, 8), (4, 8), (3, 8), (2, 8), (1, 8), (0, 8), (0, 7),
//             (1, 7), (2, 7), (3, 7), (4, 7), (5, 7), (6, 7)
//         };
//     }
// }

// public class MathVector(){
//     public int X {get; set;}
//     public int Y {get; set;}
// }
