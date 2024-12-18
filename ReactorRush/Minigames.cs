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
            new CableFix(),
            new Pindle()
            // Add other minigames here
        };
    }
}