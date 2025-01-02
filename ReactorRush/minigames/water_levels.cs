using System;

namespace Minigames
{
    public class WaterLevels : IMinigame
    {
        public int Score { get; private set; }
        public int GetWaterLevel() // Or create a getter method
        {
            return waterLevel;
        }
        public int waterLevel = 3;  // Initial water level (0-10)
        private readonly int maxLevel = 10;
        private readonly int minLevel = 0;
        private bool isGameOver = false;
        private DateTime gameStartTime;
        private readonly TimeSpan timeLimit = TimeSpan.FromSeconds(45); // 45 s time limit

        public void Run()
        {
            Console.Title = "Water Levels Minigame";

            gameStartTime = DateTime.Now;

            while (!isGameOver)
            {
                TimeSpan elapsedTime = DateTime.Now - gameStartTime;
                TimeSpan remainingTime = timeLimit - elapsedTime;

                if (remainingTime.TotalSeconds <= 0)
                {
                    DisplayMessage("Time's up!", ConsoleColor.Red, true);
                    break;
                }

                Console.Clear();
                DisplayHeader();
                DisplayWaterLevel();
                DisplayControls();
                DisplayRemainingTime(remainingTime);

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && waterLevel < maxLevel)
                {
                    waterLevel++;
                    Score += 5;
                    DisplayMessage("You added water!\nYou got 5 points by the way.", ConsoleColor.DarkBlue);

                }
                else if (key == ConsoleKey.DownArrow && waterLevel > minLevel)
                {
                    waterLevel--;
                    Score += 5;
                    DisplayMessage("You removed water!\nYou got 5 points by the way.", ConsoleColor.Blue);

                }
                else if (key == ConsoleKey.Escape)
                {
                    Console.WriteLine("You quit the game.");
                    break;
                }
                else
                {
                    if (waterLevel >= maxLevel && key == ConsoleKey.UpArrow)
                    {
                    }
                    else if (waterLevel <= minLevel && key == ConsoleKey.DownArrow)
                    {
                    }
                    else
                    {
                        DisplayMessage("Invalid action! Press only valid keys.", ConsoleColor.Red);
                    }
                }

                CheckWaterLevel();

                System.Threading.Thread.Sleep(500);
            }
        }

        private void DisplayHeader()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("=== Water Levels Minigame ===");
            Console.ResetColor();
        }

        private void DisplayWaterLevel()
        {
            Console.WriteLine("\nWater Level:\n");

            // Display the water level vertically like a reservoir
            for (int i = maxLevel - 1; i >= 0; i--)
            {
                if (i < waterLevel)
                {

                    Console.WriteLine("|██████████████████████|");
                }
                else
                {
                    Console.WriteLine("|                      |");
                }
            }

            Console.WriteLine("------------------------");

            Console.WriteLine($"\nCurrent Level: {waterLevel}/{maxLevel}");
            Console.WriteLine($"Score: {Score}");
        }

        private void DisplayControls()
        {
            Console.WriteLine("\nControls:");
            Console.WriteLine("[Up Arrow] Add water.");
            Console.WriteLine("[Down Arrow] Remove water.");
            Console.WriteLine("[Esc] Exit.");
            Console.ResetColor();
        }

        private void DisplayRemainingTime(TimeSpan remainingTime)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nTime Remaining: {remainingTime.Seconds} seconds");
            Console.ResetColor();
        }

        private void DisplayMessage(string message, ConsoleColor color, bool isLongMessage = false)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();

            int delay = isLongMessage ? 2000 : 900; // Longer delay for important messages
            System.Threading.Thread.Sleep(delay);
        }

        private void CheckWaterLevel()
        {
            if (waterLevel >= maxLevel)
            {
                DisplayMessage("Overflow! Water level is too high! Stabilize it quickly!\nRecommendation from the colleague: it's most efficient when it is in the middle.\nYou lost 5 points by the way.", ConsoleColor.Red, true);
                Score -= 10;
            }
            else if (waterLevel <= minLevel)
            {
                DisplayMessage("Underflow! Water level is too low! Stabilize it quickly!\nRecommendation from the colleague: it's most efficient when it is in the middle.\nYou lost 10 points by the way.", ConsoleColor.Red, true);
                Score -= 10;
            }
            else
            {
                if (waterLevel == maxLevel - 1 || waterLevel == minLevel + 1)
                {
                    DisplayMessage("Warning: Approaching dangerous water level. Adjust valves carefully!\nYou lost 5 points by the way.", ConsoleColor.Yellow);
                    Score -= 5;
                }
            }
        }

        
    }
}

