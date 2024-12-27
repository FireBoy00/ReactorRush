namespace Rooms
{
    public interface IRooms
    {
        int StartLevel();
    }

    public static class MinigameList
    {
        public static List<IRooms> Minigames { get; } = new List<IRooms>
        {
            new VisitorCenter() // 0
            // Add other rooms here
        };
    }
}