namespace Rooms
{
    public interface IRooms
    {
        int StartLevel();
        int Score { get; }
    }

    public static class RoomsList
    {
        public static List<IRooms> Rooms { get; } = new List<IRooms>
        {
            new VisitorCenter(), // 0
            new CoolingSystem(), // 1
            // Add other rooms here
        };
    }
}