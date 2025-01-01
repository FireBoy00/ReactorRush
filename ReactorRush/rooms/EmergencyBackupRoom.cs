using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class EmergencyBackupRoom : IRooms
    {
        public int Score { get; private set; }
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel(Player player)
        {
            AnsiConsole.Clear();

            Utility.Narrator = "Critical Support Technician"; //Critical Support Technician or Backup Operations Technician

            Utility.PrintStory("The Emergency Backup room is a critical part of the nuclear power plant’s safety system. Its primary role is to ensure that essential functions can continue during power outages, preventing potential disasters. This room houses backup power systems designed to maintain operations like cooling spent fuel, managing remained decay heat from the reactor, and supporting crucial services such as system controls, communication, lighting, and ventilation. ");
            Utility.PrintStory("What are the functionality of this room you may ask me and I would tell you..." +
            "\n   1. Reliability in Crisis, backup systems are activated during a power failure immediately."
            + "\n  2. Different solutions of redundancy, including fuel-based generators, battery backups, and sometimes compressed air for auxiliary systems. These ensure resilience in various scenarios, including natural disasters like tsunamis or human-caused threats."
            + "\n  3. All backup systems are tested and maintained regularly, for example, batteries must have high energy density and quick discharge capabilities.");

            string prompt1 = Utility.Prompt("As you stand in this crucial facility, lets assess your understanding of its purpose\n\nWhat is the key role of the Emergency Backup room?", ["To store extra tools for maintenance.", "To provide emergency power during outages.", "To monitor radiation levels in real-time."]);

            switch (prompt1)
            {
                case "To store extra tools for maintenance.":
                    Utility.PrintStory("Wrong, do you think we actually need a separate room to store extra tools, no...");
                    break;
                case "To provide emergency power during outages.":
                    Utility.PrintStory("Of course this is right, because all the time we were talking about backup systems.");
                    Score += 2;
                    break;
                case "To monitor radiation levels in real-time.":
                    Utility.PrintStory("Wrong, that is what Radiation Monitoring room is for (we will introduce that to you later).");
                    break;
            }

            prompt1 = Utility.Prompt("Now think about what could potentially happen if the Emergency Backup room's systems failed during a crisis?", ["Operations would pause temporarily but resume shortly.", "It could lead to the overheating of reactor components and increased risks of the accident.", "Nothing significant, the plant has no critical need for backup power."]);

            switch (prompt1)
            {
                case "Operations would pause temporarily but resume shortly.":
                    Utility.PrintStory("Wrong! Read the question again “if <…> failed during crisis.”");
                    break;
                case "It could lead to the overheating of reactor components and increased risks of the accident.":
                    Utility.PrintStory("Right, we do not want something similar, so it is important to have such room, test and maintain it.");
                    Score += 2;
                    break;
                case "Nothing significant, the plant has no critical need for backup power.":
                    Utility.PrintStory("Not true, like almost everything in the world, it needs backup resources. Do not forget that we are speaking about the nuclear reactor, which includes radiation.");
                    break;
            }
            Utility.PrintStory("Now that you understand the purpose of the Emergency Backup room, it is time to put your skills to the test. During an unexpected outage, a critical power cable has been damaged. It is your job to fix the cables and restore backup power to the system. \n\nYou need to drag and connect the left and right sides of the cable.");
            minigames[0].Run();
            if(minigames[0].Score == 1)
            //if (true)
            {
                Utility.PrintStory("Well done! The backup system is fully operational, and you have ensured the reactor’s safety. Move on to the next room to continue.");
            }
            else
            {
                Utility.PrintStory("Close, but not quite! Focus and try again to restore power");
                do
                {
                    minigames[0].Run();
                } while (minigames[0].Score == 0);
                Utility.PrintStory("Well done, move on to the next room to continue the game.");
            }
            AnsiConsole.Clear();
            player.UpdateRoomStatus(this.GetType().Name, Score > 0); // Update the room status
            return Score;
        }
    }
}