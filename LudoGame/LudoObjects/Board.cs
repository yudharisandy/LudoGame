using System.Dynamic;
using LudoGame.GameObject;
using LudoGame.Utility;

namespace LudoGame.LudoObjects;

public class Board
{
    private int _xBoard = 14; // Size of Ludo Board (14, 14) - start from 0
    private int _yBoard = 14; // Size of Ludo board (14, 14) - start from 0
    private List<(int, int)> _boardCoordinates;
    public Board(){
        _boardCoordinates = new List<(int, int)>();
        AddBoardCoordinate();
    }
    public List<Cell> Cells {get; private set;}
    public List<PathBoard> Paths {get; private set;}

    private void AddBoardCoordinate(){
        // Up part of the borad (x, y)
        for (int y = 0; y <= 5; y++){
            for (int x = 6; x <= 8; x++){
                _boardCoordinates.Add((x, y));
            }
        }
        // Left part of the borad (x, y)
        for (int y = 6; y <= 8; y++){
            for (int x = 0; x <= 5; x++){
                _boardCoordinates.Add((x, y));
            }
        }
        // Right part of the board (x, y)
        for (int y = 6; y <= 8; y++){
            for (int x = 9; x <= _xBoard; x++){
                _boardCoordinates.Add((x, y));
            }
        }
        // Bottom part of the board (x, y)
        for (int y = 9; y <= _yBoard; y++){
            for (int x = 6; x <= 8; x++){
                _boardCoordinates.Add((x, y));
            }
        }
        // 4 Middle-cell of the board (x, y)
        _boardCoordinates.Add((7, 6));
        _boardCoordinates.Add((7, 8));
        _boardCoordinates.Add((6, 7));
        _boardCoordinates.Add((7, 8));

        // Add 4 home coordinates (2x2 each)
        

    }
    public List<(int, int)> GetBoardCoordinate(){
        return _boardCoordinates;
    }

}