using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class VisitorCenter : IRooms
    {
        private int score = 0;
        private readonly List<IMinigame> minigames = new List<IMinigame>();

        public int StartLevel() {
            AnsiConsole.Clear();

            Utility.Narrator = "Gabi";
            var welcomeMsg = "Welcome to the Power Room. Your task is to fix the power cables and restore power to the reactor. Good luck!Welcome to the Power Room. Your task is to fix the power cables and restore power to the reactor. Good luck!Welcome to the Power Room. Your task is to fix...Welcome to the Power Room. Your task is to fix the power cables and restore power to the reactor. Good luck!Welcome to the Power Room. Your task is to fix the power cables and restore power to the reactor. Good luck!Welcome to the Power Room. Your task is to fix...";
            Utility.PrintStory(welcomeMsg);
            Utility.PrintStory("Test concluded!", "FireBoy");
            minigames[1].Run();
            string prompt1 = Utility.Prompt("What's your name?", ["I don't know my name...", "In case of any anomalies or alarms, operators quickly diagnose and address the issues to prevent potential hazards.", "They adjust control rods and other mechanisms to manage the reactor's power output and maintain stability.", "The control room team coordinates with other plant personnel to manage routine operations and maintenance activities."]);
            AnsiConsole.Clear();
            AnsiConsole.Write($"You chose: {prompt1}");
            Thread.Sleep(1000);
            string prompt2 = Utility.Prompt("What's your name?", ["..."]);
            AnsiConsole.Clear();
            AnsiConsole.Write($"You chose: {prompt2}");
            Thread.Sleep(1000);

            AnsiConsole.Clear();
            return score;
        }
    }
}