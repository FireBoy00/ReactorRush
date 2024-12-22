using System;
using System.Text;
using Spectre.Console;

namespace ReactorRush
{
    public static class Utility
    {
        public static void PrintStory(string author, string text, int typingSpeed = 25)
        {
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

            var centeredPanel = new Padder(panel)
                .PadLeft((Console.WindowWidth - (panel.Width ?? 0)) / 2)
                .PadTop((Console.WindowHeight - (panel.Height ?? 0)) / 2);

            AnsiConsole.Write(centeredPanel);

            // Typing effect
            var currentText = string.Empty;
            var instructionText = "Press SPACE to skip the story...";
            var instruction = new Panel("[grey]Press [yellow]SPACE[/] to skip the story...[/]")
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

                centeredPanel = new Padder(panel)
                    .PadLeft((Console.WindowWidth - (panel.Width ?? 0)) / 2)
                    .PadTop((Console.WindowHeight - (panel.Height ?? 0)) / 2);

                Console.SetCursorPosition(0, 0);
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

            centeredPanel = new Padder(panel)
                .PadLeft((Console.WindowWidth - (panel.Width ?? 0)) / 2)
                .PadTop((Console.WindowHeight - (panel.Height ?? 0)) / 2);

            Console.Clear();
            AnsiConsole.Write(centeredPanel);

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
    }
}