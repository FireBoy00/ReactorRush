using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class CoolingSystemRoom : IRooms
    {
        private int score = 0;
        private readonly List<IMinigame> minigames = new List<IMinigame>();
        
        public int StartLevel() {
            AnsiConsole.Clear();

            Utility.Narrator = "Operator";
            var welcomeMsg = "Cooling System room - the hero of the nuclear reactor! This is where operators manage heat transfer, keep coolant flowing smoothly, and ensure the pipes and pumps are in perfect shape. A working cooling system is what keeps the reactor safe and prevents any overheating disasters.\nUh-oh! We have a problem.\nOne of the pipes underneath the Containment building has cracked a leak, and it’s up to you to fix it before things get out of the control. Time to roll up your sleeves and get to work!";
            Utility.PrintStory(welcomeMsg);
            // minigames[{number of Pipe Repair Game}].Run();

            AnsiConsole.Clear();
            return score;
        }
    }
}