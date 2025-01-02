using System;
using Spectre.Console;
using ReactorRush;
using Minigames;

namespace Rooms
{
    public class Laboratory : IRooms
    {
        public int Score { get; private set; }
        private readonly List<IMinigame> minigames = MinigameList.Minigames;

        public int StartLevel(Player player)
        {
            AnsiConsole.Clear();

            Utility.Narrator = "Nuclear Energy Specialist";

            Utility.PrintStory("You have reached the Laboratory of nuclear innovation. It is important to note that not every nuclear reactor includes a laboratory on-site: these are specialized facilities found in research or advanced industrial reactors rather than standard energy-producing reactors. We think that you as a player should know that there are separate reactors where scientists are testing and finding new ways to secure, get the most of energy. Research reactors are where scientific breakthroughs are made, materials are tested under extreme conditions, and the potential of nuclear energy is unlocked.");
            Utility.PrintStory("But what exactly happens here? Let us dive in!");
            Utility.PrintStory("Research reactors are the representation of scientific exploration in the nuclear field. Unlike power reactors that generate electricity, these reactors produce neutron beams for research and development. These beams help scientists study materials at the atomic level, analyze chemical compositions, and even create life-saving medical isotopes.");
            Utility.PrintStory("Interesting fact!\n\nOver 800 research reactors have been built worldwide. Most of them are multi-purpose, and many have been operational for more than 30 years. Russia and the USA are leading in operational reactors. ");

            Utility.PrintStory($"Quick Check! We believe in you!");
            string prompt1 = Utility.Prompt("What is the primary purpose of research reactors?", ["Generating electricity", "Producing neutron beams for research", "Powering spacecraft"]);
            if (prompt1 == "Generating electricity")
            {
                Utility.PrintStory($"You chose: {prompt1}\nIncorrect! The right one is below.");
                Score -= 2;
            }
            else if (prompt1 == "Producing neutron beams for research")
            {
                Utility.PrintStory($"You chose: {prompt1}\nCorrect!");
                Score += 10;
            }
            else
            {
                Utility.PrintStory($"You chose: {prompt1}\nIncorrect! The right one is above.");
                Score -= 2;
            }

            while (true)
            {
                string prompt2 = Utility.Prompt("Which country has the most research reactors?", ["China", "India", "USA", "Russia"]);

                switch (prompt2)
                {
                    case "China":
                        AnsiConsole.MarkupLine("China");
                        Utility.PrintStory("Incorrect! It has 16 research reactors.");
                        Score -= 2;
                        string prompt7 = Utility.Prompt("Try again! Maybe this time you will choose the right one.", ["USA", "India", "Russia"]);
                        if (prompt7 == "USA")
                        {
                            Utility.PrintStory($"You chose: {prompt7}\nCorrect! It is in second place and has 50 research reactors.");
                            Score += 10;
                            string prompt14 = Utility.Prompt("What is the second one?", ["India", "Russia"]);

                            if (prompt14 == "Russia")
                            {
                                Utility.PrintStory($"You chose: {prompt14}\nCorrect! It is in the first place and has 52 research reactors.");
                                Score += 10;
                            }
                            else
                            {
                                Utility.PrintStory($"You chose: {prompt14}\nIncorrect! It has 7 research reactors.\n\nThe correct answers were Russia and USA.");
                                Score -= 2;
                            }
                        }
                        else if (prompt7 == "India")
                        {
                            Utility.PrintStory($"You chose: {prompt7}\nIncorrect! It has 7 research reactors.\n\nThe right answers were Russia and USA.");
                            Score -= 2;
                        }
                        else
                        {
                            Utility.PrintStory($"You chose: {prompt7}\nCorrect! It is in the first place and has 52 research reactors.");
                            Score += 10;

                            string prompt14 = Utility.Prompt("What is the second one?", ["India", "USA"]);

                            if (prompt14 == "USA")
                            {
                                Utility.PrintStory($"You chose: {prompt14}\nCorrect! It is in the first place and has 50 research reactors.");
                                Score += 10;
                            }
                            else
                            {
                                Utility.PrintStory($"You chose: {prompt14}\nIncorrect! It has 7 research reactors.\n\nThe correct answers were Russia and USA.");
                                Score -= 2;
                            }
                        }
                        break;

                    case "India":
                        AnsiConsole.MarkupLine("India");
                        Utility.PrintStory("Incorrect! It has 7 research reactors.");
                        Score -= 2;
                        string prompt8 = Utility.Prompt("Try again! Maybe this time you will choose the right one.", ["China", "USA", "Russia"]);
                        if (prompt8 == "USA")
                        {
                            Utility.PrintStory($"You chose: {prompt8}\nCorrect! It is in second place and has 50 research reactors.");
                            Score += 10;

                            string prompt15 = Utility.Prompt("What is the second one?", ["China", "Russia"]);
                            if (prompt15 == "Russia")
                            {
                                Utility.PrintStory($"You chose: {prompt15}\nCorrect! It is in the first place and has 52 research reactors.");
                                Score += 10;
                            }
                            else
                            {
                                Utility.PrintStory($"You chose: {prompt15}\nIncorrect! It has 16 research reactors.\n\nThe correct answers were Russia and USA.");
                                Score -= 2;
                            }
                        }
                        else if (prompt8 == "China")
                        {
                            Utility.PrintStory($"You chose: {prompt8}\nIncorrect! It has 16 research reactors.\n\nThe right answers were Russia and USA.");
                            Score -= 2;
                        }
                        else
                        {
                            Utility.PrintStory($"You chose: {prompt8}\nCorrect! It is in the first place and has 52 research reactors.");
                            Score += 10;

                            string prompt14 = Utility.Prompt("What is the second one?", ["China", "USA"]);
                            if (prompt14 == "USA")
                            {
                                Utility.PrintStory($"You chose: {prompt14}\nCorrect! It is in the first place and has 50 research reactors.");
                                Score += 10;
                            }
                            else
                            {
                                Utility.PrintStory($"You chose: {prompt14}\nIncorrect! It has 16 research reactors.\n\nThe correct answers were Russia and USA.");
                                Score -= 2;
                            }
                        }
                        break;

                    case "USA":
                        AnsiConsole.MarkupLine("USA");
                        Utility.PrintStory("Correct! It is in second place and has 50 research reactors.");
                        Score += 10;
                        string prompt9 = Utility.Prompt("What is the second one?", ["China", "India", "Russia"]);
                        if (prompt9 == "China")
                        {
                            Utility.PrintStory($"You chose: {prompt9}\nIncorrect! It is in second place and has 16 research reactors.");
                            Score -=2;
                            string prompt11 = Utility.Prompt("Try again! Maybe this time you will choose the right one.", ["India", "Russia"]);
                            if (prompt11 == "India")
                            {
                                Utility.PrintStory($"You chose: {prompt11}\nIncorrect! It has 7 research reactors.\n\nThe right answers were Russia and USA.");
                                Score -= 2;
                            }
                            else
                            {
                                Utility.PrintStory($"You chose: {prompt11}\nCorrect! It is in the first place and has 52 research reactors.");
                                Score += 10;
                            }
                        }
                        else if (prompt9 == "India")
                        {
                            Utility.PrintStory($"You chose: {prompt9}\nIncorrect! It has 7 research reactors.");
                            Score -=2;
                            string prompt12 = Utility.Prompt("Try again! Maybe this time you will choose the right one.", ["China", "Russia"]);
                            if (prompt12 == "China")
                            {
                                Utility.PrintStory($"You chose: {prompt12}\nIncorrect! It has 16 research reactors.\n\nThe right answers were Russia and USA.");
                                Score -= 2;
                            }
                            else
                            {
                                Utility.PrintStory($"You chose: {prompt12}\nCorrect! It is in the first place and has 52 research reactors.");
                                Score += 10;
                            }
                        }
                        else
                        {
                            Utility.PrintStory($"You chose: {prompt9}\nCorrect! It is in the first place and has 52 research reactors.");
                            Score += 10;
                        }
                        break;

                    case "Russia":
                        AnsiConsole.MarkupLine("Russia");
                        Utility.PrintStory("Correct! It is in the first place and has 52 research reactors.");
                        string prompt10 = Utility.Prompt("What is the second one?", ["China", "India", "USA"]);
                        Score += 10;
                        if (prompt10 == "China")
                        {
                            Utility.PrintStory($"You chose: {prompt10}\nIncorrect! It is in second place and has 16 research reactors.");
                            Score -=2;
                            string prompt13 = Utility.Prompt("Try again! Maybe this time you will choose the right one.", ["India", "Russia"]);
                            if (prompt13 == "India")
                            {
                                Utility.PrintStory($"You chose: {prompt13}\nIncorrect! It has 7 research reactors.\n\nThe right answers were Russia and USA.");
                                Score -= 2;
                            }
                            else
                            {
                                Utility.PrintStory($"You chose: {prompt13}\nCorrect! It is in the first place and has 50 research reactors.");
                                Score += 10;
                            }
                        }
                        else if (prompt10 == "India")
                        {
                            Utility.PrintStory($"You chose: {prompt10}\nIncorrect! It has 7 research reactors.");
                            Score -= 2;
                            string prompt11 = Utility.Prompt("Try again! Maybe this time you will choose the right one.", ["China", "Russia"]);
                            if (prompt11 == "China")
                            {
                                Utility.PrintStory($"You chose: {prompt11}\nIncorrect! It has 16 research reactors.\n\nThe right answers were Russia and USA.");
                                Score -= 2;
                            }
                            else
                            {
                                Utility.PrintStory($"You chose: {prompt11}\nCorrect! It is in the first place and has 50 research reactors.");
                                Score += 10;
                            }
                        }
                        else
                        {
                            Utility.PrintStory($"You chose: {prompt10}\nCorrect! It is in the first place and has 50 research reactors.");
                            Score += 10;
                        }
                        break;

                    default:
                        AnsiConsole.MarkupLine("Invalid choice!");
                        continue;
                }
                break;
            }

            string prompt3 = Utility.Prompt("Our research capabilities are always evolving. Now it is your turn to contribute! We're looking for innovative ideas to push the boundaries of what research reactors can achieve. Choose an area to suggest advancements:", ["Medical Advancements: focus on enhancing isotope production or exploring new medical applications", "Material Science: propose ideas for testing materials or developing next-generation alloys", "Energy Innovations: explore how research reactors can support cleaner energy solutions"]);
            switch (prompt3)
            {
                case "Medical Advancements: focus on enhancing isotope production or exploring new medical applications":
                    AnsiConsole.MarkupLine("Medical Advancements: focus on enhancing isotope production or exploring new medical applications");
                    string prompt4 = Utility.Prompt("What other medical applications could benefit from neutron irradiation?", ["Cancer treatment", "Sterilization of medical equipment", "Imaging for detailed scans of soft tissues"]);
                    Utility.PrintStory($"You chose {prompt3} and you suggested {prompt4}");
                    Score += 5;
                    break;

                case "Material Science: propose ideas for testing materials or developing next-generation alloys":
                    AnsiConsole.MarkupLine("Material Science: propose ideas for testing materials or developing next-generation alloys");
                    string prompt5 = Utility.Prompt("What extreme conditions should we simulate to evaluate materials further?", ["High temperatures to mimic reactor cores", "Intense radiation exposure over extended periods", "Rapid pressure changes, referring to accidents in reactor"]);
                    Utility.PrintStory($"You chose {prompt3} and you suggested {prompt5}");
                    Score += 5;
                    break;

                case "Energy Innovations: explore how research reactors can support cleaner energy solutions":
                    AnsiConsole.MarkupLine("Energy Innovations: explore how research reactors can support cleaner energy solutions");
                    string prompt6 = Utility.Prompt("Recycling spent fuel using advanced reprocessing", ["Using alternative fuel types that produce less waste"]);
                    Utility.PrintStory($"You chose {prompt3} and you suggested {prompt6}");
                    Score += 5;
                    break;
            }
            Utility.PrintStory("Feedback Time!\n\nThank you for your suggestions! Remember, science thrives on collaboration and fresh perspectives.\n\nNow, you are ready to move on to the final room. The end awaits!");

            AnsiConsole.Clear();
            player.UpdateRoomStatus(this.GetType().Name, Score > 0); // Update the room status
            return Score;
        }
    }
}