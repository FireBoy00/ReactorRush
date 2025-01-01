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
            new Control(), //1
            new CoolingSystem(), // 3
            new SteamTurbine(), //4
            new ReactorCore() //12
            // Add other rooms here
        };
    }
}