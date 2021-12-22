using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_21
{
    internal class Dice
    {
        private int _value;

        public Dice()
        {
            this._value = 100;
        }

        public int DieRoll { get; set; }

        public int Roll()
        {
            this.DieRoll++;

            this._value++;

            if (this._value > 100)
                this._value -= 100;

            return this._value;
        }

        public int RollThreeTimes()
        {
            var total = 0;

            total += this.Roll();
            total += this.Roll();
            total += this.Roll();

            return total;
        }
    }
}
