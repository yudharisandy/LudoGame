using LudoGame.GameObject;
using LudoGame.LudoObjects;

namespace LudoGame.Game;

public class LudoContext
{
    public List<IPlayerWithAction> players;
    public Board board;
    public LudoDice dice;
    private Dictionary<IPlayer, List<Totem>> _playerTotem;
    
    // Constructor
    public LudoContext(){
        // Create new list when Ludo context created
        players = new List<IPlayerWithAction>();
        _playerTotem = new Dictionary<IPlayer, List<Totem>>();
    }
    public List<Totem> GetTotems(IPlayer player){
        foreach(var i in _playerTotem){
            if (i.Key == player){
                return i.Value;
            }
        }
        return null;
    }
    public void RegisterTotems(IPlayer player, List<Totem> totems){
        _playerTotem.Add(player, totems);
    }

}
