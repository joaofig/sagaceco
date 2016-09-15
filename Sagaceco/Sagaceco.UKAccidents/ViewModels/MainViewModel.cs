using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

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

        //

        private void LoadData()
        {

        }

        private void ClusterData()
        {

        }
    }
}