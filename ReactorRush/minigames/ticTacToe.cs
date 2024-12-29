using System;
using Spectre.Console;

namespace Minigames
{
    public class TicTacToe : IMinigame
    {
        public int SizeOfBoard { get; private set; }
        public int Score { get; private set; }
        private int[,] board;
        private int rollX;
        private int rollY;
        private int[] placeOfCursor = new int[2];
        Random dice = new();
        int ratEnergy;
        public TicTacToe(int sizeOfBoard = 3, int givenRatEnergy = 1)
        {
            SizeOfBoard = sizeOfBoard;
            board = new int[SizeOfBoard, SizeOfBoard];
            ratEnergy = givenRatEnergy;
            //Run();
        }
        /*public GameTicTacToe(int sizeOfBoard = 10, int winningAmountgiven = 5)
        {
            SizeOfBoard = sizeOfBoard;
            board = new int[SizeOfBoard, SizeOfBoard];
            winningAmount = winningAmountgiven;
            NewGameSlider();
        }*/
        public void Run()
        {
            Console.CursorVisible = false;
            Array.Clear(board);
            Score = 0;
            placeOfCursor[0] = 0;
            placeOfCursor[1] = 0;
            PrintPlotline();
            PrintBoard();
            //Console.ReadKey();
            board[0, 0] = 3;
            while (!IsEnd() && !IsLost())
            {
                int ratDamage = dice.Next(1, ratEnergy);
                if (NextRound()&&!IsEnd())
                {
                    for (int i = 0; i < ratDamage; i++)
                    {
                        NextRoll();
                    }
                }
            }
            PrintBoard();
            if (!IsLost())
            {
                Console.WriteLine("Congratulations! You fixed the wires.");
            }
            else
            {
                Console.WriteLine("Unfortunately, the rats were stronger, try again next time.");
            }
            Console.ReadKey();
            //Console.WriteLine("Number of moves made: "+Score);
        }
        #region Layout

        private void PrintPlotline()
        {
            Console.WriteLine("Modified Tic Tac Toe when the board size is the same as the size to win:");
            Console.WriteLine("Connect any two corners of the board or or edge to parallel edge, \"O\" are your wires X are the damage caused by the rats. Once the current starts flowing in the wires (when the two corners of the board are connected), the rats will not be able to destroy your installation. \nGood luck.");
            Console.WriteLine("\nClick any key to continiue.");
            Console.ReadKey();
            Console.Clear();
        }
        private void PrintBoard()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Number of energy used: " + Score);
            Console.WriteLine();
            AnsiConsole.Markup($"[maroon on Yellow3_1]  [/]");
            for (int x = 0; x < SizeOfBoard; x++)
            {
                AnsiConsole.Markup($"[maroon on Yellow3_1]_____ [/]");
            }
            AnsiConsole.Markup($"[maroon on Yellow3_1] [/]");
            Console.WriteLine();
            for (int y = 0; y < SizeOfBoard; y++)
            {
                AnsiConsole.Markup($"[maroon on Yellow3_1] |[/]");
                for (int x = 0; x < SizeOfBoard; x++)
                {
                    switch (board[y, x])
                    {
                        case 1:
                            AnsiConsole.Markup($"[maroon on Yellow3_1]__[/]");
                            AnsiConsole.Markup($"[green3 on Yellow3_1]O[/]");
                            AnsiConsole.Markup($"[maroon on Yellow3_1]__|[/]");
                            break;
                        case 2:
                            AnsiConsole.Markup($"[maroon on Yellow3_1]__[/]");
                            AnsiConsole.Markup($"[red3_1 on Yellow3_1]X[/]");
                            AnsiConsole.Markup($"[maroon on Yellow3_1]__|[/]");
                            break;
                        case 3:
                            AnsiConsole.Markup($"[maroon on darkslategray3]_____[/]");
                            AnsiConsole.Markup($"[maroon on Yellow3_1]|[/]");
                            break;
                        case 4:
                            AnsiConsole.Markup($"[maroon on darkslategray3]__[/]");
                            AnsiConsole.Markup($"[purple_2 on darkslategray3]O[/]");
                            AnsiConsole.Markup($"[maroon on darkslategray3]__[/]");
                            AnsiConsole.Markup($"[maroon on Yellow3_1]|[/]");
                            break;
                        case 5:
                            AnsiConsole.Markup($"[maroon on darkslategray3]__[/]");
                            AnsiConsole.Markup($"[purple_2 on darkslategray3]X[/]");
                            AnsiConsole.Markup($"[maroon on darkslategray3]__[/]");
                            AnsiConsole.Markup($"[maroon on Yellow3_1]|[/]");
                            break;
                        default:
                            AnsiConsole.Markup($"[maroon on Yellow3_1]_____|[/]");
                            break;
                    }
                    //Console.Write(board[y, x] + "\t");
                }
                AnsiConsole.Markup($"[maroon on Yellow3_1] [/]");
                Console.WriteLine("");
            }
            AnsiConsole.Markup($"[maroon on Yellow3_1]   [/]");
            for (int x = 0; x < SizeOfBoard; x++)
            {
                AnsiConsole.Markup($"[maroon on Yellow3_1]      [/]");
            }
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
        }
        #endregion
        #region Control
        private void NextRoll()
        {
            do
            {
                rollX = dice.Next(0, SizeOfBoard);
                rollY = dice.Next(0, SizeOfBoard);
                board[rollY, rollX] = 2;
            } while (board[rollY, rollX]%3 != 2);
            if (rollX == placeOfCursor[1] && rollY == placeOfCursor[0])
            {
                board[rollY, rollX] += 3;
            }
        }

