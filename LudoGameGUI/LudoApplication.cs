namespace LudoGameGUI;

using LudoGame.Game;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

public partial class LudoApplication : Form
{
    private LudoGameScene _ludoGameScene;

    public LudoApplication()
    {  
        // Logging
        ILoggerFactory loggerFactory = LoggerFactory.Create(log =>
		{
			log.SetMinimumLevel(LogLevel.Information);
			log.AddNLog("nlog.config");
			//log.AddLog4Net("log4net.config");
		});
		ILogger<LudoGameScene> logger = loggerFactory.CreateLogger<LudoGameScene>();
        
        // Instanciate
        _ludoGameScene = new LudoGameScene(logger);
        
        InitializeComponent();           
    }
}
