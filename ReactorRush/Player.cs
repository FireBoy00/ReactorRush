using System;
using Minigames;
using Rooms;

namespace ReactorRush
{
    public class Player
    {
        private readonly List<IRooms> rooms = RoomsList.Rooms;
        private readonly List<IMinigame> minigames = MinigameList.Minigames;
        public int Score { get { 
            return rooms.Sum(room => room.Score);
        } }
        private readonly Dictionary<string, bool> roomsPassed = [];
        private readonly Dictionary<string, bool> minigamesPassed = [];

        public void UpdateRoomStatus(string roomName, bool passed)
        {
            if (rooms == null)
            {
                throw new InvalidOperationException("Rooms list is not initialized.");
            }

            var room = rooms.FirstOrDefault(r => r.GetType().Name == roomName);
            if (room != null)
            {
                roomsPassed[roomName] = passed;
            }
        }
        public void UpdateMinigameStatus(string minigameName, bool passed)
        {
            if (minigames == null)
            {
                throw new InvalidOperationException("Minigames list is not initialized.");
            }

            var minigame = minigames.FirstOrDefault(r => r.GetType().Name == minigameName);
            if (minigames != null)
            {
                minigamesPassed[minigameName] = passed;
            }
        }

        public bool HasPassedRoom(string roomName)
        {
            return roomsPassed.TryGetValue(roomName, out var passed) && passed;
        }
        public bool HasPassedMinigame(string minigameName)
        {
            return minigamesPassed.TryGetValue(minigameName, out var passed) && passed;
        }
    }
}