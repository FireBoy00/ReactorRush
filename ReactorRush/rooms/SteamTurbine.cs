using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class SteamTurbine : IRooms
    {
        public int Score { get; private set; }
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel(Player player)
        {
            Score = 0;
            AnsiConsole.Clear();

            Utility.Narrator = "Agatta";
            Utility.PrintStory("The Steam Turbine room - the heart of energy generation in the reactor. Here, all the thermal energy generated by the reactor is turned into electricity that powers homes, industries, and more.");
            string prompt1 = Utility.Prompt("Do you want to learn more about how it works?", ["Yes", "No"]);
            if (prompt1 == "Yes")
            {
                Utility.PrintStory("Superheated steam from the reactor is directed onto the blades of the turbine, causing it to spin at high speeds. This mechanical energy is then transferred to a generator, which produces electricity. Operators monitor the turbine's performance, ensuring optimal efficiency and addressing any mechanical issues promptly. The room is a hub of activity, with constant adjustments and maintenance to keep the system running smoothly.");
            }
            else
            {
                Utility.PrintStory("No problem! Let us skip the technical details and jump straight into action. ");
            }
            Utility.PrintStory("In the game below, you will have a simulation of this process by playing a 2048-style mini-game, aiming to achieve the highest score to represent efficient energy conversion. Think you’ve got what it takes to power the facility? Let’s find out!");
            do
            {
                AnsiConsole.Clear();
                minigames[3].Run();
                if (minigames[3].Score >= 3200)
                {
                    Utility.PrintStory("Outstanding! You have achieved a score of " + minigames[3].Score + "! Your energy conversion skills are top-notch, powering the entire facility with ease. Keep it up, your efforts are seen! ");
                    Score += 5;
                }
                else if (minigames[3].Score >= 1600)
                {
                    Utility.PrintStory("Wonderful job! You scored " + minigames[3].Score + ". You have made a significant contribution to the plant’s output. With a few practices, you will reach maximum efficiency in no time. Keep going! ");
                    Score += 3;
                }
                else
                {
                    Utility.PrintStory("Good try! You have scored " + minigames[3].Score + ". While your energy conversion needs improvement, every bit helps. Keep practicing, enhancing your skills and boosting the plant’s performance! ");
                }
            } while (minigames[3].Score < 1600);
            AnsiConsole.Clear();
            player.UpdateRoomStatus(this.GetType().Name, Score > 0); // Update the room status
            return Score;
        }
    }
}