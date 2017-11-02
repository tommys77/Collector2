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
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Collector2.UWP.ViewModels
{
    public class UndefinedImagesViewModel : ViewModelBase
    {

        private RelayCommand getUndefinedItems;
        private string status;

        private const string BaseUri = "http://collectorv2.azurewebsites.net/api/";

        public UndefinedImagesViewModel()
        {
            Images = new ObservableCollection<ItemImage>();
            Status = "Loading page, please be patient";
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
                     SelectedItem = Images.ElementAt(Images.Count() - 1);
                 }
             }
             catch (Exception ex)
             {
                 Status = "Error: " + ex.Message;
             }
         }));
            }
        }

        private ItemImage selectedItem;

        public ItemImage SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (!Equals(SelectedItem, value))
                {
                    selectedItem = value;
                    RaisePropertyChanged(nameof(SelectedItem));
                }
            }
        }

        public void SelectedItemClick()
        {
            Status = "You clicked on " + SelectedItem.ItemImageId;
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


    }
}

