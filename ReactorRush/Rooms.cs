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
            new ControlRoom(), // 2
            new CoolingSystem(), // 3
            new SteamTurbineRoom(), // 4
            new WasteStorageFacility(), // 5
            new ContainmentBuilding(), // 6
            new FuelHandlingArea(), // 7
            new EmergencyBackupRoom(), // 8
            new RadiationMonitor(), // 10
            new Laboratory(), // 11
            new ReactorCore(), //12
            // Add other rooms here
        };
    }
}