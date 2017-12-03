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

        private const string BaseUri = "http://collectorv2.azurewebsites.net/api/";

        public static ApplicationViewModel Current { get; private set; }

        public ApplicationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            CheckIfUnattachedExistsAsync();
        }

        public async void CheckIfUnattachedExistsAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                var result = await client.GetAsync("UnattachedImagesExists");
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    UnattachedImagesExists = !UnattachedImagesExists;
                    _navigationService.NavigateTo("UnattachedImagesPage");
                }
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

        //Query to find out if there are any unattached images in the database, goes to relevant page if so.

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