namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication
{
    private void AddCircle(int x, int y, Color color)
    {
        // Create a circle panel
        Panel circlePanel = new Panel();
        circlePanel.BackColor = color; // Change color as needed
        circlePanel.Width = 20; // Adjust size as needed
        circlePanel.Height = 20; // Adjust size as needed
        circlePanel.BorderStyle = BorderStyle.Fixed3D; // Add border if needed
        circlePanel.Dock = DockStyle.Fill;

        // Add label with the specified letter
        Label label = new Label();
        label.Text = "O"; // T: Totem
        label.TextAlign = ContentAlignment.MiddleCenter;
        label.Dock = DockStyle.Fill;
        label.ForeColor = Color.White; // Change text color as needed
        circlePanel.Controls.Add(label);

        // Add circle panel to the specific cell
        this.tableLayoutPanel.Controls.Add(circlePanel, x, y);
    }
}

