using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class VisitorCenter : IRooms
    {
        string prompt3 = "";
        bool isCorrect = false;
        bool correctSDG1 = false;
        bool correctSDG2 = false;
        string sdgInput1 = ""; 
        string sdgInput2 = "";
        public int Score { get; private set; }

        public int StartLevel(Player player)
        {
            Score = 0;
            correctSDG1 = false;
            correctSDG2 = false;
            sdgInput1 = "";
            sdgInput2 = "";

            AnsiConsole.Clear();

            Utility.Narrator = "Nuclear Energy Specialist";
            string prompt1 = Utility.Prompt("Welcome aboard Reactor Rush, where the future of sustainable energy begins. Inspired by the 7th and 12th Sustainable Development Goals, it's designed to tackle the energy challenges of today. Are you familiar with these goals? Let's find out together!", ["Yes", "No"]);
            if (prompt1 == "Yes")
            {
                Utility.PrintStory($"You chose: {prompt1}\nPerfect! In that case let us skip the details. Stay tuned for the game idea!");
            }
            else
            {
                Utility.PrintStory($"You chose: {prompt1}\nI see, well it's not hard to understand it all so here's a short breakdown. The \"Sustainable Development Goals\", or SDGs for short, are 17 global goals created by the United Nations to make the world a better place by the year 2030. They focus on big issues like ending poverty, protecting the planet, and ensuring everyone has access to education, clean energy and good health. In short, the idea is for everyone - governments, businesses and individuals - to work together to create a fairer, healthier, and more sustainable future for all. Isn't that just great? We here focus more on the ones I mentioned earlier, the 7th one - which focuses on ensuring access to affordable, reliable, sustainable, and modern energy for all, and the 12th one - which emphasizes on ensuring sustainable consumption and production patterns.\n\nThese goals are critical in addressing some of the most pressing challenges facing our world today, including climate change, resource depletion, and environmental degradation.\n\nNow that we have got the details out of the way, you can proceed to get to know our game idea and then finish with some questions.");
            }

            Utility.PrintStory("You're about to spend approximately 30 minutes discovering the incredible potential of nuclear energy. It's a subject that gets people talking - but how much of that talk is grounded in reality? Let's clear things up.");
            Utility.PrintStory("Why nuclear, you ask? Well, while it's not considered renewable, nuclear fuel uses a tiny amount to produce electricity equivalent to what coal or gas would burn entire forests for. That's right - nuclear is the “overachiever” of energy sources: small effort and big results.");
            string prompt2 = Utility.Prompt("This game is not just about pressing buttons and watching the game flow. It is your chance to step into the reactor room and tackle the big decisions. Does managing energy production, handling waste and tackling the ups and downs of running high-tech facility sound like something you would enjoy doing today?", ["Yes", "Yes by all means"]);
            Utility.PrintStory("Good. While this is designed to entertain, it is also meant to educate. By the end, we hope you'll feel empowered to tackle sustainable energy challenges—and maybe even dream up your own solutions.\nYour little journey into the heart of sustainable energy starts now!");

            while (!isCorrect)
            {
                prompt3 = Utility.Prompt("What does SDG stand for?", ["Strategic Development Guide (SDG)", "Sustainable Development Goals (SDG)", "Sustainable Destiny Goals (SDG)"]);

                if (prompt3 == "Sustainable Development Goals (SDG)")
                {
                    Utility.PrintStory($"You chose: {prompt3}\nCorrect! Well done! Let us move on.");
                    isCorrect = true;
                    Score += 10;
                }
                else if (prompt3 == "Strategic Development Guide (SDG)")
                {
                    Utility.PrintStory($"You chose: {prompt3}\nIncorrect! Try again. Remember, it is related to sustainability!");
                    Score -= 2;
                }
                else if (prompt3 == "Sustainable Destiny Goals (SDG)")
                {
                    Utility.PrintStory($"You chose: {prompt3}\nClose… But not quite,  give it another shot!");
                    Score -= 2;
                }
                else
                {
                    Utility.PrintStory("Invalid selection. Please try again.");
                }
            }

            Utility.PrintStory("Proceeding to the next section...");
            Utility.PrintStory("Do you remember on which SDGs we are focusing?");

            while (!correctSDG1)
            {
                sdgInput1 = Utility.PromptTextInput("Enter the first SDG number:");
                if (sdgInput1 == "7" || sdgInput1 == "12")
                {
                    Utility.PrintStory("Good! Now enter the second SDG number.");
                    correctSDG1 = true;
                }
                else
                {
                    if (Score < 0)
                    {
                        Score = 0;
                        Utility.PrintStory("The first number is incorrect. \nYour score has been reset to 0. \nTry again!");
                    }else {
                        Score -= 1;
                        Utility.PrintStory("The first number is incorrect. \nYou lose 1 point. \nTry again!");
                    }
                }
            }

            while (!correctSDG2) 
            {
                sdgInput2 = Utility.PromptTextInput("Enter the second SDG number:");
                if ((sdgInput1 == "7" && sdgInput2 == "12") || (sdgInput1 == "12" && sdgInput2 == "7"))
                {
                    Utility.PrintStory("Impressive! You have a great memory. Let us proceed!");
                    correctSDG2 = true;
                    Score += 10;
                }
                else
                {
                    if (Score < 0)
                    {
                        Score = 0;
                        Utility.PrintStory("The second number is incorrect. \nYour score has been reset to 0. \nTry again!");
                    }else {
                        Score -= 1;
                        Utility.PrintStory("The second number is incorrect. \nYou lose 1 point. \nTry again!");
                    }
                }
            }

            AnsiConsole.Clear();
            player.UpdateRoomStatus(this.GetType().Name, Score > 0); // Update the room status
            return Score;
        }
    }
}