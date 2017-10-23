using Collector2.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using GalaSoft.MvvmLight;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Collector2.UWP.ViewModels
{
    public class UndefinedItemsViewModel : ViewModelBase
    {

        private RelayCommand getUndefinedItems;
        private string status;

        private const string BaseUri = "http://collectorv2.azurewebsites.net/api/";

        public UndefinedItemsViewModel()
        {
            Items = new ObservableCollection<UndefinedItem>();
            Status = "Loading page, please be patient";
            GetUndefinedItems.Execute(getUndefinedItems);
        }

        private ObservableCollection<UndefinedItem> _items;
        public List<UndefinedItem> UndefinedItemsList = new List<UndefinedItem>();
        public string bindingTest = "Undefined";

        public ObservableCollection<UndefinedItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public RelayCommand GetUndefinedItems
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
                     
                     var json = await client.GetStringAsync("UndefinedItems");
                     UndefinedItem[] items = JsonConvert.DeserializeObject<UndefinedItem[]>(json);
                    // Status = items.ElementAt(0).ToString();
                     foreach (var i in items)
                     {
                         Items.Add(i);
                     }
                     SelectedItem = Items.ElementAt(Items.Count() - 1);
                 }
             }
             catch (Exception ex)
             {
                 Status = "Error: " + ex.Message;
             }
         }));
            }
        }

        private UndefinedItem selectedItem;

        public UndefinedItem SelectedItem
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

