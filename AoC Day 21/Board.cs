using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_21
{
    internal class Board
    {
        private int _spaces;

        public Board(int numberOfSpaces)
        {
            this._spaces = numberOfSpaces;
        }

        public int Move(int startingPosition, int diceRoll)
        {
            var position = startingPosition + diceRoll;

            while(position > this._spaces)
                position -= this._spaces;

            return position;
        }
    }
}
