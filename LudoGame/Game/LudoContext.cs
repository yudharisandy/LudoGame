namespace LudoGame.Game;

using LudoGame.Interface;
using LudoGame.LudoObjects;

/// <summary>
/// A class for setting up the game at the beginning (initial state).
/// </summary>
public class LudoContext
{
    public List<IPlayer> _players; 
    public IBoard board;
    public ILudoDice dice;
    public Dictionary<IPlayer, List<ITotem>> _playerTotems;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public LudoContext(){
        board = new Board();
        dice = new LudoDice();
        _players = new List<IPlayer>();
        _playerTotems = new Dictionary<IPlayer, List<ITotem>>();
    }
    
    /// <summary>
    /// Initiate the coordinates of totem home position.
    /// </summary>
    public void AssignTotemHomePosition(){
        foreach(var player in _playerTotems){
            var coord = new List<(int,int)>();
            if(player.Key.ID == 0){ // Player 1
                coord.AddRange(new List<(int,int)> { (2, 11), (3, 11), (2, 12), (3, 12) });
            }
            else if(player.Key.ID == 1){ // Player 2
                coord.AddRange(new List<(int,int)> { (11, 2), (12, 2), (11, 3), (12, 3) }); 
            }
            else if(player.Key.ID == 2){ // Player 3
                coord.AddRange(new List<(int,int)> { (11, 11), (12, 11), (11, 12), (12, 12) });
            }
            else{ // Player 4
                coord.AddRange(new List<(int,int)> { (2, 2), (3, 2), (2, 3), (3, 3) });
            }
            AssignCoordinate(player, coord);
        }
    }

    /// <summary>
    /// Apply the totem home coordinates to each totem, each player
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="coord">Coordinate</param>
    private void AssignCoordinate(KeyValuePair<IPlayer, List<ITotem>> player, List<(int,int)> coord){
        int index = 0;
        foreach(var totem in player.Value){ // List<Totem>
            totem.HomePosition.X = coord[index].Item1;
            totem.HomePosition.Y = coord[index].Item2;

            // In the beginning set Position to HomePosition
            totem.Position.X = totem.HomePosition.X;
            totem.Position.Y = totem.HomePosition.Y;
            
            index++;
        }
    }

    /// <summary>
    /// Gets a total totem number assigned to each player.
    /// </summary>
    /// <returns>Number of totems</returns>
    public int GetTotalNumberTotemsEachPlayer(){
        int numberTotems = 0;
        foreach(var totems in _playerTotems){
            numberTotems = totems.Value.Count;
            break;
        }
        return numberTotems;
    }

    /// <summary>
    /// A method to fulfil a dictionary of totems of each player.
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="totems">List of totem</param>
    /// <returns></returns>
    public bool RegisterTotems(IPlayer player, List<ITotem> totems){
        _playerTotems.Add(player, totems);
        return true;
    }

    /// <summary>
    /// A method to fulfil a list of IPlayer.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool RegisterPlayers(IPlayer player){
        _players.Add(player);
        return true;
    }

    /// <summary>
    /// A method to change the game status to true.
    /// </summary>
    /// <returns>True</returns>
    public bool StartGame(){
        return true;
    }
}
