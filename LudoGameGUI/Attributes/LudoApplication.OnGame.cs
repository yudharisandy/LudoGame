namespace LudoGameGUI;

using System;
using System.Windows.Forms;
using LudoGame;
using LudoGame.Game;
using LudoGame.LudoObjects;

public partial class LudoApplication
{
    private Label _playerTurnLabel;
    private int userInputTotemID;
    private TaskCompletionSource<bool> chooseTotemToMove;
    private UserChoiceSixInDice userChoiceSixInDice; // Temporary (for trial only)
    private TaskCompletionSource<bool> chooseOutHomeOrPlayForward; // Temporary (for trial only)
    
    private async void Play(){
        while(true){
            // Loop for every player
            foreach(var player in _ludoGameScene.ludoContext._playerTotems){
                Color color = SetTotemColor(player.Key);
                _playerTurnLabel.Text = $"Turn: Player {player.Key.ID + 1}";
                
                // Wait player push "Roll Dice"
                rollDiceClickedTask = new TaskCompletionSource<bool>();
                await rollDiceClickedTask.Task; // Change the "diceValue"
                
                chooseOutHomeOrPlayForward = new TaskCompletionSource<bool>();
                await chooseOutHomeOrPlayForward.Task;
                // UserChoiceSixInDice userChoiceSixInDice = UserChoiceSixInDice.GetOutHome; // Example

                chooseTotemToMove = new TaskCompletionSource<bool>();
                await chooseTotemToMove.Task;
                // int userinputTotemID = 0; // Example (0-3)
                
                _ludoGameScene.NextTurn(player.Key, player.Value, diceValue, userInputTotemID, userChoiceSixInDice);

                if (diceValue == 6 && userChoiceSixInDice == UserChoiceSixInDice.GetOutHome){
                    RemoveTotem(player.Value[userInputTotemID].HomePosition.x, player.Value[userInputTotemID].HomePosition.y);
                    MoveTotem(player.Value[userInputTotemID].Position.x, player.Value[userInputTotemID].Position.y, color, player.Value[userInputTotemID]);
                }
                else{
                    // if (there is Totem OnPlay, choose one to move)
                    if(player.Value[userInputTotemID].totemStatus == TotemStatus.OnPlay){
                        RemoveTotem(player.Value[userInputTotemID].PreviousPosition.x, player.Value[userInputTotemID].PreviousPosition.y);
                        MoveTotem(player.Value[userInputTotemID].Position.x, player.Value[userInputTotemID].Position.y, color, player.Value[userInputTotemID]);
                    }
                    // else (nothing to do)
                }

                await Task.Delay(1000);
            }
        }
    }

    private void MoveTotem(int x, int y, Color color, Totem totem)
    {  
        // Create a circle panel
        Panel totemPanel = new Panel();
        totemPanel.BackColor = color; // Change color as needed
        totemPanel.Width = 20; // Adjust size as needed
        totemPanel.Height = 20; // Adjust size as needed
        totemPanel.BorderStyle = BorderStyle.FixedSingle; // Add border if needed
        totemPanel.Dock = DockStyle.Fill;

        // Add label with the specified letter
        Label label = new Label();
        label.Text = $"T{totem.ID}"; // T: Totem
        label.TextAlign = ContentAlignment.MiddleCenter;
        label.Dock = DockStyle.Fill;
        label.ForeColor = Color.White; // Change text color as needed
        totemPanel.Controls.Add(label);

        // Add circle panel to the specific cell
        this.tableLayoutPanel.Controls.Add(totemPanel, x, y);
    }
    
    private void RemoveTotem(int x, int y)
    {
        // Retrieve the control at the specified cell
        Control control = this.tableLayoutPanel.GetControlFromPosition(x, y);

        // If a control is found, remove it from the tableLayoutPanel
        if (control != null)
        {
            this.tableLayoutPanel.Controls.Remove(control);
            control.Dispose(); // Dispose of the control to release resources
        }
    }

    private void AddTotem(int x, int y, Color color, Totem totem)
    {
        // Create a circle panel
        Panel circlePanel = new Panel();
        circlePanel.BackColor = color; // Change color as needed
        circlePanel.Width = 20; // Adjust size as needed
        circlePanel.Height = 20; // Adjust size as needed
        circlePanel.BorderStyle = BorderStyle.FixedSingle; // Add border if needed
        circlePanel.Dock = DockStyle.Fill;

        // Add label with the specified letter
        Label label = new Label();
        label.Text = $"T{totem.ID}"; // T: Totem
        label.TextAlign = ContentAlignment.MiddleCenter;
        label.Dock = DockStyle.Fill;
        label.ForeColor = Color.White; // Change text color as needed
        circlePanel.Controls.Add(label);

        // Add circle panel to the specific cell
        this.tableLayoutPanel.Controls.Add(circlePanel, x, y);
    }
    private void CreatePlayerTurnLabel()
    {
        // Add a label to display player names
        this._playerTurnLabel = new Label();
        this._playerTurnLabel.Text = "Turn: -";
        this._playerTurnLabel.ForeColor = Color.DarkBlue;
        this._playerTurnLabel.Font = new Font("Arial", 10, FontStyle.Bold);
        this._playerTurnLabel.AutoSize = true;
        this._playerTurnLabel.Location = new Point(80, 110);
        this.Controls.Add(this._playerTurnLabel);
    }
}

