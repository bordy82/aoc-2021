using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_21
{
    internal class Player
    {
        public Player(int startingPosition)
        {
            this.Position = startingPosition;
        }

        public int Score { get; set; }

        public int Position { get; set; }

        public void MoveTo(int newPosition)
        {
            this.Score += newPosition;
            this.Position = newPosition;
        }
    }
}
