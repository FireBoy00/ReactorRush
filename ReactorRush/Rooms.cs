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
            new Control(), //1
            new SteamTurbine() //4
            // Add other rooms here
        };
    }
}