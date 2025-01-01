using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class Laboratory : IRooms
    {
        private int score = 0;
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel()
        {
            AnsiConsole.Clear();

            Utility.Narrator = "Nuclear Energy Specialist";


            Utility.PrintStory("You have reached the Laboratory of nuclear innovation. It is important to note that not every nuclear reactor includes a laboratory on-site: these are specialized facilities found in research or advanced industrial reactors rather than standard energy-producing reactors. We think that you as a player should know that there are separate reactors where scientists are testing and finding new ways to secure, get the most of energy. Research reactors are where scientific breakthroughs are made, materials are tested under extreme conditions, and the potential of nuclear energy is unlocked.");
            Utility.PrintStory("But what exactly happens here? Let us dive in!");
            Utility.PrintStory("Research reactors are the representation of scientific exploration in the nuclear field. Unlike power reactors that generate electricity, these reactors produce neutron beams for research and development. These beams help scientists study materials at the atomic level, analyze chemical compositions, and even create life-saving medical isotopes.");
            Utility.PrintStory("Interesting fact!\n\nOver 800 research reactors have been built worldwide. Most of them are multi-purpose, and many have been operational for more than 30 years. Russia and the USA are leading in operational reactors. ");

            Utility.PrintStory("Quick Check!");
            string prompt1 = Utility.Prompt("What is the primary purpose of research reactors?", ["Generating electricity", "Producing neutron beams for research", "Powering spacecraft"]);
            if (prompt1 == "Generating electricity")
            {
                Utility.PrintStory($"You chose: {prompt1}\nIncorrect! The right one is below.");
            }
            else if (prompt1 == "Producing neutron beams for research")
            {
                Utility.PrintStory($"You chose: {prompt1}\nCorrect!");
            }
            else
            {
                Utility.PrintStory($"You chose: {prompt1}\nIncorrect! The right one is above.");
            }
            Utility.PrintStory("\nWhich country has the most research reactors?");
            string[] options = { "China", "India", "USA", "Russia" };

            while (true)
            {
                string selected = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select a country:")
                        .AddChoices(options));

                switch (selected)
                {
                    case "China":
                        AnsiConsole.MarkupLine("China");
                        AnsiConsole.MarkupLine("Incorrect! It has 16 research reactors.");
                        score -= 2;
                        break;

                    case "India":
                        AnsiConsole.MarkupLine("India");
                        AnsiConsole.MarkupLine("Incorrect! It has 7  research reactors.");
                        score -= 2;
                        break;

                    case "USA":
                        AnsiConsole.MarkupLine("USA ");
                        AnsiConsole.MarkupLine("Correct! It is in second place and has 50  research reactors.");
                        score += 3;
                        break;

                    case "Russia":
                        AnsiConsole.MarkupLine("Russia ");
                        AnsiConsole.MarkupLine("Correct! It is in the first place and has 52 research reactors.");
                        score += 5;
                        break;

                    default:
                        AnsiConsole.MarkupLine("Invalid choice!");
                        continue;
                }
                break;
            }

            string prompt2 = Utility.Prompt("Our research capabilities are always evolving. Now it is your turn to contribute! We're looking for innovative ideas to push the boundaries of what research reactors can achieve. Choose an area to suggest advancements:", ["Medical Advancements: focus on enhancing isotope production or exploring new medical applications", "Material Science: propose ideas for testing materials or developing next-generation alloys", "Energy Innovations: explore how research reactors can support cleaner energy solutions"]);
            switch (prompt2)
            {
                case "Medical Advancements: focus on enhancing isotope production or exploring new medical applications":
                    AnsiConsole.MarkupLine("Medical Advancements: focus on enhancing isotope production or exploring new medical applications");
                    string prompt3 = Utility.Prompt("What other medical applications could benefit from neutron irradiation?", ["Cancer treatment", "Sterilization of medical equipment", "Imaging for detailed scans of soft tissues"]);
                    Utility.PrintStory($"You chose {prompt2} and you suggested {prompt3}");
                    score += 5;
                    break;

                case "Material Science: propose ideas for testing materials or developing next-generation alloys":
                    AnsiConsole.MarkupLine("Material Science: propose ideas for testing materials or developing next-generation alloys");
                    string prompt4 = Utility.Prompt("What extreme conditions should we simulate to evaluate materials further?", ["High temperatures to mimic reactor cores", "Intense radiation exposure over extended periods", "Rapid pressure changes, referring to accidents in reactor"]);
                    Utility.PrintStory($"You chose {prompt2} and you suggested {prompt4}");
                    score += 5;
                    break;

                case "Energy Innovations: explore how research reactors can support cleaner energy solutions":
                    AnsiConsole.MarkupLine("Energy Innovations: explore how research reactors can support cleaner energy solutions");
                    string prompt5 = Utility.Prompt("Recycling spent fuel using advanced reprocessing", ["Using alternative fuel types that produce less waste"]);
                    Utility.PrintStory($"You chose {prompt2} and you suggested {prompt5}");
                    score += 5;
                    break;
            }
            Utility.PrintStory("Feedback Time!\n\nThank you for your suggestions! Remember, science thrives on collaboration and fresh perspectives.\n\nNow, you are ready to move on to the final room. The end awaits!");

            AnsiConsole.Clear();
            return score;
        }
    }
}