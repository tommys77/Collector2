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

        private UnattachedImagesViewModel VM;

        private const string BaseUri = "http://collectorv2.azurewebsites.net/api/";

        public UnattachedImagesViewModel(INavigationService navigationService)
        {
            VM = this;
            _navigationService = navigationService;
            Images = new ObservableCollection<ItemImage>();
            Status = "Loading page, please be patient";
            GetUnattachedImages.Execute(getUnattachedImages);
        }

        private ObservableCollection<ItemImage> _images;
        public string bindingTest = "Undefined";

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
                     client.BaseAddress = new Uri(BaseUri);
                     var json = await client.GetStringAsync("UnattachedImages");
                     ItemImage[] images = JsonConvert.DeserializeObject<ItemImage[]>(json);
                     // Status = items.ElementAt(0).ToString();
                     foreach (var i in images)
                     {
                         if (i.IsAttached == false)
                         {
                             Images.Add(i);
                         }
                     }
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
                return newItemCommand = new RelayCommand(() =>
                {
                    //Very simple way of navigating to the page from here without losing the hamburger menu
                    if (SelectedImage != null)
                    {
                        Status = SelectedImage.ItemImageId.ToString();
                    }

                    ItemSelectionHelper.SetCurrentItemImage(SelectedImage);
                    _navigationService.NavigateTo("NewItemPage");

                    //
                    //MainPageViewModel.Current.CurrentFrame = typeof(NewItemPage);
                });
            }
        }

        public void Activate(object parameter)
        {
            Status = "Loading data, please wait!";
        }

        public void Deactivate(object parameter)
        {
            this.Cleanup();
        }
    }
}