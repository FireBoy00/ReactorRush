using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class ReactorCore : IRooms
    {
        public int Score { get; private set; }
        private readonly int minigameIndex = 5; // Index of the minigame in the minigames list
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel(Player player)
        {
            Console.Title = "Reactor Core";
            Score = 0;
            AnsiConsole.Clear();

            Utility.Narrator = "Reactor Safety Engineer"; //Reactor Core Technician or Critical Systems Technician or Reactor Safety Engineer
            Utility.PrintStory("Welcome to the last room of the nuclear reactor! It operates under high pressure and requires constant monitoring to ensure stability. As the control room operator, you are responsible for keeping things running smoothly. However, sometimes things go wrong, and you will need to quickly fix any valve (a device for controlling the passage of fluid) failures that may occur. ");
            Utility.PrintStory("The reactor core, with its carefully designed hexagonal fuel blocks and graphite moderator, operates with precision. Overheating or coolant loss can lead to shutdowns, but your task today is to prevent a failure within the core's intricate systems.");
            Utility.PrintStory("I have a crucial task for you in this room, but I'm sure you can handle it, after all, you already know almost everything about how the reactor works." + "\n\nWelcome to Reactor Line Check, a place where you can test the operational stability of a nuclear reactor system. Your mission is to verify the system's integrity by marking stable points on the reactor grid while addressing unexpected errors.");

            string prompt1 = Utility.Prompt("Do you want to know how to operate this system?", ["Yes", "No"]);
            if (prompt1 == "Yes")
            {
                Utility.PrintStory("Okay, here is the instruction of how the system works\n\nArrange 5 '+' symbols in a row - vertically, horizontally or diagonally - confirming the reactor's stability.\n\nDuring your checks, unexpected errors may occur, marked by a '!' symbol. To remove '!' click on the field where it is located." +
                "\n\nMove your cursor to a grid cell using arrow keys. Press Enter or Space to mark a stable point with a '+' or remove '!'.\n\nGood luck, Reactor Technician! Can you stabilize the reactor in time?");
            }
            else
            {
                prompt1 = Utility.Prompt("Okay, as you wish, but be careful not to break anything, that would be a disaster. Are you sure you don't want to see the instructions?", ["Yes", "No"]);
                if (prompt1 == "No")
                {
                    Utility.PrintStory("Okay, here is the instruction of how the system works\n\nArrange 5 '+' symbols in a row - vertically, horizontally or diagonally - confirming the reactor's stability.\n\nDuring your checks, unexpected errors may occur, marked by a '!' symbol. To remove '!' click on the field where it is located." +
                    "\n\nMove your cursor to a grid cell using arrow keys. Press Enter or Space to mark a stable point with a '+' or remove '!'.\n\nGood luck, Reactor Technician! Can you stabilize the reactor in time?");
                }
            }
            AnsiConsole.Clear();
            minigames[minigameIndex].Run();
            Console.Title = "Reactor Core";
            if (minigames[minigameIndex].Score == 1)
            {
                player.UpdateMinigameStatus(minigames[minigameIndex].GetType().Name, true); // Update the minigame status
                Utility.PrintStory("The valve system stabilized. Reactor core pressure and temperature are within safe limits. Well done, operator.");
                Score += 10;
            }
            else
            {
                Utility.PrintStory("Critical valve failure detected. Reactor core overheating. Immediate action required. This time you cannot make a mistake. Try again! ");
                do
                {
                    AnsiConsole.Clear();
                    minigames[minigameIndex].Run();
                    Console.Title = "Reactor Core";
                    if (minigames[minigameIndex].Score == 1)
                    {
                        player.UpdateMinigameStatus(minigames[minigameIndex].GetType().Name, true); // Update the minigame status
                        Utility.PrintStory("Success!");
                        Score += 5;
                    }
                    else
                    {
                        Utility.PrintStory("Critical valve failure detected. Reactor core overheating. Immediate action required. This time you cannot make a mistake. Try again! ");
                    }
                } while (minigames[minigameIndex].Score == 0);
            }

            Utility.PrintStory("Congratulations, you have ensured the reactor's continued safety.");

            AnsiConsole.Clear();
            player.UpdateRoomStatus(this.GetType().Name, Score > 0); // Update the room status
            return Score;
        }
    }
}