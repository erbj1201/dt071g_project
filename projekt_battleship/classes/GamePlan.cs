namespace BattleshipGame
{
        public enum ShotResult
    {
        Hit,
        Miss,
        AlreadyShot
    }
    // Class representing the game board
    class GamePlan
    {
        // size of the gameplan
        public const int Size = 2;

        private char[,] board;
        private Ship[] ships;
        // Constructor
        public GamePlan()
        {
            board = new char[Size, Size];
            ships = new Ship[]
            {
                new Ship(1),
            };

            InitializeGamePlan();
            PlaceShips();  // Place ships during the board initialization
        }
        // Method to initialize the board with empty spaces
        private void InitializeGamePlan()
        {
            for (int i = 0; i < Size; i++)
            {
                Console.Write($"{i} ");
                for (int j = 0; j < Size; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        // Method to get a random position on the board
        private int[] GetRandomPosition()
        {
            Random random = new Random();
            int row = random.Next(0, Size);
            int column = random.Next(0, Size);
            return new int[] { row, column };
        }

        // Method to place ships on the board
        public void PlaceShips()
        {
            foreach (Ship ship in ships)
            {
                bool placed = false;
                while (!placed)
                {
                    int[] start = GetRandomPosition();
                    bool horizontal = new Random().Next(2) == 0;
                    placed = ship.PlaceShip(this, start, horizontal);
                }
            }
        }

// Method to display the current state of the board
public void ShowGamePlan(bool hideShips)
{
    Console.WriteLine("  0 1");

    for (int i = 0; i < Size; i++)
    { Console.Write($"{i} ");
        for (int j = 0; j < Size; j++)
        {
            char cellValue = board[i, j];

            // Check if it's the player's ship and we want to show it
            if (cellValue == 'S' && !hideShips)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("S ");
                Console.ResetColor(); // Reset color after printing
            }
            else if (cellValue == 'H')
            {
                Console.ForegroundColor = ConsoleColor.Red; // Set color for hits
                Console.Write("H ");
                Console.ResetColor(); // Reset color after printing
            }
            else if (cellValue == 'M')
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Set color for misses
                Console.Write("M ");
                Console.ResetColor(); // Reset color after printing
            }
            else if (!hideShips && cellValue == ' ')
            {
                Console.Write("  "); // Display empty space
            }
            else
            {
                Console.Write("X ");
            }
        }
        Console.WriteLine();
    }
}

        // Method to process a shot on the board and return whether it's a hit
public ShotResult ProcessShot(int[] target)
{
    int row = target[0];
    int column = target[1];

    if (board[row, column] == ' ')
    {
        board[row, column] = 'M';
        return ShotResult.Miss; // Miss
    }
    else if (board[row, column] == 'S')
    {
        board[row, column] = 'H';
        return ShotResult.Hit; // Hit
    } 
    else if (board[row, column] == 'H' || board[row, column] == 'M')
    {
        return ShotResult.AlreadyShot; // Already shot there
    }
    // Default return if none of the conditions match
    return ShotResult.AlreadyShot;
}

        // Method to check if all ships on the board are destroyed
      public bool AllShipsHit()
{
    foreach (Ship ship in ships)
    {
        if (!ship.IsHit())
        {
            return false;
        }
    }
    
    for (int i = 0; i < Size; i++)
    {
        for (int j = 0; j < Size; j++)
        {
            if (board[i, j] == 'S')
            {
                return false; // If any 'S' is found, at least one ship is not hit
            }
        }
    }
    return true; // No 'S' found, all ships are hit
}
        // Method to get the value at a specific position on the board
        public char GetGamePlanValue(int row, int column)
        {
            return board[row, column];
        }

        // Method to set the value at a specific position on the board
        public void SetGamePlanValue(int row, int column, char value)
        {
            board[row, column] = value;
        }

        // Method to update the computer's board based on shots
public void UpdateComputerGP(int[] target, ShotResult result)
{
    int row = target[0];
    int column = target[1];

    switch (result)
    {
        case ShotResult.Hit:
            board[row, column] = 'H'; // Hit
            break;
        case ShotResult.Miss:
            board[row, column] = 'M'; // Miss
            break;
    }
}
}
}