namespace LudoGameGUI;

using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication : Form
{
    private Cell _cell;
    private LudoDice _dice;
    private Board _board;
    private LudoContext _ludoContext;

    public LudoApplication()
    {
        // Instanciate
        _cell = new Cell();
        _dice = new LudoDice();
        _board = new Board();   
        _ludoContext = new LudoContext(); 
        
        InitializeComponent();           
    }
}
