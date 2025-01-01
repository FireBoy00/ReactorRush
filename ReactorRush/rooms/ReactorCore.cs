using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class ReactorCore : IRooms
    {
        private int score = 0;
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel() {
            AnsiConsole.Clear();
            

            Utility.Narrator = "Core operator";

            Utility.PrintStory("This is the core—the heart of the reactor, where uranium atoms split in a controlled chain reaction. A neutron strikes, the atom splits, energy is released, and more neutrons propagate the cycle. Control rods absorb excess neutrons to keep this process in balance.");
            Utility.PrintStory("The energy heats water, turning it to steam, which drives turbines to generate electricity. It’s a precise, elegant system, safeguarded by layers of protection—steel, concrete, and constant monitoring.");
            Utility.PrintStory("From the split of an atom comes power for entire cities. It’s a testament to science, harnessing nature’s immense energy with care and respect.");
            AnsiConsole.Clear();
            return score;
        }
    }
}