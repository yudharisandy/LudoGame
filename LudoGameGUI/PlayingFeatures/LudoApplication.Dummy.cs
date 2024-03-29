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
    private Button refreshRenderingButton;
    private TaskCompletionSource<bool> refreshRenderingButtonTask;
    private bool _refreshRenderingStatus;
    private TextBox _inputDiceTextBox;

        
    private void CreateRefreshRenderingButton()
    {
        // Button to refresh all of the totem panel on field
        refreshRenderingButton = new Button();
        refreshRenderingButton.Text = "Re-Render The Objects";
        refreshRenderingButton.Size = new Size(123, 70);
        refreshRenderingButton.Location = new Point(10, 690);
        Controls.Add(refreshRenderingButton);
        refreshRenderingButton.Click += refreshRenderingButton_Click;
    }

    private void refreshRenderingButton_Click(object sender, EventArgs e)
    {
        _refreshRenderingStatus = true;
        refreshRenderingButtonTask.SetResult(true);
    }

    private void CreateInputDiceTextBox()
    {
        // Create a text box for inputting the number of players
        _inputDiceTextBox = new TextBox();
        _inputDiceTextBox.Location = new System.Drawing.Point(210, 20);
        _inputDiceTextBox.Size = new System.Drawing.Size(50, 70);
        Controls.Add(_inputDiceTextBox);
    }
}

