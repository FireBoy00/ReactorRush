using System;
using System.Text;
using Spectre.Console.Rendering;
using System.Threading;
using Spectre.Console;

namespace Minigames
{
    public class WasteDisposal : IMinigame
    {
        public int Score { get; private set; }
        public string Name { get; private set; } = string.Empty;

        public WasteDisposal()
        {
            Score = 0;
            Name = "Waste Transfer Minigame";
        }
        
        public void Run()
        {
            Name = "Waste Transfer Minigame";
            Score = 0;
            var originalEncoding = Console.OutputEncoding;

            Console.OutputEncoding = System.Text.Encoding.UTF8; // for special characters

            int tank1Waste = 7; // Initial waste in Tank 1
            int tank2Waste = 0; // Tank 2 starts empty

            while (tank1Waste > 0)
            {
                Console.Clear();
                DrawTanks(tank1Waste, tank2Waste);

                Console.WriteLine("\nPress Enter to transfer waste...");

                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    AnimateWasteTransfer(ref tank1Waste, ref tank2Waste);
                    Score += 5; 
                }
            }

            Console.Clear();
            DrawTanks(tank1Waste, tank2Waste);
            Console.WriteLine("\nAll waste has been transferred!");
            Console.WriteLine($"\nYour final Score is: {Score}");

            Console.WriteLine("\nPress any key to exit...");
            Console.OutputEncoding = originalEncoding;
            Console.ReadKey(true);
            AnsiConsole.Clear();
        }

        private static void DrawTanks(int tank1Waste, int tank2Waste)
        {
            DrawTank1(tank1Waste);
            DrawTank2(tank2Waste);
        }

        private static void DrawTank1(int wasteLevel)
        {
            Console.WriteLine(@"Tank 1:
           ___________
          / _ _ _ _ _ \     
         / _ _ _ _ _ _ \       
        |               |");

            for (int i = 0; i < 8; i++)
            {
                if (i < wasteLevel)
                    Console.WriteLine("        |    | ☢  |     |");
                else
                    Console.WriteLine("        |               |");
            }

            Console.WriteLine(@"        |_______________|");
        }

        private static void DrawTank2(int wasteLevel)
        {
            Console.WriteLine(@"Tank 2:
        ________________ ");

            Console.WriteLine("        ▮▮▮▮▮▮▮☢ ▮▮▮▮▮▮▮");

            for (int i = 6; i >= 0; i--)
            {
                if (i < wasteLevel)
                    Console.WriteLine("        ‖▫ ▫ ▫▫ ▫ ▫  ▫▫‖");
                else
                    Console.WriteLine("        ‖              ‖");
            }
            Console.WriteLine("         ‾‾‾‾‾‾‾‾‾‾‾‾‾‾ ");
        }

        private static void AnimateWasteTransfer(ref int tank1Waste, ref int tank2Waste)
        {
            string[] transferFrames = new string[]
            {
                @"
                 /‾‾‾‾‾‾‾‾‾‾‾‾‾‾|
                /               |
               |              __|__
               |             |  ▫  |
               |             | ▫▫▫ |
               |             |__ __|
               |              _(≬)_
               |             |     |
               |             |     |
               |             |_____|
            ___⊥___                   

        ",
                @"

                                  /‾‾‾‾‾‾‾‾‾‾‾‾‾‾|
                                 /               |
                                |              __|__
                                |             |  ▫  |
                                |             | ▫   |
                                |             |__ __|
                                |              _(≬)_
                                |             |   ▫ |
                                |             |  ▫  |
                                |             |_____|
                             ___⊥___                   
        ",
                @"
                
                                                  /‾‾‾‾‾‾‾‾‾‾‾‾‾‾|
                                                 /               |
                                                |              __|__
                                                |             |  ▫  |
                                                |             |     |
                                                |             |__ __|
                                                |              _(≬)_
                                                |             | ▫ ▫ |
                                                |             |  ▫▫ |
                                                |             |_____|
                                             ___⊥___                
        "
            };

            foreach (string frame in transferFrames)
            {
                Console.Clear();
                Console.WriteLine("Transferring waste...");
                Console.WriteLine(frame);
                Thread.Sleep(500); 
            }

            tank1Waste--;
            tank2Waste++;
        }
    }
}
