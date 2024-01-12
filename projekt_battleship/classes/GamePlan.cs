/*Class Gameplan by Erika Vestin HT 2023*/
//Namespace
namespace BattleshipGame
{
    // Enum for result after shot
    public enum ShotResult
    {
        Hit,
        Miss,
        AlreadyShot
    }

    // Class gameplan
    class GamePlan
    {
        // size gameplan
        public const int Size = 7;

        //properties with set, get methods
        private char[,] Board { get; set; }
        private Ship[] Ships { get; set; }

        // Constructor
        public GamePlan()
        { //Create gameplan and ships
            Board = new char[Size, Size];
            Ships = [new Ship(2), new Ship(2), new Ship(3), new Ship(3)];

            CreateGamePlan();//Create game plan
            PlaceShips(); // Place ships
        }

        // Create gamplan, empty spaces
        private void CreateGamePlan()
        {
            for (int i = 0; i < Size; i++)
            {
                Console.Write($"{i} ");
                for (int j = 0; j < Size; j++)
                {
                    Board[i, j] = ' ';
                }
            }
        }

        //get random position, gameplan
        private static int[] GetRandomPosition()
        {
            Random random = new();
            int row = random.Next(0, Size);
            int column = random.Next(0, Size);
            return [row, column];
        }

        //place ships on gameplan
        public void PlaceShips()
        { //Loop every ship
            foreach (Ship ship in Ships)
            {
                bool placed = false;
                //Loop as long as not placed
                while (!placed)
                { //initialize random positions horizontal and vertically
                    int[] start = GetRandomPosition();
                    bool horizontal = new Random().Next(2) == 0;
                    placed = ship.PlaceShip(this, start, horizontal);
                }
            }
        }

        // Display gameplan
        public void ShowGamePlan(bool hideShips)
        { //column indexes
            Console.WriteLine("  0 1 2 3 4 5 6 ");
            //Loop every row
            for (int i = 0; i < Size; i++)
            //Show index row
            {
                Console.Write($"{i} ");
                //Loop columns
                for (int j = 0; j < Size; j++)
                { //Value for current cell
                    char cellValue = Board[i, j];

                    // Check if player's ship, show ships
                    if (cellValue == 'S' && !hideShips)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue; //Color blue for ships
                        Console.Write("S ");
                        Console.ResetColor(); // Reset color
                    } //Look for H
                    else if (cellValue == 'H')
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Color Red for hits
                        Console.Write("H ");
                        Console.ResetColor(); // Reset color
                    }
                    //Look for M
                    else if (cellValue == 'M')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; // Color Yellow for miss
                        Console.Write("M ");
                        Console.ResetColor(); // Reset color
                    } //Look for empty spaces
                    else if (!hideShips && cellValue == ' ')
                    {
                        Console.Write("  "); // Empty space
                    }
                    else
                    {
                        Console.Write("X ");
                    }
                }
                Console.WriteLine();
            }
        }

        // process shot on game plan, return hit or not
        public ShotResult ProcessShot(int[] target)
        { //Extract row, columns values from array
            int row = target[0];
            int column = target[1];
            //Check content of target coordinates
            //If empty cell
            if (Board[row, column] == ' ')
            { //Show miss (M)
                Board[row, column] = 'M';
                return ShotResult.Miss; // Miss
            } //If S (Ship)
            else if (Board[row, column] == 'S')
            { //Show H (hit)
                Board[row, column] = 'H';
                return ShotResult.Hit; // Hit
            } //If M or H,
            else if (Board[row, column] == 'H' || Board[row, column] == 'M')
            {
                return ShotResult.AlreadyShot; // Already shot there
            }
            // if no condition match
            return ShotResult.AlreadyShot;
        }

        //check all ships destroyed
        public bool AllShipsHit()
        { //loop to see if all shios are hit
            foreach (Ship ship in Ships)
            {
                if (!ship.IsHit())
                { //If there are ships not hit
                    return false;
                }
            }
            //Check for remaining ships
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                { //if remaining ships
                    if (Board[i, j] == 'S')
                    {
                        return false; // If S, there are still ships
                    }
                }
            }
            return true; // No S = no ships
        }

        // get value, specific position on gameplan
        public char GetGamePlanValue(int row, int column)
        {
            return Board[row, column];
        }

        // set the value, specific position on gameplan
        public void SetGamePlanValue(int row, int column, char value)
        {
            Board[row, column] = value;
        }

        // Update computer's gameplan based on shots
        public void UpdateComputerGP(int[] target, ShotResult result)
        {
            int row = target[0];
            int column = target[1];
            //Switch, to update with hit or miss
            switch (result)
            {
                case ShotResult.Hit:
                    Board[row, column] = 'H'; // Hit
                    break;
                case ShotResult.Miss:
                    Board[row, column] = 'M'; // Miss
                    break;
            }
        }
    }
}
