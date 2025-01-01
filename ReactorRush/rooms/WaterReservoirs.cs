using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class WaterReservoir : IRooms
    {
        public int Score { get; private set; }
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel(Player player)
        {
            AnsiConsole.Clear();

            Utility.Narrator = "Gabi";
            Utility.PrintStory("This room plays a critical role in ensuring the reactor operates efficiently and safely. Water is a vital component in the cooling and energy generation process of a nuclear power plant, and here, you will learn how this system works and why maintaining it is essential. ");
            Utility.PrintStory("I think this is the right time to talk about its purpose, what is it?");
            Utility.PrintStory("The Water Reservoir is a key part of the plant’s secondary cooling system. It stores and supplies water that condenses steam discharged from the turbines. This ensures a continues cycle of steam generation and condensation, which is crucial for maintaining the efficiency and safety of the plant.");
            string prompt1 = Utility.Prompt("Did you know that the Water Reservoir helps prevent potential hazards?", ["Yes, tell me more", "No, I never thought or saw anything about it"]);
            Utility.PrintStory("It condenses any steam that might escape, avoiding pressure buildup and ensuring that hydrogen does not mix with oxygen to create an explosive environment. A nitrogen atmosphere in the reservoir further safeguards against it.");

            //? Minigame minigames[2].Run();
            AnsiConsole.Clear();
            player.UpdateRoomStatus(this.GetType().Name, Score > 0); // Update the room status
            return Score;
        }
    }
}