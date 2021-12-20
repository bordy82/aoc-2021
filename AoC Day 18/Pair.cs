using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_18
{
    internal class Pair
    {
        public Pair()
        {
        }

        public Pair(int value)
        {
            this.Value = value;
            this.IsValue = true;
        }

        public bool IsValue { get; set; }

        public int Value { get; set; }

        public Pair Left { get; set; }

        public Pair Right { get; set; }

        public Pair Parent { get; set; }
    }
}
