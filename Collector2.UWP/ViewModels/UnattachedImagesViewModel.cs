using Collector2.Models;
using Collector2.UWP.Helpers;
using Collector2.UWP.Interface;
using Collector2.UWP.Repository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace Collector2.UWP.ViewModels
{
    public class UnattachedImagesViewModel : ViewModelBase, INavigable
    {
        private RelayCommand getUnattachedImages;

        private readonly INavigationService _navigationService;

        private const string BASE_URI = "http://collectorv2.azurewebsites.net/api/";

        public UnattachedImagesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            if (GetUnattachedImages.CanExecute(UnattachedImages))
            {
                GetUnattachedImages.Execute(UnattachedImages);
            }

        }

        private ObservableCollection<ItemImage> _unattachedImages;

        public ObservableCollection<ItemImage> UnattachedImages
        {
            get { return _unattachedImages; }
            set
            {
                _unattachedImages = value;
                RaisePropertyChanged(nameof(UnattachedImages));
            }
        }

        public RelayCommand GetUnattachedImages
        {
            get
            {
                return (getUnattachedImages = new RelayCommand(async () =>
                {
                    UnattachedImages = new ObservableCollection<ItemImage>();
                    UnattachedImages.Clear();
                    var repository = new ItemImageRepository();
                    UnattachedImages = await repository.GetAllAsync();
                    StatusBarHelper.Instance.StatusBarMessage = $"You have {UnattachedImages.Count} unattached images. Click on them to attach them to a new or existing item.";
                }));
            }
        }

        private ItemImage selectedImage;

        public ItemImage SelectedImage
        {
            get { return selectedImage; }
            set
            {
                if (!Equals(SelectedImage, value))
                {
                    selectedImage = value;
                    RaisePropertyChanged(nameof(SelectedImage));
                }
            }
        }

        private RelayCommand newItemCommand;

        public RelayCommand NewItemCommand
        {
            get
            {
                return newItemCommand ?? (newItemCommand = new RelayCommand(() =>
                {
                    ItemSelectionHelper.SetCurrentItemImage(SelectedImage);
                    _navigationService.NavigateTo("UnattachedImageEditPage");
                }));
            }
        }

        public void Activate(object parameter)
        {
            GetUnattachedImages.Execute(UnattachedImages);
        }

        public void Deactivate(object parameter)
        {
        }
    }
}