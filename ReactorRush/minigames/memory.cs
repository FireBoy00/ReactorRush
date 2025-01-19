using System;
using Spectre.Console;

// we will give feedback based on the number of tries 

namespace Minigames
{
    public class Memory : IMinigame
    {
        private const int Rows = 3;
        private const int Cols = 4;
        public int Score { get; private set; }
        private const int MaxScore = Rows * Cols / 2;
        private Dictionary<int, string> cardMap = new Dictionary<int, string>();
        private List<int> flippedCards = new List<int>();
        private int mistakes = 0; // increase every time the user flips two non-pair cards
        public int MaxMistakes { get; private set; } = 8;
        public int MemoriseSec { get; private set; } = 45;
        List<string> items = new List<string>
                {
                    "Nuclear reactors", "contain and control nuclear chain reactions that produce heat",
                    "Nuclear fission", "a process where atoms split and release energy",
                    "Nuclear fuel", "most reactors use uranium",
                    "Water", "acts as both a coolant and moderator",
                    "Moderator", "helps slow down the neutrons produced by fission to sustain the chain reaction",
                    "Heat", "turns the water into steam, which spins a turbine to produce carbon-free electricity"
                };
        private Dictionary<string, string> pairs = new Dictionary<string, string>
        {
            { "Nuclear reactors", "contain and control nuclear chain reactions that produce heat" },
            { "Nuclear fission", "a process where atoms split and release energy" },
            { "Nuclear fuel", "most reactors use uranium" },
            { "Water", "acts as both a coolant and moderator" },
            { "Moderator", "helps slow down the neutrons produced by fission to sustain the chain reaction" },
            { "Heat", "turns the water into steam, which spins a turbine to produce carbon-free electricity" }
        };

        public void Run()
        {
            Console.Title = "Memory Minigame";
            Score = 0;
            Countdown();
        }

        private bool IsEnd()
        {
            // Check if the game should end: either all pairs are found or mistakes exceed the maximum allowed
            return Score == MaxScore || mistakes >= MaxMistakes;
        }

        private void ShowBackside()
        {
            // Create and display the cards -- with their backsides upwards
            var table = new Table();

            table.AddColumn(new TableColumn("header 1").Centered());
            table.AddColumn(new TableColumn("header 2").Centered());
            table.AddColumn(new TableColumn("header 3").Centered());
            table.AddColumn(new TableColumn("header 4").Centered());

            table.AddEmptyRow();

            table.AddRow("Card 1", "Card 2", "Card 3", "Card 4");
            table.AddEmptyRow();
            table.AddEmptyRow();
            table.AddRow("Card 5", "Card 6", "Card 7", "Card 8");
            table.AddEmptyRow();
            table.AddEmptyRow();
            table.AddRow("Card 9", "Card 10", "Card 11", "Card 12");
            table.AddEmptyRow();

            table.HideHeaders();
            table.Width(150);
            table.LeftAligned();
            table.Border = TableBorder.Rounded;

            AnsiConsole.Write(table);
        }

        private void ShowFrontside()
        {
            // Shuffle the pairs, display them in a table and assign card numbers to the text on the frontside
            Random rand = new Random();
            items = items.OrderBy(x => rand.Next()).ToList();
            cardMap = new Dictionary<int, string>();
            for (int i = 0; i < items.Count; i++)
            {
                cardMap[i + 1] = items[i];
            }
            DisplayTable(items, Rows, Cols);
        }

        static void DisplayTable(List<string> items, int Rows, int Cols)
        {
            var table = new Table();

            // Adding columns
            for (int i = 0; i < Cols; i++)
            {
                table.AddColumn(new TableColumn($"Column {i + 1}").Centered());
            }

            // Adding rows
            for (int i = 0; i < Rows; i++)
            {
                var row = new List<string>();
                for (int j = 0; j < Cols; j++)
                {
                    row.Add(items[i * Cols + j]);
                }
                table.AddEmptyRow();
                table.AddRow(row.ToArray());
                table.AddEmptyRow();
            }

            table.HideHeaders();
            table.Width(150);
            table.LeftAligned();
            table.Border = TableBorder.Rounded;

            AnsiConsole.Write(table);
        }

