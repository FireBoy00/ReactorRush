using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class RadiationMonitor : IRooms
    {
        public int Score { get; private set; }
        private readonly int minigameIndex = -1; // Index of the minigame in the minigames list
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel(Player player)
        {
            AnsiConsole.Clear();

            Utility.Narrator = "Nuclear Energy Specialist";

            minigames[8].Run(); // Run the minigame
            Utility.PrintStory("Welcome to the Radiation Monitoring room - a critical hub for ensuring safety and control in the plant. Here you will find real-time data on radiation levels, displayed through dynamic bars that change based on the environment's activity.");
            Utility.PrintStory("What does this room do?");
            Utility.PrintStory("The monitoring systems in this room continuously track radiation levels using advanced detectors. These systems help ensure the safety of workers and the integrity of the plant by identifying changes in gamma radiation across different areas. From low levels that safeguard workers to high ranges that indicate breaches, the monitors are designed to act quickly and effectively.");
            Utility.PrintStory("Did You Know?\n\nRadiation is a natural part of life and exists everywhere—even in the air we breathe and the bananas we eat! However, in a nuclear plant, keeping radiation levels under control is vital to protect people and maintain smooth operations. ");
            //bars here
            Utility.PrintStory("The bars in front of you represent radiation levels in different parts of the plant. Watch them carefully - each movement could indicate a story.");
            string prompt1 = Utility.Prompt("Is the system to track radiation levels functioning as it should? Is there a need for attention?", ["The radiation levels are steady and within the expected range, indicating the system is functioning normally, so there is no need for more attention", "I assume the system is fine without checking it on the displayed levels, no need for attention in any case"]);
            if (prompt1 == "The radiation levels are steady and within the expected range, indicating the system is functioning normally, so there is no need for more attention")
            {
                Utility.PrintStory($"You chose: {prompt1}\nExactly the response we expected from you!");
                Score += 10;
            }
            else
            {
                Utility.PrintStory($"You chose: {prompt1}\nThis is not the right way to act in the nuclear reator! You should have checked the radiation levels... The right one was above.");
                Score -= 2;
            }
            string prompt2 = Utility.Prompt("What would you do if the levels spike?", ["Notify the control room immediately and follow emergency procedures, such as evacuating the area and isolating the radiation source", "Ignore the spike or assume it's a false alarm without verification, risking safety and protocol breaches"]);
            if (prompt2 == "Notify the control room immediately and follow emergency procedures, such as evacuating the area and isolating the radiation source")
            {
                Utility.PrintStory($"You chose: {prompt2}\nCorrect!");
                Score += 10;
            }
            else
            {
                Utility.PrintStory($"You chose: {prompt2}\nIncorrect! You should never ignore such monitors, in general all monitoring devices.");
                Score -= 2;
            }
            Utility.PrintStory("Thank you for monitoring these critical indicators! Now you know that you have to keep an eye on those bars - they are more than just lines. Go to the next room! ");

            Thread.Sleep(1000);

            AnsiConsole.Clear();
            player.UpdateRoomStatus(this.GetType().Name, Score > 0); // Update the room status
            return Score;
        }
    }
}