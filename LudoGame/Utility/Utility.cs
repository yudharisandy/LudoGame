using System.Security.Principal;
using LudoGame.LudoObjects;

namespace LudoGame.Utility;

public class PathBoard
{
    public List<MathVector> pathPlayer1;
    public List<MathVector> pathPlayer2;
    public List<MathVector> pathPlayer3;
    public List<MathVector> pathPlayer4;

    private List<(int, int)> _player1PathCoordinate;
    private List<(int, int)> _player2PathCoordinate;
    private List<(int, int)> _player3PathCoordinate;
    private List<(int, int)> _player4PathCoordinate;

    public PathBoard(){
        pathPlayer1 = new List<MathVector>();
        pathPlayer2 = new List<MathVector>();
        pathPlayer3 = new List<MathVector>();
        pathPlayer4 = new List<MathVector>();
        SetCoordinate();
        RegisterPath();

    }
    
    private void RegisterPath()
    {
        AssignToList(_player1PathCoordinate, pathPlayer1);
        AssignToList(_player2PathCoordinate, pathPlayer2);
        AssignToList(_player3PathCoordinate, pathPlayer3);
        AssignToList(_player4PathCoordinate, pathPlayer4);
    }

    private void AssignToList(List<(int, int)> playerPathCoordinate, List<MathVector> pathPlayer){
        for (int index = 0; index < 57; index++)
        { // 57 points
            MathVector vector = new();
            vector.x = playerPathCoordinate[index].Item1;
            vector.y = playerPathCoordinate[index].Item2;
            pathPlayer.Add(vector);
        }
    }

    private void SetCoordinate()
    {
        _player1PathCoordinate = new List<(int, int)> {
            (6, 13), (6, 12), (6, 11), (6, 10), (6, 9),
            (5, 8), (4, 8), (3, 8), (2, 8), (1, 8), (0, 8), (0, 7),
            (0, 6), (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
            (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0),
            (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
            (9, 6), (10, 6), (11, 6), (12, 6), (13, 6), (14, 6), (14, 7),
            (14, 8), (13, 8), (12, 8), (11, 8), (10, 8), (9, 8),
            (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (7, 14),
            (7, 13), (7, 12), (7, 11), (7, 10), (7, 9), (7, 8)
        };

        _player2PathCoordinate = new List<(int, int)> {
            (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
            (9, 6), (10, 6), (11, 6), (12, 6), (13, 6), (14, 6), (14, 7),
            (14, 8), (13, 8), (12, 8), (11, 8), (10, 8), (9, 8),
            (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (7, 14),
            (6, 14), (6, 13), (6, 12), (6, 11), (6, 10), (6, 9),
            (5, 8), (4, 8), (3, 8), (2, 8), (1, 8), (0, 8), (0, 7),
            (0, 6), (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
            (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0),
            (7, 1), (7, 2), (7, 3), (7, 4), (7, 5), (7, 6)
         };

        _player3PathCoordinate = new List<(int, int)> {
            (13, 8), (12, 8), (11, 8), (10, 8), (9, 8),
            (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (7, 14),
            (6, 14), (6, 13), (6, 12), (6, 11), (6, 10), (6, 9),
            (5, 8), (4, 8), (3, 8), (2, 8), (1, 8), (0, 8), (0, 7),
            (0, 6), (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
            (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0),
            (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
            (9, 6), (10, 6), (11, 6), (12, 6), (13, 6), (14, 6), (14, 7),
            (13, 7), (12, 7), (11, 7), (10, 7), (9, 7), (8, 7)
        };

        _player4PathCoordinate = new List<(int, int)> {
            (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
            (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0),
            (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
            (9, 6), (10, 6), (11, 6), (12, 6), (13, 6), (14, 6), (14, 7),
            (14, 8), (13, 8), (12, 8), (11, 8), (10, 8), (9, 8),
            (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (7, 14),
            (6, 14), (6, 13), (6, 12), (6, 11), (6, 10), (6, 9),
            (5, 8), (4, 8), (3, 8), (2, 8), (1, 8), (0, 8), (0, 7),
            (1, 7), (2, 7), (3, 7), (4, 7), (5, 7), (6, 7)
        };
    }
}
