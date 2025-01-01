using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class CoolingSystem : IRooms
    {
        private int score = 0;
        private readonly List<IMinigame> minigames = MinigameList.Minigames;
        
        public int StartLevel() {
            AnsiConsole.Clear();

            Utility.Narrator = "Operator";
            var welcomeMsg = "Cooling System room - the hero of the nuclear reactor! This is where operators manage heat transfer, keep coolant flowing smoothly, and ensure the pipes and pumps are in perfect shape. A working cooling system is what keeps the reactor safe and prevents any overheating disasters.\nUh-oh! We have a problem.\nOne of the pipes underneath the Containment building has cracked a leak, and it’s up to you to fix it before things get out of the control. Time to roll up your sleeves and get to work!";
            Utility.PrintStory(welcomeMsg);

            AnsiConsole.Clear();
            minigames[4].Run();
 
            int NumberOfTries = 1;
            if (minigames[4].Score >= 40) {
                Utility.PrintStory("Oh no! You couldn’t repair the broken pipe, and it led to a disaster.\nThis is the most likely disaster in a nuclear power plant: the cooling system failed, causing rapid overheating. Normally, reactors are designed in such a way that they cannot form a supercritical mass of fissionable material and therefore cannot create a nuclear explosion. However, failures of systems and safeguards can cause catastrophic accidents, including chemical explosions and nuclear meltdowns.\nYou can still try to repair it, so try again!");
                NumberOfTries++;
                AnsiConsole.Clear();
                minigames[4].Run();
                if (minigames[4].Score < 40) {
                    Utility.PrintStory("Success! You did it! Now the reactor is safe, and you can move to the next room.");
                }
                else {
                    Utility.PrintStory("Try one more time");
                    NumberOfTries++;
                    AnsiConsole.Clear();
                    minigames[4].Run();
                }
            }
            else {
                Utility.PrintStory("Congratulations, you’ve managed to repair the broken pipe and therefore avoid a disaster.\nThe cooling system is one of the most important parts in a reactor – if not the most. Power plants are designed in such a way that they cannot form a supercritical mass of fissionable material and therefore cannot create a nuclear explosion. However, failures of systems and safeguards can cause catastrophic accidents, including chemical explosions and nuclear meltdowns.\nGreat job - now head to the next room for your next challenge."); 
            }

            if (minigames[4].Score < 16) {
                score = 5;
            } 
            else if (minigames[4].Score < 21) {
                score = 4;
            }
            else if (minigames[4].Score < 26) {
                score = 3;
            }
            else if (minigames[4].Score < 31) {
                score = 2;
            }
            else {
                score = 1;
            }

            AnsiConsole.Clear();
            return score;
        }
    }
}