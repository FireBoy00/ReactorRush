﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Spectre.Console;

namespace ReactorRush
{
    class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Write(new Markup("\n[bold yellow]Note:[/] [red]Please maximize the window before running the game.[/]").Centered());
            AnsiConsole.Write(new Markup("\nThis will ensure that you have a full-screen experience.").Centered());
            Console.WriteLine();
            AnsiConsole.Write(new Markup("\nIf you're ready, press any key...").Centered());
            Console.WriteLine();
            Console.ReadKey();
            Console.Clear();

            Game game = new Game();
            game.Run();
        }
    }
}
