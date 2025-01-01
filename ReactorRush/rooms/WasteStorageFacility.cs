using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class WasteStorageFacility : IRooms
    {
        public int Score { get; private set; }
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel(Player player)
        {
            AnsiConsole.Clear();

            Utility.Narrator = "Nuclear Energy Specialist";

            Utility.PrintStory("This room is one of the most vital areas of the nuclear reactor facility. It is here that we safely store radioactive waste to protect nature, animals, people and the energy we generate. Proper waste storage ensures the safety of the entire operation and is a big step toward making nuclear energy both reliable and sustainable. ");
            Utility.PrintStory("Why is waste storage so important?");
            Utility.PrintStory("Radioactive materials remain dangerous for a long time, but if stored properly, the risk of radiation exposure drops drastically. Think of it like storing tea or coffee beans in airtight containers to keep all the flavor - only here we are dealing with waste that ensures the reactor operates safely without any danger to the environment or organisms.");
            Utility.PrintStory("By following strict safety procedures, we not only keep the surroundings safe but also align with Sustainable Development Goal 7 (affordable and clean energy) and Goal 12 (responsible consumption and production). Nuclear energy may not be renewable, but its waste, when handled responsibly, minimizes environmental impact, supporting a cleaner future.");
            Utility.PrintStory("Sooo let us check if you understood the radiation specifications.");

            string prompt1 = Utility.Prompt("Does radioactive waste lose its radioactivity over time?", ["Yes", "No"]);
            if (prompt1 == "Yes")
            {
                Utility.PrintStory($"You chose: {prompt1}\nOf course it does, high-level waste, for example, is stored for decades to allow its radioactivity to decay, making it much safer to handle and dispose of later.");
                Score += 10;
            }
            else
            {
                Utility.PrintStory($"You chose: {prompt1}\nIt does lose its radioactivity over time. You are wrong!");
                Score -= 2;
            }

            AnsiConsole.Clear();
            Utility.PrintStory("Your turn has come, help maintain this crucial system! One of the waste containers has shifted, so you should transfer it to another one before any issues arise.");

            if (minigames.Count >= 5)
            {
                minigames[6].Run();

                Utility.PrintStory("Wonderful job! You successfully secured the radioactive waste container, preventing a potential hazard. This is exactly how responsible waste management works - protecting people, the environment, and the future of nuclear energy. Now let's move to the next challenge!");
                Score += minigames[6].Score;
            }
            else
            {
                Utility.PrintStory("Waste transfer minigame is not available.");
            }

            Thread.Sleep(1000);

            AnsiConsole.Clear();
            player.UpdateRoomStatus(this.GetType().Name, Score > 0); // Update the room status
            return Score;
        }
    }
}