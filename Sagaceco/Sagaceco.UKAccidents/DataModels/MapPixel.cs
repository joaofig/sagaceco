using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.UKAccidents.DataModels
{
    public struct MapPixel
    {
        public MapPixel(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public MapTile ToMapTile()
        {
            return new MapTile(X / 256, Y / 256);
        }
    }
}
