using MapControl;
using Sagaceco.UKAccidents.Map;
using Sagaceco.UKAccidents.Views.MapAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sagaceco.UKAccidents.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : UserControl
    {
        private AccidentTileLayer accidentTileLayer;

        public MapView()
        {
            InitializeComponent();

            ViewModels.MainViewModel mainViewModel = DependencyInjection.Get<ViewModels.MainViewModel>();
            if(mainViewModel != null)
                mainViewModel.PropertyChanged += MainViewModel_PropertyChanged;
        }

        public MapPanel PointLayer
        {
            get { return pointLayer; }
        }

        //

        private void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ViewModels.MainViewModel mainViewModel = sender as ViewModels.MainViewModel;

            if(e.PropertyName == "ShowAccidentsLayer")
            {
                if(mainViewModel.ShowAccidentsLayer)
                {
                    accidentTileLayer = new AccidentTileLayer(mainViewModel.ZoomData);

                    accidentTileLayer.Opacity = 0.65;

                    map.TileLayers.Add(accidentTileLayer);
                }
                else
                {
                    if(accidentTileLayer != null)
                        map.TileLayers.Remove(accidentTileLayer);
                    accidentTileLayer = null;
                }
            }
        }
    }
}
