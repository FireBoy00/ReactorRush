using System;
using Spectre.Console;

namespace Minigames
{
    public class PipeRepair : IMinigame
    {
        public int SizeOfBoard { get; private set; }
        public int Score { get; private set; }
        public bool isWon { get; private set; }
        public int maxEnergy { get; private set; }
        private int[,] board;
        private bool[,] isWaterGo;
        private int[] placeOfCursor = new int[2]; //board[placeOfCursor[1], placeOfCursor[0]]
        private int previousX;
        private int previousY;
        private int presentX;
        private int presentY;
        Random dice = new();
        public PipeRepair(int sizeOfBoard = 10)
        {
            SizeOfBoard = sizeOfBoard;
            board = new int[SizeOfBoard + 2, SizeOfBoard];
            isWaterGo = new bool[SizeOfBoard + 2, SizeOfBoard];
            maxEnergy = SizeOfBoard * 4;
            //Run();
        }
        public void Run()
        {
            Console.CursorVisible = false;
            Array.Clear(board);
            Array.Clear(isWaterGo);
            placeOfCursor[0] = 0;
            placeOfCursor[1] = 1;
            Score = 0;
            for (int i = 0; i < SizeOfBoard; i++)
            {
                board[0, i] = 5;
                isWaterGo[0, i] = true;
                board[SizeOfBoard + 1, i] = 5;
                //isWaterGo[SizeOfBoard+1,i] = false;
            }
            PrintPlotline();
            for (int y = 1; y < SizeOfBoard + 1; y++)
            {
                for (int x = 0; x < SizeOfBoard; x++)
                {
                    board[y, x] = dice.Next(1, 7);
                }
            }
            StartWaterGo();
            PrintBoard();
            //Console.ReadKey();
            while (!IsEnd() && Score <= maxEnergy)
            {
                if (NextRound())
                    StartWaterGo();
            }
            PrintBoard();
            Console.WriteLine();
            if(Score > maxEnergy){
                isWon = false;
                Console.WriteLine("YOU LOSE THAT GAME.");
            }
            else{
                isWon = true;
                Console.WriteLine("CONGRATULATIONS,");
                Console.WriteLine("YOU WON THAT GAME!");
            }
            Console.ReadKey();
            //Console.WriteLine("Number of moves made: "+Score);
        }
        #region Layout