        private bool IsEnd()
        {
            bool notFoundX = true;
            for (int y = 0; y < SizeOfBoard; y++)
            {
                notFoundX = true;
                for (int x = 0; x < SizeOfBoard; x++)
                {
                    if (board[y, x] % 3 != 1)
                    {
                        notFoundX = false;
                        break;
                    }
                }
                if (notFoundX)
                {
                    return true;
                }
            }
            for (int x = 0; x < SizeOfBoard; x++)
            {
                notFoundX = true;
                for (int y = 0; y < SizeOfBoard; y++)
                {
                    if (board[y, x] % 3 != 1)
                    {
                        notFoundX = false;
                        break;
                    }
                }
                if (notFoundX)
                {
                    return true;
                }
            }
            notFoundX = true;
            for (int i = 0; i < SizeOfBoard; i++)
            {
                if (board[i, i] % 3 != 1)
                {
                    notFoundX = false;
                    break;
                }
            }
            if (notFoundX)
            {
                return true;
            }
            notFoundX = true;
            for (int i = 0; i < SizeOfBoard; i++)
            {
                if (board[SizeOfBoard - i - 1, i] % 3 != 1)
                {
                    notFoundX = false;
                    break;
                }
            }
            if (notFoundX)
            {
                return true;
            }
            return false;
        }
        private bool IsLost()
        {
            for (int y = 0; y < SizeOfBoard; y++)
            {
                for (int x = 0; x < SizeOfBoard; x++)
                {
                    if (board[y, x] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region Move
        private bool NextRound()
        {
            PrintBoard();
            var key = Console.ReadKey();
            bool read = false;
            bool next = false;
            while (!read && !next)
            {
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        read = true;
                        if (placeOfCursor[1] != SizeOfBoard - 1)
                        {
                            board[placeOfCursor[1], placeOfCursor[0]] -= 3;
                            board[placeOfCursor[1] + 1, placeOfCursor[0]] += 3;
                            placeOfCursor[1]++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        read = true;
                        if (placeOfCursor[1] != 0)
                        {
                            board[placeOfCursor[1], placeOfCursor[0]] -= 3;
                            board[placeOfCursor[1] - 1, placeOfCursor[0]] += 3;
                            placeOfCursor[1]--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        read = true;
                        if (placeOfCursor[0] != SizeOfBoard - 1)
                        {
                            board[placeOfCursor[1], placeOfCursor[0]] -= 3;
                            board[placeOfCursor[1], placeOfCursor[0] + 1] += 3;
                            placeOfCursor[0]++;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        read = true;
                        if (placeOfCursor[0] != 0)
                        {
                            board[placeOfCursor[1], placeOfCursor[0]] -= 3;
                            board[placeOfCursor[1], placeOfCursor[0] - 1] += 3;
                            placeOfCursor[0]--;
                        }
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        read = true;
                        next = true;
                        if (board[placeOfCursor[1], placeOfCursor[0]] % 3 == 2)
                        {
                            board[placeOfCursor[1], placeOfCursor[0]] = 0;
                        }
                        else
                        {
                            board[placeOfCursor[1], placeOfCursor[0]] = 1;
                        }
                        board[placeOfCursor[1], placeOfCursor[0]] += 3;
                        Score++;
                        break;
                    default:
                        key = Console.ReadKey();
                        break;
                }
            }
            return next;
        }
        #endregion
    }
}