/*Projec DT071G - Programmering i C#.NET 
Battelship created by Erika Vestin, 2024
erbj1201@student.miun.se */

using System;


namespace BattleshipGame
{
    //Entry point
    class Program
    {
        static void Main()
        {  // Display the menu first
            startMenuScreen();
        }
 public static ConsoleKeyInfo ReadKeyCaseInsensitive()
{
    ConsoleKeyInfo key = Console.ReadKey(true);
    bool altKeyPressed = (key.Modifiers & ConsoleModifiers.Alt) != 0;
    return new ConsoleKeyInfo(char.ToLower(key.KeyChar), key.Key, altKeyPressed, false, false);
}
        static void startMenuScreen()
        {
            Console.ForegroundColor = ConsoleColor.Magenta; // Set the text color
            Console.WriteLine("    ____          __   __   __             __     _      ");
            Console.WriteLine("   / __ ) ____ _ / /_ / /_ / /___   _____ / /_   (_)____ ");
            Console.WriteLine("  / __  |/ __ `// __// __// // _ \\ / ___// __ \\ / // __ \\");
            Console.WriteLine(" / /_/ // /_/ // /_ / /_ / //  __/(__  )/ / / // // /_/ /");
            Console.WriteLine("/_____/ \\__,_/ \\__/ \\__//_/ \\___//____//_/ /_//_// .___/ ");
            Console.WriteLine("                                                /_/      ");
            Console.ResetColor(); // Reset text color
            Console.WriteLine("W E L C O M E   T O   B A T T L E S H I P !");

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Start a new game");
                Console.WriteLine("2. How to play");
                Console.WriteLine("X. Exit");

                Console.Write("What to you want to do? Press 1, 2 or X and then Enter: ");
                string menuNavigation = Console.ReadLine()!;

                switch (menuNavigation)
                {
                    case "1":
                        PlayBattleshipGame();
                        break;

                    case "2":
                        ShowHowToPlay();
                        break;

                    case "X":
                    case "x":
                        Console.WriteLine("G O O D B Y E! \u2665");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                 }
            }
        }

        static void PlayBattleshipGame()
{
    Console.Clear();
    // Prompt for player's name
    Console.Write("Before the game starts... \nPlease, enter your name: ");
    string playerName = Console.ReadLine()!;
    // Use the entered name or default to "Player_1"
    playerName = string.IsNullOrWhiteSpace(playerName) ? "Player_1" : playerName;
    Console.WriteLine($"Welcome, {playerName}!");

    do
    {
        // Prompt to start a new game
        Console.WriteLine("Do you want to start a new game? Press Enter to start!");
        Console.WriteLine("");
        Console.WriteLine("\nPress Z to go back to the menu.");
        // Read player input
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            Console.Clear();
            // Add your game logic here
            Console.WriteLine("Let the game begin...");
            // Start new game and pass the startMenuScreen method
            Game game = new Game(startMenuScreen);
            game.Play();
            Console.ReadLine();
        }
        else if (keyInfo.Key == ConsoleKey.Z)
        {
            Console.Clear();
            Console.WriteLine("Going back to the menu...");
            return; // Exit the method to return to the menu
        }
        Console.Clear(); // Clear the console for the next iteration
    } while (true);
}
        static void ShowHowToPlay()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n. . . . . . . . . . . . . . . . . . .");
            Console.WriteLine();
            Console.WriteLine("H O W   T O   S T A R T   A   G A M E");
            Console.WriteLine();
            Console.WriteLine("\n1. Choose 'Play' in the menu to start a game.");
            Console.WriteLine("2. Enter your name or leave blank and press Enter (if blank, you will be assigned the name 'Player_1').");
            Console.WriteLine("3. Press Enter to start a game.");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n. . . . . . . . . . . . . . . . . . . . . ");
            Console.WriteLine();
            Console.WriteLine("H O W   T O   P L A Y   &   R U L E S");
            Console.WriteLine();
            Console.WriteLine("\nAll ships on the game plans are automatically placed in the game plan by the app. Every game plan can have two ships including three coordinates and two with two coordinates.");
            Console.WriteLine("\nThe Computer's game plan is the game plan where you try to hit ships and every coordinate in the game plan is marked as X. The ships are not visable for you.");
            Console.WriteLine("\nYour's game plan is the game plan where the computer try to hit your ships. Your ships visible for you and are marked as S.");
            Console.WriteLine("\nWhen you have shot to one coordinate you will get a message, which coordinate you have targeted and if it is a hit or a miss.");
            Console.WriteLine("\nTo continue the game you have to press Enter every time you or the computer have shot one time.");
            Console.WriteLine("\nWhen the computer have shot to one coordinate you will get a message, which coordinate the computer have targeted and if it is a hit or a miss.");
            Console.WriteLine("\nA hit is marked with a red 'H' in the game plans and a miss is marked with a yellow 'M'");
            Console.WriteLine("\nYou have to write coordinates between 0 and 6 and you can not leave the coordinates empty");
            Console.WriteLine("\nYou can not shot to the same coordinates more then once.");
            Console.WriteLine("\nWhen it is you turn you can press Z to exit the game and go back to menu. The game can not be saved.");
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("\nPress Z to go back to the menu.");
         ConsoleKeyInfo key = ReadKeyCaseInsensitive();

        if (key.Key == ConsoleKey.Z)
        {
            Console.Clear();
            startMenuScreen();
        }
        else
        {
            ShowHowToPlay(); // Keep showing instructions until X or Z is pressed
        }
            Console.ReadLine();
            Console.Clear();
        }
    }
}



