namespace Rooms
{
    public interface IRooms
    {
        int StartLevel();
    }

    public static class RoomsList
    {
        public static List<IRooms> Rooms { get; } = new List<IRooms>
        {
            new VisitorCenter(), // 0
            new CoolingSystem() // 1    - but should be changed to 2 later, so it's in the order of the rooms displayed
            // Add other rooms here
        };
    }
}