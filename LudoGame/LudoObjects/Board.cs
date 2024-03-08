namespace LudoGame.LudoObjects;

using System.Dynamic;
using LudoGame.GameObject;
using LudoGame.Utility;

public class Board
{
    private int _xBoard = 14; // Size of Ludo Board (14, 14) - start from 0
    private int _yBoard = 14; // Size of Ludo board (14, 14) - start from 0
    private List<(int, int)> _boardCoordinates;
    public List<Cell> Cells {get; private set;}
    public List<PathBoard> Paths {get; private set;}

    public Board(){
        _boardCoordinates = new List<(int, int)>();
        AddBoardCoordinate();
    }
    // public void PutTotemOnBoard(){

    // }

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
        _boardCoordinates.Add((8, 7));

        #region Add 4 home coordinates (2x2 each)
        // Player 1 (bottom-left)
        _boardCoordinates.Add((2, 11));
        _boardCoordinates.Add((3, 11));
        _boardCoordinates.Add((2, 12));
        _boardCoordinates.Add((3, 12));
        // Player 2 (up-right)
        _boardCoordinates.Add((11, 2));
        _boardCoordinates.Add((12, 2));
        _boardCoordinates.Add((11, 3));
        _boardCoordinates.Add((12, 3));
        // Player 3 (bottom-right)
        _boardCoordinates.Add((11, 11));
        _boardCoordinates.Add((12, 11));
        _boardCoordinates.Add((11, 12));
        _boardCoordinates.Add((12, 12));
        // Player 4 (up-left)
        _boardCoordinates.Add((2, 2));
        _boardCoordinates.Add((3, 2));
        _boardCoordinates.Add((2, 3));
        _boardCoordinates.Add((3, 3));        
        #endregion

    }
    public List<(int, int)> GetBoardCoordinate(){
        return _boardCoordinates;
    }

}