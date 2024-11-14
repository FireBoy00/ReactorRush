using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using Spectre.Console;

namespace ReactorRushGame
{
    public class Game
    {
        public bool menuChosen = false;
        public void Run()
        {
            // ShowLogo();
            // Console.WriteLine();
            // Thread.Sleep(2000);
            DisplayMenu();
        }

        private void ShowLogo()
        {
            AnsiConsole.Write(
                new FigletText("Reactor Rush")
                    .Centered()
                    .Color(Color.Red));
        }

        private void DisplayMenu(int selected = 1)
        {
            Console.Clear();
            // AnsiConsole.Write(new Padder(new FigletText("Main Menu").Centered().Color(Color.Aqua)).PadTop(7));
            AnsiConsole.Write(new Padder(new FigletText("Reactor Rush").Centered().Color(Color.DarkOrange3)).PadTop(7));

            #region  V1
            // var btn1 = new Text("1. Start Game");
            // var btn2 = new Text("2. Settings");
            // var btn3 = new Text("3. Quit");
            // var pad = Console.WindowWidth / 2 - 5;
            // AnsiConsole.Write(new Padder(btn1).PadLeft(pad));
            // AnsiConsole.Write(new Padder(btn2).PadLeft(pad));
            // AnsiConsole.Write(new Padder(btn3).PadLeft(pad));
            #endregion

            #region V2
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
            // Demo StartGame logic with a few messages and then it quits
            Console.Clear();
            Console.WriteLine("Starting the game...");
            Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine("Welcome to Reactor Rush!");
            Thread.Sleep(3000);
            Console.Clear();
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
            Thread.Sleep(2000); // pause for 2 seconds before closing the console
            Environment.Exit(1); // close the game
        }
    }
}
