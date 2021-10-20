using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U.Reactor
{
    internal static class S
    {
        // Tested
        internal static int MinInt(this int num, int min)
        {
            if (num < min) return min;
            else return num;
        }

    }
}
