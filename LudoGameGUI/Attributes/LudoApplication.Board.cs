namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication
{
    private Button _startButton;
    private Label _startLabel;
    private void StartButton()
    {
        // Add a button for confirming the number of players
        this._startButton = new Button();
        this._startButton.Text = "Start The Game";
        this._startButton.Size = new Size(123, 70);
        this._startButton.Location = new Point(40, 670); // Position the button below the text box
        this.Controls.Add(this._startButton);
        this._startButton.Click += StartButton_Click;
    }
    private void CreateStartLabel()
    {
        // Add a label to display player names
        this._startLabel = new Label();
        this._startLabel.Text = "Status: False";
        this._startLabel.AutoSize = true;
        this._startLabel.Location = new System.Drawing.Point(46, 760);
        this.Controls.Add(this._startLabel);
    }
    private void StartButton_Click(object sender, EventArgs e)
    {
        // Clear previous player names
        _startLabel.Text = "";
        bool status = _ludoContext.StartGame();
        _startLabel.Text += $"Status: {status}\n";
    }
    private void CreateGrid()
    {
        // Create TableLayoutPanel for the grid
        this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
        this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
        this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Right; // Dock to the right side
        this.tableLayoutPanel.Size = new System.Drawing.Size(700, 800); // Adjust the size to fit the form
        this.tableLayoutPanel.Location = new System.Drawing.Point(200, 0); // Adjusted the starting position from the left edge
        this.tableLayoutPanel.Name = "tableLayoutPanel";
        this.tableLayoutPanel.RowCount = 15;
        this.tableLayoutPanel.ColumnCount = 15;

        for (int i = 0; i < 15; i++)
        {
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F / 15F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F / 15F));
        }
        this.Controls.Add(this.tableLayoutPanel);
    }
    private void ColorTheGrid(){
        List<(int, int)> playCoordinate = _board.GetBoardCoordinate();
        foreach(var i in playCoordinate){
            Panel coloredPanel = new Panel();
            coloredPanel.BackColor = Color.SteelBlue;
            coloredPanel.Dock = DockStyle.Fill;
            // Add panels to each cell and set their background color
            int x = i.Item1; // Example row index
            int y = i.Item2; // Example column index
            this.tableLayoutPanel.Controls.Add(coloredPanel, x, y);
        }
    }

}

