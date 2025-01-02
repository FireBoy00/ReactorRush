using System;
using Spectre.Console;

namespace Minigames
{
    public class Radiationmonitoringgame : IMinigame
    {
        public int Score { get; private set; }
        private int[] waterLevels;
        private bool[] valves;
        private int timeLimit;
        public Radiationmonitoringgame (int arraySize, int limit) {
            waterLevels = new int[arraySize];
            
            for (int i = 0; i < waterLevels.Length; i++)
            {
                Random randomrand = new Random();
                waterLevels[i] = randomrand.Next(1,10);
            }
            valves = new bool[arraySize];
            timeLimit = limit;
        }
        public void Run()
        {   
            int time = 0;
            Random randomrand = new Random();
            while (time < timeLimit) {
                time++;
                waterLevels[randomrand.Next(waterLevels.Length)] += 1;
                for (int i = 0; i < waterLevels.Length; i++)
                {
                    if (i == waterLevels.Length - 1) {
                        Console.WriteLine("WIN");
                        return;
                    }
                    if (Math.Abs(waterLevels[i] - waterLevels[i+1]) > 1)  {
                        break;
                    }
                }
                
                DrawValves();
            }
            Console.ReadKey();
        }
        void Valve(int index) {
            if (valves[index]) {
                waterLevels[index]++;
            }
            else {
                waterLevels[index]--;
            }
        }
        void Draw() {
            // Create a canvas
            var canvas = new Canvas(128, 30);

            // Draw some shapes
            for(var i = 0; i < waterLevels.Length; i++)
            {
                // Cross
                for (int j = 0; j < waterLevels[i]; j++) {
                    canvas.SetPixel(j+45, i*waterLevels.Length, Color.Aqua);
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
                        waterLevels[selected-1]-=2;
                        return;
                }
            
        }
    }
}