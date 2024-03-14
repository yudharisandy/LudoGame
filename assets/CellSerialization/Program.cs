using System.Text.Json;

using System;
using LudoGame;
using LudoGame.Enums;
using LudoGame.Game;
using LudoGame.GameObject;
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
        // IBoard board = new Board();

		// string json = JsonSerializer.Serialize(board.Cells);

		// using(StreamWriter sw = new("CellTypeCoordinate.json")) 
		// {
		// 	sw.WriteLine(json);
		// }

        // [Deserialize]
        string resultCell;
		using(StreamReader sr2 = new("CellTypeCoordinate.json")) 
		{
			resultCell = sr2.ReadToEnd();
		}
        List<ICell>? Cells = JsonSerializer.Deserialize<List<ICell>>(resultCell);
        System.Console.WriteLine(Cells?.Count);
	}
}