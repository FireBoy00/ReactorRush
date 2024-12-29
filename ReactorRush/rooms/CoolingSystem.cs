using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class CoolingSystem : IRooms
    {
        private int score = 0;
        private readonly List<IMinigame> minigames = new List<IMinigame>();
        
        public int StartLevel() {
            AnsiConsole.Clear();

            Utility.Narrator = "Operator";
            var welcomeMsg = "Cooling System room - the hero of the nuclear reactor! This is where operators manage heat transfer, keep coolant flowing smoothly, and ensure the pipes and pumps are in perfect shape. A working cooling system is what keeps the reactor safe and prevents any overheating disasters.\nUh-oh! We have a problem.\nOne of the pipes underneath the Containment building has cracked a leak, and it’s up to you to fix it before things get out of the control. Time to roll up your sleeves and get to work!";
            Utility.PrintStory(welcomeMsg);
            // minigames[{number of Pipe Repair Game}].Run();

            bool IsWon = false; // I need this value from the game
            int NumberOfTries;
            if (IsWon) {
                if (NumberOfTries == 1) {
                    Utility.PrintStory("Congratulations, you’ve managed to repair the broken pipe and therefore avoid a disaster.\nThe cooling system is one of the most important parts in a reactor – if not the most. Power plants are designed in such a way that they cannot form a supercritical mass of fissionable material and therefore cannot create a nuclear explosion. However, failures of systems and safeguards can cause catastrophic accidents, including chemical explosions and nuclear meltdowns.\nGreat job - now head to the next room for your next challenge.");
                }
                else {
                    Utility.PrintStory("Success! You did it! Now the reactor is safe, and you can move to the next room.");
                }
            }
            else {
                Utility.PrintStory("Oh no! You couldn’t repair the broken pipe, and it led to a disaster.\nThis is the most likely disaster in a nuclear power plant: the cooling system failed, causing rapid overheating. Normally, reactors are designed in such a way that they cannot form a supercritical mass of fissionable material and therefore cannot create a nuclear explosion. However, failures of systems and safeguards can cause catastrophic accidents, including chemical explosions and nuclear meltdowns.\nYou can still try to repair it, so try again!");
                // minigames[{number of Pipe Repair Game}].Run();
            }

            int EnergyUsed = 10;  // instead of this, I need this value from the game
            if (EnergyUsed < 9) {
                score = 5;
                Utility.PrintStory("Excellent job! You managed to repair the pipe in less than 9 steps.");
                // I'm not sure if we need these messages
            } 
            else if (EnergyUsed == 9) {
                score = 4;
                Utility.PrintStory("Well done! I see that you repaired the pipe quite easily.");
            }
            else if (EnergyUsed == 10) {
                score = 3;
                Utility.PrintStory("Congratulation! You were able to repair the pipe in only 10 steps.");
            }
            else if (EnergyUsed == 11) {
                score = 2;
                Utility.PrintStory("There's alway room for improvement, but the most important thing is that you repaired the pipe.");
            }
            else {
                score = 1;
                Utility.PrintStory("You've managed to repair the pipe, although it took you quite long.");
            }

            AnsiConsole.Clear();
            return score;
        }
    }
}