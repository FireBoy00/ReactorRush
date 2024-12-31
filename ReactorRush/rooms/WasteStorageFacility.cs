using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class WasteStorageFacility : IRooms
    {
        private int score = 0;
        private readonly List<IMinigame> minigames = new List<IMinigame>();

        public int StartLevel() {
            AnsiConsole.Clear();

            Utility.Narrator = "New Narrator Here";


            var welcomeMsg = "This is the waste storage room.";
            Utility.PrintStory(welcomeMsg);
            AnsiConsole.Clear();
            return score;
        }
    }
}