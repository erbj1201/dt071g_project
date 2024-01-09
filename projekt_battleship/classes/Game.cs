using BattleshipGame;

namespace BattleshipGame
{
class Game
{
    private GamePlan playerBoard;
    private GamePlan computerBoard;

    // Constructor to initialize player and computer boards
    public Game()
    {
        playerBoard = new GamePlan();
        computerBoard = new GamePlan();
    }

 public void Play()
{
    Console.Clear(); // Clear the console before the game loop starts

    // Place ships on player and computer boards
    playerBoard.PlaceShips();
    computerBoard.PlaceShips();

    // Continue playing until all ships are destroyed on one side
    while (!playerBoard.AllShipsDestroyed() && !computerBoard.AllShipsDestroyed())
    {
        PlayerTurn();
        // Check if all ships are destroyed after the player's turn
        if (!computerBoard.AllShipsDestroyed())
        {
            ComputerTurn();
        }
    }

    // Display game over message and determine the winner
    Console.Clear();
    Console.WriteLine("G A M E  O V E R!");

    if (playerBoard.AllShipsDestroyed())
    {
        Console.WriteLine("You were defeated by the computer! Computer wins! You lost!");
    }
    else if (computerBoard.AllShipsDestroyed())
    {
        Console.WriteLine("Congratulations! You defeated the computer! You won!");
    }

    Console.ReadLine();
}
private void PlayerTurn()
{
    // Display the player's game plan before taking the shot
    Console.WriteLine("Your gameplan:");
    playerBoard.ShowGamePlan(false);

    // Display the computer's game plan before the player takes a shot
    Console.WriteLine("\nComputer's gameplan:");
    computerBoard.ShowGamePlan(true);

Console.WriteLine("\nYour Turn:"); 

    Console.WriteLine(); // Add an empty line

    int[] target = GetTarget();
    ShotResult result = computerBoard.ProcessShot(target);

    switch (result)
    {
        case ShotResult.Hit:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"You targeted {target[0]}-{target[1]}, HIT! Good job!");
            Console.ResetColor(); // Reset color after printing
            break;
        case ShotResult.Miss:
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"You targeted {target[0]}-{target[1]}, MISS! Better luck next time!");
             Console.ResetColor(); // Reset color after printing
            break;
        case ShotResult.AlreadyShot:
            Console.WriteLine($"You targeted {target[0]}-{target[1]}, already shot there!");
            break;
    }
    // Display the player's game plan after the shot
    Console.WriteLine("\nYour updated game plan:");
    playerBoard.ShowGamePlan(false);

    // Display the computer's game plan after the shot
    Console.WriteLine("\nComputer's updated game plan:");
    computerBoard.ShowGamePlan(true);
        // Move the cursor down to make space for the message
    Console.WriteLine(); // Add an empty line

    // Pause and wait for Enter key press before moving to the next round
    Console.WriteLine("Press Enter to continue to the next round of the game.");
    Console.ReadLine();
}


  private void ComputerTurn()
{ // Clear the console before displaying the game plans
    Console.Clear();
    // Display the player's game plan before the computer takes a shot
    Console.WriteLine("Your game plan:");
    playerBoard.ShowGamePlan(false);

    // Display the computer's game plan before the computer takes a shot
    Console.WriteLine("\nComputer's game plan:");
    computerBoard.ShowGamePlan(true);
    Console.WriteLine("\nComputer's Turn:");
    // Move the cursor down to make space for the message
    Console.WriteLine(); // Add an empty line

    // Get a random target for the computer
    int[] target = GetRandomTarget();

    // Process the shot on the player's board
    ShotResult result = playerBoard.ProcessShot(target);

    // Display the player's and computer's game plans after the shot
    Console.WriteLine("Your game plan:");
    playerBoard.ShowGamePlan(false);
    Console.WriteLine("\nComputer's game plan:");
    computerBoard.ShowGamePlan(true);

    // Move the cursor down to make space for the message
    Console.WriteLine(); // Add an empty line

    // Display a message based on the result
    if (result == ShotResult.Hit)
    {
         Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Computer targeted {target[0]}-{target[1]}, HIT!");
        Console.ResetColor(); // Reset color after printing
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Computer targeted {target[0]}-{target[1]}, MISS!");
        Console.ResetColor(); // Reset color after printing
    }

    // Pause and wait for Enter key press before moving to the next round
    Console.WriteLine("\nPress Enter to continue to the next round of the game.");
    Console.ReadLine();
}

// Method to get target coordinates from the player
private int[] GetTarget()
{
    Console.Write("Enter target coordinates (row, column): ");
    string input = Console.ReadLine()!;
    if (input != null)
    {
        string[] inputArray = input.Split(',');

        if (inputArray.Length == 2 && int.TryParse(inputArray[0], out int row) && int.TryParse(inputArray[1], out int column))
        {
            return new int[] { row, column };
        }
    }

    // If input is null or parsing fails, return an array with default values
    Console.WriteLine("Invalid input. Please enter valid coordinates.");
    return new int[] { -1, -1 };
}

 // Method to generate random target coordinates for the computer
   // Method to generate random target coordinates for the computer
int[] GetRandomTarget()
{
    Random random = new Random();
    int row, column;

    do
    {
        row = random.Next(0, GamePlan.Size);
        column = random.Next(0, GamePlan.Size);
    } while (playerBoard.GetGamePlanValue(row, column) == 'H' || playerBoard.GetGamePlanValue(row, column) == 'M');

    return new int[] { row, column };
}
}
}

