using Collector2.Models;
using Collector2.Models.ViewModels;
using Collector2.UWP.Helpers;
using Collector2.UWP.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.ViewModels
{
    public partial class SDetailsViewModel : ViewModelBase, INavigable
    {


        private bool _isInViewMode;
        private ItemImage _selectedImage;
        private List<ItemImage> _images;
        private SoftwareViewModel _current;


        #region Properties

        public SoftwareViewModel Current
        {
            get { return _current; }
            set
            {
                _current = value;
                RaisePropertyChanged(nameof(Current));
            }
        }

        public bool IsInViewMode
        {
            get { return _isInViewMode; }
            set
            {
                _isInViewMode = value;
                RaisePropertyChanged(nameof(IsInViewMode));
            }
        }

        public ItemImage SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
                RaisePropertyChanged(nameof(SelectedImage));
            }
        }

        public List<ItemImage> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                RaisePropertyChanged(nameof(Images));
            }
        }

        #endregion

        public SDetailsViewModel()
        {
            IsInViewMode = true;
            
            if (IsInDesignMode)
            {

            }
        }

        public void Activate(object parameter)
        {
            GoBackHelper.Instance.CanGoBack = true;
            if (ItemSelectionHelper.GetCurrentSoftware() != null)
            {
                Current = new SoftwareViewModel(ItemSelectionHelper.GetCurrentSoftware());
            }
            SelectedImage = Current.ItemImages.First();
        }

        public void Deactivate(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
