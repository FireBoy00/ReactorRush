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
            new VisitorCenter(), // 0
            new CoolingSystem(), // 1
            new FuelHandlingArea(), // 2
            new WasteStorageFacility(), // 6
            new RadiationMonitor(), // 11
            new Laboratory() // 12
            // Add other rooms here
        };
    }
}