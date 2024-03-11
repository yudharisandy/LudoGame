namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication
{
    private Button diceButton;
    private Label diceResultLabel; // New label to display the dice result
    private int diceValue;
    private TaskCompletionSource<bool> rollDiceClickedTask;


    private async void CreateDiceButton()
    {
        // Add a button for rolling dice
        this.diceButton = new System.Windows.Forms.Button();
        this.diceButton.Text = "Roll Dice";
        this.diceButton.Size = new System.Drawing.Size(100, 50);
        this.diceButton.Location = new System.Drawing.Point(100, 10); // Position the button on the left side
        this.Controls.Add(this.diceButton);

        this.diceButton.Click += DiceButton_Click;
    }

    private void CreateDiceResultLabel()
    {
        // Add a label to display the dice result
        this.diceResultLabel = new Label();
        this.diceResultLabel.Text = "?";
        this.diceResultLabel.Font = new Font("Arial", 12, FontStyle.Bold); // Set font size to 12 and make it bold
        this.diceResultLabel.AutoSize = true;
        this.diceResultLabel.Location = new System.Drawing.Point(133, 70); // Position the label below the player label
        this.Controls.Add(this.diceResultLabel);
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
        diceResultLabel.Text = $"{diceValue}"; // Update the label with the dice result
        rollDiceClickedTask.SetResult(true);
    }
}
