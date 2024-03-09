namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication
{
    private Button chooseToMoveOutHome;
    private Button chooseToMoveForward;
    private TextBox _inputDiceTextBox;

    
    private void CreateChooseMoveOutHomeButton()
    {
        chooseToMoveOutHome = new Button();
        chooseToMoveOutHome.Text = "0";
        chooseToMoveOutHome.Size = new Size(50, 50);
        chooseToMoveOutHome.Location = new Point(10, 10);
        Controls.Add(chooseToMoveOutHome);
        chooseToMoveOutHome.Click += ChooseMoveOutHome_Click;
    }
    private void CreateChooseToMoveForwardButton()
    {
        chooseToMoveForward = new Button();
        chooseToMoveForward.Text = "1";
        chooseToMoveForward.Size = new Size(50, 50);
        chooseToMoveForward.Location = new Point(10, 70);
        Controls.Add(chooseToMoveForward);
        chooseToMoveForward.Click += ChooseToMoveForward_Click;
    }
    private void ChooseMoveOutHome_Click(object sender, EventArgs e)
    {
        userChoiceSixInDice = UserChoiceSixInDice.GetOutHome;
        chooseOutHomeOrPlayForward.SetResult(true);
    }
    private void ChooseToMoveForward_Click(object sender, EventArgs e)
    {
        userChoiceSixInDice = UserChoiceSixInDice.MoveTotemForward;
        chooseOutHomeOrPlayForward.SetResult(true);
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

