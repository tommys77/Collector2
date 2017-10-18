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

namespace Collector2.UWP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private bool isPaneOpen;
        private RelayCommand openPaneCommand;
        private RelayCommand navigateCommand;
        private readonly INavigationService _navigationService;

        private const string BaseUri = "http://collectorv2.azurewebsites.net/api/";



        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public async Task GetUndefinedItemsCount()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var gameJson = await client.GetStringAsync("Items");
                    Item[] games = JsonConvert.DeserializeObject<Game[]>(gameJson);
                    Games.Clear();
                    foreach (var g in games)
                    {
                        Games.Add(g);
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

        private Type currentPage = null;

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
                    if(IsPaneOpen)
                    {
                        IsPaneOpen = !IsPaneOpen;
                    }
                    CurrentPage = typeof(SoftwarePage);
                }));
            }
        }
    }
}
