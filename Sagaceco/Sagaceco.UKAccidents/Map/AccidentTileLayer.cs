using MapControl;
using Sagaceco.UKAccidents.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.UKAccidents.Map
{
    public class AccidentTileLayer : TileLayer
    {
        public AccidentTileLayer(ZoomData zoomData) : base(new OverlayTileImageLoader(zoomData))
        {
            MinZoomLevel = 1;
            MaxZoomLevel = 22;
            Background = null;
            Foreground = null;
            TileSource = new AccidentTileSource();
        }
    }
}
