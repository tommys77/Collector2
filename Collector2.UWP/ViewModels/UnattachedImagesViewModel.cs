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
        private string status;

        private readonly INavigationService _navigationService;

        private const string BASE_URI = "http://collectorv2.azurewebsites.net/api/";

        public UnattachedImagesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Images = new ObservableCollection<ItemImage>();
            GetUnattachedImages.Execute(getUnattachedImages);
        }

        private ObservableCollection<ItemImage> _images;

        public ObservableCollection<ItemImage> Images
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
             try
             {
                 using (var client = new HttpClient())
                 {
                     Images.Clear();
                     client.BaseAddress = new Uri(BASE_URI);
                     var json = await client.GetStringAsync("UnattachedImages");
                     ItemImage[] images = JsonConvert.DeserializeObject<ItemImage[]>(json);
                     foreach (var i in images)
                     {
                         Images.Add(i);
                     }
                     Status = $"You have {Images.Count} unattached images. Click on them to attach them to a new or existing item.";
                 }
             }
             catch (Exception ex)
             {
                 Status = "Error: " + ex.Message;
             }
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

        public string Status
        {
            get { return status; }
            set
            {
                if (!Equals(Status, value))
                {
                    status = value;
                    RaisePropertyChanged(nameof(Status));
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