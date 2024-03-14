namespace LudoGameGUI;

using System;
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
    private Button diceButton;
    private Label diceResultLabel; // New label to display the dice result
    private int diceValue;
    private TaskCompletionSource<bool> rollDiceClickedTask;


    private async void CreateDiceButton()
    {
        // Add a button for rolling dice
        diceButton = new System.Windows.Forms.Button();
        diceButton.Text = "Roll Dice";
        diceButton.Size = new System.Drawing.Size(100, 50);
        diceButton.BackColor = Color.Gold;
        diceButton.Location = new System.Drawing.Point(100, 10); // Position the button on the left side
        Controls.Add(diceButton);

        diceButton.Click += DiceButton_Click;
    }

    private void CreateDiceResultLabel()
    {
        // Add a label to display the dice result
        diceResultLabel = new Label();
        diceResultLabel.Text = "?";
        diceResultLabel.Font = new Font("Arial", 12, FontStyle.Bold); // Set font size to 12 and make it bold
        diceResultLabel.AutoSize = true;
        diceResultLabel.Location = new System.Drawing.Point(133, 70); // Position the label below the player label
        Controls.Add(diceResultLabel);
    }

    private void DiceButton_Click(object sender, EventArgs e)
    {
        // Generate a random number from 1 to 6 and display it
        if(int.TryParse(_inputDiceTextBox.Text, out diceValue)){
            // ... something
        }
        else{
            diceValue = _ludoGameScene.ludoContext.dice.Roll();
        }
        diceButton.BackColor = Color.Gainsboro;
        diceResultLabel.Text = $"{diceValue}"; // Update the label with the dice result
        rollDiceClickedTask.SetResult(true);
    }
}

