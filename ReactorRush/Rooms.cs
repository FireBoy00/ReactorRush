using ReactorRush;

namespace Rooms
{
    public interface IRooms
    {
        int StartLevel(Player player);
        int Score { get; }
    }

    public static class RoomsList
    {
        public static List<IRooms> Rooms { get; } = new List<IRooms>
        {
            new VisitorCenter(), // 1
            new CoolingSystem(), // 3
            new WasteStorageFacility(), // 5
            new FuelHandlingArea(), // 7
            new RadiationMonitor(), // 10
            new Laboratory() // 11
            // Add other rooms here
        };
    }
}