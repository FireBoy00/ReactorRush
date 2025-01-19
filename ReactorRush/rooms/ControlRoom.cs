using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class ControlRoom : IRooms
    {
        public int Score { get; private set; }
        private readonly int minigameIndex = 2; // Index of the minigame in the minigames list
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel(Player player)
        {
            Console.Title = "Control Room";
            Score = 0;
            AnsiConsole.Clear();

            Utility.Narrator = "Control Room Ambassador";
            Utility.PrintStory("You step into the Control Room, a space alive with data and sound. This is where decisions are made, and the reactor’s power is carefully managed.\n\nThis is where science meets strategy. As the operator, you will oversee energy production, balance outputs, and ensure that everything runs smoothly. All your actions here will define the success of the reactor and its contribution to a sustainable future.");

            Utility.PrintStory("Here is what the team does in this room: \n\nKeep an Eye on Things \n      Operators continuously monitor the reactor's systems and parameters, ensuring everything is functioning within safe limits" +
            "\n\nAdjusting Controls\n      They adjust control rods and other mechanisms to manage the reactor's power output and maintain stability. " +
            "\n\nRespond to Warnings\n      In case of any anomalies or alarms, operators quickly diagnose and address the issues to prevent potential hazards." +
            "\n\nWork as a Team\n      The control room crew coordinates with other plant workers to handle routine tasks and maintenance." +
            "\n\nFocus on Safety\n      Safety protocols are strictly followed to protect both the plant and the surrounding environment from any risks.");

            Utility.PrintStory("The system has flagged some settings that need tweaking. To stabilize the reactor, you need to get the controls back in order. But first: ");
            string prompt1;
            do
            {
                prompt1 = Utility.Prompt("Do you remember what is meant by Work as a Team?",
                ["In case of any anomalies or alarms, operators quickly diagnose and address the issues to prevent potential hazards",
                "They adjust control rods and other mechanisms to manage the reactor's power output and maintain stability",
                "The control room team coordinates with other plant personnel to handle routine and maintenance"]);
                switch (prompt1)
                {
                    case "In case of any anomalies or alarms, operators quickly diagnose and address the issues to prevent potential hazards":
                    case "They adjust control rods and other mechanisms to manage the reactor's power output and maintain stability":
                        Utility.PrintStory("This is not the right answer, pay attention to such words: handle, coordinate... ");
                        Score -= 2;
                        break;
                    case "The control room team coordinates with other plant personnel to handle routine and maintenance":
                        Score += 10;
                        Utility.PrintStory("You chose the right answer, procced with your given task. Good luck! ");
                        break;
                }
            } while (prompt1 != "The control room team coordinates with other plant personnel to handle routine and maintenance");
            
            AnsiConsole.Clear();
            Console.Title = $"Control Room - {minigames[minigameIndex].Name}";
            minigames[minigameIndex].Run();
            player.UpdateMinigameStatus(minigames[minigameIndex].GetType().Name, true); // Update the minigame status
            Console.Title = "Control Room";
            Utility.PrintStory("Well done! You've reset the controls, and the reactor is running smoothly again. That's one big step toward mastering this facility! Go to the next room.");
            Utility.PrintStory("Oh, you seem confused. I forgot to mention that this tour will be conducted in a period of 12 days ensuring you know how a state of the art reactor works! Enough of this for now."); //it shuld be configurable

            if (minigames[minigameIndex].Score < 31) {
                Score += 10;
            }
            else {
                Score += 5;
            }

            AnsiConsole.Clear();
            player.UpdateRoomStatus(this.GetType().Name, Score > 0); // Update the room status
            return Score;
        }
    }
}