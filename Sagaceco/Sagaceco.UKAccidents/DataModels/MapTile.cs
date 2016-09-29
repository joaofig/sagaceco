using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.UKAccidents.DataModels
{
    public struct MapTile
    {
        public MapTile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public long ToQuadKey(int zoom)
        {
            long quadKey = 0;

            for (int i = zoom; i > 0; i--)
            {
                int digit = 0;
                int mask = 1 << (i - 1);
                if ((X & mask) != 0)
                {
                    digit++;
                }
                if ((Y & mask) != 0)
                {
                    digit++;
                    digit++;
                }
                quadKey = (quadKey << 2) + digit;
            }
            return quadKey;
        }
    }
}