        // show the cards with the texts for x seconds in the beginning, while counting down
        public void Countdown()
        {
            System.Timers.Timer timer = new(1000); // 1000 milliseconds = 1 second
            int remainingTime = MemoriseSec;
            ShowFrontside();

            timer.Elapsed += (sender, e) =>
            {
                if (remainingTime > 0)
                {
                    Console.CursorVisible = false; // hide the cursor
                    Console.WriteLine($"{remainingTime} seconds remaining to memorise the cards...");
                    ClearLastLine();
                    remainingTime--;
                }
                else
                {
                    timer.Stop();
                    StartGame();
                }
            };
            timer.Start();

            // Keep the console application running until the countdown is complete
            while (timer.Enabled)
            {
                Thread.Sleep(100);
            }

            // Ensure the game does not auto-close after the timer ends
            StartGame();
        }

        private void StartGame()
        {
            // This method starts the game after the countdown ends
            Console.Clear();
            Console.WriteLine("Time's up, now it's time for you to find the pairs!");
            ShowBackside();
            Console.CursorVisible = true; // show the cursor
            Console.WriteLine("Please type in the number of the card you want to flip, then hit enter.");

            // Enter a loop to read user input and flip cards
            while (!IsEnd())
            {
                Console.WriteLine($"Score: {Score}");
                Console.WriteLine($"Maximum mistakes allowed: {MaxMistakes}");
                Console.WriteLine($"Mistakes made: {mistakes}");
                Console.Write("Enter card number: ");
                string input = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(input, out int cardNumber))
                {
                    FlipCard(cardNumber);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 12.");
                    DisplayTableWithFlippedCards();
                }
            }

            if (Score == MaxScore)
            {
                Console.WriteLine("Congrats, you've completed the memory game!");
                Console.WriteLine($"Score: {Score}");
                Console.WriteLine($"Mistakes made: {mistakes}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(); // Wait for user input before closing
            }
            else
            {
                Console.WriteLine("Game over, you've reached the maximum number of mistakes!");
                Console.WriteLine($"Score: {Score}");
                Console.WriteLine($"Mistakes made: {mistakes}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(); // Wait for user input before closing
                mistakes = 0;
                Score = 0;
                flippedCards.Clear();
            }
            Console.CursorVisible = false; // hide the cursor
        }

        static void ClearLastLine()
        {
            // Move the cursor to the beginning of the last line
            Console.SetCursorPosition(0, Console.CursorTop);
            // Write spaces to clear the line
            Console.Write(new string(' ', Console.WindowWidth));
            // Move the cursor back to the beginning of the last line
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        public void FlipCard(int cardNumber)
        {
            if (cardMap.ContainsKey(cardNumber))
            {
                if (flippedCards.Contains(cardNumber))
                {
                    Console.Clear();
                    Console.WriteLine("This card has already been paired. Please choose another card.");
                    DisplayTableWithFlippedCards();
                    return;
                }

                Console.Clear();
                flippedCards.Add(cardNumber);
                DisplayTableWithFlippedCards();

                if (flippedCards.Count % 2 == 0)
                {
                    CheckPair();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid card number.");
                DisplayTableWithFlippedCards();
            }
        }

        private void DisplayTableWithFlippedCards()
        {
            var table = new Table();

            // Add columns
            for (int i = 0; i < Cols; i++)
            {
                table.AddColumn(new TableColumn($"Column {i + 1}").Centered());
            }

            // Add rows
            for (int i = 0; i < Rows; i++)
            {
                var row = new List<string>();
                for (int j = 0; j < Cols; j++)
                {
                    int cardNumber = i * Cols + j + 1;
                    if (flippedCards.Contains(cardNumber))
                    {
                        row.Add(cardMap[cardNumber]);
                    }
                    else
                    {
                        row.Add($"Card {cardNumber}");
                    }
                }
                table.AddEmptyRow();
                table.AddRow(row.ToArray());
                table.AddEmptyRow();
            }

            table.HideHeaders();
            table.Width(150);
            table.LeftAligned();
            table.Border = TableBorder.Rounded;

            AnsiConsole.Write(table);
        }


        private void CheckPair()
        {
            // Check if the two flipped cards form a pair based on predefined pairs
            string card1 = cardMap[flippedCards[flippedCards.Count - 2]]; // second last element
            string card2 = cardMap[flippedCards[flippedCards.Count - 1]]; // last element

            Thread.Sleep(3000); // stay for 3 sec
            Console.Clear();

            if ((pairs.ContainsKey(card1) && pairs[card1] == card2) || (pairs.ContainsKey(card2) && pairs[card2] == card1))
            {
                Console.WriteLine("It's a match!");
                Score++;
            }
            else
            {
                Console.WriteLine("Not a match.");
                mistakes++;
                // Remove the cards from flippedCards if they are not a match
                flippedCards.RemoveAt(flippedCards.Count - 1);
                flippedCards.RemoveAt(flippedCards.Count - 1);
            }
            DisplayTableWithFlippedCards();
        }
    }
}