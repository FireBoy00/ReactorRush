using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class ContainmentBuilding : IRooms
    {
        bool isCorrect1 = false;
        string prompt1 = "";
        bool isCorrect2 = false;
        string prompt2 = "";
        public int Score { get; private set; }
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel(Player player)
        {
            Score = 0;
            AnsiConsole.Clear();

            Utility.Narrator = "Operator";
            var welcomeMsg = "The Containment building is the most recognizable structure in a nuclear power plant. It is designed as a gas-tight enclosure (or shell) around the nuclear reactor and its primary systems. Think of it as the ultimate shield built to keep the reactor safe and the environment protected.";
            Utility.PrintStory(welcomeMsg);

            var welcomeMsg2 = "This building serves two critical functions:\n  1) Confinement of radioactive material in operational states and accident conditions.\n   In other words it keeps radiation securely inside, even in the event of an accident.\n  2) Protection of the power plant against external natural and human-induced events.\n   Whether it is a natural disaster or human-caused event, the building stands strong.\n\nThis building is like the last line of defense – if all else fails, this is what keeps us and the environment safe. It is specifically designed to contain steam and coolant in case of accidents, preventing them from escaping into the outside world.\n\nNow, let us test your knowledge about this vital structure.";
            Utility.PrintStory(welcomeMsg2);

            while (!isCorrect1)
            {
                prompt1 = Utility.Prompt("What is the main purpose of this building that you are in right now?", ["To produce electricity", "To prevent radiation from escaping", "To cool down the reactor"]);

                if (prompt1 == "To prevent radiation from escaping")
                {
                    Utility.PrintStory($"You chose: {prompt1}\nThat is right, you have an excellent memory!");
                    isCorrect1 = true;
                    Score += 10;
                }
                else if (prompt1 == "To produce electricity")
                {
                    Utility.PrintStory($"You chose: {prompt1}\nWrong. Small hint: it is related to radiation.");
                    Score -= 2;
                }
                else if (prompt1 == "To cool down the reactor")
                {
                    Utility.PrintStory($"You chose: {prompt1}\nWrong. Small hint: it is related to radiation.");
                    Score -= 2;
                }
                else
                {
                    Utility.PrintStory("Invalid selection. Please try again.");
                }
            }

            while (!isCorrect2)
            {
                prompt2 = Utility.Prompt("What could happen if there were no Containment Building in the nuclear reactor?", ["Radiation could escape, endangering people and the environment", "The reactor would stop working entirely", "Nothing; it is just an extra feature"]);

                if (prompt2 == "Radiation could escape, endangering people and the environment")
                {
                    Utility.PrintStory($"You chose: {prompt2}\nThis is right, keep going!");
                    isCorrect2 = true;
                    Score += 10;
                }
                else if (prompt2 == "The reactor would stop working entirely")
                {
                    Utility.PrintStory($"You chose: {prompt2}\nThe reactor itself is still working, do you understand why? Because it still has fuel, turbines and so on.");
                    Score -= 2;
                }
                else if (prompt2 == "Nothing; it is just an extra feature")
                {
                    Utility.PrintStory($"You chose: {prompt2}\nThis does not make sense at all, in such a reactor everything is made on purpose.");
                    Score -= 2;
                }
                else
                {
                    Utility.PrintStory("Invalid selection. Please try again.");
                }
            }

            var welcomeMsg3 = "Time for action!\n\nNow that you understand the importance of this building, it is time to put your skills to the test. Imagine there has been an emergency, and the system is locked. You must input the correct pin-code to contain the situation and escape safely (you can use numbers from 0 to 9).";
            Utility.PrintStory(welcomeMsg3);

            AnsiConsole.Clear();
            minigames[1].Run();

            while(minigames[1].Score == 0)
            {
                Utility.PrintStory("Close, but not quite! Focus and give it another shot.");
                AnsiConsole.Clear();
                minigames[1].Run();
            }
            player.UpdateMinigameStatus(minigames[1].GetType().Name, true); // Update the minigame status
            Utility.PrintStory("Wonderful job, you have successfully escaped the incident by quickly entering the pin-code. Your sharp thinking has prevented any major damage. Which correlates to the 12th SDG by having responsible production.\nTime to move to the next challenge!");

            Utility.PrintStory("Halfway there!\n\nYou are doing great and are halfway to completing the final challenge. Let us head to the next room and see what awaits you!");

            if (minigames[1].Score == 6) {
                Score += 10;
            }
            else if (minigames[1].Score == 5) {
                Score += 9;
            }
            else if (minigames[1].Score == 4) {
                Score += 8;
            }
            else if (minigames[1].Score == 3) {
                Score += 7;
            }
            else if (minigames[1].Score == 2) {
                Score += 6;
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