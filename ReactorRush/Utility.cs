using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Spectre.Console;

namespace ReactorRush
{
    public static class Utility
    {
        public static string Narrator = "Narrator v1.0";
        public static void PrintStory(string text, string? author = null, bool prompt = false, int typingSpeed = 25)
        {
            author ??= Narrator;
            Console.Clear();
            var panel = new Panel(string.Empty)
            {
                Header = new PanelHeader($"<[bold yellow] {author} [/]>"),
                Border = BoxBorder.Rounded,
                Padding = new Padding(2, 1)
            };

            // Determine the width and height based on the text
            const int maxLineLength = 100;
            var formattedText = new StringBuilder();
            var words = text.Split(' ');
            var currentLine = new StringBuilder();

            foreach (var word in words)
            {
                if (currentLine.Length + word.Length + 1 > maxLineLength)
                {
                    formattedText.AppendLine(currentLine.ToString());
                    currentLine.Clear();
                }
                currentLine.Append(word + " ");
            }
            formattedText.AppendLine(currentLine.ToString().TrimEnd());

            var lines = formattedText.ToString().Split('\n');
            int maxWidth = lines.Max(line => line.Length) + 4; // Adding padding
            int height = lines.Length + 3; // Adding padding and header space

            panel.Width = maxWidth;
            panel.Height = height;

            var topPadding = (Console.WindowHeight - (panel.Height ?? 0)) / 2;
            topPadding = !prompt ? topPadding : Console.WindowHeight * 20 / 100;
            Console.SetCursorPosition(0, topPadding);
            var centeredPanel = new Padder(panel)
                .PadLeft((Console.WindowWidth - (panel.Width ?? 0)) / 2);

            AnsiConsole.Write(centeredPanel);

            // Typing effect
            var currentText = string.Empty;
            var instructionText = "Press SPACE to skip the animation...";
            var instruction = new Panel("[grey]Press [yellow]SPACE[/] to skip the animation...[/]")
            {
                Border = BoxBorder.Rounded,
                Padding = new Padding(1, 0)
            };
            var instructionWidth = instructionText.Length + 4; // Adding padding and border
            var centeredInstructionFinal = new Padder(instruction)
                .PadLeft((Console.WindowWidth - instructionWidth) / 2)
                .PadBottom(0);

            foreach (char c in formattedText.ToString())
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                {
                    // Skip the typing animation and print the remaining message
                    currentText = formattedText.ToString();
                    break;
                }

                currentText += c;
                panel = new Panel(currentText)
                {
                    Header = panel.Header,
                    Border = panel.Border,
                    Padding = panel.Padding,
                    Width = panel.Width,
                    Height = panel.Height
                };

                Console.SetCursorPosition(0, topPadding);
                centeredPanel = new Padder(panel)
                    .PadLeft((Console.WindowWidth - (panel.Width ?? 0)) / 2);
                AnsiConsole.Write(centeredPanel);

                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                AnsiConsole.Write(centeredInstructionFinal);

                Task.Delay(typingSpeed).Wait();
            }

            // Ensure the final text is displayed
            panel = new Panel(currentText)
            {
                Header = panel.Header,
                Border = panel.Border,
                Padding = panel.Padding,
                Width = panel.Width,
                Height = panel.Height
            };

            Console.Clear();
            Console.SetCursorPosition(0, topPadding);
            centeredPanel = new Padder(panel)
                .PadLeft((Console.WindowWidth - (panel.Width ?? 0)) / 2);

            AnsiConsole.Write(centeredPanel);

            if (prompt) return;
            // Instruct the user to press ENTER to continue
            instructionText = "Press ENTER to continue...";
            instruction = new Panel("[grey]Press [yellow]ENTER[/] to continue...[/]")
            {
                Border = BoxBorder.Rounded,
                Padding = new Padding(1, 0)
            };
            instructionWidth = instructionText.Length + 4; // Adding padding and border
            var centeredInstruction = new Padder(instruction)
                .PadLeft((Console.WindowWidth - instructionWidth) / 2)
                .PadBottom(0);

            Console.SetCursorPosition(0, Console.WindowHeight - 5);
            AnsiConsole.Write(centeredInstruction);

            var enterPressed = false;
            while (!enterPressed)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        enterPressed = true;
                    }
                }
            }
        }

        public static void PrintOptions(int x, string[] options, int selected = 1) {
            Console.SetCursorPosition(0, x);
            var table = new Table
            {
                Border = TableBorder.None,
                ShowHeaders = false
            }.Centered();

            table.AddColumn(new TableColumn("").Centered());

            bool expandAll = options.Any(opt => opt.Length > 100);

            for (int i = 0; i < options.Length; i++) {
                var opt = new Panel(new Markup($"[yellow]{options[i]}[/]")).Border(BoxBorder.Square);
                
                if (i == selected - 1) {
                    opt = new Panel(new Markup($"[bold aqua]{options[i]}[/]")).Border(BoxBorder.Double);
                }
                
                if (expandAll) {
                    table.Expand();
                    table.Centered();
                    opt = opt.Expand();
                }
                
                table.AddRow(opt);
            }

            var minimTableWidth = Console.WindowWidth * 75 / 100;
            table.Width = table.Width > minimTableWidth ? table.Width : minimTableWidth;

            // var tableHeight = table.Rows.Count;
            // var topPadding = Console.WindowHeight * 20 / 100;
            // Console.SetCursorPosition(0, topPadding);

            AnsiConsole.Write(table);
        }

        public static string Prompt (string question, string[] options, string? author = null) {
            author ??= Narrator;

            Console.Clear();
            PrintStory(question, author, true);
            int x = Console.GetCursorPosition().Top;
            PrintOptions(x, options);

            var selectionMade = false;
            var selectedOption = 1;
            while (!selectionMade) {
                if (Console.KeyAvailable) {
                    var key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (selectedOption == options.Length) {
                                selectedOption = 1;
                            }else {
                                selectedOption++;
                            }
                            PrintOptions(x, options, selectedOption);
                            break;
                        case ConsoleKey.UpArrow:
                            if (selectedOption == 1) {
                                selectedOption = options.Length;
                            }else {
                                selectedOption--;
                            }
                            PrintOptions(x, options, selectedOption);
                            break;
                        case ConsoleKey.Enter:
                            selectionMade = true;
                            return options[selectedOption-1];
                        default:
                            break;
                    }
                }
            }
            return options[selectedOption-1];
        }

    }
}