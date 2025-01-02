using System;
using Spectre.Console;

namespace Minigames
{
    public class RadiationLevels : IMinigame
    {
        public int Score { get; private set; }
        private int[] radiationLevels;
        private bool[] valves;
        private int timeLimit;
        public RadiationLevels(int arraySize, int limit) {
            radiationLevels = new int[arraySize];
            
            for (int i = 0; i < radiationLevels.Length; i++)
            {
                Random randomrand = new Random();
                radiationLevels[i] = randomrand.Next(1,10);
            }
            valves = new bool[arraySize];
            timeLimit = limit;
        }
        /// <summary>
        /// Runs the radiation monitoring game.
        /// </summary>
        /// <remarks>
        /// The game simulates monitoring radiation levels in a reactor. The game runs until the time limit is reached.
        /// 
        /// Instructions to win:
        /// - The game increments radiation levels randomly.
        /// - To win, the difference between consecutive radiation levels must not exceed 1.
        /// - If the condition is met for all radiation levels, the game prints "WIN" and ends.
        /// </remarks>
        public void Run()
        {   
            int time = 0;
            Random randomrand = new Random();
            while (time < timeLimit) {
                time++;
                radiationLevels[randomrand.Next(radiationLevels.Length)] += 1;
                for (int i = 0; i < radiationLevels.Length; i++)
                {
                    if (i == radiationLevels.Length - 1) {
                        Console.WriteLine("WIN");
                        Thread.Sleep(3000);
                        return;
                    }
                    if (Math.Abs(radiationLevels[i] - radiationLevels[i+1]) > 1)  {
                        break;
                    }
                }
                
                DrawValves();
            }
            Console.ReadKey();
        }
        void Valve(int index) {
            if (valves[index]) {
                radiationLevels[index]++;
            }
            else {
                radiationLevels[index]--;
            }
        }
        void Draw() {
            // Create a canvas
            var canvas = new Canvas(128, 30);

            // Draw some shapes
            for(var i = 0; i < radiationLevels.Length; i++)
            {
                // Cross
                for (int j = 0; j < radiationLevels[i]; j++) {
                    canvas.SetPixel(j+45, i*radiationLevels.Length, Color.GreenYellow);
                }
                
            }

            // Render the canvas
            AnsiConsole.Write(canvas);
        }
        void DrawValves(int selected = 1) {
            Console.SetCursorPosition(0,0   );
            Draw();
            var menu = new Table();
            menu.Centered();
            menu.HideHeaders();
            menu.Border(TableBorder.None);

            menu.AddColumn(new TableColumn("Menu").Centered());
            for (int i = 0; i < valves.Length; i++)
            {
                if (selected == i + 1)
                {
                    menu.AddRow(new Panel(new Markup($"[bold aqua]Release valve {i+1}[/]")).Expand().Border(BoxBorder.Square));
                }
                else
                {
                    menu.AddRow(new Panel(new Markup($"[yellow]Release valve {i+1}[/]")).Expand().Border(BoxBorder.None));
                }
            }

            AnsiConsole.Write(menu);

                var key = Console.ReadKey(true); // Read key without displaying it
                switch (key.Key) {
                    case ConsoleKey.UpArrow:
                        if (selected == 1)
                            selected = valves.Length; // If it would go past 1, reset it to 3
                        else
                            selected--;
                        DrawValves(selected);
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected == valves.Length)
                            selected = 1; // If it would go past 3, reset it to 1
                        else
                            selected++;
                        DrawValves(selected);
                        break;
                    case ConsoleKey.Enter:
                        radiationLevels[selected-1]-=2;
                        return;
                }
            
        }
    }
}