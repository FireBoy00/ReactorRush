using System;
using System.Dynamic;
using Minigames;
using Rooms;
using System.IO;

namespace ReactorRush
{
    public class Player
    {
        private readonly List<IRooms> rooms = RoomsList.Rooms;
        private readonly List<IMinigame> minigames = MinigameList.Minigames;
        public int Score { get { 
            return rooms.Sum(room => room.Score);
        } }
        /*public int Score { get { 
            return roomsResults.Values.Sum();
        } }*/
        public string? Name { get; private set; }
        private readonly Dictionary<string, bool> roomsPassed = [];
        private readonly Dictionary<string, bool> minigamesPassed = [];
        private readonly Dictionary<string, int> roomsResults = [];
        private readonly Dictionary<string, int> minigamesResults = [];

        public Player(string name = "player1")
        {
            Name = name;
        }
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

        public void UpdateRoomResults(string roomName, int score)
        {
            if (rooms == null)
            {
                throw new InvalidOperationException("Rooms list is not initialized.");
            }

            var room = rooms.FirstOrDefault(r => r.GetType().Name == roomName);
            if (room != null)
            {
                if(roomsResults[roomName] < score)
                    roomsResults[roomName] = score;
            }
        }
        public void UpdateMinigameResults(string minigameName, int score)
        {
            if (minigames == null)
            {
                throw new InvalidOperationException("Minigames list is not initialized.");
            }

            var minigame = minigames.FirstOrDefault(r => r.GetType().Name == minigameName);
            if (minigames != null)
            {
                if(minigamesResults[minigameName] < score)
                    minigamesResults[minigameName] = score;
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

        public int RoomScore(string roomName)
        {
            if(roomsResults.TryGetValue(roomName, out var score))
                return score;
            else
                return 0;
        }
        public int MinigameScore(string minigameName)
        {
            if(minigamesResults.TryGetValue(minigameName, out var score))
                return score;
            else 
                return 0;
        }


    }
}