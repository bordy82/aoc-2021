using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_12
{
    internal class Cave
    {
        public Cave(string name)
        {
            this.Name = name;
            this.ConnectedCaves = new List<Cave>();

            if (name == name.ToLower())
                this.IsSmallCave = true;
        }

        public bool IsSmallCave { get; private set; }

        public string Name { get; set; }

        public List<Cave> ConnectedCaves { get; private set; }
    }
}
