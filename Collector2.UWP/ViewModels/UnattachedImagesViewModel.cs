using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Collector2.Models;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using GalaSoft.MvvmLight;
using Collector2.UWP.ViewModels;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Collector2.UWP.Views;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Collector2.UWP.Helpers;

namespace Collector2.UWP.ViewModels
{
    public class UnattachedImagesViewModel : ViewModelBase
    {

        private RelayCommand getUndefinedItems;
        private string status;
        private readonly INavigationService _navigationService;

        private const string BaseUri = "http://collectorv2.azurewebsites.net/api/";

        public UnattachedImagesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Images = new ObservableCollection<ItemImage>();
            //Status = "Loading page, please be patient";
            GetUnattachedImages.Execute(getUndefinedItems);
        }

        private ObservableCollection<ItemImage> _images;
        public List<ItemImage> UndefinedItemsList = new List<ItemImage>();
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
                return (getUndefinedItems = new RelayCommand(async () =>
         {
             try
             {
                 using (var client = new HttpClient())
                 {
                     UndefinedItemsList.Clear();
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
                     SelectedImage = Images.ElementAt(Images.Count() - 1);
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

        public void Refresh()
        {
            _navigationService.NavigateTo("SoftwarePage");
        }

        public void SelectedItemClick()
        {

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
                    ItemSelectionHelper.CurrentItemImage = SelectedImage;
                    MainPageViewModel.Current.CurrentFrame = typeof(NewItemPage);
                });
            }
        }
    }
}

