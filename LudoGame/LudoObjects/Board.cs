namespace LudoGame.LudoObjects;

using System.Text.Json;
using LudoGame.Utility;
using LudoGame.Interface;
using LudoGame.Enums;

/// <summary>
/// Represent a playing board of the Ludo Game.
/// </summary>
public class Board : IBoard
{
    /// <summary>
    /// Represent a container cell of all available cells in the game.
    /// </summary>
    public List<ICell>? Cells { get; set; }

    /// <summary>
    /// Represent a container cell of all available cells in the game, for serialization purpose only.
    /// </summary>
    public List<Cell>? CellsToBeSerialized { get; set; }

    /// <summary>
    /// Represent a container cell of all available cells in the game, for deserialization purpose only.
    /// </summary>
    public List<Cell>? CellsToBeDeserialized { get; set; }

    /// <summary>
    /// Represent an object that contains all players' totem path.
    /// </summary>
    public PathBoard? Paths { get; set; }

    /// <summary>
    /// [Deprecated]
    /// Represent all normal cells coordinate.
    /// </summary>
    // private List<(int, int)>? _allNormalCellsCoordinate;

    /// <summary>
    /// [Deprecated]
    /// Represent all safe cells coordinate.
    /// </summary>
    // private List<(int, int)>? _allSafeCellsCoordinate;

    /// <summary>
    /// [Deprecated]
    /// Represent all final cells coordinate.
    /// </summary>
    // private List<(int, int)>? _allFinalCellsCoordinate;

    /// <summary>
    /// Initialize a new instance of LudoGame.LudoObjects.Board, and instanciate other properties.
    /// </summary>
    public Board(){

        // [Deserialize Paths]
        string resultPath;
		using(StreamReader sr = new("../LudoGame/Utility/PathBoardCoordinate.json")) 
		{
			resultPath = sr.ReadToEnd();
		}
        Paths = JsonSerializer.Deserialize<PathBoard>(resultPath);
        // Paths = new PathBoard(); // Not needed -> Has been deserialized.

        // [Deserialize Cells]
        string resultCell;
		using(StreamReader sr2 = new("../LudoGame/Utility/CellTypeCoordinate.json")) 
		{
			resultCell = sr2.ReadToEnd();
		}
        CellsToBeDeserialized = JsonSerializer.Deserialize<List<Cell>>(resultCell);
        Cells = new List<ICell>();
        foreach(var cells in CellsToBeDeserialized){
            ICell subject = cells as ICell;
            if (subject != null)
            {
                Cells.Add(subject);
            }
        }

        // RegisterAllCell(); // Not needed -> Has been deserialized.
        // For serilization only:
        // CellsToBeSerialized = new List<Cell>();
        // RegisterAllCellToBeSerialized();        
    }

    /// <summary>
    /// [Deprecated]
    /// Assign the coordinate to the Cells and Paths property.
    /// </summary>
    // private void RegisterAllCell(){
    //     UpdateNormalCellsCoordinate();
    //     UpdateSafeCellsCoordinate();
    //     UpdateFinalCellsCoordinate();

    //     if(_allNormalCellsCoordinate is not null){ // Reduce the warning
    //         foreach(var i in _allNormalCellsCoordinate){
    //             ICell cell = new Cell(i.Item1, i.Item2, CellType.Normal);
    //             Cells.Add(cell);
    //         }
    //     }

    //     if(_allSafeCellsCoordinate is not null){
    //         foreach(var i in _allSafeCellsCoordinate){
    //             ICell cell = new Cell(i.Item1, i.Item2, CellType.Safe);
    //             Cells.Add(cell);
    //         }
    //     }

    //     if(_allFinalCellsCoordinate is not null){
    //         foreach(var i in _allFinalCellsCoordinate){
    //             ICell cell = new Cell(i.Item1, i.Item2, CellType.Final);
    //             Cells.Add(cell);
    //         }
    //     }
    // }

    /// <summary>
    /// [Deprecated]
    /// Assign normal cell coordinate(tuple of int) into a list.
    /// </summary>
    // private void UpdateNormalCellsCoordinate(){
    //     _allNormalCellsCoordinate = new List<(int, int)> { // 44
    //         (6, 14), (6, 12), (6, 11), (6, 10), (6, 9),
    //         (5, 8), (4, 8), (3, 8), (1, 8), (0, 8), (0, 7),
    //         (0, 6), (2, 6), (3, 6), (4, 6), (5, 6),
    //         (6, 5), (6, 4), (6, 3), (6, 1), (6, 0), (7, 0),
    //         (8, 0), (8, 2), (8, 3), (8, 4), (8, 5),
    //         (9, 6), (10, 6), (11, 6), (13, 6), (14, 6), (14, 7),
    //         (14, 8), (12, 8), (11, 8), (10, 8), (9, 8),
    //         (8, 9), (8, 10), (8, 11), (8, 13), (8, 14), (7, 14),
            
    //         (7, 13), (7, 12), (7, 11), (7, 10), (7, 9), // Normal cells in final area Player 1
    //         (1, 7), (2, 7), (3, 7), (4, 7), (5, 7), // Normal cells in final area Player 4
    //         (7, 1), (7, 2), (7, 3), (7, 4), (7, 5), // Normal cells in final area Player 2
    //         (13, 7), (12, 7), (11, 7), (10, 7), (9, 7) // Normal cells in final area Player 3
    //     };
    // }

    /// <summary>
    /// [Deprecated]
    /// Assign normal safe coordinates (tuple of int) into a list.
    /// </summary>
    // private void UpdateSafeCellsCoordinate(){
    //     _allSafeCellsCoordinate = new List<(int, int)> { // 
    //         (2, 8), (6, 2), (12, 6), (8, 12),
    //         (6, 13), (1, 6), (8, 1), (13, 8)
    //     };
    // }

    /// <summary>
    /// [Deprecated]
    /// Assign normal final coordinate(tuple of int) into a list.
    /// </summary>
    // private void UpdateFinalCellsCoordinate(){
    //     _allFinalCellsCoordinate = new List<(int, int)> { // 
    //         (7, 8), (6, 7), (7, 6), (8, 7)
    //     };
    // }

    /// <summary>
    /// [Deprecated]
    /// Method to compliment the cell serialization.
    /// For creating CellTypeCoordinate.json file.
    /// </summary>
    // private void RegisterAllCellToBeSerialized(){
    //     UpdateNormalCellsCoordinate();
    //     UpdateSafeCellsCoordinate();
    //     UpdateFinalCellsCoordinate();

    //     if(_allNormalCellsCoordinate is not null){ // Reduce the warning
    //         foreach(var i in _allNormalCellsCoordinate){
    //             Cell cell = new Cell(i.Item1, i.Item2, CellType.Normal);
    //             CellsToBeSerialized.Add(cell);
    //         }
    //     }

    //     if(_allSafeCellsCoordinate is not null){
    //         foreach(var i in _allSafeCellsCoordinate){
    //             Cell cell = new Cell(i.Item1, i.Item2, CellType.Safe);
    //             CellsToBeSerialized.Add(cell);
    //         }
    //     }

    //     if(_allFinalCellsCoordinate is not null){
    //         foreach(var i in _allFinalCellsCoordinate){
    //             Cell cell = new Cell(i.Item1, i.Item2, CellType.Final);
    //             CellsToBeSerialized.Add(cell);
    //         }
    //     }
    // }
}