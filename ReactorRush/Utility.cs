using System;
using System.Text;
using Spectre.Console;

namespace ReactorRush
{
    public static class Utility
    {
        public static string Narrator = "Narrator v1.0";
        public static void PrintStory(string text, string? author = null, bool prompt = false, bool newspaper = false, int typingSpeed = 25)
        {
            author ??= Narrator;
            Console.Clear();
            var panel = new Panel(string.Empty)
            {
                Header = new PanelHeader($"<[bold yellow] {author} [/]>" ),
                Border = BoxBorder.Rounded,
                Padding = new Padding(2, 1)
            };

            // Determine the width and height based on the text
            const int maxLineLength = 100;
            var formattedText = new StringBuilder();
            var paragraphs = text.Split('\n');
            
            foreach (var paragraph in paragraphs)
            {
                var words = paragraph.Split(' ');
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
            }

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

            ConsoleKey continueKey = ConsoleKey.Enter;
            string[] continueKeyText = ["ENTER", "continue"];
            if (newspaper) {
                continueKey = ConsoleKey.Backspace;
                continueKeyText = ["BACKSPACE", "exit the article"];
            }
            // Instruct the user to press ENTER to continue
            instructionText = $"Press {continueKeyText[0]} to {continueKeyText[1]}...";
            instruction = new Panel($"[grey]Press [yellow]{continueKeyText[0]}[/] to {continueKeyText[1]}...[/]")
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

            var continueKeyPressed = false;
            while (!continueKeyPressed)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == continueKey)
                    {
                        continueKeyPressed = true;
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

        public static void PrintNewspaper(string headline, string[] articles, string author = "Newspaper")
        {
            bool exitNewspaper = false;
            while (!exitNewspaper)
            {
                Console.Clear();
                PrintStory(headline, author, true);
                int x = Console.GetCursorPosition().Top;
                PrintOptions(x, articles);

                // Inform the user they can use "SPACE" to exit the newspaper
                string exitInstructionText = "Press BACKSPACE to exit the newspaper...";
                var exitInstruction = new Panel("[grey]Press [yellow]BACKSPACE[/] to exit the newspaper...[/]")
                {
                    Border = BoxBorder.Rounded,
                    Padding = new Padding(1, 0)
                };
                var exitInstructionWidth = exitInstructionText.Length + 4;
                var centeredExitInstruction = new Padder(exitInstruction)
                    .PadLeft((Console.WindowWidth - exitInstructionWidth) / 2)
                    .PadBottom(0);

                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                AnsiConsole.Write(centeredExitInstruction);

                var selectionMade = false;
                var selectedArticle = 1;
                while (!selectionMade)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true);
                        switch (key.Key)
                        {
                            case ConsoleKey.DownArrow:
                                if (selectedArticle == articles.Length)
                                {
                                    selectedArticle = 1;
                                }
                                else
                                {
                                    selectedArticle++;
                                }
                                PrintOptions(x, articles, selectedArticle);
                                break;
                            case ConsoleKey.UpArrow:
                                if (selectedArticle == 1)
                                {
                                    selectedArticle = articles.Length;
                                }
                                else
                                {
                                    selectedArticle--;
                                }
                                PrintOptions(x, articles, selectedArticle);
                                break;
                            case ConsoleKey.Enter:
                                selectionMade = true;
                                PrintStory(articles[selectedArticle - 1], author, false, true);
                                break;
                            case ConsoleKey.Backspace:
                                exitNewspaper = true;
                                selectionMade = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        public static string PromptTextInput(string question, string? author = null)
        {
            author ??= Narrator;

            Console.Clear();
            PrintStory(question, author, true);

            var input = new StringBuilder();

            var topPadding = Console.GetCursorPosition().Top + 2;
            Console.SetCursorPosition(0, topPadding);

            while (true)
            {
                Panel? inputPanel = new Panel(input.ToString())
                {
                    Border = BoxBorder.Rounded,
                    Padding = new Padding(1, 0),
                    Width = Math.Max(input.Length + 4, 20), // Set minimum width
                    Height = Math.Max(3, 3) // Set minimum height (1 line of text + padding)
                };

                var centeredInputPanel = new Padder(inputPanel)
                    .PadLeft((Console.WindowWidth - (inputPanel.Width ?? 0)) / 2);

                // Move cursor to display the panel
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, topPadding);
                AnsiConsole.Write(centeredInputPanel);

                // Set cursor position inside the input panel
                int cursorLeft = (Console.WindowWidth - (inputPanel.Width ?? 0)) / 2 + 2 + input.Length;
                int cursorTop = topPadding + 2;
                Console.CursorVisible = true;
                Console.SetCursorPosition(cursorLeft, cursorTop);

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.CursorVisible = false;
                    // Move cursor normally after the input panel
                    Console.SetCursorPosition(0, topPadding + inputPanel.Height + 2 ?? 0);
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    input.Append(key.KeyChar);
                }
            }

            return input.ToString();
        }
    }
}