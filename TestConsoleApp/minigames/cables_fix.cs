using System;
using Spectre.Console;

namespace ReactorRushGame
{
    public class MinigameCableFix
    {
        private int width;
        private int height;
        private int startX;
        private int startY;
        private int endX;
        private int endY;
        private bool[,] visited = null!;
        private int playerX;
        private int playerY;
        private DateTime startTime;

        public void Run()
        {
            Console.CursorVisible = false; // Hide the cursor
            width = Console.WindowWidth / 4; // Make the table smaller
            height = Console.WindowHeight / 4; // Make the table smaller
            visited = new bool[height, width];
            var random = new Random();

            startX = 0;
            startY = random.Next(height);
            endX = width - 1;
            endY = random.Next(height);

            playerX = startX;
            playerY = startY;

            startTime = DateTime.Now; // Initialize the start time

            Console.Clear();
            while (true)
            {
                Console.SetCursorPosition(0, 0); // Set cursor to the start of the table
                DisplayTimer();
                Console.SetCursorPosition(0, Console.WindowHeight / 2 - height / 2); // Set cursor to the start of the table, below the timer
                DisplayCable();
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (MovePlayer(key))
                    {
                        if (playerX == endX && playerY == endY)
                        {
                            Console.Clear();
                            Console.WriteLine("You have successfully fixed the cable!");
                            DisplayCompletionTime();
                            break;
                        }
                    }
                    if (IsGameOver())
                    {
                        Console.Clear();
                        Console.WriteLine("Game Over! You couldn't reach the end.");
                        break;
                    }
                    while (Console.KeyAvailable) Console.ReadKey(true); // Clear the input buffer
                }
            }
        }

        private void DisplayTimer()
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;
            // var panel = new Panel(new Markup($"{elapsedTime:mm\\:ss}").Centered())
            // .Border(BoxBorder.Rounded)
            // .BorderColor(Color.DarkOrange3)
            // .Padding(1, 2)
            // .Collapse();

            // var centeredPanel = new Padder(panel, new Padding((Console.WindowWidth - (panel.Width ?? 0)) / 2, 0));
            // AnsiConsole.Write(centeredPanel);
            AnsiConsole.Write(new Padder(new FigletText($"{elapsedTime:mm\\:ss}").Centered().Color(Color.DarkOrange3)).PadTop(7));
        }

        private void DisplayCable()
        {
            var table = new Table();
            table.HideHeaders();
            table.Collapse();
            table.Centered();
            table.Border(TableBorder.None);

            // Define the columns
            for (int x = 0; x < width; x++)
            {
                table.AddColumn(new TableColumn("").PadLeft(0).PadRight(0).PadTop(0).PadBottom(0));
            }

            for (int y = 0; y < height; y++)
            {
                var row = new List<Markup>();
                for (int x = 0; x < width; x++)
                {
                    string cellContent = "  "; // Double space to make cells wider (for square cells)
                    Style cellStyle = new Style();

                    if (x == startX && y == startY)
                    {
                        cellStyle = new Style().Background(Color.DarkRed_1);
                    }
                    else if (x == endX && y == endY)
                    {
                        cellStyle = new Style().Background(Color.DarkRed_1);
                    }
                    else if (x == playerX && y == playerY)
                    {
                        cellStyle = new Style().Background(Color.DarkCyan);
                    }
                    else if (visited[y, x])
                    {
                        cellStyle = new Style().Background(Color.DarkGreen);
                    }
                    else
                    {
                        cellStyle = new Style().Background(Color.Grey15);
                    }

                    row.Add(new Markup(cellContent, cellStyle));
                }
                table.AddRow(new TableRow(row));
            }

            AnsiConsole.Write(table);
        }

        private bool MovePlayer(ConsoleKey key)
        {
            int newX = playerX;
            int newY = playerY;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    newY--;
                    break;
                case ConsoleKey.DownArrow:
                    newY++;
                    break;
                case ConsoleKey.LeftArrow:
                    newX--;
                    break;
                case ConsoleKey.RightArrow:
                    newX++;
                    break;
                default:
                    return false;
            }

            if (newX >= 0 && newX < width && newY >= 0 && newY < height && !visited[newY, newX])
            {
                playerX = newX;
                playerY = newY;
                visited[playerY, playerX] = true;
                return true;
            }

            return false;
        }

        private bool IsSoftLocked()
        {
            var queue = new Queue<(int x, int y)>();
            var visitedCheck = new bool[height, width];
            queue.Enqueue((playerX, playerY));
            visitedCheck[playerY, playerX] = true;

            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();
                if (x == endX && y == endY)
                {
                    return false; // Found a path to the end
                }

                foreach (var (dx, dy) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
                {
                    int newX = x + dx;
                    int newY = y + dy;
                    if (newX >= 0 && newX < width && newY >= 0 && newY < height && !visited[newY, newX] && !visitedCheck[newY, newX])
                    {
                        queue.Enqueue((newX, newY));
                        visitedCheck[newY, newX] = true;
                    }
                }
            }

            return true; // No path to the end
        }

        private bool IsGameOver()
        {
            return !CanMoveTo(playerX - 1, playerY) && !CanMoveTo(playerX + 1, playerY) &&
                   !CanMoveTo(playerX, playerY - 1) && !CanMoveTo(playerX, playerY + 1) || IsSoftLocked();
        }

        private bool CanMoveTo(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height && !visited[y, x];
        }

        private void DisplayCompletionTime()
        {
            TimeSpan totalTime = DateTime.Now - startTime;
            Console.WriteLine($"You took {totalTime:mm\\:ss} to fix the cable.");
        }
    }
}