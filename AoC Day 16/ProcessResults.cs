using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_16
{
    internal class ProcessResults
    {
        public ProcessResults(int index)
        {
            Index = index;
        }

        public ProcessResults(int index, long value)
        {
            this.Index = index;
            this.Value = value;
        }

        public int Index { get; set; }

        public long Value { get; set; }

        public int VersionSum { get; set; }

        public int TypeId { get; set; }
    }
}
