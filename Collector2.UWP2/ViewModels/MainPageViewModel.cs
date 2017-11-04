using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Collector2.UWP2.Views;
using System.Net.Http;
using Newtonsoft.Json;
using Collector2.Models;

namespace Collector2.UWP2.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private bool isPaneOpen;
        private bool unattachedImagesExists;
        private RelayCommand openPaneCommand;
        private RelayCommand navigateCommand;
        private readonly INavigationService _navigationService;

        private const string BaseUri = "http://collectorv2.azurewebsites.net/api/";



        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            // GetUndefinedItemsCount();
            GetUnattachedImages.Execute(unattachedImagesExists);
        }
        
        private RelayCommand getUnattachedImages;
        public RelayCommand GetUnattachedImages
        {
            get
            {
                return getUnattachedImages
                    ?? (getUnattachedImages = new RelayCommand(async () =>
              {
                  using (var client = new HttpClient())
                  {
                      client.BaseAddress = new Uri(BaseUri);
                      var json = await client.GetStringAsync("ItemImages");
                      ItemImage[] items = JsonConvert.DeserializeObject<ItemImage[]>(json);
                      if (items.Count() != 0)
                      {
                          UnattachedImagesExists = !UnattachedImagesExists;
                          NavigateToUnattachedItems.Execute(CurrentPage);
                      }
                  }
              }));
            }
        }

        public RelayCommand NavigateToUnattachedItems
        {
            get
            {
                return navigateCommand = new RelayCommand(() =>
                    {
                        CurrentPage = typeof(UnattachedImagesPage);
                    });
            }
        }

        public async Task GetUndefinedItemsCount()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                var json = await client.GetStringAsync("Items");
                Item[] items = JsonConvert.DeserializeObject<Item[]>(json);
                if (items.Count() != 0)
                {
                    UnattachedImagesExists = true;
                    CurrentPage = typeof(SoftwarePage);
                }
            }
        }

        public bool UnattachedImagesExists
        {
            get
            {
                return unattachedImagesExists;
            }
            set
            {
                if (!Equals(UnattachedImagesExists, value))
                {
                    unattachedImagesExists = value;
                    RaisePropertyChanged(nameof(UnattachedImagesExists));
                }
            }
        }

        public bool IsPaneOpen
        {
            get
            {
                return isPaneOpen;
            }
            set
            {
                if (!Equals(IsPaneOpen, value))
                {
                    isPaneOpen = value;
                    RaisePropertyChanged(nameof(IsPaneOpen));
                }
            }
        }

        public RelayCommand OpenPaneCommand
        {
            get
            {
                return openPaneCommand
                    ?? (openPaneCommand = new RelayCommand(() =>
                    {
                        IsPaneOpen = !IsPaneOpen;
                    }));
            }
        }

        private Type currentPage;

        public Type CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                this.currentPage = value;
                RaisePropertyChanged(nameof(CurrentPage));
            }
        }

        public RelayCommand NavigateToSoftware
        {
            get
            {
                return navigateCommand
                    ?? (navigateCommand = new RelayCommand(() =>
                {
                    if (IsPaneOpen)
                    {
                        IsPaneOpen = !IsPaneOpen;
                    }
                    CurrentPage = typeof(SoftwarePage);
                }));
            }
        }
    }
}
