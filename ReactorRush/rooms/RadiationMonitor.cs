using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class RadiationMonitor : IRooms
    {
        private int score = 0;
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel()
        {
            AnsiConsole.Clear();

            Utility.Narrator = "Nuclear Energy Specialist";


            Utility.PrintStory("Welcome to the Radiation Monitoring room - a critical hub for ensuring safety and control in the plant. Here you will find real-time data on radiation levels, displayed through dynamic bars that change based on the environment's activity.");
            Utility.PrintStory("What does this room do?");
            Utility.PrintStory("The monitoring systems in this room continuously track radiation levels using advanced detectors. These systems help ensure the safety of workers and the integrity of the plant by identifying changes in gamma radiation across different areas. From low levels that safeguard workers to high ranges that indicate breaches, the monitors are designed to act quickly and effectively.");
            Utility.PrintStory("Did You Know?\n\nRadiation is a natural part of life and exists everywhere—even in the air we breathe and the bananas we eat! However, in a nuclear plant, keeping radiation levels under control is vital to protect people and maintain smooth operations. ");
            //bars here
            Utility.PrintStory("The bars in front of you represent radiation levels in different parts of the plant. Watch them carefully - each movement could indicate a story.");           
            string prompt1 = Utility.Prompt("Is the system managing routine operations, or is there a need for attention?", ["The bars are steady and within the expected range, indicating the system is functioning normally", "Assuming the system is fine without checking the displayed levels against the acceptable range or overlooking fluctuations that could indicate an issue"]);
            if (prompt1 == "The bars are steady and within the expected range, indicating the system is functioning normally")
            {
                Utility.PrintStory($"You chose: {prompt1}\nCorrect!");
            }
            else
            {
                Utility.PrintStory($"You chose: {prompt1}\nIncorrect! The right one is above.");
            }
            string prompt2 = Utility.Prompt("What would you do if the levels spike?",["Notify the control room immediately and follow emergency procedures, such as evacuating the area and isolating the radiation source", "Ignore the spike or assume it's a false alarm without verification, risking safety and protocol breaches"]);
            if (prompt2 == "Notify the control room immediately and follow emergency procedures, such as evacuating the area and isolating the radiation source")
            {
                Utility.PrintStory($"You chose: {prompt2}\nCorrect!");
            }
            else
            {
                Utility.PrintStory($"You chose: {prompt2}\nIncorrect! You should never ignore such monitors, in general all monitoring devices.");
            }
            Utility.PrintStory("Thank you for monitoring these critical indicators! Now you know that you have to keep an eye on those bars - they are more than just lines. ");

            Thread.Sleep(1000);

            AnsiConsole.Clear();
            return score;
        }
    }
}