using System;
using Spectre.Console;

namespace Minigames
{
    public class WaterLevels : IMinigame
    {
        public int Score { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int GetWaterLevel() // Or create a getter method
        {
            return WaterLevel;
        }
        public int WaterLevel = 3;  // Initial water level (0-10)
        private readonly int maxLevel = 10;
        private readonly int minLevel = 0;
        private bool isGameOver = false;
        private DateTime gameStartTime;
        private readonly TimeSpan timeLimit = TimeSpan.FromSeconds(10); // 10s time limit
        private int previousCursorPos = 27;

        public void Run()
        {
            Name = "Water Levels Minigame";
            Score = 0;
            isGameOver = false;
            WaterLevel = 3;
            previousCursorPos = 27;
            gameStartTime = DateTime.Now;

            Console.Clear();
            DisplayHeader();
            DisplayControls();

            while (!isGameOver)
            {
                TimeSpan elapsedTime = DateTime.Now - gameStartTime;
                TimeSpan remainingTime = timeLimit - elapsedTime;

                if (remainingTime.TotalSeconds <= 0)
                {
                    DisplayMessage("Time's up!", ConsoleColor.Red);
                    isGameOver = true;
                    break;
                }

                DisplayWaterLevel();
                DisplayRemainingTime(remainingTime);

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow && WaterLevel < maxLevel)
                    {
                        WaterLevel++;
                        Score += 5;
                        DisplayMessage("You added water!\nYou got 5 points by the way.", ConsoleColor.DarkBlue);
                    }
                    else if (key == ConsoleKey.DownArrow && WaterLevel > minLevel)
                    {
                        WaterLevel--;
                        Score += 5;
                        DisplayMessage("You removed water!\nYou got 5 points by the way.", ConsoleColor.Blue);
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("You quit the game.");
                        isGameOver = true;
                        break;
                    }
                    else
                    {
                        if (WaterLevel >= maxLevel && key == ConsoleKey.UpArrow)
                        {
                        }
                        else if (WaterLevel <= minLevel && key == ConsoleKey.DownArrow)
                        {
                        }
                        else
                        {
                            DisplayMessage("Invalid action! Press only valid keys.", ConsoleColor.Red);
                        }
                    }
                    CheckWaterLevel();
                }
            }
            AnsiConsole.Clear();
        }

        private void DisplayHeader()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("=== Water Levels Minigame ===");
            Console.ResetColor();
        }

        private void DisplayWaterLevel()
        {
            Console.SetCursorPosition(0, 3); // Move cursor to the water level display position
            Console.WriteLine("\nWater Level:\n");

            // Display the water level vertically like a reservoir
            for (int i = maxLevel - 1; i >= 0; i--)
            {
                if (i < WaterLevel)
                {

                    Console.WriteLine("|██████████████████████|");
                }
                else
                {
                    Console.WriteLine("|                      |");
                }
            }

            Console.WriteLine("------------------------");
            
            // Display current level and score with padding
            Console.SetCursorPosition(0, 18);
            Console.WriteLine($"Current Level: {WaterLevel}/{maxLevel}".PadRight(Console.WindowWidth));
            Console.SetCursorPosition(0, 19);
            Console.WriteLine($"Score: {Score}".PadRight(Console.WindowWidth));
        }

        private void DisplayControls()
        {
            Console.SetCursorPosition(0, 20); // Move cursor to the controls display position
            Console.WriteLine("\nControls:");
            Console.WriteLine("[Up Arrow] Add water.");
            Console.WriteLine("[Down Arrow] Remove water.");
            Console.WriteLine("[Esc] Exit.");
            Console.ResetColor();
        }

        private void DisplayRemainingTime(TimeSpan remainingTime)
        {
            Console.SetCursorPosition(0, 25); // Move cursor to the remaining time display position
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nTime Remaining: {remainingTime.Seconds} seconds");
            Console.ResetColor();
        }
        private void DisplayMessage(string message, ConsoleColor color)
        {
            Console.SetCursorPosition(0, 27); // Move cursor to the message display position
            Console.ForegroundColor = color;

            for (int i = 0; i < previousCursorPos - 27; i++)
            {
                Console.SetCursorPosition(0, 27 + i);
                Console.WriteLine(new string(' ', Console.WindowWidth)); // Clear previous message
            }
            Console.SetCursorPosition(0, 27); // Reset cursor position
            Console.WriteLine($"\n{message}");
            // Console.WriteLine($"\n{previousCursorPos - 27} lines to clear");
            previousCursorPos = Console.CursorTop;
            Console.ResetColor();
        }

        private void CheckWaterLevel()
        {
            if (WaterLevel >= maxLevel)
            {
                DisplayMessage("Overflow! Water level is too high! Stabilize it quickly!\nRecommendation from the colleague: it's most efficient when it is in the middle.\nYou lost 5 points by the way.", ConsoleColor.Red);
                Score -= 10;
                if (Score < 0) Score = 0;
            }
            else if (WaterLevel <= minLevel)
            {
                DisplayMessage("Underflow! Water level is too low! Stabilize it quickly!\nRecommendation from the colleague: it's most efficient when it is in the middle.\nYou lost 10 points by the way.", ConsoleColor.Red);
                Score -= 10;
                if (Score < 0) Score = 0;
            }
            else
            {
                if (WaterLevel == maxLevel - 1 || WaterLevel == minLevel + 1)
                {
                    DisplayMessage("Warning: Approaching dangerous water level. Adjust valves carefully!\nYou lost 5 points by the way.", ConsoleColor.Yellow);
                    Score -= 5;
                    if (Score < 0) Score = 0;
                }
            }
        }

        
    }
}

