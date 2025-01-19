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
        public bool continueGame = false;
        private readonly List<IMinigame> minigames = MinigameList.Minigames;
        private readonly List<IRooms> rooms = RoomsList.Rooms;
        private readonly Player player = new Player();

        public void Run()
        {
            Console.Clear();
            Console.Title = "Reactor Rush";

            //! [DEBUG] Set the player's progress to true for all rooms except the WasteStorageFacility [DEBUG]
            // foreach (var room in rooms)
            // {
            //     if (room.GetType().Name != "WasteStorageFacility")
            //     {
            //         player.UpdateRoomStatus(room.GetType().Name, true);
            //     }
            // }

            if (!continueGame && rooms.All(room => player.HasPassedRoom(room.GetType().Name)))
            {
                DisplayEndScreen();
                return;
            }
            DisplayMenu();
        }

        private void DisplayMenu(int selected = 1)
        {
            Console.Title = "Reactor Rush: Main Menu";
            Console.CursorVisible = false; // Hide the cursor
            Console.SetCursorPosition(0, 0);
            AnsiConsole.Write(new Padder(new FigletText("Reactor Rush").Centered().Color(Color.DarkOrange3)).PadTop(7));

            #region Menu
            var menu = new Table();
            menu.Centered();
            menu.HideHeaders();
            menu.Border(TableBorder.None);

            menu.AddColumn(new TableColumn("Menu").LeftAligned());
            string[] options = ["Start Game", "Minigames", "Statistics", "Quit"];
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
                                Statistics();
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

        private void Statistics()
        {
            Console.Clear();
            Console.Title = "Reactor Rush: Statistics";
            AnsiConsole.Write(new Markup($"[bold yellow]Your score is: {player.Score}[/]\n"));

            var table = new Table();
            table.AddColumn(new TableColumn("[bold underline]Rooms[/]").LeftAligned());
            table.AddColumn(new TableColumn("[bold underline]Status[/]").RightAligned());
            table.AddColumn(new TableColumn("[bold underline]Score[/]").RightAligned());

            foreach (var room in rooms)
            {
                var roomName = room.GetType().Name;
                var status = player.HasPassedRoom(roomName) ? "[green]Passed[/]" : "[red]Not Passed[/]";
                table.AddRow(roomName, status, room.Score.ToString());
            }

            AnsiConsole.Write(table);

            var minigameTable = new Table();
            minigameTable.AddColumn(new TableColumn("[bold underline]Minigames[/]").LeftAligned());
            minigameTable.AddColumn(new TableColumn("[bold underline]Status[/]").RightAligned());
            minigameTable.AddColumn(new TableColumn("[bold underline]Score[/]").RightAligned());

            foreach (var minigame in minigames)
            {
                var minigameName = minigame.GetType().Name;
                var status = player.HasPassedMinigame(minigameName) ? "[green]Passed[/]" : "[red]Not Passed[/]";
                minigameTable.AddRow(minigameName, status, minigame.Score.ToString());
            }

            AnsiConsole.Write(minigameTable);

            AnsiConsole.Write(new Markup("\n[bold]Press any key to return to the main menu...[/]"));
            Console.ReadKey();
            Run();
        }

        private static void Quit() {
            Console.Clear();
            Console.Title = "Reactor Rush: Quit";
            Console.WriteLine("Thanks for playing!");
            Thread.Sleep(1000); // pause for 1 seconds before closing the console
            Environment.Exit(1); // close the game
        }

        private void DisplayMinigameMenu(int selected = 1)
        {
            Console.Title = "Reactor Rush: Minigame Menu";
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
                            Console.Title = minigames[selected - 1].GetType().Name;
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
            Console.Title = "Reactor Rush: Level Selection Menu";
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
                                if (player.HasPassedRoom(levels[index].Trim().Replace(" ", "")))
                                {
                                    row.Add(new Panel(new Markup($"[green]{levels[index]}[/]")).Expand().Border(BoxBorder.Square));
                                }
                                else
                                {
                                    row.Add(new Panel(new Markup($"[yellow]{levels[index]}[/]")).Expand().Border(BoxBorder.Square));
                                }
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
            Console.SetCursorPosition(0, Console.WindowHeight * 1 / 3);
            AnsiConsole.Write(new FigletText($"SCORE: {score}").Centered().Color(Color.Yellow));
            Console.SetCursorPosition(2, Console.WindowHeight - 1);
            AnsiConsole.Write(new Markup("[bold]Press any key to return to the level selection...[/]"));
            Console.ReadKey();
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            AnsiConsole.Write(new string(' ', Console.WindowWidth)); // Clear the "Press any key..." message
            DisplayLevelSelectionMenu();
        }

        private void DisplayEndScreen() {
            AnsiConsole.Clear();
            Console.Title = "Reactor Rush: End Screen";
            Console.SetCursorPosition(0, 0);
            AnsiConsole.Write(new Padder(new FigletText($"{player.Score}").Centered().Color(Color.DarkOrange3)).PadTop(4));

            Panel messagePanel;
            if (player.Score > 185) {
                messagePanel = new Panel(new Markup("[bold]What an outstanding score! You are clearly well-educated on the topic of nuclear energy.[/]").Centered()).Expand().Border(BoxBorder.None);
            }
            else if (player.Score <= 185 && player.Score > 102) {
                messagePanel = new Panel(new Markup("[bold]A respectable score. We hope we were able to add to your existing knowledge through our game.[/]").Centered()).Expand().Border(BoxBorder.None);
            }
            else {
                messagePanel = new Panel(new Markup("[bold]We hope you found our game fun and insightful! However, you might want to take a look back at some of the rooms.[/]").Centered()).Expand().Border(BoxBorder.None);
            }

            AnsiConsole.Write(messagePanel);

            Color personColor = Color.Orange3;
            Color roleColor = Color.DarkOrange3;

            #region Credits
            var menu = new Table();
            menu.Centered();
            menu.Border(TableBorder.None);
            menu.HideHeaders();
            menu.AddColumn(new TableColumn("Credits").Centered());
            menu.AddRow(new Panel(new Markup($"[bold {roleColor}]Credit goes to[/]").Centered()).Expand().Border(BoxBorder.None).PadBottom(2));
            menu.AddEmptyRow();
            menu.AddRow(new Panel(new Markup($"[bold {personColor}]Adrian Stancu[/]").Centered()).Expand().Border(BoxBorder.None));
            menu.AddRow(new Panel(new Markup($"[bold {roleColor}]Developer, Github & Discord manager[/]").Centered()).Expand().Border(BoxBorder.None).PadBottom(2));
            menu.AddRow(new Panel(new Markup($"[bold {personColor}]Agata Majewska[/]").Centered()).Expand().Border(BoxBorder.None));
            menu.AddRow(new Panel(new Markup($"[bold {roleColor}]Developer, Lead designer[/]").Centered()).Expand().Border(BoxBorder.None).PadBottom(2));
            menu.AddRow(new Panel(new Markup($"[bold {personColor}]Emma Sólyom[/]").Centered()).Expand().Border(BoxBorder.None));
            menu.AddRow(new Panel(new Markup($"[bold {roleColor}]Lead researcher[/]").Centered()).Expand().Border(BoxBorder.None).PadBottom(2));
            menu.AddRow(new Panel(new Markup($"[bold {personColor}]Gabija Staskeviciute[/]").Centered()).Expand().Border(BoxBorder.None));
            menu.AddRow(new Panel(new Markup($"[bold {roleColor}]Story & Research[/]").Centered()).Expand().Border(BoxBorder.None).PadBottom(2));
            menu.AddRow(new Panel(new Markup($"[bold {personColor}]Morten Lins[/]").Centered()).Expand().Border(BoxBorder.None));
            menu.AddRow(new Panel(new Markup($"[bold {roleColor}]Developer, QA[/]").Centered()).Expand().Border(BoxBorder.None).PadBottom(2));
            menu.AddRow(new Panel(new Markup($"[bold {personColor}]Paul Donici[/]").Centered()).Expand().Border(BoxBorder.None));
            menu.AddRow(new Panel(new Markup($"[bold {roleColor}]Designer[/]").Centered()).Expand().Border(BoxBorder.None).PadBottom(2));
            menu.AddRow(new Panel(new Markup($"[bold {personColor}]Spectre Console[/]").Centered()).Expand().Border(BoxBorder.None));
            menu.AddRow(new Panel(new Markup($"[bold {roleColor}]Text formatting library[/]").Centered()).Expand().Border(BoxBorder.None));

            Console.SetCursorPosition(0, Console.CursorTop + 4);
            AnsiConsole.Write(menu);
            #endregion Credits

            Console.SetCursorPosition(0, Console.WindowHeight - 4);
            string[] options = ["Continue Playing", "Quit Game"];
            PrintButtons(options);
        }
    
        private void PrintButtons(string[] options, int selected = 1, (int, int)? cursorPosition = null, bool clear = false) {
            if (!clear) {
                cursorPosition = (Console.CursorLeft, Console.CursorTop);
            }else {
                if (cursorPosition.HasValue)
                {
                    Console.SetCursorPosition(cursorPosition.Value.Item1, cursorPosition.Value.Item2);
                }
            }

            var endMenu = new Table();
            endMenu.Centered();
            endMenu.HideHeaders();
            endMenu.Border(TableBorder.None);

            foreach (var option in options)
            {
                endMenu.AddColumn(new TableColumn(option).Centered());
            }

            var buttons = new List<Panel>();
            for (int i = 0; i < options.Length; i++)
            {
                var border = selected == i + 1 ? BoxBorder.Double : BoxBorder.Square;
                var color = selected == i + 1 ? "aqua" : "orange3";
                buttons.Add(new Panel(new Markup($"[bold {color}]{options[i]}[/]")).Expand().Border(border));
            }
            endMenu.AddRow(buttons.ToArray());

            AnsiConsole.Write(endMenu);

            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (selected == 1)
                            selected = options.Length;
                        else
                            selected--;
                        PrintButtons(options, selected, cursorPosition, true);
                        break;
                    case ConsoleKey.RightArrow:
                        if (selected == options.Length)
                            selected = 1;
                        else
                            selected++;
                        PrintButtons(options, selected, cursorPosition, true);
                        break;
                    case ConsoleKey.Enter:
                        switch (selected)
                        {
                            case 1:
                                continueGame = true;
                                Run();
                                break;
                            case 2:
                                Quit();
                                break;
                        }
                        return;
                }
            }
        }
    }
}
