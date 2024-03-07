using ASD;
namespace LudoGame
{
    public class Program
    {
        public static int ReturnGetNumberOfPlayers()
        {
            Player p = new();
            int numP = p.GetNumberOfPlayers();
            // Here you can implement your logic to get the number of players
            // For demonstration purposes, let's say we return a hardcoded value
            return numP;
        }
        
        static void Main(string[] args)
        {
            // This is the entry point of the NumPlayer console application
            // You can add additional functionality here if needed
            // For example, you might want to allow user input to specify the number of players
            // But for this example, we'll keep it simple
        }
    }
}
