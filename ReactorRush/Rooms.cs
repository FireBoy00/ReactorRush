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
            new FuelHandlingArea() 
            // Add other rooms here
        };
    }
}