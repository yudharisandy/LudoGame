namespace LudoGameGUI;


using System.Windows.Forms;
using LudoGame;
using LudoGame.Enums;
using LudoGame.Game;
using LudoGame.GameObject;
using LudoGame.Interface;
using LudoGame.LudoObjects;
using LudoGame.Utility;

public partial class LudoApplication
{
    private System.ComponentModel.IContainer components = null;
    private TableLayoutPanel tableLayoutPanel;


    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1100, 800);
        this.Text = "LudoApp";
        
        #region Preparation
        CreateTotemPlayerButton();
        #endregion

        #region Board
        CreateGrid();
        // AddColorGrid(); // Interact with library -> LudoContext.Board
        #endregion

        #region Dice
        CreateDiceButton(); // Interact with library -> LudoContext.LudoDice
        CreateDiceResultLabel();
        #endregion

        #region Player
        CreateAddPlayerButton(); // Interact with library -> LudoPlayer, LudoContext
        CreatePlayersLabel();
        CreateInputPlayerTextBox();
        #endregion

        #region Totem
        CreateAddTotemButton(); // Interact with library -> Totem, LudoContext
        CreateInputTotemTextBox();
        CreateTotemsLabel();
        #endregion

        #region Start
        CreateStartButton(); // Interact with library -> LudoContext
        CreateStartLabel();
        #endregion

        #region  On-game
        CreatePlayerTurnLabel();
        #endregion
    }
    #endregion

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }
}

