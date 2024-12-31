using System;
using System.Text;
using System.Threading;
using Spectre.Console;

namespace Minigames
{
    public class WasteDisposalGame : IMinigame
    {
        private int score;
        public int Score
        {
            get { return score; }
            private set { score = value; }
        }
        public void Run()
        {
            var originalEncoding = Console.OutputEncoding;
            Console.OutputEncoding = System.Text.Encoding.UTF8; // for ☢, ▮, ⁖⁙⁝, ¯, ‖

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
                }
            }

            Console.Clear();
            DrawTanks(tank1Waste, tank2Waste);
            Console.WriteLine("\nAll waste has been transferred!");

            Console.WriteLine("\nPress any key to exit...");
            Console.OutputEncoding = originalEncoding;
            Console.ReadKey(true);
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

            // Loop through frames to simulate movement
            foreach (string frame in transferFrames)
            {
                Console.Clear();
                Console.WriteLine("Transferring waste...");
                Console.WriteLine(frame);
                Thread.Sleep(500); // Delay to make animation visible
            }

            // Update waste levels after transfer animation completes
            tank1Waste--;
            tank2Waste++;
        }

    }
}
