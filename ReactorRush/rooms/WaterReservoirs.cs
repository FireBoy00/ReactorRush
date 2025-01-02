using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class WaterReservoir : IRooms
    {
        public int Score { get; private set; }
        private readonly List<IMinigame> minigames = MinigameList.Minigames;
        private readonly int minigameIndex = 9;
        public int StartLevel(Player player)
        {
            Score = 0;
            AnsiConsole.Clear();

            Utility.Narrator = "Nuclear Energy Specialist";
            Utility.PrintStory("This room plays a critical role in ensuring the reactor operates efficiently and safely. Water is a vital component in the cooling and energy generation process of a nuclear power plant, and here, you will learn how this system works and why maintaining it is essential. ");
            Utility.PrintStory("I think this is the right time to talk about its purpose, what is it?");
            Utility.PrintStory("The Water Reservoir is a key part of the plant’s secondary cooling system. It stores and supplies water that condenses steam discharged from the turbines. This ensures a continues cycle of steam generation and condensation, which is crucial for maintaining the efficiency and safety of the plant.");
            string prompt12 = Utility.Prompt("Did you know that the Water Reservoir helps prevent potential hazards?", ["Yes, tell me more", "No, I never thought or saw anything about it"]);
            Utility.PrintStory("It condenses any steam that might escape, avoiding pressure buildup and ensuring that hydrogen does not mix with oxygen to create an explosive environment. A nitrogen atmosphere in the reservoir further safeguards against it.");
            Utility.PrintStory("What exactly happens in the room? ");
            Utility.PrintStory("Firstly, steam exiting the turbines is cooled in the condenser and returned to water, which collects in the reservoir for reuse. Then the water is purified to remove impurities, protecting the steam generator and ensuring efficient heat transfer. And finally heaters regulate the water's temperature, optimizing plant efficiency. ");
            Utility.PrintStory("We have some questions for you about this reservoir, do you think you could answer them correctly? ");
            do
            {
                prompt12 = Utility.Prompt("Why is it essential to remove impurities from the water in the reservoir before it enters the steam generator?", ["To prevent steam explosion risks.", "To reduce heat transfer efficiency.", "To avoid damage to the steam generator and maintain efficiency.", "To lower the water pressure in the system."]);


                if (prompt12 == "To prevent steam explosion risks.") {
                    Utility.PrintStory("Incorrect! Try again.");
                Score -= 2;
            }
                else if (prompt12 == "To reduce heat transfer efficiency.") {
                    Utility.PrintStory("Incorrect! Try again.");
                Score -= 2;
            }    
                else if (prompt12 == "To avoid damage to the steam generator and maintain efficiency.") {
                    Utility.PrintStory("You chose the right answer, procced with your given task. Good luck!");
                Score += 10;
            }    
                else if (prompt12 == "To lower the water pressure in the system.") {
                    Utility.PrintStory("Incorrect! Try again.");
                Score -= 2;
            }    
            } while (prompt12 != "To avoid damage to the steam generator and maintain efficiency.");
            Utility.PrintStory("You are faced with a practical challenge: a sudden swing in water levels! The system alerts you to correct the water level to maintain stability. ");
            Utility.PrintStory("Your task is to adjust valves to stabilize the water levels. Watch the indicators closely and react quickly to prevent overflows or shortages. ");

            var waterLevelsGame = (WaterLevels)minigames[minigameIndex]; // Cast to access WaterLevels-specific properties
            if (minigames.Count > minigameIndex)
            {
                bool firstAttempt = true; // Flag to check if it's the first attempt
                waterLevelsGame.Run();

                int waterLevel = waterLevelsGame.GetWaterLevel();
                int score = waterLevelsGame.Score; // Access the Score

                while (waterLevel == 0 || waterLevel == 1 || waterLevel == 9 || waterLevel == 10)
                {
                    Utility.PrintStory("Water levels are still critical! Please try again to prevent a system shutdown.");
                    waterLevelsGame.Run();
                    waterLevel = waterLevelsGame.GetWaterLevel();
                    firstAttempt = false;
                }

                if (firstAttempt)
                {
                    Utility.PrintStory("Excellent work! The water levels are stable, and the system is functioning perfectly! You may proceed to the next room. ");
                }else {
                    Utility.PrintStory("The water levels are stable, and the system is functioning perfectly! You may proceed to the next room.");
                }
                Score += score;
            }
            else
            {
                Utility.PrintStory($"{waterLevelsGame.GetType().Name} minigame is not available.");
            }

            AnsiConsole.Clear();
            player.UpdateRoomStatus(this.GetType().Name, Score > 0); // Update the room status
            return Score;
        }
    }
}