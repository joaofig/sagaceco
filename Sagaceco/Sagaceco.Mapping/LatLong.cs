using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.Mapping
{
    public class LatLong
    {
        public LatLong() { }

        public LatLong(double latitude, double longitude)
        {
            Latitude    = latitude;
            Longitude   = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
