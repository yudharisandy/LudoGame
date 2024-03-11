using LudoGame.GameObject;
using LudoGame.LudoObjects;

namespace LudoGame.Game;

public class LudoContext
{
    public List<IPlayerWithAction> _players; 
    public Board board;
    public LudoDice dice;
    public Dictionary<IPlayer, List<Totem>> _playerTotems;
    
    // Constructor
    public LudoContext(){
        // Create new list when Ludo context created
        board = new Board();
        dice = new LudoDice();
        _players = new List<IPlayerWithAction>();
        _playerTotems = new Dictionary<IPlayer, List<Totem>>();
    }
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

    private void AssignCoordinate(KeyValuePair<IPlayer, List<Totem>> player, List<(int,int)> coord){
        int index = 0;
        foreach(var totem in player.Value){ // List<Totem>
            totem.HomePosition.x = coord[index].Item1;
            totem.HomePosition.y = coord[index].Item2;
            index++;
        }
    }

    public List<Totem> GetTotems(IPlayer player){
        foreach(var i in _playerTotems){
            if (i.Key == player){
                return i.Value;
            }
        }
        return null;
    }

    public bool RegisterTotems(IPlayer player, List<Totem> totems){
        _playerTotems.Add(player, totems);
        return true;
    }

    public bool RegisterPlayers(IPlayerWithAction player){
        _players.Add(player);
        return true;
    }

    public List<IPlayerWithAction> GetAllPlayers(){
        return _players;
    }

    public bool StartGame(){
        // _playerTotems -> dictionary
        return true;
    }
}