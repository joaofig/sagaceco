using MapControl;
using Sagaceco.UKAccidents.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.UKAccidents.Map
{
    public class OverlayTileImageLoader : ITileImageLoader
    {
        private ZoomData zoomData;

        public OverlayTileImageLoader(ZoomData zoomData)
        {
            this.zoomData = zoomData;
        }

        public void BeginLoadTiles(TileLayer tileLayer, IEnumerable<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                TilePainter tilePainter = new TilePainter(tile, zoomData);

                tilePainter.Paint();
            }
        }

        public void CancelLoadTiles(TileLayer tileLayer)
        {
            //throw new NotImplementedException();
        }
    }
}
