namespace Minigames
{
    public interface IMinigame
    {
        void Run();
        int Score { get; }
    }

    public static class MinigameList
    {
        public static List<IMinigame> Minigames { get; } = new List<IMinigame>
        {
            new CableFix(), // 0
            new Pindle(), // 1
            new Slider(), // 2
            new Game2048(), // 3
            new PipeRepair(), // 4
            new TicTacToe(5,2) // 5
            // Add other minigames here
        };
    }
}