namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication
{
    private void CreateGrid()
    {
        // Create TableLayoutPanel for the grid
        this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
        this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Right; // Dock to the right side
        this.tableLayoutPanel.Size = new System.Drawing.Size(800, 800); // Adjust the size to fit the form
        this.tableLayoutPanel.Location = new System.Drawing.Point(200, 0); // Adjusted the starting position from the left edge
        this.tableLayoutPanel.Name = "tableLayoutPanel";
        this.tableLayoutPanel.RowCount = 15;
        this.tableLayoutPanel.ColumnCount = 15;
        string imagePath = "E:/Kelapa/Course, Seminar, Pelatihan/2024-2 Formulatrix Bootcamp C#/LudoGame/";
        Image backgroundImage = Image.FromFile(imagePath + "assets/ludoBoard.jpg");
        backgroundImage = new Bitmap(backgroundImage, this.tableLayoutPanel.Width, this.tableLayoutPanel.Height);
        this.tableLayoutPanel.BackgroundImage = backgroundImage;

        for (int i = 0; i < 15; i++)
        {
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F / 15F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F / 15F));
        }
        this.Controls.Add(this.tableLayoutPanel);
    }
    private void AddColorGrid(){
        // [Interact to Library]
        List<(int, int)> playCoordinate = _ludoContext.board.GetBoardCoordinate();
        
        foreach(var i in playCoordinate){
            Panel coloredPanel = new Panel();
            coloredPanel.BackColor = Color.Turquoise;
            // coloredPanel.Dock = DockStyle.Fill;
            // Add panels to each cell and set their background color
            int x = i.Item1; // Example row index
            int y = i.Item2; // Example column index
            this.tableLayoutPanel.Controls.Add(coloredPanel, x, y);
        }
    }

}

