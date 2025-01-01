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
            new WasteStorageFacility(), //4
            new RadiationMonitor(), //9
            new Laboratory() // 10
            // Add other rooms here
        };
    }
}