using System;
using Spectre.Console;

namespace Rooms
{
    public class VisitorCenter : IRooms
    {
        private int score = 0;

        public int StartLevel() {
            
            return score;
        }
    }
}