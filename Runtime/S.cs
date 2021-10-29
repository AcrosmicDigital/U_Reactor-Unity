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

        internal static UInt32 NewIdShort()
        {

            byte[] buffer = Guid.NewGuid().ToByteArray();

            UInt32 num = BitConverter.ToUInt32(buffer, 0);

            if (num < 1000000000)
                num += 1000000000;

            return num;

        }

    }
}
