using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class SteamTurbine : IRooms
    {
        private int score = 0;
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel() {
            AnsiConsole.Clear();

            Utility.Narrator = "Agata";

            AnsiConsole.Clear();
            return score;
        }
    }
}