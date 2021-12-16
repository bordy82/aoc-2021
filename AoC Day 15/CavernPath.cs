using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_15
{
    internal class CavernPath
    {
        public CavernPath(int value, int i, int j)
        {
            this.Value = value;
            this.Index = $"!{i},{j}!";
            this.X = i;
            this.Y = j;
            this.AdjacentPaths = new List<CavernPath>();
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Value { get; set; }  

        public string Index { get; set; }

        public bool IsVisited { get; set; }

        public List<CavernPath> AdjacentPaths { get; private set; }
    }
}
