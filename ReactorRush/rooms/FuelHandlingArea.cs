using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class FuelHandlingArea : IRooms
    {
        public int Score { get; private set; }
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel(Player player) {
            AnsiConsole.Clear();

            Utility.Narrator = "Operator";
            var welcomeMsg = "Here, you’ll learn how nuclear fuel is safely managed and why proper handling is essential for efficient energy production and safety. Let’s dive into its key features and importance. ";
            Utility.PrintStory(welcomeMsg);

            AnsiConsole.Clear();
            var welcomeMsg2 = "What happens in the Fuel Handling Area?\nThe fuel handling area is where nuclear fuel, typically uranium, is carefully stored, transported, and prepared for use in the reactor. It is also where spent fuel is safely removed and temporarily stored before further processing or disposal. This area plays a vital role in maintaining the reactor’s efficiency and ensuring safety at every stage of the fuel cycle.";
            Utility.PrintStory(welcomeMsg2); 

            AnsiConsole.Clear();
            var welcomeMsg3 = "You should know:\nPublic perception of nuclear energy often emphasizes safety concerns, especially about radioactive materials. This is why nuclear reactors, along with their fuel handling areas, are located away from residential areas. This ensures that, in the rare event of an incident, the impact on people and the environment is minimized.";
            Utility.PrintStory(welcomeMsg3);

            AnsiConsole.Clear();
            var welcomeMsg4 = "Test your knowledge about nuclear reactors with a memory game! Match cards based on your knowledge.";
            Utility.PrintStory(welcomeMsg4);

            AnsiConsole.Clear();
            minigames[6].Run();

            int NumberOfTries = 1;
            if (minigames[6].Score < 6) {
                Utility.PrintStory("Oops! Try again to strengthen your understanding. Pay close attention to the key roles and concepts before your next attempt!");
                NumberOfTries++;
                AnsiConsole.Clear();
                minigames[6].Run();
                if (minigames[6].Score == 6) {
                    Utility.PrintStory("Good job! Now you understand the main concepts of a reactor. Keep going!");
                }
                else {
                    Utility.PrintStory("Try one more time");
                    NumberOfTries++;
                    AnsiConsole.Clear();
                    minigames[6].Run();
                }
            }
            else {
                Utility.PrintStory("Congratulations! You have matched all the cards and answered correctly. You are now one step closer to mastering nuclear energy systems!"); 
            }

            if (NumberOfTries == 1) {
                Score = 5;
            } 
            else if (NumberOfTries == 2) {
                Score = 3;
            }
            else {
                Score = 1;
            }

            AnsiConsole.Clear();
            player.UpdateRoomStatus(this.GetType().Name, Score > 0); // Update the room status
            return Score;
        }
    }
}