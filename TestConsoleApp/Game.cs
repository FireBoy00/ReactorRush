using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using Spectre.Console;
using System.IO;
using System.Collections.Generic;
using Minigames;

namespace ReactorRushGame
{
    public class Game
    {
        public bool menuChosen = false;
        private List<IMinigame> minigames = MinigameList.Minigames;

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
            if (selected == 1) {
                menu.AddRow(new Panel(new Markup("[bold aqua]1. Start Game[/]")).Expand().PadLeft(2).PadRight(1));
            }else {
                menu.AddRow(new Panel(new Markup("[bold orange3]1. [/][yellow]Start Game[/]")).Expand().PadRight(2));
            }
            if (selected == 2) {
                menu.AddRow(new Panel(new Markup("[bold aqua]2. Settings[/]")).Expand().PadLeft(2));
            }else {
                menu.AddRow(new Panel(new Markup("[bold orange3]2. [/][yellow]Settings[/]")).Expand());
            }
            if (selected == 3) {
                menu.AddRow(new Panel(new Markup("[bold aqua]3. Quit[/]")).Expand().PadLeft(2));
            }else {
                menu.AddRow(new Panel(new Markup("[bold orange3]3. [/][yellow]Quit[/]")).Expand());
            }

            AnsiConsole.Write(menu);
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.GetCursorPosition().Top + 4);

            while (!menuChosen) {
                var key = Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.UpArrow:
                        if (selected == 1)
                            selected = 3; // If it would go past 1, reset it to 3
                        else
                            selected--;
                        DisplayMenu(selected);
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected == 3)
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
                                Settings();
                                break;
                            case 3:
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
            DisplayMinigameMenu();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Quit();
        }

        private void Settings()
        {
            // Demo Settings logic with a few messages and then it quits
            Console.Clear();
            Console.WriteLine("Settings:");
            Console.WriteLine("1. Difficulty Level");
            Console.WriteLine("2. Sound On/Off");
            Thread.Sleep(3000);

            Console.Clear();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Quit();
        }
    
        private void Quit() {
            Console.Clear();
            Console.WriteLine("Thanks for playing!");
            Thread.Sleep(1000); // pause for 2 seconds before closing the console
            Environment.Exit(1); // close the game
        }

        private void DisplayMinigameMenu(int selected = 1)
        {
            Console.CursorVisible = false; // Hide the cursor
            Console.SetCursorPosition(0, 0);
            AnsiConsole.Write(new Padder(new FigletText("Select Minigame").Centered().Color(Color.DarkOrange3)).PadTop(7));

            #region MinigameMenu
            var menu = new Table();
            menu.Centered();
            menu.HideHeaders();
            menu.Border(TableBorder.None);

            menu.AddColumn(new TableColumn("Minigames").LeftAligned());
            for (int i = 0; i < minigames.Count; i++)
            {
                var minigameName = minigames[i].GetType().Name;
                if (selected == i + 1)
                {
                    menu.AddRow(new Panel(new Markup($"[bold aqua]{i + 1}. {minigameName}[/]")).Expand().PadLeft(2).PadRight(1));
                }
                else
                {
                    menu.AddRow(new Panel(new Markup($"[bold orange3]{i + 1}. [/][yellow]{minigameName}[/]")).Expand().PadRight(2));
                }
            }

            // Add the "Back" button
            if (selected == minigames.Count + 1)
            {
                menu.AddRow(new Panel(new Markup($"[bold aqua]{minigames.Count + 1}. Back[/]")).Expand().PadLeft(2).PadRight(1));
            }
            else
            {
                menu.AddRow(new Panel(new Markup($"[bold orange3]{minigames.Count + 1}. [/][yellow]Back[/]")).Expand().PadRight(2));
            }

            AnsiConsole.Write(menu);
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.GetCursorPosition().Top + 4);

            var menuChosen = false;
            while (!menuChosen)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected == 1)
                            selected = minigames.Count + 1; // If it would go past 1, reset it to the last option
                        else
                            selected--;
                        DisplayMinigameMenu(selected);
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected == minigames.Count + 1)
                            selected = 1; // If it would go past the last option, reset it to 1
                        else
                            selected++;
                        DisplayMinigameMenu(selected);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        if (selected == minigames.Count + 1)
                        {
                            Run(); // Go back to the main menu
                        }
                        else
                        {
                            minigames[selected - 1].Run();
                            Thread.Sleep(1000);
                            DisplayMinigameMenu(selected);
                        }
                        menuChosen = true;
                        break;
                }
            }
            #endregion
        }
    }
}
