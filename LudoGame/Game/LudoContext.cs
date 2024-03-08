using LudoGame.GameObject;
using LudoGame.LudoObjects;

namespace LudoGame.Game;

public class LudoContext
{
    private List<IPlayerWithAction> _players; // Done
    public Board board;
    public LudoDice dice;
    private Dictionary<IPlayer, List<Totem>> _playerTotems; // Done
    
    // Constructor
    public LudoContext(){
        // Create new list when Ludo context created
        _players = new List<IPlayerWithAction>();
        _playerTotems = new Dictionary<IPlayer, List<Totem>>();
    }
    public void SetTotemCoordinate(){
        foreach(var player in _playerTotems){

            if(player.Key.ID == 1){ // Player 1
                List<(int,int)> coord = new();
                coord.Add((2, 11));
                coord.Add((3, 11));
                coord.Add((2, 12));
                coord.Add((3, 12));
                int index = 0;
                foreach(var totem in player.Value){ // List<Totem>
                    totem.Position.x = coord[index].Item1;
                    totem.Position.y = coord[index].Item2;
                    index++;
                }

            }
            else if(player.Key.ID == 2){ // Player 2

            }
            else if(player.Key.ID == 3){ // Player 3

            }
            else{ // Player 4
                
            }
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
    public bool RegisterTotems(List<Totem> totems){
        foreach(var player in _players){
            _playerTotems.Add(player, totems);
        }
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
        return true;
    }
}
