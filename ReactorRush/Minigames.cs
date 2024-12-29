namespace Minigames
{
    public interface IMinigame
    {
        void Run();
    }

    public static class MinigameList
    {
        public static List<IMinigame> Minigames { get; } = new List<IMinigame>
        {
            new CableFix(), // 0
            new Pindle(), // 1
            new Slider(), //2
            new Game2048() //3
            // Add other minigames here
        };
    }
}