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

namespace Collector2.UWP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private bool isPaneOpen;
        private RelayCommand openPaneCommand;
        private RelayCommand navigateCommand;
        private readonly INavigationService _navigationService;


        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
                    CurrentPage = typeof(SoftwarePage);
                }));
            }
        }

       //public RelayCommand NavigateToSoftware
        //{
        //    get
        //    {
        //        return navigateCommand
        //            ?? (navigateCommand = new RelayCommand(() =>
        //            {
        //                _navigationService.NavigateTo("SoftwarePage");
        //            }));
        //    }
        //}
    }
}
