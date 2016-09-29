using Sagaceco.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.UKAccidents.DataModels
{
    public static class QuadKey
    {
        private const double EarthRadius = 6378137;
        private const double MinLatitude = -85.05112878;
        private const double MaxLatitude = 85.05112878;
        private const double MinLongitude = -180;
        private const double MaxLongitude = 180;

        public static MapPixel ToMapPixel(LatLong latLong, int zoom)
        {
            int ptX;
            int ptY;

            double latitude     = Clip( latLong.Latitude, MinLatitude, MaxLatitude);
            double longitude    = Clip( latLong.Longitude, MinLongitude, MaxLongitude);

            double x = (longitude + 180) / 360; 
            double sinLatitude = Math.Sin(latitude * Math.PI / 180);
            double y = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI);

            long mapSize = MapSize( zoom );
            ptX = (int) Clip( x * mapSize + 0.5, 0, mapSize - 1 );
            ptY = (int) Clip( y * mapSize + 0.5, 0, mapSize - 1 );

            return new MapPixel(ptX, ptY);
        }

        public static MapTile ToMapTile(LatLong latLong, int zoom)
        {
            int ptX;
            int ptY;

            double latitude     = Clip( latLong.Latitude, MinLatitude, MaxLatitude);
            double longitude    = Clip( latLong.Longitude, MinLongitude, MaxLongitude);

            double x = (longitude + 180) / 360; 
            double sinLatitude = Math.Sin(latitude * Math.PI / 180);
            double y = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI);

            long mapSize = MapSize( zoom );
            ptX = (int) Clip( x * mapSize + 0.5, 0, mapSize - 1 );
            ptY = (int) Clip( y * mapSize + 0.5, 0, mapSize - 1 );

            return new MapTile(ptX / 256, ptY / 256);
        }

        public static MapTile ToMapTile(double latitude, double longitude, int zoom)
        {
            int ptX;
            int ptY;

            latitude    = Clip( latitude, MinLatitude, MaxLatitude);
            longitude   = Clip( longitude, MinLongitude, MaxLongitude);

            double x = (longitude + 180) / 360; 
            double sinLatitude = Math.Sin(latitude * Math.PI / 180);
            double y = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI);

            long mapSize = MapSize( zoom );
            ptX = (int) Clip( x * mapSize + 0.5, 0, mapSize - 1 );
            ptY = (int) Clip( y * mapSize + 0.5, 0, mapSize - 1 );

            return new MapTile(ptX / 256, ptY / 256);
        }

        public static long ToQuadKey(LatLong latLong, int zoom)
        {
            return ToMapTile(latLong, zoom).ToQuadKey(zoom);
        }

        public static long ToQuadKey(double latitude, double longitude, int zoom)
        {
            return ToMapTile(latitude, longitude, zoom).ToQuadKey(zoom);
        }

        //

        private static long MapSize( int zoom )
        {
            return (long)256 << zoom;
        }

        private static double Clip( double x, double min, double max )
        {
            return Math.Min( Math.Max( x, min ), max );
        }
    }
}
