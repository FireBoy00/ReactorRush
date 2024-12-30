using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class VisitorCenter : IRooms
    {
        private int score = 0;
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel() {
            AnsiConsole.Clear();

            Utility.Narrator = "Gabi";
            var welcomeMsg = "Welcome to the Power Room. Your task is to fix the power cables and restore power to the reactor. Good luck!\nWelcome to the Power Room. Your task is to fix the power cables and restore power to the reactor. \n\nGood luck!Welcome to the Power Room. Your task is to fix...\nWelcome to the Power Room. Your task is to fix the power cables and restore power to the reactor. \n\nGood luck!\n\nWelcome to the Power Room. Your task is to fix the power cables and restore power to the reactor. Good luck!Welcome to the Power Room. Your task is to fix...";
            Utility.PrintStory(welcomeMsg);
            Utility.PrintNewspaper("Breaking news, today a man won the lottery of nuclear energy!", ["This would be my first article, please escuse any none important information!!!", "This would be, in theory the 2nd article, but... I'm not sure."]);
            Utility.PrintStory("Test concluded!", "FireBoy");
            // minigames[1].Run();
            string prompt1 = Utility.Prompt("What's your name?", ["I don't know my name...", "In case of any anomalies or alarms, operators quickly diagnose and address the issues to prevent potential hazards.", "They adjust control rods and other mechanisms to manage the reactor's power output and maintain stability.", "The control room team coordinates with other plant personnel to manage routine operations and maintenance activities."]);
            AnsiConsole.Clear();
            AnsiConsole.Write($"You chose: {prompt1}");
            Thread.Sleep(1000);
            string prompt2 = Utility.Prompt("What's your name?", ["..."]);
            AnsiConsole.Clear();
            AnsiConsole.Write($"You chose: {prompt2}");

            // Ensure the index is within the valid range
            if (minigames.Count > 4) {
                AnsiConsole.Write($"Score in PipeRepair: {minigames[4].Score}");
            } else {
                AnsiConsole.Write("PipeRepair minigame is not available.");
            }

            Thread.Sleep(1000);

            AnsiConsole.Clear();
            return score;
        }
    }
}