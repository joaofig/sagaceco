using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using MapControl;
using NPOI.SS.UserModel;
using Sagaceco.UKAccidents.DataModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Sagaceco.UKAccidents.ViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ZoomData zoomData = new ZoomData();

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                LoadDataCommand = new RelayCommand( LoadData );
                ClusterDataCommand = new RelayCommand( ClusterData );
            }
        }

        public RelayCommand LoadDataCommand { get; private set; }

        public RelayCommand ClusterDataCommand { get; private set; }

        private string statusMessage = "Ready";

        public string StatusMessage
        {
            get { return statusMessage; }
            set
            {
                if (statusMessage != value)
                {
                    statusMessage = value;
                    RaisePropertyChanged("StatusMessage");
                }
            }
        }

        private bool isLoading;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    RaisePropertyChanged("IsLoading");
                }
            }
        }

        private bool showAccidentsLayer;

        public bool ShowAccidentsLayer
        {
            get { return showAccidentsLayer; }
            set
            {
                if (showAccidentsLayer != value)
                {
                    showAccidentsLayer = value;
                    RaisePropertyChanged("ShowAccidentsLayer");
                }
            }
        }

        public ZoomData ZoomData
        {
            get { return zoomData; }
        }
        //

        private void LoadData()
        {
            IsLoading = true;
            ThreadPool.QueueUserWorkItem(LoadDataWorker);
        }

        private void LoadDataWorker(object state)
        {
            using (StreamReader streamReader = File.OpenText("Data\\DfTRoadSafety_Accidents_2015.csv"))
            {
                int count = 0;
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();

                    if(count++ > 0 && !string.IsNullOrEmpty(line))
                    {
                        string[]    items       = line.Split(new char[] { ',' } );
                        double      latitude    = 0.0;
                        double      longitude   = 0.0;

                        if(double.TryParse(items[3], NumberStyles.Any, CultureInfo.InvariantCulture, out longitude) &&
                           double.TryParse(items[4], NumberStyles.Any, CultureInfo.InvariantCulture, out latitude))
                        {
                            zoomData.AddAccident(latitude, longitude);
                        }
                    }
                }
            }
            DispatcherHelper.UIDispatcher.BeginInvoke(new Action(LoadDataWorkerCompleted));
        }

        private void LoadDataWorkerCompleted()
        {
            IsLoading = false;
            ShowAccidentsLayer = true;
        }

        private void ClusterData()
        {

        }
    }
}