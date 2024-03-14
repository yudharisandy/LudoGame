namespace LudoGameGUI;

using System.Windows.Forms;
using LudoGame;
using LudoGame.Enums;
using LudoGame.Game;
using LudoGame.Interface;
using LudoGame.LudoObjects;
using LudoGame.Utility;

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
        Image backgroundImage = Image.FromFile("../assets/ludoBoard3.jpg");
        backgroundImage = new Bitmap(backgroundImage, this.tableLayoutPanel.Width, this.tableLayoutPanel.Height);
        this.tableLayoutPanel.BackgroundImage = backgroundImage;

        for (int i = 0; i < 15; i++)
        {
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F / 15F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F / 15F));
        }
        this.Controls.Add(this.tableLayoutPanel);
    }
}

