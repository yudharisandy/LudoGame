using System.Text.Json;

using LudoGame;
using LudoGame.Enums;
using LudoGame.Game;
using LudoGame.Interface;
using LudoGame.LudoObjects;
using LudoGame.Utility;

// Only public property that can be serialized (by default)
// Can add contional serialization 
// Parameterless constructor

class Program 
{
	static void Main() 
	{
        // [Serialize]
        // Board board = new Board();

		// string json = JsonSerializer.Serialize(board.CellsToBeSerialized);

		// using(StreamWriter sw = new("CellTypeCoordinate.json")) 
		// {
		// 	sw.Write(json);
		// }

        // [Deserialize]
		
		string resultCell;
		// using(StreamReader sr2 = new("../../LudoGame/Utility/CellTypeCoordinate.json")) 
		using(StreamReader sr2 = new("CellTypeCoordinate.json")) 
		{
			resultCell = sr2.ReadToEnd();
		}
        List<Cell> _cellsToBeDeserialized = JsonSerializer.Deserialize<List<Cell>>(resultCell);   
		// System.Console.WriteLine(_cellsToBeDeserialized?.Count);
		// System.Console.WriteLine(_cellsToBeDeserialized?.Count);   

		// foreach(var i in _cellsToBeDeserialized){
		// 	System.Console.WriteLine($"{i.Position.X}, {i.Position.Y}");
		// }

		List<ICell> Cells = new List<ICell>();
        foreach(var cells in _cellsToBeDeserialized){
            ICell subject = cells as ICell;
            if (subject != null)
            {
                Cells.Add(subject);
            }
        }
        System.Console.WriteLine(Cells?.Count);
		foreach(var i in Cells){
			System.Console.WriteLine($"{i.Position.X}, {i.Position.Y}");
		}
	}
}