using Collector2.Models;
using Collector2.UWP.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Collector2.UWP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private bool isPaneOpen;
        private bool unattachedImagesExists;
        private RelayCommand openPaneCommand;
        private RelayCommand navigateCommand;
        private readonly INavigationService _navigationService;

        private const string BaseUri = "http://collectorv2.azurewebsites.net/api/";

        public static MainPageViewModel Current { get; private set; }

        public MainPageViewModel(INavigationService navigationService)
        {
            Current = this;
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
                      var result = await client.GetAsync("UnattachedImagesExists");
                      if (result.StatusCode == System.Net.HttpStatusCode.OK)
                      {
                          UnattachedImagesExists = !UnattachedImagesExists;
                          NavigateToUnattachedItems.Execute(CurrentFrame);
                      }
                      if (result.StatusCode == System.Net.HttpStatusCode.Forbidden)
                      {
                          NavigateToSoftware.Execute(CurrentFrame);
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
                        CurrentFrame = typeof(UnattachedImagesPage);
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
                    CurrentFrame = typeof(SoftwarePage);
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

        private Type currentFrame;

        public Type CurrentFrame
        {
            get
            {
                return currentFrame;
            }
            set
            {
                this.currentFrame = value;
                RaisePropertyChanged(nameof(CurrentFrame));
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
                    CurrentFrame = typeof(SoftwarePage);
                }));
            }
        }
    }
}