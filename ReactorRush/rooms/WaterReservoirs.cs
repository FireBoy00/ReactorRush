using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class WaterReservoir : IRooms
    {
        private int score = 0;
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel()
        {
            AnsiConsole.Clear();

            Utility.Narrator = "Gabi";
            var welcomeMsg = "You entered the Water Reservoir.";
            Utility.PrintStory(welcomeMsg);
            Utility.PrintStory("This room plays a critical role in ensuring the reactor operates efficiently and safely. Water is a vital component in the cooling and energy generation process of a nuclear power plant, and here, you will learn how this system works and why maintaining it is essential. ");
            AnsiConsole.Clear();
            return score;
        }
    }
}