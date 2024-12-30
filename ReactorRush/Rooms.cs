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
            new SteamTurbine() //1
            // Add other rooms here
        };
    }
}