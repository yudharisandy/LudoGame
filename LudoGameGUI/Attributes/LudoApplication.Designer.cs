namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication
{
    private System.ComponentModel.IContainer components = null;
    private TableLayoutPanel tableLayoutPanel;


    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(900, 800); // Increased width to accommodate additional space
        this.Text = "LudoApp";
        
        #region // Board
        CreateGrid();
        StartButton();
        CreateStartLabel();
        ColorTheGrid();
        #endregion

        #region // Dice
        CreateDiceButton();
        CreateDiceResultLabel(); // Create the label to display the dice result
        #endregion

        #region // Player
        CreatePlayersLabel();
        CreateInputPlayerTextBox();
        CreateAddPlayerButton();
        #endregion

        #region // Totem
        CreateAddTotemButton();
        CreateInputTotemTextBox();
        CreateTotemsLabel();
        #endregion
    }
    #endregion
}

