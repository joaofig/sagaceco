using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.UKAccidents.DataModels
{
    public class PointData
    {
        public PointData(double latitude, double longitude, int zoom)
        {
            MapTile tile = QuadKey.ToMapTile(latitude, longitude, zoom);
            Key = tile.ToQuadKey(zoom);
        }

        public long Key { get; private set; }
        public int AccidentCount { get; set; }
    }
}
