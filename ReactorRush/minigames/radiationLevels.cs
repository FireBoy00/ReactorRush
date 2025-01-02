using System;
using Spectre.Console;

namespace Minigames
{
    public class RadiationLevels : IMinigame
    {
        public int Score { get; private set; }
        private int[] radiationLevels;
        private bool[] valves;
        private int timeLimit;
        private int time;
        private Random randomrand;

        public RadiationLevels(int arraySize, int limit) {
            radiationLevels = new int[arraySize];
            randomrand = new Random();
            for (int i = 0; i < radiationLevels.Length; i++)
            {
                radiationLevels[i] = randomrand.Next(1, 10);
            }
            valves = new bool[arraySize];
            timeLimit = limit;
            Score = 0;
        }

        public void Run()
        {
            Score = 0;
            for (int i = 0; i < radiationLevels.Length; i++)
            {
                radiationLevels[i] = randomrand.Next(1, 10);
            }
            valves = new bool[radiationLevels.Length];
            time = 0;
            DisplayInstructions();
            while (time < timeLimit)
            {
                time++;
                radiationLevels[randomrand.Next(radiationLevels.Length)] += 1;
                if (CheckWinCondition())
                {
                    Score = (timeLimit - time) * 10;
                    DisplayWinScreen();
                    return;
                }
                Console.SetCursorPosition(0, 0);
                DisplayTimer();
                DrawValves();
            }
            Score = 0;
            DisplayLoseScreen();
        }

        private void DisplayInstructions()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new Panel(new Markup("[bold yellow]Welcome to the Radiation Monitoring Game![/]\n\n" +
                                     "Instructions:\n" +
                                     "- The game increments radiation levels randomly.\n" +
                                     "- To win, the difference between consecutive radiation levels must not exceed 1.\n" +
                                     "- Use the arrow keys to navigate and Enter to release a valve.\n" +
                                     "- Score is based on how quickly you stabilize the radiation levels.\n\n" +
                                     "Press any key to start..."))
                .Expand()
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Green)));
            Console.ReadKey(true);
            AnsiConsole.Clear();
        }

        private bool CheckWinCondition()
        {
            for (int i = 0; i < radiationLevels.Length - 1; i++)
            {
                if (Math.Abs(radiationLevels[i] - radiationLevels[i + 1]) > 1)
                {
                    return false;
                }
            }
            return true;
        }

        private void DisplayWinScreen()
        {
            AnsiConsole.Clear();

            Console.SetCursorPosition(0, Console.WindowHeight * 1 / 3);
            AnsiConsole.Write(new FigletText("WIN").Centered().Color(Color.Green));

            Console.SetCursorPosition(0, Console.CursorTop + 2);
            var table = new Table().Centered();
            table.AddColumn(new TableColumn(new Markup($"[bold yellow]Score: {Score}[/]")).Centered());
            table.AddRow(new Markup($"- Time Bonus: [bold yellow]{(timeLimit - time) * 10}[/]"));
            AnsiConsole.Write(new Panel(table).Expand().Border(BoxBorder.None));

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            AnsiConsole.Write(new Markup("[bold yellow]Press any key to continue...[/]").Centered());
            Console.ReadKey(true);
            AnsiConsole.Clear();
        }

        private void DisplayLoseScreen()
        {
            AnsiConsole.Clear();

            Console.SetCursorPosition(0, Console.WindowHeight * 1 / 3);
            AnsiConsole.Write(new FigletText("LOSE").Centered().Color(Color.Red));

            Console.SetCursorPosition(0, Console.CursorTop + 2);
            AnsiConsole.Write(new Panel(new Markup("[red]Better luck next time! \n\nTry to keep the radiation levels stable by releasing the valves in time.[/]").Centered()).Expand().Border(BoxBorder.None));
            
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            AnsiConsole.Write(new Markup("[bold yellow]Press any key to continue...[/]").Centered());
            Console.ReadKey(true);
            AnsiConsole.Clear();
        }

        private void DisplayTimer()
        {
            AnsiConsole.Write(new FigletText($"Time: {time} / {timeLimit}").Centered().Color(Color.Blue));
        }

        void Draw()
        {
            var canvas = new Canvas(128, 30);
            for (var i = 0; i < radiationLevels.Length; i++)
            {
                for (int j = 0; j < radiationLevels[i]; j++)
                {
                    canvas.SetPixel(j + 45, i * radiationLevels.Length + 5, Color.GreenYellow); // Adjusted position
                }
            }
            AnsiConsole.Write(canvas);
        }

        void DrawValves(int selected = 1)
        {
            Console.SetCursorPosition(0, 5); // Adjusted position
            Draw();
            var menu = new Table();
            menu.Centered();
            menu.HideHeaders();
            menu.Border(TableBorder.None);

            menu.AddColumn(new TableColumn("Menu").Centered());
            for (int i = 0; i < valves.Length; i++)
            {
                if (selected == i + 1)
                {
                    menu.AddRow(new Panel(new Markup($"[bold aqua]Release valve {i + 1}[/]")).Expand().Border(BoxBorder.Square));
                }
                else
                {
                    menu.AddRow(new Panel(new Markup($"[yellow]Release valve {i + 1}[/]")).Expand().Border(BoxBorder.None));
                }
            }

            AnsiConsole.Write(menu);

            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selected == 1)
                        selected = valves.Length;
                    else
                        selected--;
                    DrawValves(selected);
                    break;
                case ConsoleKey.DownArrow:
                    if (selected == valves.Length)
                        selected = 1;
                    else
                        selected++;
                    DrawValves(selected);
                    break;
                case ConsoleKey.Enter:
                    radiationLevels[selected - 1] -= 2;
                    return;
            }
        }
    }
}