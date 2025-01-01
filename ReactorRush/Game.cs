using System;
using Spectre.Console;
using Spectre.Console.Rendering;
using Minigames;
using Rooms;

namespace ReactorRush
{
    public class Game
    {
        public bool menuChosen = false;
        private readonly List<IMinigame> minigames = MinigameList.Minigames;
        private readonly List<IRooms> rooms = RoomsList.Rooms;
        private readonly Player player = new Player();

        public void Run()
        {
            Console.Clear();
            DisplayMenu();
        }

        private void DisplayMenu(int selected = 1)
        {
            Console.CursorVisible = false; // Hide the cursor
            Console.SetCursorPosition(0, 0);
            AnsiConsole.Write(new Padder(new FigletText("Reactor Rush").Centered().Color(Color.DarkOrange3)).PadTop(7));

            #region Menu
            var menu = new Table();
            menu.Centered();
            menu.HideHeaders();
            menu.Border(TableBorder.None);

            menu.AddColumn(new TableColumn("Menu").LeftAligned());
            string[] options = ["Start Game", "Minigames", "Settings", "Quit"];
            for (int i = 0; i < options.Length; i++)
            {
                if (selected == i + 1)
                {
                    menu.AddRow(new Panel(new Markup($"[bold aqua]{i + 1}. {options[i]}[/]")).Expand().PadLeft(2).PadRight(1).Border(BoxBorder.Double));
                }
                else
                {
                    menu.AddRow(new Panel(new Markup($"[bold orange3]{i + 1}. [/][yellow]{options[i]}[/]")).Expand().PadRight(2).Border(BoxBorder.Square));
                }
            }

            AnsiConsole.Write(menu);
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.GetCursorPosition().Top + 4);

