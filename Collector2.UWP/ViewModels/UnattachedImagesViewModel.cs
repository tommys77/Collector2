using Collector2.Models;
using Collector2.UWP.Helpers;
using Collector2.UWP.Interface;
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

        private ObservableCollection<ItemImage> _images;

        public ObservableCollection<ItemImage> UnattachedImages
        {
            get { return _images; }
            set { _images = value; }
        }

        public RelayCommand GetUnattachedImages
        {
            get
            {
                return (getUnattachedImages = new RelayCommand(async () =>
                {
                    
                    UnattachedImages = new ObservableCollection<ItemImage>();
                    UnattachedImages.Clear();
                    await DatabaseHelper.GetAllObjectsAsync(UnattachedImages, "UnattachedImages");

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
                    //Very simple way of navigating to the page from here without losing the hamburger menu
                    //if (SelectedImage != null)
                    //{
                    //    Status = SelectedImage.ItemImageId.ToString();
                    //}
                    ItemSelectionHelper.SetCurrentItemImage(SelectedImage);
                    _navigationService.NavigateTo("UnattachedImageEditPage");
                }));
            }
        }

        public void Activate(object parameter)
        {
        }

        public void Deactivate(object parameter)
        {
        }
    }
}