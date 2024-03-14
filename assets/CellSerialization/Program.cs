using System.Text.Json;

using System;
// using LudoGame;
// using LudoGame.Game;
// using LudoGame.LudoObjects;

// using LudoGame.GameObject;
// using LudoGame.Utility;
// using LudoGame.Interface;
// using LudoGame.Enums;

// Only public property that can be serialized (by default)
// Can add contional serialization 
// Parameterless constructor

class Program 
{
	static void Main() 
	{
        // [Serialize]
        Board board = new();

		string json = JsonSerializer.Serialize(board.Cells);

		using(StreamWriter sw = new("CellTypeCoordinate.json")) 
		{
			sw.WriteLine(json);
		}

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

public class Board
{
    public List<ICell> Cells {get; set;}
    public PathBoard? Paths { get; set; }
    private List<(int, int)>? _allNormalCellsCoordinate;
    private List<(int, int)>? _allSafeCellsCoordinate;
    private List<(int, int)>? _allFinalCellsCoordinate;

    public Board(){

        string result;
		using(StreamReader sr = new("../LudoGame/Utility/pathBoard.json")) 
		{
			result = sr.ReadToEnd();
		}
        Paths = JsonSerializer.Deserialize<PathBoard>(result);

        Cells = new List<ICell>();

        RegisterAllCell();
    }


    private void RegisterAllCell(){
        UpdateNormalCellsCoordinate();
        UpdateSafeCellsCoordinate();
        UpdateFinalCellsCoordinate();

        if(_allNormalCellsCoordinate is not null){ // Reduce the warning
            foreach(var i in _allNormalCellsCoordinate){
                ICell cell = new Cell(i.Item1, i.Item2, CellType.Normal);
                Cells.Add(cell);
            }
        }

        if(_allSafeCellsCoordinate is not null){
            foreach(var i in _allSafeCellsCoordinate){
                ICell cell = new Cell(i.Item1, i.Item2, CellType.Safe);
                Cells.Add(cell);
            }
        }

        if(_allFinalCellsCoordinate is not null){
            foreach(var i in _allFinalCellsCoordinate){
                ICell cell = new Cell(i.Item1, i.Item2, CellType.Final);
                Cells.Add(cell);
            }
        }
    }

    private void UpdateNormalCellsCoordinate(){
        _allNormalCellsCoordinate = new List<(int, int)> { // 44
            (6, 14), (6, 12), (6, 11), (6, 10), (6, 9),
            (5, 8), (4, 8), (3, 8), (1, 8), (0, 8), (0, 7),
            (0, 6), (2, 6), (3, 6), (4, 6), (5, 6),
            (6, 5), (6, 4), (6, 3), (6, 1), (6, 0), (7, 0),
            (8, 0), (8, 2), (8, 3), (8, 4), (8, 5),
            (9, 6), (10, 6), (11, 6), (13, 6), (14, 6), (14, 7),
            (14, 8), (12, 8), (11, 8), (10, 8), (9, 8),
            (8, 9), (8, 10), (8, 11), (8, 13), (8, 14), (7, 14),
        };
    }

    private void UpdateSafeCellsCoordinate(){
        _allSafeCellsCoordinate = new List<(int, int)> { // 
            (2, 8), (6, 2), (12, 6), (8, 12),
            (6, 13), (1, 6), (8, 1), (13, 8)
        };
    }

    private void UpdateFinalCellsCoordinate(){
        _allFinalCellsCoordinate = new List<(int, int)> { // 
            (7, 8), (6, 7), (7, 6), (8, 7)
        };
    }
}



// Archive
// namespace LudoGame.LudoObjects;

// using System.Dynamic;
// using LudoGame.Game;
// using LudoGame.GameObject;
// using LudoGame.Utility;

public class Cell : ICell
{
    public CellType Type { get; set; }
    public Dictionary<IPlayer, List<Totem>>? Occupants { get; set; }
    public MathVector? Position {get; set;}

    public Cell(int x, int y, CellType type){
        Occupants = new Dictionary<IPlayer, List<Totem>>();
        
        Type = new CellType();
        Type = type;

        Position = new MathVector();
        Position.X = x;
        Position.Y = y;
    }

    public void AddTotem(IPlayer player, Totem totem){
        var totemList = GetListTotemOccupants(player);
        totemList.Add(totem);
        if (Occupants.TryGetValue(player, out _)){ // if the key exist
            Occupants[player] = totemList; // Change the value
        }
        else{ // if the key doesn't exist
            Occupants.Add(player, totemList); // Assign the value
        }
        
    }

    public bool KickTotem(IPlayer player){
        Occupants.Remove(player);
        return true; // example
    }

    public List<Totem> GetListTotemOccupants(IPlayer player){
        if(Occupants.Count != 0){ // Make sure the dictionary is not null
            foreach(var occupant in Occupants){
                if (occupant.Key == player){
                    return occupant.Value;
                }
            }
        }
        return new List<Totem>();
    }
    // public IPlayer GetOwnership(){}
}

public enum CellType
{
    Normal,
    Safe,
    Final
}

public interface ICell
{
    public CellType Type { get; set; }
    public Dictionary<IPlayer, List<Totem>>? Occupants { get; set; }
    public MathVector? Position { get; set; }

}

public interface IPlayer
{
    public int ID {get;}
    public PlayerTotemHome PlayersTotemHome {get; set;}
}


public class Totem
{
    public int ID {get; set;}
    public MathVector Position {get;set;}
    public MathVector HomePosition {get;set;}
    public MathVector PreviousPosition {get; set;}
    // private List<int>? path;
    // public IPlayer? Owner {get; private set;}
    public TotemStatus totemStatus;
    public int pathStatus;
    // Constructor
    public Totem(int id){
        ID = id;
        totemStatus = (int)TotemStatus.OnHome;
        pathStatus = 0;
        Position = new MathVector();
        HomePosition = new MathVector();
        PreviousPosition = new MathVector();
    }
    public void AdvanceOnce(){}
    public void GoHome(){}
}


public class MathVector
{
    public int X { get; set; }
    public int Y { get; set; }
}

public enum TotemStatus
{
    OnHome,
    OnPlay,
    OnFinal
}

public enum PlayerTotemHome
{
    StillHaveInHome,
    AllOnPlay
}


public class PathBoard
{
    public List<MathVector> pathPlayer1 { get; set; } = null!;
    public List<MathVector> pathPlayer2 { get; set; } = null!;
    public List<MathVector> pathPlayer3 { get; set; } = null!;
    public List<MathVector> pathPlayer4 { get; set; } = null!;

    // private List<(int, int)> _player1PathCoordinate;
    // private List<(int, int)> _player2PathCoordinate;
    // private List<(int, int)> _player3PathCoordinate;
    // private List<(int, int)> _player4PathCoordinate;

    public PathBoard(){
        // pathPlayer1 = new List<MathVector>();
        // pathPlayer2 = new List<MathVector>();
        // pathPlayer3 = new List<MathVector>();
        // pathPlayer4 = new List<MathVector>();
        // SetCoordinate();
        // RegisterPath();
    }
    
    // private void RegisterPath()
    // {
    //     AssignToList(_player1PathCoordinate, pathPlayer1);
    //     AssignToList(_player2PathCoordinate, pathPlayer2);
    //     AssignToList(_player3PathCoordinate, pathPlayer3);
    //     AssignToList(_player4PathCoordinate, pathPlayer4);
    // }

    // private void AssignToList(List<(int, int)> playerPathCoordinate, List<MathVector> pathPlayer){
    //     for (int index = 0; index < 57; index++)
    //     { // 57 points
    //         MathVector vector = new();
    //         vector.x = playerPathCoordinate[index].Item1;
    //         vector.y = playerPathCoordinate[index].Item2;
    //         pathPlayer.Add(vector);
    //     }
    // }

    // private void SetCoordinate()
    // {
    //     _player1PathCoordinate = new List<(int, int)> {
    //         (6, 13), (6, 12), (6, 11), (6, 10), (6, 9),
    //         (5, 8), (4, 8), (3, 8), (2, 8), (1, 8), (0, 8), (0, 7),
    //         (0, 6), (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
    //         (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0),
    //         (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
    //         (9, 6), (10, 6), (11, 6), (12, 6), (13, 6), (14, 6), (14, 7),
    //         (14, 8), (13, 8), (12, 8), (11, 8), (10, 8), (9, 8),
    //         (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (7, 14),
    //         (7, 13), (7, 12), (7, 11), (7, 10), (7, 9), (7, 8)
    //     };

    //     _player2PathCoordinate = new List<(int, int)> {
    //         (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
    //         (9, 6), (10, 6), (11, 6), (12, 6), (13, 6), (14, 6), (14, 7),
    //         (14, 8), (13, 8), (12, 8), (11, 8), (10, 8), (9, 8),
    //         (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (7, 14),
    //         (6, 14), (6, 13), (6, 12), (6, 11), (6, 10), (6, 9),
    //         (5, 8), (4, 8), (3, 8), (2, 8), (1, 8), (0, 8), (0, 7),
    //         (0, 6), (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
    //         (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0),
    //         (7, 1), (7, 2), (7, 3), (7, 4), (7, 5), (7, 6)
    //      };

    //     _player3PathCoordinate = new List<(int, int)> {
    //         (13, 8), (12, 8), (11, 8), (10, 8), (9, 8),
    //         (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (7, 14),
    //         (6, 14), (6, 13), (6, 12), (6, 11), (6, 10), (6, 9),
    //         (5, 8), (4, 8), (3, 8), (2, 8), (1, 8), (0, 8), (0, 7),
    //         (0, 6), (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
    //         (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0),
    //         (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
    //         (9, 6), (10, 6), (11, 6), (12, 6), (13, 6), (14, 6), (14, 7),
    //         (13, 7), (12, 7), (11, 7), (10, 7), (9, 7), (8, 7)
    //     };

    //     _player4PathCoordinate = new List<(int, int)> {
    //         (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
    //         (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0),
    //         (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
    //         (9, 6), (10, 6), (11, 6), (12, 6), (13, 6), (14, 6), (14, 7),
    //         (14, 8), (13, 8), (12, 8), (11, 8), (10, 8), (9, 8),
    //         (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (7, 14),
    //         (6, 14), (6, 13), (6, 12), (6, 11), (6, 10), (6, 9),
    //         (5, 8), (4, 8), (3, 8), (2, 8), (1, 8), (0, 8), (0, 7),
    //         (1, 7), (2, 7), (3, 7), (4, 7), (5, 7), (6, 7)
    //     };
    // }
}
