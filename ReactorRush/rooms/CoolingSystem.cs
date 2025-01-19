using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class CoolingSystem : IRooms
    {
        public int Score { get; private set; }
        private readonly int minigameIndex = 4; // Index of the minigame in the minigames list
        private readonly List<IMinigame> minigames = MinigameList.Minigames;
        
        public int StartLevel(Player player)
        {
            Console.Title = "Cooling System";
            Score = 0;
            AnsiConsole.Clear();

            Utility.Narrator = "Operator";
            var welcomeMsg = "Cooling System room - the hero of the nuclear reactor! This is where operators manage heat transfer, keep coolant flowing smoothly, and ensure the pipes and pumps are in perfect shape. A working cooling system is what keeps the reactor safe and prevents any overheating disasters.\nUh-oh! We have a problem.\nOne of the pipes underneath the Containment building has cracked a leak, and it’s up to you to fix it before things get out of the control. Time to roll up your sleeves and get to work!";
            Utility.PrintStory(welcomeMsg);

            AnsiConsole.Clear();
            Console.Title = $"Cooling System - {minigames[minigameIndex].Name}";
            minigames[minigameIndex].Run();
            Console.Title = "Cooling System";
 
            int NumberOfTries = 1;
            if (minigames[minigameIndex].Score >= 40) {
                Utility.PrintStory("Oh no! You couldn’t repair the broken pipe, and it led to a disaster.\nThis is the most likely disaster in a nuclear power plant: the cooling system failed, causing rapid overheating. Normally, reactors are designed in such a way that they cannot form a supercritical mass of fissionable material and therefore cannot create a nuclear explosion. However, failures of systems and safeguards can cause catastrophic accidents, including chemical explosions and nuclear meltdowns.\nYou can still try to repair it, so try again!");
                NumberOfTries++;
                AnsiConsole.Clear();
                Console.Title = $"Cooling System - {minigames[minigameIndex].Name}";
                minigames[minigameIndex].Run();
                Console.Title = "Cooling System";
                if (minigames[minigameIndex].Score < 40) {
                    player.UpdateMinigameStatus(minigames[minigameIndex].GetType().Name, true); // Update the minigame status
                    Utility.PrintStory("Success! You did it! Now the reactor is safe, and you can move to the next room.");
                }
                else {
                    Utility.PrintStory("Try one more time");
                    NumberOfTries++;
                    AnsiConsole.Clear();
                    Console.Title = $"Cooling System - {minigames[minigameIndex].Name}";
                    minigames[minigameIndex].Run();
                    Console.Title = "Cooling System";
                }
            }
            else {
                player.UpdateMinigameStatus(minigames[minigameIndex].GetType().Name, true); // Update the minigame status
                Utility.PrintStory("Congratulations, you’ve managed to repair the broken pipe and therefore avoid a disaster.\nThe cooling system is one of the most important parts in a reactor – if not the most. Power plants are designed in such a way that they cannot form a supercritical mass of fissionable material and therefore cannot create a nuclear explosion. However, failures of systems and safeguards can cause catastrophic accidents, including chemical explosions and nuclear meltdowns.\nGreat job - now head to the next room for your next challenge."); 
            }

            if (minigames[minigameIndex].Score < 16) {
                Score += 10;
            } 
            else if (minigames[minigameIndex].Score < 21) {
                Score += 8;
            }
            else if (minigames[minigameIndex].Score < 26) {
                Score += 6;
            }
            else if (minigames[minigameIndex].Score < 31) {
                Score += 4;
            }
            else {
                Score += 2;
            }

            AnsiConsole.Clear();
            player.UpdateRoomStatus(this.GetType().Name, Score > 0); // Update the room status
            return Score;
        }
    }
}