using Collector2.Models;
using Collector2.UWP.Helpers;
using Collector2.UWP.Interface;
using Collector2.UWP.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Collector2.UWP.ViewModels
{
    public class NewItemPageViewModel : ViewModelBase, INavigable
    {
        private ItemImage selectedItemImage = ItemSelectionHelper.GetCurrentItemImage();
        private string status;

        //private readonly INavigationService _navigationService;

        public ItemImage SelectedItemImage
        {
            get
            {
                return selectedItemImage;
            }
            set
            {
                selectedItemImage = value;
                RaisePropertyChanged(nameof(SelectedItemImage));
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

        //public NewItemPageViewModel(INavigationService navigationService)
        //{
        //    _navigationService = navigationService;
        //}


        private const string ATTACH_TO_ITEM = "Attach to...";
        private const string EDIT_ITEM = "Edit existing item";
        private const string CREATE_NEW_ITEM = "Create new...";

        public ObservableCollection<string> ActionSelections
        {
            get
            {
                return new ObservableCollection<string>
                {
                    ATTACH_TO_ITEM,
                    EDIT_ITEM,
                    CREATE_NEW_ITEM
                };
            }
        }

        private string _actionSelected;

        public string ActionSelected
        {
            get
            {
                return _actionSelected;
            }
            set
            {
                _actionSelected = value;
            }
        }

        private RelayCommand _actionSelectedCommand;

        public RelayCommand ActionSelectedCommand
        {
            get
            {
                return (_actionSelectedCommand = new RelayCommand(() =>
          {
              switch (ActionSelected)
              {
                  case ATTACH_TO_ITEM:
                      ActionPage = typeof(AttachToItem);
                      break;
                  case EDIT_ITEM:
                      break;
                  case CREATE_NEW_ITEM:
                      ActionPage = typeof(ItemCreationSelect);
                      break;
              }
          }));
            }
        }

        private Type _actionPage;

        public Type ActionPage
        {
            get { return _actionPage; }
            set
            {
                this._actionPage = value;
                RaisePropertyChanged(nameof(ActionPage));
            }
        }

        public void Activate(object parameter)
        {
            SelectedItemImage = ItemSelectionHelper.GetCurrentItemImage();
        }

        public void Deactivate(object parameter)
        {
        }

        private RelayCommand navigateToSoftwareEditor;

        public RelayCommand NavigateToSoftwareEditor
        {
            get
            {
                return navigateToSoftwareEditor ?? (navigateToSoftwareEditor = new RelayCommand(() =>
                {
                    //_navigationService.NavigateTo("SoftwarePage");
                }));
            }
        }
    }
}