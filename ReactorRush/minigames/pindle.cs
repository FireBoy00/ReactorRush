using System;
using Spectre.Console;

namespace Minigames
{
    //? Replace the colors with brighter ones
    //? Replace the "You Win!" message with a more generic one, for tasks

    public class Pindle : IMinigame
    {
        private const int NumberLength = 5;
        private const int MaxAttempts = 6;
        private readonly Random _random = new Random();
        private string _targetNumber = string.Empty;
        private int _attempts;
        private bool _gameOver = false;
        private string _currentGuess = string.Empty;
        private string[] _guesses = new string[MaxAttempts];

        public void Run()
        {
            Console.CursorVisible = false;
            _targetNumber = GenerateTargetNumber();
            _attempts = 0;
            _gameOver = false;
            _currentGuess = string.Empty;
            _guesses = new string[MaxAttempts];

            Console.Clear();
            while (_attempts <= MaxAttempts)
            {
                Console.SetCursorPosition(0, 0);
                DisplayAttempts();
                Console.SetCursorPosition(0, Console.WindowHeight / 2 - MaxAttempts / 2);
                DisplayGuesses();

                if (_gameOver)
                {
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("Congratulations! You guessed the number!");
                    break;
                }

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    HandleKeyPress(key);
                }
            }
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
                AnsiConsole.Write($"Your answer: {_currentGuess} | {_targetNumber}");
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
                    Console.Clear();
                    Console.WriteLine($"Game Over! The correct number was {_targetNumber}.");
                }
            }
        }
    }
}