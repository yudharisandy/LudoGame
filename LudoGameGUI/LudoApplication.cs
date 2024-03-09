namespace LudoGameGUI;

using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication : Form
{
    private LudoGameScene _ludoGameScene;

    public LudoApplication()
    {  
        // Instanciate
        _ludoGameScene = new LudoGameScene();
        
        InitializeComponent();           
    }
}
