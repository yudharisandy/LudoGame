namespace LudoGameGUI;

using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication : Form
{
    private LudoContext _ludoContext;

    public LudoApplication()
    {  
        // Instanciate
        _ludoContext = new LudoContext(); 
        
        InitializeComponent();           
    }
}
