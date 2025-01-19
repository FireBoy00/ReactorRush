using System;
using Spectre.Console;

namespace Minigames
{
    public class Slider : IMinigame
    {
        public int SizeOfBoard { get; private set; }
        public int Score { get; private set; }
        public string Name { get; private set; } = string.Empty;
        private int[,] board;
        private int rollX;
        private int rollY;
        private int[] placeOfZero = new int[2];
        Random dice = new();
        public Slider(int sizeOfBoard = 4)
        {
            SizeOfBoard = sizeOfBoard;
            board = new int[SizeOfBoard, SizeOfBoard];
            Score = 0;
            Name = "Slider Minigame";
            //Run();
        }
        public void Run()
        {
            Name = "Slider Minigame";
            Console.CursorVisible = false;
            placeOfZero[0] = SizeOfBoard - 1;
            placeOfZero[1] = SizeOfBoard - 1;
            do
            {
                Array.Clear(board);
                //seting new values for board
                board[SizeOfBoard - 1, SizeOfBoard - 1] = SizeOfBoard * SizeOfBoard;
                for (int i = 1; i < SizeOfBoard * SizeOfBoard; i++)
                {
                    NextRoll(i);
                }
                //IfSolvable();
                board[SizeOfBoard - 1, SizeOfBoard - 1] = 0;
            } while (!IfSolvable() || IsEnd()); //if it is not possible to solve, generate new game OR when it is already solved
            Score = 0;
            //FindZero(); //It is good but normaly that game start from right bottom edge
            board[SizeOfBoard - 1, SizeOfBoard - 1] = 0;
            PrintPlotline();
            PrintBoard();

            //Console.ReadKey();
            while (!IsEnd())
            {
                NextRound();
            }
            PrintBoard();
            Console.WriteLine("YOU WON THIS MINIGAME!");
            Console.WriteLine("\nTo continue, click any key...");
            Console.ReadKey();
            //Console.WriteLine("Number of moves made: "+Score);
        }
        private void PrintPlotline()
        {
            Console.WriteLine("Your task is to put the numbers on the board in order.\n");
            Console.WriteLine("To do this, use the arrows (you can only move the numbers next to the empty field).");
            Console.WriteLine("\nClick any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }
        #region Control
        private void NextRoll(int forNumber)
        {
            do
            {
                rollX = dice.Next(0, SizeOfBoard);
                rollY = dice.Next(0, SizeOfBoard);
            } while (board[rollY, rollX] != 0);
            board[rollY, rollX] = forNumber;
        }
        private void PrintBoard()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Number of moves made: " + Score);
            Console.WriteLine();
            for (int y = 0; y < SizeOfBoard; y++)
            {
                for (int x = 0; x < SizeOfBoard; x++)
                {
                    switch (board[y, x])
                    {
                        case 0:
                            AnsiConsole.Markup($"[maroon on Yellow3_1]        [/]");
                            break;
                        case < 10:
                            AnsiConsole.Markup($"[maroon on Yellow2]    {board[y, x]}   [/]");
                            break;
                        case < 100:
                            AnsiConsole.Markup($"[maroon on Yellow2]   {board[y, x]}   [/]");
                            break;
                        case < 1000:
                            AnsiConsole.Markup($"[maroon on Yellow3_1]   {board[y, x]}  [/]");
                            break;
                        case < 10000:
                            AnsiConsole.Markup($"[maroon on Yellow3_1]  {board[y, x]}  [/]");
                            break;
                        case < 100000:
                            AnsiConsole.Markup($"[maroon on Yellow3_1]  {board[y, x]} [/]");
                            break;
                        default:
                            AnsiConsole.Markup($"[maroon on Yellow3_1] {board[y, x]} [/]");
                            break;
                    }
                    //Console.Write(board[y, x] + "\t");
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        private bool FindZero()
        {
            //probably it is not nessesary because it is good but normaly that game start from right bottom edge
            for (int y = 0; y < SizeOfBoard; y++)
            {
                for (int x = 0; x < SizeOfBoard; x++)
                {
                    if (board[y, x] == 0)
                    {
                        placeOfZero[0] = x;
                        placeOfZero[1] = y;
                        return true;
                    }
                }
            }
            return false;
        }
        private bool IsEnd()
        {
            if (placeOfZero[0] != SizeOfBoard - 1 || placeOfZero[1] != SizeOfBoard - 1)
            {
                return false;
            }
            for (int y = 0; y < SizeOfBoard; y++)
            {
                for (int x = 0; x < SizeOfBoard; x++)
                {
                    if (y == SizeOfBoard - 1 && x >= SizeOfBoard - 2)
                    {
                        break;
                    }
                    else
                    {
                        // Now we check if next element on board is higher on 1, if no that mean that we still must play
                        if (x != SizeOfBoard - 1)
                        {
                            if (board[y, x] + 1 != board[y, x + 1])
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (board[y, x] + 1 != board[y + 1, 0])
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        private bool IfSolvable() //this function was written by GPT chat
        {
            //this method was describing in this article (in Polish): https://www.beta-iks.pl/index.php/2022/10/13/pietnastka/
            // Flatten the board into a one-dimensional array for easier processing
            List<int> flatBoard = new List<int>();

            for (int i = 0; i < SizeOfBoard; i++)
            {
                for (int j = 0; j < SizeOfBoard; j++)
                {
                    flatBoard.Add(board[i, j]);
                }
            }

            // Count the number of inversions in the flat board
            int inversions = 0;
            for (int i = 0; i < flatBoard.Count; i++)
            {
                for (int j = i + 1; j < flatBoard.Count; j++)
                {
                    if (flatBoard[i] > flatBoard[j] && flatBoard[j] != 0) // Ignore the empty space
                    {
                        inversions++;
                    }
                }
            }

            // Determine solvability based on board size and inversions
            /*if (SizeOfBoard % 2 == 1)
            {
                // Odd-sized board: solvable if inversions are even
                return inversions % 2 == 0;
            }
            else
            {*/
            // Even-sized board: solvable if inversions are even (empty space fixed at bottom-right)
            return inversions % 2 == 0;
            //}
        }
        #endregion

        #region Move
        private void NextRound()
        {
            PrintBoard();
            var key = Console.ReadKey();
            bool read = false;

            while (!read)
            {
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        read = true;
                        if (placeOfZero[1] != SizeOfBoard - 1)
                        {
                            board[placeOfZero[1], placeOfZero[0]] = board[placeOfZero[1] + 1, placeOfZero[0]];
                            board[placeOfZero[1] + 1, placeOfZero[0]] = 0;
                            placeOfZero[1]++;
                            Score++;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        read = true;
                        if (placeOfZero[1] != 0)
                        {
                            board[placeOfZero[1], placeOfZero[0]] = board[placeOfZero[1] - 1, placeOfZero[0]];
                            board[placeOfZero[1] - 1, placeOfZero[0]] = 0;
                            placeOfZero[1]--;
                            Score++;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        read = true;
                        if (placeOfZero[0] != SizeOfBoard - 1)
                        {
                            board[placeOfZero[1], placeOfZero[0]] = board[placeOfZero[1], placeOfZero[0] + 1];
                            board[placeOfZero[1], placeOfZero[0] + 1] = 0;
                            placeOfZero[0]++;
                            Score++;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        read = true;
                        if (placeOfZero[0] != 0)
                        {
                            board[placeOfZero[1], placeOfZero[0]] = board[placeOfZero[1], placeOfZero[0] - 1];
                            board[placeOfZero[1], placeOfZero[0] - 1] = 0;
                            placeOfZero[0]--;
                            Score++;
                        }
                        break;
                    default:
                        key = Console.ReadKey();
                        break;
                }
            }
        }
        #endregion
    }
}