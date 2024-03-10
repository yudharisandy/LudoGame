namespace LudoGame.LudoObjects;

using System.Dynamic;
using LudoGame.GameObject;
using LudoGame.Utility;

public class Board
{
    private int _xBoard = 14; // Size of Ludo Board (14, 14) - start from 0
    private int _yBoard = 14; // Size of Ludo board (14, 14) - start from 0
    // private List<(int, int)> _boardCoordinates;
    public List<Cell> Cells {get; private set;}
    public PathBoard Paths {get;set;}
    private List<(int, int)> _allNormalCellsCoordinate;
    private List<(int, int)> _allSafeCellsCoordinate;

    public Board(){
        // _boardCoordinates = new List<(int, int)>();
        Paths = new PathBoard();
        Cells = new List<Cell>();

        RegisterAllCell();
    }
    private void RegisterAllCell(){
        UpdateNormalCellsCoordinate();
        UpdateSafeCellsCoordinate();

        foreach(var i in _allNormalCellsCoordinate){
            Cell cell = new(i.Item1, i.Item2, CellType.Normal);
            Cells.Add(cell);
        }

        foreach(var i in _allSafeCellsCoordinate){
            Cell cell = new(i.Item1, i.Item2, CellType.Safe);
            Cells.Add(cell);
        }
    }
    private void UpdateNormalCellsCoordinate(){
        _allNormalCellsCoordinate = new List<(int, int)> {
            (6, 13), (6, 12), (6, 11), (6, 10), (6, 9),
            (5, 8), (4, 8), (3, 8), (2, 8), (1, 8), (0, 8), (0, 7),
            (0, 6), (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
            (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0),
            (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
            (9, 6), (10, 6), (11, 6), (12, 6), (13, 6), (14, 6), (14, 7),
            (14, 8), (13, 8), (12, 8), (11, 8), (10, 8), (9, 8),
            (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (7, 14),
            (6, 14)
        };
    }
    private void UpdateSafeCellsCoordinate(){
        _allSafeCellsCoordinate = new List<(int, int)> {
            (6, 13), (6, 12), (6, 11), (6, 10), (6, 9),
            (5, 8), (4, 8), (3, 8), (2, 8), (1, 8), (0, 8), (0, 7),
            (0, 6), (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
            (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0),
            (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
            (9, 6), (10, 6), (11, 6), (12, 6), (13, 6), (14, 6), (14, 7),
            (14, 8), (13, 8), (12, 8), (11, 8), (10, 8), (9, 8),
            (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (7, 14),
            (6, 14)
        };
    }
    // public void PutTotemOnBoard(){

    // }

    // private void AddBoardCoordinate(){
    //     // Up part of the borad (x, y)
    //     for (int y = 0; y <= 5; y++){
    //         for (int x = 6; x <= 8; x++){
    //             _boardCoordinates.Add((x, y));
    //         }
    //     }
    //     // Left part of the borad (x, y)
    //     for (int y = 6; y <= 8; y++){
    //         for (int x = 0; x <= 5; x++){
    //             _boardCoordinates.Add((x, y));
    //         }
    //     }
    //     // Right part of the board (x, y)
    //     for (int y = 6; y <= 8; y++){
    //         for (int x = 9; x <= _xBoard; x++){
    //             _boardCoordinates.Add((x, y));
    //         }
    //     }
    //     // Bottom part of the board (x, y)
    //     for (int y = 9; y <= _yBoard; y++){
    //         for (int x = 6; x <= 8; x++){
    //             _boardCoordinates.Add((x, y));
    //         }
    //     }
    //     // 4 Middle-cell of the board (x, y)
    //     _boardCoordinates.Add((7, 6));
    //     _boardCoordinates.Add((7, 8));
    //     _boardCoordinates.Add((6, 7));
    //     _boardCoordinates.Add((8, 7));

    //     #region Add 4 home coordinates (2x2 each)
    //     // Player 1 (bottom-left)
    //     _boardCoordinates.Add((2, 11));
    //     _boardCoordinates.Add((3, 11));
    //     _boardCoordinates.Add((2, 12));
    //     _boardCoordinates.Add((3, 12));
    //     // Player 2 (up-right)
    //     _boardCoordinates.Add((11, 2));
    //     _boardCoordinates.Add((12, 2));
    //     _boardCoordinates.Add((11, 3));
    //     _boardCoordinates.Add((12, 3));
    //     // Player 3 (bottom-right)
    //     _boardCoordinates.Add((11, 11));
    //     _boardCoordinates.Add((12, 11));
    //     _boardCoordinates.Add((11, 12));
    //     _boardCoordinates.Add((12, 12));
    //     // Player 4 (up-left)
    //     _boardCoordinates.Add((2, 2));
    //     _boardCoordinates.Add((3, 2));
    //     _boardCoordinates.Add((2, 3));
    //     _boardCoordinates.Add((3, 3));        
    //     #endregion

    // }
    // public List<(int, int)> GetBoardCoordinate(){
    //     return _boardCoordinates;
    // }

}