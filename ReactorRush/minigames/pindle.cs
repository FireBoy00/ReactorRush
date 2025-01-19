using System;
using Spectre.Console;

namespace Minigames
{
    //? Replace the colors with brighter ones
    //? Replace the "You Win!" message with a more generic one, for tasks

    public class Pindle : IMinigame
    {
        public int Score { get; private set; }
        private const int NumberLength = 5;
        private const int MaxAttempts = 6;
        private readonly Random _random = new Random();
        private string _targetNumber = string.Empty;
        private int _attempts;
        private bool _gameOver = false;
        private bool _gameFailed = false;
        private string _currentGuess = string.Empty;
        private string[] _guesses = new string[MaxAttempts];

        public void Run()
        {
            Console.Title = "Pindle Minigame";
            Console.CursorVisible = false;
            _targetNumber = GenerateTargetNumber();
            _attempts = 0;
            _gameOver = false;
            _gameFailed = false;
            _currentGuess = string.Empty;
            _guesses = new string[MaxAttempts];
            Score = 0;

            PrintPlotline();

            Console.Clear();
            while (_attempts <= MaxAttempts)
            {
                Console.SetCursorPosition(0, 0);
                DisplayAttempts();
                Console.SetCursorPosition(0, Console.WindowHeight / 2 - MaxAttempts / 2);
                DisplayGuesses();

                if (_gameOver)
                {
                    Score = MaxAttempts - _attempts + 1;
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("Congratulations! You guessed the number!\n\n Your score is: " + Score);
                    break;
                }

                if (Console.KeyAvailable && !_gameFailed)
                {
                    var key = Console.ReadKey(true).Key;
                    HandleKeyPress(key);
                }
            }
        }

        private void PrintPlotline()
        {
            Console.WriteLine("Your task is to figure out what the code is.\n");
            Console.WriteLine("After you typed in a 5-digits number, you will see if the digits are included in the code or not.");
            Console.WriteLine("GREEN: the digit is in the correct place; \nYELLOW: the digit is included in the code but it's in the incorrect place; \nRED: the code does not include the digit");
            Console.WriteLine("\nClick any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        private string GenerateTargetNumber()
        {
            var digits = new List<char>();
            while (digits.Count < NumberLength)
            {
                char digit = (char)('0' + _random.Next(10));
                if (!digits.Contains(digit))
                {
                    digits.Add(digit);
                }
            }
            return new string(digits.ToArray());
        }

        private void DisplayAttempts()
        {
            if (_gameOver) {
                AnsiConsole.Write(new Padder(new FigletText("You Win!").Centered().Color(Color.Green)).PadTop(1));
                return;
            }
            AnsiConsole.Write(new Padder(new FigletText($"Attempts: {_attempts}/{MaxAttempts}").Centered().Color(Color.DarkOrange3)).PadTop(1));
        }

        private void DisplayGuesses()
        {
            var table = new Table();
            table.HideHeaders();
            table.Collapse();
            table.Centered();
            table.Border(TableBorder.Rounded);

            for (int i = 0; i < NumberLength; i++)
            {
                table.AddColumn(new TableColumn(""));
            }

            for (int i = 0; i < MaxAttempts; i++)
            {
                var row = new List<Markup>();
                for (int j = 0; j < NumberLength; j++)
                {
                    string cellContent = " ";
                    Style cellStyle = new Style();

                    if (i < _attempts)
                    {
                        cellContent = $"[bold]{_guesses[i][j]}[/]";
                        if (_guesses[i][j] == _targetNumber[j])
                        {
                            cellStyle = new Style().Foreground(Color.DarkGreen);
                        }
                        else if (_targetNumber.Contains(_guesses[i][j]))
                        {
                            cellStyle = new Style().Foreground(Color.DarkGoldenrod);
                        }
                        else
                        {
                            cellStyle = new Style().Foreground(Color.DarkRed);
                        }
                    }
                    else if (i == _attempts && j < _currentGuess.Length)
                    {
                        cellContent = $"[bold]{_currentGuess[j]}[/]";
                    }

                    row.Add(new Markup(cellContent, cellStyle));
                }
                table.AddRow(row.ToArray());
            }

            AnsiConsole.Write(table);
        }

        private void HandleKeyPress(ConsoleKey key)
        {
            if (key >= ConsoleKey.D0 && key <= ConsoleKey.D9 && _currentGuess.Length < NumberLength)
            {
                _currentGuess += (char)('0' + (key - ConsoleKey.D0));
            }
            else if (key == ConsoleKey.Backspace && _currentGuess.Length > 0)
            {
                _currentGuess = _currentGuess.Substring(0, _currentGuess.Length - 1);
            }
            else if (key == ConsoleKey.Enter && _currentGuess.Length == NumberLength)
            {
                // Console.WriteLine($"{_currentGuess} | {_targetNumber}"); //! Debug
                _guesses[_attempts] = _currentGuess;
                _attempts++;
                if (_currentGuess == _targetNumber)
                {
                    _currentGuess = string.Empty;
                    _gameOver = true;
                    return;
                }
                else
                {
                    _currentGuess = string.Empty;
                }

                if (_attempts == MaxAttempts)
                {
                    _attempts++;
                    Score = 0;
                    Console.Clear();
                    Console.WriteLine($"Game Over! The correct number was {_targetNumber}.\n\n Your score is: " + Score);
                }
            }
        }
    }
}