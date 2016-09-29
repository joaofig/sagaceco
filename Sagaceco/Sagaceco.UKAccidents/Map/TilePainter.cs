using MapControl;
using Sagaceco.Mapping;
using Sagaceco.UKAccidents.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sagaceco.UKAccidents.Map
{
    public class TilePainter
    {
        private PropertyInfo    dpiXProperty;
        private PropertyInfo    dpiYProperty;
        private ZoomData        zoomData;
        private Tile            tile;
        private int             dpiX;
        private int             dpiY;

        public TilePainter(Tile tile, ZoomData zoomData)
        {
            dpiXProperty = typeof(SystemParameters).GetProperty("DpiX", BindingFlags.NonPublic | BindingFlags.Static);
            dpiYProperty = typeof(SystemParameters).GetProperty("Dpi", BindingFlags.NonPublic | BindingFlags.Static);

            dpiX = (int)dpiXProperty.GetValue(null, null);
            dpiY = (int)dpiYProperty.GetValue(null, null);

            this.tile = tile;
            this.zoomData = zoomData;
        }

        public /*async*/ void Paint()
        {
            WriteableBitmap bitmap = PaintTile();   // await Task.Run(() => PaintTile());

            tile.SetImage(bitmap);
        }

        //

        private WriteableBitmap PaintTile()
        {
            WriteableBitmap bmp = new WriteableBitmap(264, 264, dpiX, dpiY, PixelFormats.Bgra32, null);

            List<DotInfo> dots = new List<DotInfo>(5000);       // Cache the dot information

            for (int y = 0; y < 264; y++)
            {
                int pixelY = tile.Y * 256 + y - 4;

                for (int x = 0; x < 264; x++)
                {
                    int     pixelX      = tile.X * 256 + x - 4;
                    MapTile pixelTile   = new MapTile(pixelX, pixelY);
                    long    pixelKey    = pixelTile.ToQuadKey(tile.ZoomLevel + 8);
                    int     accidents   = zoomData.GetAccidentCountAt(pixelKey, tile.ZoomLevel + 8);

                    if (accidents > 5)
                        dots.Add(new DotInfo(x, y, 3, Colors.DarkRed) );           //bmp.FillEllipseCentered(x, y, 3, 3, Colors.DarkRed);
                    else if (accidents > 2)
                        dots.Add(new DotInfo(x, y, 2, Colors.OrangeRed));          //bmp.FillEllipseCentered(x, y, 2, 2, Colors.OrangeRed);
                    else if (accidents >= 1)
                        dots.Add(new DotInfo(x, y, 1, Colors.Goldenrod));          //bmp.FillEllipseCentered(x, y, 1, 1, Colors.DarkOrange);
                }
            }

            // Place the dots in the specified hierarchy
            for(int radius = 1; radius < 4; radius++ )
            {
                foreach(DotInfo dot in dots)
                    if(dot.Radius == radius)
                        bmp.FillEllipseCentered(dot.X, dot.Y, radius, radius, dot.Color);
            }

            WriteableBitmap final   = new WriteableBitmap(256, 256, dpiX, dpiY, PixelFormats.Bgra32, null);
            Rect            tgtRect = new Rect(new Size(256, 256));
            Rect            srcRect = new Rect(new Point(4, 4), new Size(256, 256));

            final.Blit(tgtRect, bmp, srcRect);
            return final;
        }

        private byte[] BackgroundPaint()
        {
            byte[] buffer = new byte[256 * 256 * 4];

            for(int y = 0; y < 256; y++ )
            {
                int pixelY = tile.Y * 256 + y;

                for (int x = 0; x < 256; x++)
                {
                    int     pixelX      = tile.X * 256 + x;
                    MapTile pixelTile   = new MapTile(pixelX, pixelY);
                    long    pixelKey    = pixelTile.ToQuadKey(tile.ZoomLevel + 8);
                    int     accidents   = zoomData.GetAccidentCountAt(pixelKey, tile.ZoomLevel + 8);

                    int b = x * 4 + y * 256 * 4;
                    int g = b + 1;
                    int r = b + 2;
                    int a = b + 3;

                    byte colorG = 0;        //(distribution.BrandenburgDensity(location.Latitude, location.Longitude) / distribution.MaxBrandenburg * 255);
                    byte colorB = 0;        //(distribution.RiverDensity(location.Latitude, location.Longitude) / distribution.MaxRiver * 255);
                    byte colorR = 0xff;     //(distribution.SatelliteDensity(location.Latitude, location.Longitude) / distribution.MaxSatellite * 255);

                    buffer[b] = colorB;
                    buffer[g] = colorG;
                    buffer[r] = colorR;
                    buffer[a] = (byte)((accidents > 0) ? 196 : 0);
                }
            }
            return buffer;
        }
    }

    public class DotInfo
    {
        public DotInfo(int x, int y, int r, Color c)
        {
            X = x;
            Y = y;
            Radius = r;
            Color = c;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Radius { get; private set; }
        public Color Color { get; private set; }
    }
}
