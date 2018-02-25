using Collector2.Models;
using GalaSoft.MvvmLight;

namespace Collector2.UWP.ViewModels
{
    public class SoftwareDetailsViewModel : ViewModelBase
    {

        private ItemImage _selectedItemImage;
        private Software _selectedSoftware;

        public SoftwareDetailsViewModel()
        {
            if (IsInDesignMode)
            {

            }
        }

        public ItemImage SelectedItemImage {
            get
            {
                return _selectedItemImage;
            }
            set
            {
                _selectedItemImage = value;
                RaisePropertyChanged(nameof(SelectedItemImage));
            }
        }

        private Software SelectedSoftware {
            get
            {
                return _selectedSoftware;
            }
            set
            {
                _selectedSoftware = value;
                RaisePropertyChanged(nameof(SelectedSoftware));
            }
        }

    }
}