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
            new WasteStorageFacility(), //6
            new RadiationMonitor(), //11
            new Laboratory() // 12
            // Add other rooms here
        };
    }
}