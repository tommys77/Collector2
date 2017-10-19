using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml.Controls;
using Collector2.UWP.Views;
using System.Net.Http;
using Collector2.Model;
using Newtonsoft.Json;

namespace Collector2.UWP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private bool isPaneOpen;
        private bool undefinedItemsExist;
        private RelayCommand openPaneCommand;
        private RelayCommand navigateCommand;
        private readonly INavigationService _navigationService;

        private const string BaseUri = "http://collectorv2.azurewebsites.net/api/";



        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            // GetUndefinedItemsCount();
            GetUndefinedItems.Execute(undefinedItemsExist);
        }
        
        private RelayCommand getUndefinedItems;
        public RelayCommand GetUndefinedItems
        {
            get
            {
                return getUndefinedItems
                    ?? (getUndefinedItems = new RelayCommand(async () =>
              {
                  using (var client = new HttpClient())
                  {
                      client.BaseAddress = new Uri(BaseUri);
                      var json = await client.GetStringAsync("Items");
                      UndefinedItem[] items = JsonConvert.DeserializeObject<UndefinedItem[]>(json);
                      if (items.Count() != 0)
                      {
                          UndefinedItemsExists = !UndefinedItemsExists;
                          NavigateToUndefinedItems.Execute(CurrentPage);
                      }
                  }
              }));
            }
        }

        public RelayCommand NavigateToUndefinedItems
        {
            get
            {
                return navigateCommand = new RelayCommand(() =>
                    {
                        CurrentPage = typeof(UndefinedItemsPage);
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
                    UndefinedItemsExists = true;
                    CurrentPage = typeof(SoftwarePage);
                }
            }
        }

        public bool UndefinedItemsExists
        {
            get
            {
                return undefinedItemsExist;
            }
            set
            {
                if (!Equals(UndefinedItemsExists, value))
                {
                    undefinedItemsExist = value;
                    RaisePropertyChanged("UndefinedItemsExists");
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
                    RaisePropertyChanged();
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
                RaisePropertyChanged("CurrentPage");
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
