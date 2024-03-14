namespace LudoGame.LudoObjects;

using System.Dynamic;
using System.Text.Json;
using LudoGame.Utility;
using LudoGame.Interface;
using LudoGame.Enums;


public class Board : IBoard
{
    public List<ICell>? Cells { get; set; }
    public PathBoard? Paths { get; set; }
    private List<(int, int)>? _allNormalCellsCoordinate;
    private List<(int, int)>? _allSafeCellsCoordinate;
    private List<(int, int)>? _allFinalCellsCoordinate;

    public Board(){

        // Deserialize Paths
        // string result;
		// using(StreamReader sr = new("../LudoGame/Utility/pathBoard.json")) 
		// {
		// 	result = sr.ReadToEnd();
		// }
        // Paths = JsonSerializer.Deserialize<PathBoard>(result);
        Paths = new PathBoard();

        // Deserialize Cells
        // string resultCell;
		// using(StreamReader sr2 = new("../LudoGame/Utility/CellTypeCoordinate.json")) 
		// {
		// 	resultCell = sr2.ReadToEnd();
		// }
        // Cells = JsonSerializer.Deserialize<List<ICell>>(resultCell);
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