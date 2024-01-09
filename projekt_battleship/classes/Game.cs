using BattleshipGame;
using System.Threading;

namespace BattleshipGame
{
class Game
{
    private GamePlan playerBoard;
    private GamePlan computerBoard;
    private Action startMenuScreen;
    
   // Constructor to initialize player and computer boards
    public Game(Action startMenuScreen)
    {
        playerBoard = new GamePlan();
        computerBoard = new GamePlan();
        this.startMenuScreen = startMenuScreen;
    }

     private ConsoleKeyInfo ReadKeyCaseInsensitive()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            bool altKeyPressed = (key.Modifiers & ConsoleModifiers.Alt) != 0;
            return new ConsoleKeyInfo(char.ToLower(key.KeyChar), key.Key, altKeyPressed, false, false);
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

private void WaitForEnterContinueOrZExit(){
     Console.WriteLine("\nPress Enter to continue or Z to exit the game and go back to the menu. \nYour game will not be saved.");

    ConsoleKeyInfo key;
    do
    {
        // Read a key, including Enter
        key = ReadKeyCaseInsensitive();

        if (key.Key == ConsoleKey.Z)
        {
            Console.Clear();
            startMenuScreen();
            return; // Exit the method to return to the menu
        }

        // If the pressed key is not Enter, keep looping
    } while (key.Key != ConsoleKey.Enter);
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

    ShotResult result;
    int[] target;

    do
    {
        target = GetTarget();
        result = computerBoard.ProcessShot(target);

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

        // If the result is AlreadyShot, prompt the player for a new target
    } while (result == ShotResult.AlreadyShot);
    WaitForEnterContinueOrZExit();

    // Pause and wait for Enter key press before moving to the next round
    Console.WriteLine("Press Enter to continue to the next round of the game.");
    Console.WriteLine();
    // Display the message at the bottom
    Console.WriteLine(". . . . . . . . . . . . . . . . . . . . . . . . . . .");
    Console.WriteLine("\nPress Z to exit the game and go back to the menu. \nYour game will not be saved.");
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
    int[] defaultValues = { -1, -1 };

    while (true)
    {
        Console.Write("Enter target coordinates (row, column): ");
        string input = Console.ReadLine()!;

        if (input != null)
        {
            string[] inputArray = input.Split(',');

            if (inputArray.Length == 2 && int.TryParse(inputArray[0], out int row) && int.TryParse(inputArray[1], out int column))
            {
                // Check if coordinates are within the valid range (0 to 6 inclusive)
                if (row >= 0 && row < GamePlan.Size && column >= 0 && column < GamePlan.Size)
                {
                    return new int[] { row, column };
                }
                else
                {
                    Console.WriteLine("Invalid coordinates. Row and column can not be empty, must be between 0 and 6.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter valid coordinates. Row and column can not be empty, must be between 0 and 6.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter valid coordinates. Row and column can not be empty, must be between 0 and 6.");
        }
    }
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