            while (!menuChosen) {
                var key = Console.ReadKey(true); // Read key without displaying it
                switch (key.Key) {
                    case ConsoleKey.UpArrow:
                        if (selected == 1)
                            selected = options.Length; // If it would go past 1, reset it to 3
                        else
                            selected--;
                        DisplayMenu(selected);
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected == options.Length)
                            selected = 1; // If it would go past 3, reset it to 1
                        else
                            selected++;
                        DisplayMenu(selected);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        switch (selected) {
                            case 1:
                                StartGame();
                                break;
                            case 2:
                                DisplayMinigameMenu();
                                break;
                            case 3:
                                Settings();
                                break;
                            case 4:
                                Quit();
                                break;
                            default:
                                Console.WriteLine("Invalid choice!");
                                break;
                        }
                        menuChosen = true;
                        break;
                }
            }
            #endregion
        }

        private void StartGame()
        {
            Console.Clear();
            DisplayLevelSelectionMenu();
        }

        private void Settings()
        {
            Console.Clear();
            AnsiConsole.Write(new Markup($"[bold yellow]Your score is: {player.Score}[/]\n"));

            AnsiConsole.Write(new Markup("\n[bold underline]Rooms:[/]\n"));
            foreach (var room in rooms)
            {
                var roomName = room.GetType().Name;
                var status = player.HasPassedRoom(roomName) ? "[green]Passed[/]" : "[red]Not Passed[/]";
                AnsiConsole.Write(new Markup($"{roomName}: {status}\n"));
            }

            AnsiConsole.Write(new Markup("\n[bold underline]Minigames:[/]\n"));
            foreach (var minigame in minigames)
            {
                var minigameName = minigame.GetType().Name;
                var status = player.HasPassedMinigame(minigameName) ? "[green]Passed[/]" : "[red]Not Passed[/]";
                AnsiConsole.Write(new Markup($"{minigameName}: {status}\n"));
            }

            AnsiConsole.Write(new Markup("\n[bold]Press any key to return to the main menu...[/]"));
            Console.ReadKey();
            Run();
        }

        private void Quit() {
            Console.Clear();
            Console.WriteLine("Thanks for playing!");
            Thread.Sleep(1000); // pause for 1 seconds before closing the console
            Environment.Exit(1); // close the game
        }

        private void DisplayMinigameMenu(int selected = 1)
        {
            var passedMinigames = minigames.Where(m => player.HasPassedMinigame(m.GetType().Name)).ToList();
            if (passedMinigames.Count == 0)
            {
                Console.Clear();
                AnsiConsole.Write(new Padder(new FigletText("No passed minigames available!").Centered().Color(Color.Red)).PadTop(10));
                Thread.Sleep(1500);
                Run();
                return;
            }

            Console.CursorVisible = false; // Hide the cursor
            Console.SetCursorPosition(0, 0);
            AnsiConsole.Write(new Padder(new FigletText("Select Minigame").Centered().Color(Color.DarkOrange3)).PadTop(7));

            #region MinigameMenu
            var menu = new Table();
            menu.Centered();
            menu.HideHeaders();
            menu.Border(TableBorder.None);

            menu.AddColumn(new TableColumn("Minigames").LeftAligned());
            for (int i = 0; i < passedMinigames.Count; i++)
            {
                var minigameName = passedMinigames[i].GetType().Name;
                if (selected == i + 1)
                {
                    menu.AddRow(new Panel(new Markup($"[bold aqua]{i + 1}. {minigameName}[/]")).Expand().PadLeft(2).PadRight(1).Border(BoxBorder.Double));
                }
                else
                {
                    menu.AddRow(new Panel(new Markup($"[bold orange3]{i + 1}. [/][yellow]{minigameName}[/]")).Expand().PadRight(2).Border(BoxBorder.Square));
                }
            }

            // Add the "Back" button
            if (selected == passedMinigames.Count + 1)
            {
                menu.AddRow(new Panel(new Markup($"[bold aqua]{passedMinigames.Count + 1}. Back[/]")).Expand().PadLeft(2).PadRight(1).Border(BoxBorder.Double));
            }
            else
            {
                menu.AddRow(new Panel(new Markup($"[bold orange3]{passedMinigames.Count + 1}. [/][yellow]Back[/]")).Expand().PadRight(2).Border(BoxBorder.Square));
            }

            AnsiConsole.Write(menu);
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.GetCursorPosition().Top + 4);

            var menuChosen = false;
            while (!menuChosen)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected == 1)
                            selected = passedMinigames.Count + 1; // If it would go past 1, reset it to the last option
                        else
                            selected--;
                        DisplayMinigameMenu(selected);
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected == passedMinigames.Count + 1)
                            selected = 1; // If it would go past the last option, reset it to 1
                        else
                            selected++;
                        DisplayMinigameMenu(selected);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        if (selected == passedMinigames.Count + 1)
                        {
                            Run(); // Go back to the main menu
                        }
                        else
                        {
                            passedMinigames[selected - 1].Run();
                            Thread.Sleep(1000);
                            DisplayMinigameMenu(selected);
                        }
                        menuChosen = true;
                        break;
                    case ConsoleKey.Backspace:
                    case ConsoleKey.Escape:
                        Run();
                        break;
                }
            }
            #endregion
        }

        private void DisplayLevelSelectionMenu(int selected = 1) 
        {
            Console.CursorVisible = false; // Hide the cursor
            Console.SetCursorPosition(0, 0);
            AnsiConsole.Write(new Padder(new FigletText("Select Level").Centered().Color(Color.DarkOrange3)).PadTop(7));

            #region LevelSelectionMenu
            var menu = new Grid();
            menu.Centered();

            string[] levels = ["Visitor Center", "Control Room", "Cooling System", "Steam Turbine Room", "Waste Storage Facility", "Containment Building", "Fuel Handling Area", "Emergency Backup Room", "Water Reservoir", "Radiation Monitor", "Laboratory", "Reactor Core"];
            int columns = 3;
            int rows = 4;

            for (int i = 0; i < columns; i++)
            {
                menu.AddColumn(new GridColumn().Centered());
            }
            for (int i = 0; i < rows; i++)
            {
                var row = new List<IRenderable>();
                for (int j = 0; j < columns; j++)
                {
                    int index = i * columns + j;
                    if (index < levels.Length)
                    {
                        if (selected == index + 1)
                        {
                            row.Add(new Panel(new Markup($"[bold aqua]{levels[index]}[/]")).Expand().Border(BoxBorder.Double));
                        }
                        else
                        {
                            // Check if the level is available
                            if (rooms.Any(m => m.GetType().Name == levels[index].Trim().Replace(" ", "")))
                            {
                                row.Add(new Panel(new Markup($"[yellow]{levels[index]}[/]")).Expand().Border(BoxBorder.Square));
                            }
                            else
                            {
                                row.Add(new Panel(new Markup($"[bold red]{levels[index]}[/]")).Expand().Border(BoxBorder.Square));
                            }
                        }
                    }
                    else
                    {
                        row.Add(new Panel(new Markup($"[red]Coming soon[/]")).Expand()); // Placeholder for future levels
                    }
                }
                menu.AddRow(row.ToArray());
            }

            // Add the "Back" button row
            var backRow = new List<IRenderable>();
            for (int i = 0; i < columns; i++)
            {
                if (i == columns / 2)
                {
                    if (selected == levels.Length + 1)
                    {
                        backRow.Add(new Panel(new Markup($"[bold aquamarine3]Back[/]")).Expand().Border(BoxBorder.Double));
                    }
                    else
                    {
                        backRow.Add(new Panel(new Markup($"[orange3]Back[/]")).Expand().Border(BoxBorder.Square));
                    }
                }
                else
                {
                    backRow.Add(new Panel(new Markup("")).Expand().Border(BoxBorder.None)); // Left and right empty columns
                }
            }
            menu.AddRow(backRow.ToArray());

            var paddedMenu = new Panel(Align.Center(menu)).Expand().Border(BoxBorder.None);
            AnsiConsole.Write(paddedMenu);
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.GetCursorPosition().Top + 4);

            while (true) {
                var key = Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.LeftArrow:
                        if (selected == 1)
                            selected = levels.Length + 1; // If it would go past 1, reset it to the last option
                        else
                            selected--; // Move left
                        DisplayLevelSelectionMenu(selected);
                        break;
                    case ConsoleKey.UpArrow:
                        if (selected > columns)
                            selected-= columns; // If it has a row above, move up
                        DisplayLevelSelectionMenu(selected);
                        break;
                    case ConsoleKey.RightArrow:
                        if (selected == levels.Length + 1)
                            selected = 1; // If it would go past the last option, reset it to 1
                        else
                            selected++; // Move right
                        DisplayLevelSelectionMenu(selected);
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected >= levels.Length + 1 - columns)
                            selected = levels.Length + 1; // If it would go past the last option, reset it to the "Back" button
                        else
                            selected+= columns; // If it has a row below, move down
                        DisplayLevelSelectionMenu(selected);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        if (selected == levels.Length + 1)
                        {
                            Run(); // Go back to the main menu
                        }
                        else
                        {
                            // Start the selected level
                            var room = rooms.FirstOrDefault(m => m.GetType().Name == levels[selected - 1].Trim().Replace(" ", ""));
                            if (room != null)
                            {
                                StartLevel(rooms.IndexOf(room) + 1);
                            }
                            else
                            {
                                Console.WriteLine("This level is not available yet.");
                                Thread.Sleep(1000);
                                DisplayLevelSelectionMenu(selected);
                            }
                        }
                        return;
                    case ConsoleKey.Escape: 
                    case ConsoleKey.Backspace:
                        Run(); // Go back to the main menu
                        return;
                }
            }
            #endregion
        }

        private void StartLevel(int level) {
            int score = rooms[level - 1].StartLevel(player);
            Console.WriteLine($"SCORE: {score} :SCORE\n\n");
            Console.WriteLine("[DEBUG] Press any key to return to menu...");
            Console.ReadKey();
            Run();
        }
    }
}
