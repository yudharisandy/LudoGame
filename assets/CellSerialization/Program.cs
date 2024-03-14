using System.Text.Json;

using System;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;


// Only public property that can be serialized (by default)
// Can add contional serialization 
// Parameterless constructor

class Program 
{
	static void Main() 
	{
        // [Serialize]
        // Board board = new();

		// string json = JsonSerializer.Serialize(board.Cells);

		// using(StreamWriter sw = new("CellTypeCoordinate.json")) 
		// {
		// 	sw.WriteLine(json);
		// }

        // [Deserialize]
        // string resultCell;
		// using(StreamReader sr2 = new("../LudoGame/Utility/CellTypeCoordinate.json")) 
		// {
		// 	resultCell = sr2.ReadToEnd();
		// }
        // List<Cell>? Cells = JsonSerializer.Deserialize<List<Cell>>(resultCell);
        // System.Console.WriteLine(Cells?.Count);

	}
}


// Archive

// namespace LudoGame.LudoObjects;

// using System.Dynamic;
// using LudoGame.GameObject;
// using LudoGame.Utility;
// using System.Text.Json;

// public class Board
// {
//     public List<Cell> Cells {get; private set;}
//     public PathBoard? Paths { get; set; }
//     private List<(int, int)>? _allNormalCellsCoordinate;
//     private List<(int, int)>? _allSafeCellsCoordinate;
//     private List<(int, int)>? _allFinalCellsCoordinate;

//     public Board(){

//         string result;
// 		using(StreamReader sr = new("../LudoGame/Utility/pathBoard.json")) 
// 		{
// 			result = sr.ReadToEnd();
// 		}
//         Paths = JsonSerializer.Deserialize<PathBoard>(result);

//         // Cells = new List<Cell>();
//         string resultCell;
// 		using(StreamReader sr = new("../LudoGame/Utility/CellTypeCoordinate.json")) 
// 		{
// 			resultCell = sr.ReadToEnd();
// 		}
//         Cells = JsonSerializer.Deserialize<List<Cell>>(resultCell);

//         // RegisterAllCell();
//     }


//     // private void RegisterAllCell(){
//     //     UpdateNormalCellsCoordinate();
//     //     UpdateSafeCellsCoordinate();
//     //     UpdateFinalCellsCoordinate();

//     //     if(_allNormalCellsCoordinate is not null){ // Reduce the warning
//     //         foreach(var i in _allNormalCellsCoordinate){
//     //             Cell cell = new(i.Item1, i.Item2, CellType.Normal);
//     //             Cells.Add(cell);
//     //         }
//     //     }

//     //     if(_allSafeCellsCoordinate is not null){
//     //         foreach(var i in _allSafeCellsCoordinate){
//     //             Cell cell = new(i.Item1, i.Item2, CellType.Safe);
//     //             Cells.Add(cell);
//     //         }
//     //     }

//     //     if(_allFinalCellsCoordinate is not null){
//     //         foreach(var i in _allFinalCellsCoordinate){
//     //             Cell cell = new(i.Item1, i.Item2, CellType.Final);
//     //             Cells.Add(cell);
//     //         }
//     //     }
//     // }

//     // private void UpdateNormalCellsCoordinate(){
//     //     _allNormalCellsCoordinate = new List<(int, int)> { // 44
//     //         (6, 14), (6, 12), (6, 11), (6, 10), (6, 9),
//     //         (5, 8), (4, 8), (3, 8), (1, 8), (0, 8), (0, 7),
//     //         (0, 6), (2, 6), (3, 6), (4, 6), (5, 6),
//     //         (6, 5), (6, 4), (6, 3), (6, 1), (6, 0), (7, 0),
//     //         (8, 0), (8, 2), (8, 3), (8, 4), (8, 5),
//     //         (9, 6), (10, 6), (11, 6), (13, 6), (14, 6), (14, 7),
//     //         (14, 8), (12, 8), (11, 8), (10, 8), (9, 8),
//     //         (8, 9), (8, 10), (8, 11), (8, 13), (8, 14), (7, 14),
//     //     };
//     // }

//     // private void UpdateSafeCellsCoordinate(){
//     //     _allSafeCellsCoordinate = new List<(int, int)> { // 
//     //         (2, 8), (6, 2), (12, 6), (8, 12),
//     //         (6, 13), (1, 6), (8, 1), (13, 8)
//     //     };
//     // }

//     // private void UpdateFinalCellsCoordinate(){
//     //     _allFinalCellsCoordinate = new List<(int, int)> { // 
//     //         (7, 8), (6, 7), (7, 6), (8, 7)
//     //     };
//     // }
// }



// Archive
// namespace LudoGame.LudoObjects;

// using System.Dynamic;
// using LudoGame.Game;
// using LudoGame.GameObject;
// using LudoGame.Utility;

// public class Cell
// {
//     public CellType Type {get; set;}
//     public Dictionary<IPlayer, List<Totem>> Occupants {get;set;}
//     public MathVector Position {get; set;}

//     public Cell(int x, int y, CellType type){
//         // Occupants = new Dictionary<IPlayer, List<Totem>>();
        
//         // Type = new CellType();
//         // Type = type;

//         // Position = new MathVector();
//         // Position.X = x;
//         // Position.Y = y;
//     }

//     public void AddTotem(IPlayer player, Totem totem){
//         var totemList = GetListTotemOccupants(player);
//         totemList.Add(totem);
//         if (Occupants.TryGetValue(player, out _)){ // if the key exist
//             Occupants[player] = totemList; // Change the value
//         }
//         else{ // if the key doesn't exist
//             Occupants.Add(player, totemList); // Assign the value
//         }
        
//     }

//     public bool KickTotem(IPlayer player){
//         Occupants.Remove(player);
//         return true; // example
//     }

//     public List<Totem> GetListTotemOccupants(IPlayer player){
//         if(Occupants.Count != 0){ // Make sure the dictionary is not null
//             foreach(var occupant in Occupants){
//                 if (occupant.Key == player){
//                     return occupant.Value;
//                 }
//             }
//         }
//         return new List<Totem>();
//     }
//     // public IPlayer GetOwnership(){}
// }

// public enum CellType
// {
//     Normal,
//     Safe,
//     Final
// }
