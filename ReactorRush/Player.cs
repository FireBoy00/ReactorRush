using System;
using System.Collections.Generic;
using System.Linq;
using Rooms;

namespace ReactorRush
{
    public class Player
    {
        private readonly List<IRooms> rooms = RoomsList.Rooms;
        public int Score { get { 
            foreach (var room in rooms)
            {
                Console.WriteLine($"Room: {room.GetType().Name}, Score: {room.Score}");
            }
            return rooms.Sum(room => room.Score);
        } }
        private readonly Dictionary<string, bool> roomsPassed = [];

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

        public bool HasPassedRoom(string roomName)
        {
            return roomsPassed.TryGetValue(roomName, out var passed) && passed;
        }
    }
}