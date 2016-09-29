using MapControl;
using Sagaceco.UKAccidents.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Sagaceco.UKAccidents.Views.MapAdapters
{
    public class PointMapAdapter
    {
        private MapView mapView;
        private LocationCollection locations;
        private BitmapImage redSpotImage;

        public PointMapAdapter(MapView mapView, LocationCollection locations)
        {
            this.mapView = mapView;
            this.locations = locations;

            redSpotImage = CreateSpotImage();

            locations.CollectionChanged += Locations_CollectionChanged;
        }

        // private stuff

        private void Locations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach(Location location in e.NewItems)
                    {
                        Image redSpot = new Image();
                
                        redSpot.Source  = redSpotImage;
                        redSpot.Margin  = new Thickness( -2, -2, 0, 0 );
                        redSpot.Opacity = 0.6;
                
                        MapPanel.SetLocation( redSpot, location );
                        mapView.PointLayer.Children.Add( redSpot );
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    mapView.PointLayer.Children.Clear();
                    break;
            }
        }

        private BitmapImage CreateSpotImage()
        {
            return new BitmapImage( new Uri( "pack://application:,,,/Sagaceco.UKAccidents;component/Images/Map/RedSpot.png", UriKind.RelativeOrAbsolute ) );
        }
    }
}
