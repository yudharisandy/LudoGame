namespace LudoGameGUI;

using LudoGame.Game;

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
