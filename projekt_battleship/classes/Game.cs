/*Class Game by Erika Vestin HT 2023*/

//Namespace
namespace BattleshipGame
{
    //Class game
    class Game(Action startMenuScreen)
    { // Initialize player and computer game plans
        private GamePlan PlayerBoard { get; set; } = new GamePlan();
        private GamePlan ComputerBoard { get; set; } = new GamePlan();

        // read a key in a case-insensitive way
        private static ConsoleKeyInfo ReadKeyCaseInsensitive()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            bool altKeyPressed = (key.Modifiers & ConsoleModifiers.Alt) != 0;
            return new ConsoleKeyInfo(
                char.ToLower(key.KeyChar),
                key.Key,
                altKeyPressed,
                false,
                false
            );
        }

        // Loop for playing game
        public void Play()
        {
            Console.Clear(); // Clear the console before the game loop starts

            // Place ships on game plans
            PlayerBoard.PlaceShips();
            ComputerBoard.PlaceShips();

            // Continue playing to all ships destroyed on one game plan
            while (!PlayerBoard.AllShipsHit() && !ComputerBoard.AllShipsHit())
            { //Players turn
                PlayerTurn();
                // Check if all ships destroyed after the player turn
                if (!ComputerBoard.AllShipsHit())
                {
                    ComputerTurn();
                }
            }

            // Show game over and show winner
            Console.Clear();
            Console.WriteLine("G A M E  O V E R!");
            //If all ships hit on players game plan
            if (PlayerBoard.AllShipsHit())
            { //message
                Console.WriteLine("You were defeated by the computer! Computer wins! You lost!");
            } //if all ships hit on computer game plan
            else if (ComputerBoard.AllShipsHit())
            { //message
                Console.WriteLine("Congratulations! You defeated the computer! You won!");
            }
            // Show exit to menu message
            Console.WriteLine("\nPress Z to exit and go back to the menu.");

            // Wait for Z key press to exit to menu
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Z)
                {
                    Console.Clear();
                    startMenuScreen();
                    return; // Exit to the menu
                }
            }
        }

        // wait for Enter to continue or Z to exit
        private void WaitForEnterContinueOrZExit()
        {
            //message
            Console.WriteLine(
                "\nPress Enter to continue or Z to exit the game and go back to the menu. \nYour game will not be saved."
            );

            ConsoleKeyInfo key;
            do
            {
                // Read a key
                key = ReadKeyCaseInsensitive();

                if (key.Key == ConsoleKey.Z)
                {
                    Console.Clear();
                    startMenuScreen();
                    return; // Exit to the menu
                }
                // If pressed key not Enter, keep looping
            } while (key.Key != ConsoleKey.Enter);
        }

        //players turn
        private void PlayerTurn()
        {
            // show player's game plan before taking shot
            Console.WriteLine("Your gameplan:");
            PlayerBoard.ShowGamePlan(false);

            // Show computer's game plan before player takes shot
            Console.WriteLine("\nComputer's gameplan:");
            ComputerBoard.ShowGamePlan(true);

            Console.WriteLine("\nYour Turn:"); //message

            Console.WriteLine(); // empty line

            ShotResult result;
            int[] target;

            do
            { //get target
                target = GetTarget();
                result = ComputerBoard.ProcessShot(target);
                //show result of shot
                switch (result)
                {
                    case ShotResult.Hit:
                        Console.ForegroundColor = ConsoleColor.Red; // color red
                        Console.WriteLine($"You targeted {target[0]}-{target[1]}, HIT! Good job!"); // message
                        Console.ResetColor(); // Reset color
                        break;
                    case ShotResult.Miss:
                        Console.ForegroundColor = ConsoleColor.Yellow; // color yellow
                        Console.WriteLine(
                            $"You targeted {target[0]}-{target[1]}, MISS! Better luck next time!"
                        ); //message
                        Console.ResetColor(); // Reset color
                        break;
                    case ShotResult.AlreadyShot:
                        Console.WriteLine(
                            $"You targeted {target[0]}-{target[1]}, you have already shot there! Try again."
                        ); //message
                        break;
                }

                // show player's game plan after shot
                Console.WriteLine("\nYour updated game plan:");
                PlayerBoard.ShowGamePlan(false);

                // show computer's game plan after shot
                Console.WriteLine("\nComputer's updated game plan:");
                ComputerBoard.ShowGamePlan(true);

                Console.WriteLine(); // Add an empty line

                // If result AlreadyShot, player need to write new target
            } while (result == ShotResult.AlreadyShot);
            //Checking for z or enter press
            WaitForEnterContinueOrZExit();

            // Pause, wait for Enter before moving to next round
            Console.WriteLine("Press Enter to continue to the next round of the game.");
            Console.WriteLine();
            // Show message at the bottom
            Console.WriteLine(". . . . . . . . . . . . . . . . . . . . . . . . . . .");
            Console.WriteLine(
                "\nPress Z to exit the game and go back to the menu. \nYour game will not be saved."
            );
        }

        //computers turn
        private void ComputerTurn()
        { // Clear console
            Console.Clear();
            // show player's game plan before computer takes shot
            Console.WriteLine("Your game plan:");
            PlayerBoard.ShowGamePlan(false);

            // Show computer's game plan before computer takes shot
            Console.WriteLine("\nComputer's game plan:");
            ComputerBoard.ShowGamePlan(true);
            Console.WriteLine("\nComputer's Turn:");

            Console.WriteLine(); // Add an empty line

            // Get random target
            int[] target;
            do
            { // Get random target
                target = GetRandomTarget();
            } while (
                PlayerBoard.GetGamePlanValue(target[0], target[1]) == 'H'
                || PlayerBoard.GetGamePlanValue(target[0], target[1]) == 'M'
            );

            // Process shot
            ShotResult result = PlayerBoard.ProcessShot(target);

            // show game plans after shot
            Console.WriteLine("Your game plan:");
            PlayerBoard.ShowGamePlan(false);
            Console.WriteLine("\nComputer's game plan:");
            ComputerBoard.ShowGamePlan(true);

            Console.WriteLine(); // Add an empty line

            // show a message result
            if (result == ShotResult.Hit)
            {
                Console.ForegroundColor = ConsoleColor.Red; //color red
                Console.WriteLine($"Computer targeted {target[0]}-{target[1]}, HIT!"); //message
                Console.ResetColor(); // Reset color after printing
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow; //color yellow
                Console.WriteLine($"Computer targeted {target[0]}-{target[1]}, MISS!"); //message
                Console.ResetColor(); // Reset color after printing
            }

            // pause, wait for Enter before moving to next round
            Console.WriteLine("\nPress Enter to continue to the next round of the game.");
            Console.ReadLine();
        }

        //get target coordinates from the player
        private static int[] GetTarget()
        {
            int[] defaultValues = [-1, -1]; // default calues for invalid coordinates

            //loop until valid coordinates
            while (true)
            {
                Console.Write("Enter target coordinates (row, column): ");
                string input = Console.ReadLine()!; // read input
                //input not null
                if (input != null)
                {
                    string[] inputArray = input.Split(','); //split input
                    //check if two int parts of input
                    if (
                        inputArray.Length == 2
                        && int.TryParse(inputArray[0], out int row)
                        && int.TryParse(inputArray[1], out int column)
                    )
                    {
                        // Check if coordinates are within the valid range (0 to 6 inclusive)
                        if (
                            row >= 0
                            && row < GamePlan.Size
                            && column >= 0
                            && column < GamePlan.Size
                        )
                        {
                            return [row, column];
                        }
                        else
                        { // if not valid
                            Console.WriteLine(
                                "Invalid coordinates. Row and column can not be empty, must be between 0 and 6."
                            );
                        }
                    }
                    else
                    { //if not valid
                        Console.WriteLine(
                            "Invalid input. Please enter valid coordinates. Row and column can not be empty, must be between 0 and 6."
                        );
                    }
                }
                else //if not valid
                {
                    Console.WriteLine(
                        "Invalid input. Please enter valid coordinates. Row and column can not be empty, must be between 0 and 6."
                    );
                }
            }
        }

        // Method to generate random target coordinates for the computer
        int[] GetRandomTarget()
        {
            Random random = new(); // new random instance
            int row,
                column;

            do
            {
                row = random.Next(0, GamePlan.Size); //random row
                column = random.Next(0, GamePlan.Size); //random column
                //continue loop if coordinates already hit
            } while (
                PlayerBoard.GetGamePlanValue(row, column) == 'H'
                || PlayerBoard.GetGamePlanValue(row, column) == 'M'
            );
            // return random cordinates, valid
            return [row, column];
        }
    }
}