        private void PrintPlotline()
        {
            Console.WriteLine("You are a plumber and you have to fix the pipes in a power plant. You can only use the elements that are already available. Rotate the elements to allow the water to flow from the beginning to the end. The beginning will be marked in dark blue, the end in light blue.\n");
            Console.WriteLine("To move around the board, use the arrow keys.");
            Console.WriteLine("To rotate a given element 90* to the right (clockwise), click Enter.");
            Console.WriteLine("To rotate it 90* to the left (counterclockwise), click Space.");
            Console.WriteLine("\nClick any key to continiue.");
            Console.ReadKey();
            Console.Clear();
        }
        private void PrintBoard()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Number of energy used: " + Score);
            Console.WriteLine();
            for (int y = 0; y < SizeOfBoard + 2; y++)
            {
                AnsiConsole.Markup($"[grey23 on grey78] [/]");
                for (int x = 0; x < SizeOfBoard; x++)
                {
                    if (!(y == placeOfCursor[1] && x == placeOfCursor[0]) && !isWaterGo[y, x])
                    {
                        switch (board[y, x])
                        {
                            case 1:
                                AnsiConsole.Markup($"[maroon on grey78]╔[/]");
                                break;
                            case 2:
                                AnsiConsole.Markup($"[maroon on grey78]╗[/]");
                                break;
                            case 3:
                                AnsiConsole.Markup($"[maroon on grey78]╝[/]");
                                break;
                            case 4:
                                AnsiConsole.Markup($"[maroon on grey78]╚[/]");
                                break;
                            case 5:
                                AnsiConsole.Markup($"[maroon on grey78]║[/]");
                                break;
                            case 6:
                                AnsiConsole.Markup($"[maroon on grey78]═[/]");
                                break;
                            default:
                                AnsiConsole.Markup($"[purple_2 on darkslategray3] [/]");
                                break;
                        }
                    }
                    else if (!isWaterGo[y, x])
                    {
                        switch (board[y, x])
                        {
                            case 1:
                                AnsiConsole.Markup($"[purple_2 on lightgoldenrod2_2]╔[/]");
                                break;
                            case 2:
                                AnsiConsole.Markup($"[purple_2 on lightgoldenrod2_2]╗[/]");
                                break;
                            case 3:
                                AnsiConsole.Markup($"[purple_2 on lightgoldenrod2_2]╝[/]");
                                break;
                            case 4:
                                AnsiConsole.Markup($"[purple_2 on lightgoldenrod2_2]╚[/]");
                                break;
                            case 5:
                                AnsiConsole.Markup($"[purple_2 on lightgoldenrod2_2]║[/]");
                                break;
                            case 6:
                                AnsiConsole.Markup($"[purple_2 on lightgoldenrod2_2]═[/]");
                                break;
                            default:
                                AnsiConsole.Markup($"[purple_2 on lightgoldenrod2_2] [/]");
                                break;
                        }
                    }
                    else
                    {
                        //water is going here
                        if ((y == placeOfCursor[1] && x == placeOfCursor[0]))
                        {
                            switch (board[y, x])
                            {
                                case 1:
                                    AnsiConsole.Markup($"[red3_1 on blue3_1]╔[/]");
                                    break;
                                case 2:
                                    AnsiConsole.Markup($"[red3_1 on blue3_1]╗[/]");
                                    break;
                                case 3:
                                    AnsiConsole.Markup($"[red3_1 on blue3_1]╝[/]");
                                    break;
                                case 4:
                                    AnsiConsole.Markup($"[red3_1 on blue3_1]╚[/]");
                                    break;
                                case 5:
                                    AnsiConsole.Markup($"[red3_1 on blue3_1]║[/]");
                                    break;
                                case 6:
                                    AnsiConsole.Markup($"[red3_1 on blue3_1]═[/]");
                                    break;
                                default:
                                    AnsiConsole.Markup($"[red3_1 on blue3_1] [/]");
                                    break;
                            }
                        }
                        else
                        {
                            switch (board[y, x])
                            {
                                case 1:
                                    AnsiConsole.Markup($"[blue3 on darkslategray3]╔[/]");
                                    break;
                                case 2:
                                    AnsiConsole.Markup($"[blue3 on darkslategray3]╗[/]");
                                    break;
                                case 3:
                                    AnsiConsole.Markup($"[blue3 on darkslategray3]╝[/]");
                                    break;
                                case 4:
                                    AnsiConsole.Markup($"[blue3 on darkslategray3]╚[/]");
                                    break;
                                case 5:
                                    AnsiConsole.Markup($"[blue3 on darkslategray3]║[/]");
                                    break;
                                case 6:
                                    AnsiConsole.Markup($"[blue3 on darkslategray3]═[/]");
                                    break;
                                default:
                                    AnsiConsole.Markup($"[blue3 on darkslategray3] [/]");
                                    break;
                            }
                        }
                    }
                }
                AnsiConsole.Markup($"[maroon on grey78] [/]");
                Console.WriteLine("");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        #endregion
        #region Control
        private bool IsEnd()
        {
            for (int x = 0; x < SizeOfBoard; x++)
            {
                if (isWaterGo[SizeOfBoard + 1, x])
                {
                    return true;
                }
            }
            return false;
        }
        private bool WaterGo()
        {
            if (presentX >= 0 && presentX < SizeOfBoard && presentY >= 1 && presentY < SizeOfBoard + 2)
            {
                switch (board[presentY, presentX]) //to complitly chagne
                {
                    case 1: //╔
                        if (previousX == presentX + 1 && previousY == presentY)
                        {
                            //right
                            isWaterGo[presentY, presentX] = true;
                            previousX = presentX;
                            //previousY = presentY;
                            presentY++;
                            return true;
                        }
                        else if (previousX == presentX && previousY == presentY + 1)
                        {
                            //down
                            isWaterGo[presentY, presentX] = true;
                            //previousX = presentX;
                            previousY = presentY;
                            presentX++;
                            return true;
                        }
                        break;
                    case 2: //╗
                        if (previousX == presentX - 1 && previousY == presentY)
                        {
                            //left
                            isWaterGo[presentY, presentX] = true;
                            previousX = presentX;
                            //previousY = presentY;
                            presentY++;
                            return true;
                        }
                        else if (previousX == presentX && previousY == presentY + 1)
                        {
                            //down
                            isWaterGo[presentY, presentX] = true;
                            //previousX = presentX;
                            previousY = presentY;
                            presentX--;
                            return true;
                        }
                        break;
                    case 3: //╝
                        if (previousX == presentX - 1 && previousY == presentY)
                        {
                            //left
                            isWaterGo[presentY, presentX] = true;
                            previousX = presentX;
                            //previousY = presentY;
                            presentY--;
                            return true;
                        }
                        else if (previousX == presentX && previousY == presentY - 1)
                        {
                            //up
                            isWaterGo[presentY, presentX] = true;
                            //previousX = presentX;
                            previousY = presentY;
                            presentX--;
                            return true;
                        }
                        break;
                    case 4: //╚
                        if (previousX == presentX + 1 && previousY == presentY)
                        {
                            //right
                            isWaterGo[presentY, presentX] = true;
                            previousX = presentX;
                            //previousY = presentY;
                            presentY--;
                            return true;
                        }
                        else if (previousX == presentX && previousY == presentY - 1)
                        {
                            //up
                            isWaterGo[presentY, presentX] = true;
                            //previousX = presentX;
                            previousY = presentY;
                            presentX++;
                            return true;
                        }
                        break;
                    case 5: //║
                        if (previousX == presentX && previousY == presentY + 1)
                        {
                            //down
                            isWaterGo[presentY, presentX] = true;
                            //previousX = presentX;
                            previousY = presentY;
                            presentY--;
                            return true;
                        }
                        else if (previousX == presentX && previousY == presentY - 1)
                        {
                            //up
                            isWaterGo[presentY, presentX] = true;
                            //previousX = presentX;
                            previousY = presentY;
                            presentY++;
                            return true;
                        }
                        break;
                    case 6: //═
                        if (previousX == presentX + 1 && previousY == presentY)
                        {
                            //right
                            isWaterGo[presentY, presentX] = true;
                            previousX = presentX;
                            //previousY = presentY;
                            presentX--;
                            return true;
                        }
                        else if (previousX == presentX - 1 && previousY == presentY)
                        {
                            //left
                            isWaterGo[presentY, presentX] = true;
                            previousX = presentX;
                            //previousY = presentY;
                            presentX++;
                            return true;
                        }
                        break;
                }
                return false;
            }
            return false;
        }
        private void StartWaterGo()
        {
            for (int y = 1; y < SizeOfBoard + 1; y++)
            {
                for (int x = 0; x < SizeOfBoard; x++)
                {
                    isWaterGo[y, x] = false;
                }
            }
            for (int x = 0; x < SizeOfBoard; x++)
            {
                presentX = x;
                presentY = 1;
                previousX = x;
                previousY = 0;
                while (WaterGo() && !IsEnd()) { }
            }
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
                        if (placeOfCursor[1] != SizeOfBoard)
                        {
                            placeOfCursor[1]++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        read = true;
                        if (placeOfCursor[1] > 1)
                        {
                            placeOfCursor[1]--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        read = true;
                        if (placeOfCursor[0] != SizeOfBoard - 1)
                        {
                            placeOfCursor[0]++;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        read = true;
                        if (placeOfCursor[0] != 0)
                        {
                            placeOfCursor[0]--;
                        }
                        break;
                    case ConsoleKey.Enter: //to right
                        switch (board[placeOfCursor[1], placeOfCursor[0]])
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 5:
                                board[placeOfCursor[1], placeOfCursor[0]] += 1;
                                break;
                            case 4:
                                board[placeOfCursor[1], placeOfCursor[0]] = 1;
                                break;
                            case 6:
                                board[placeOfCursor[1], placeOfCursor[0]] = 5;
                                break;
                        }
                        read = true;
                        next = true;
                        Score++;
                        break;
                    case ConsoleKey.Spacebar: //to left
                        switch (board[placeOfCursor[1], placeOfCursor[0]])
                        {
                            case 2:
                            case 3:
                            case 4:
                            case 6:
                                board[placeOfCursor[1], placeOfCursor[0]] -= 1;
                                break;
                            case 1:
                                board[placeOfCursor[1], placeOfCursor[0]] = 4;
                                break;
                            case 5:
                                board[placeOfCursor[1], placeOfCursor[0]] = 6;
                                break;
                        }
                        read = true;
                        next = true;
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