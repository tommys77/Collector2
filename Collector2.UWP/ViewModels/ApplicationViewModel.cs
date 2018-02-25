using Collector2.UWP.Config;
using Collector2.UWP.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Net.Http;

namespace Collector2.UWP.ViewModels
{
    public class ApplicationViewModel : ViewModelBase
    {
        private bool isPaneOpen;
        private bool unattachedImagesExists;
        private RelayCommand openPaneCommand;
        private RelayCommand navigateCommand;
        private readonly INavigationService _navigationService;

        private string Root;

        public ApplicationViewModel(INavigationService navigationService)
        {
            Root = CollectorConfig.ApiRoot;
            _navigationService = navigationService;
            CheckIfUnattachedExistsAsync();
        }

        public async void CheckIfUnattachedExistsAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Root);
                var result = await client.GetAsync("UnattachedImagesExists");
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    UnattachedImagesExists = !UnattachedImagesExists;
                    _navigationService.NavigateTo("UnattachedImagesPage");
                }
                else _navigationService.NavigateTo("SoftwarePage");
            }
        }

        #region Properties
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
        #endregion

        //Query to find out if there are any unattached images in the database.

        #region Button commands
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
                        _navigationService.NavigateTo("SoftwarePage");
                    }));
            }
        }

        public RelayCommand NavigateToUnattachedImages
        {
            get
            {
                return navigateCommand = new RelayCommand(() =>
                {
                    _navigationService.NavigateTo("UnattachedImagesPage");
                });
            }
        }

        #endregion
    }

}