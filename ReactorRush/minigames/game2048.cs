using System;
using Spectre.Console;

namespace Minigames
{
    public class Game2048 : IMinigame
    {
        public int SizeOfBoard { get; private set; }
        public int Score { get; private set; }
        private int[,] board;
        private int rollX;
        private int rollY;
        Random dice = new();
        public Game2048(int sizeOfBoard = 4)
        {
            SizeOfBoard = sizeOfBoard;
            board = new int[SizeOfBoard, SizeOfBoard];
            //Run();
        }
        public void Run()
        {
            rollX = dice.Next(0, SizeOfBoard);
            rollY = dice.Next(0, SizeOfBoard);
            board[rollY, rollX] = 2;
            Score = 0;
            NextRound();
            while (!IsEnd())
            {
                NextRound();
            }
            PrintBoard();
            Console.WriteLine("END MINIGAME!");
            Console.ReadKey();
            //Console.WriteLine("Your score: "+Score);
        }
        #region Control
        private void NextRoll()
        {
            do
            {
                rollX = dice.Next(0, SizeOfBoard);
                rollY = dice.Next(0, SizeOfBoard);
            } while (board[rollY, rollX] != 0);
            board[rollY, rollX] = 2;
        }
        private void PrintBoard()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Your score: " + Score);
            Console.WriteLine();
            for (int y = 0; y < SizeOfBoard; y++)
            {
                for (int x = 0; x < SizeOfBoard; x++)
                {
                    switch (board[y,x])
                    {
                        case 0:
                            AnsiConsole.Markup($"[maroon on Yellow2]   {board[y, x]}    [/]");
                            break;
                        case 2:
                            AnsiConsole.Markup($"[maroon on Yellow3_1]   {board[y, x]}    [/]");
                            break;
                        case 4:
                            AnsiConsole.Markup($"[maroon on Gold3_1]   {board[y, x]}    [/]");
                            break;
                        case 8:
                            AnsiConsole.Markup($"[maroon on darkgoldenrod]   {board[y, x]}    [/]");
                            break;
                        case 16:
                            AnsiConsole.Markup($"[maroon on olive]   {board[y, x]}   [/]");
                            break;
                        case 32:
                            AnsiConsole.Markup($"[maroon on Yellow3_1]   {board[y, x]}   [/]");
                            break;
                        case 64:
                            AnsiConsole.Markup($"[maroon on Yellow3_1]   {board[y, x]}   [/]");
                            break;
                        case 128:
                            AnsiConsole.Markup($"[maroon on Yellow3_1]  {board[y, x]}   [/]");
                            break;
                        case 256:
                            AnsiConsole.Markup($"[maroon on Yellow3_1]  {board[y, x]}   [/]");
                            break;
                        case 512:
                            AnsiConsole.Markup($"[maroon on Yellow3_1]  {board[y, x]}   [/]");
                            break;
                        default:
                            AnsiConsole.Markup($"[maroon on Yellow3_1]  {board[y, x]}  [/]");
                            break;
                    }
                    //Console.Write(board[y, x] + "\t");
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        private bool IsEnd()
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
        private void NextRound()
        {
            NextRoll();
            PrintBoard();
            var key = Console.ReadKey();
            bool read = false;

            while (!read)
            {
                int amountOf0;
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        read = true;
                        for (int x = 0; x < SizeOfBoard; x++)
                        {
                            amountOf0 = 0;
                            for (int y = 0; y < SizeOfBoard; y++)
                            {
                                if (board[y, x] == 0)
                                {
                                    amountOf0++;

                                }
                            }
                            if (amountOf0 != SizeOfBoard)
                            {
                                MoveUp(x, amountOf0);
                                amountOf0 = SumUp(x, amountOf0);
                                MoveUp(x, amountOf0);
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        read = true;
                        for (int x = 0; x < SizeOfBoard; x++)
                        {
                            amountOf0 = 0;
                            for (int y = 0; y < SizeOfBoard; y++)
                            {
                                if (board[y, x] == 0)
                                {
                                    amountOf0++;

                                }
                            }
                            if (amountOf0 != SizeOfBoard)
                            {
                                MoveDown(x, amountOf0);
                                amountOf0 = SumUp(x, amountOf0);
                                MoveDown(x, amountOf0);
                            }
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        read = true;
                        for (int y = 0; y < SizeOfBoard; y++)
                        {
                            amountOf0 = 0;
                            for (int x = 0; x < SizeOfBoard; x++)
                            {
                                if (board[y, x] == 0)
                                {
                                    amountOf0++;

                                }
                            }
                            if (amountOf0 != SizeOfBoard)
                            {
                                MoveRight(y, amountOf0);
                                amountOf0 = SumRight(y, amountOf0);
                                MoveRight(y, amountOf0);
                            }
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        read = true;
                        for (int y = 0; y < SizeOfBoard; y++)
                        {
                            amountOf0 = 0;
                            for (int x = 0; x < SizeOfBoard; x++)
                            {
                                if (board[y, x] == 0)
                                {
                                    amountOf0++;

                                }
                            }
                            if (amountOf0 != SizeOfBoard)
                            {
                                MoveLeft(y, amountOf0);
                                amountOf0 = SumLeft(y, amountOf0);
                                MoveLeft(y, amountOf0);
                            }
                        }
                        break;
                    default:
                        key = Console.ReadKey();
                        break;
                }
            }
        }
        private void MoveUp(int x, int amountOf0)
        {
            int inLine = 0;

            int y;
            for (y = 0; y < SizeOfBoard; y++)
            {
                if (board[y, x] != 0)
                {
                    inLine++;
                }
                else if (inLine < SizeOfBoard - amountOf0)
                {
                    while (board[y, x] == 0)
                    {
                        for (int z = y; z < SizeOfBoard - 1; z++)
                        {
                            board[z, x] = board[z + 1, x];
                        }
                        board[SizeOfBoard - 1, x] = 0;
                    }
                    inLine++;
                }
                else
                {
                    break;
                }
            }
        }
        private int SumUp(int x, int amountOf0)
        {
            int y;
            for (y = 0; y < SizeOfBoard - 1; y++)
            {
                if (board[y, x] == board[y + 1, x] && board[y, x] != 0)
                {
                    board[y, x] = board[y, x] * 2;
                    Score += board[y, x];
                    board[y + 1, x] = 0;
                    amountOf0++;
                }
            }
            return amountOf0;
        }

        private void MoveDown(int x, int amountOf0)
        {
            int inLine = 0;

            int y;
            for (y = SizeOfBoard - 1; y >= 0; y--)
            {
                if (board[y, x] != 0)
                {
                    inLine++;
                }
                else if (inLine < SizeOfBoard - amountOf0)
                {
                    while (board[y, x] == 0)
                    {
                        for (int z = y; z > 0; z--)
                        {
                            board[z, x] = board[z - 1, x];
                        }
                        board[0, x] = 0;
                    }
                    inLine++;
                }
                else
                {
                    break;
                }
            }
        }
        private int SumDown(int x, int amountOf0)
        {
            int y;
            for (y = SizeOfBoard - 1; y > 0; y--)
            {
                if (board[y, x] == board[y - 1, x] && board[y, x] != 0)
                {
                    board[y, x] = board[y, x] * 2;
                    Score += board[y, x];
                    board[y - 1, x] = 0;
                    amountOf0++;
                }
            }
            return amountOf0;
        }
        private void MoveLeft(int y, int amountOf0)
        {
            int inLine = 0;

            int x;
            for (x = 0; x < SizeOfBoard; x++)
            {
                if (board[y, x] != 0)
                {
                    inLine++;
                }
                else if (inLine < SizeOfBoard - amountOf0)
                {
                    while (board[y, x] == 0)
                    {
                        for (int z = x; z < SizeOfBoard - 1; z++)
                        {
                            board[y, z] = board[y, z + 1];
                        }
                        board[y, SizeOfBoard - 1] = 0;
                    }
                    inLine++;
                }
                else
                {
                    break;
                }
            }
        }
        private int SumLeft(int y, int amountOf0)
        {
            int x;
            for (x = 0; x < SizeOfBoard - 1; x++)
            {
                if (board[y, x] == board[y, x + 1] && board[y, x] != 0)
                {
                    board[y, x] = board[y, x] * 2;
                    Score += board[y, x];
                    board[y, x + 1] = 0;
                    amountOf0++;
                }
            }
            return amountOf0;
        }
        private void MoveRight(int y, int amountOf0)
        {
            int inLine = 0;

            int x;
            for (x = SizeOfBoard - 1; x >= 0; x--)
            {
                if (board[y, x] != 0)
                {
                    inLine++;
                }
                else if (inLine < SizeOfBoard - amountOf0)
                {
                    while (board[y, x] == 0)
                    {
                        for (int z = x; z > 0; z--)
                        {
                            board[y, z] = board[y, z - 1];
                        }
                        board[y, 0] = 0;
                    }
                    inLine++;
                }
                else
                {
                    break;
                }
            }
        }
        private int SumRight(int y, int amountOf0)
        {
            int x;
            for (x = SizeOfBoard - 1; x > 0; x--)
            {
                if (board[y, x] == board[y, x - 1] && board[y, x] != 0)
                {
                    board[y, x] = board[y, x] * 2;
                    Score += board[y, x];
                    board[y, x - 1] = 0;
                    amountOf0++;
                }
            }
            return amountOf0;
        }
        #endregion
    }
}