using Collector2.Models;
using Collector2.UWP.Helpers;
using Collector2.UWP.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Navigation;

namespace Collector2.UWP.ViewModels
{
    public class NewItemPageViewModel : ViewModelBase, INavigable
    {
        private ItemImage selectedItemImage = ItemSelectionHelper.GetCurrentItemImage();
        private string status;
        public ObservableCollection<string> ActionSelections = new ObservableCollection<string>();

        private readonly INavigationService _navigationService;

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

        public NewItemPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //AddActionSelections();
            //Status = SelectedItemImage.ItemImageId.ToString();
        }

        public void AddActionSelections()
        {
            ActionSelections.Clear();
            ActionSelections.Add("Attach to existing item");
            ActionSelections.Add("New Software Item");
            ActionSelections.Add("New Hardware Item");
        }

        public void Activate(object parameter)
        {
            SelectedItemImage = ItemSelectionHelper.GetCurrentItemImage();
        }

        public void Deactivate(object parameter)
        {
            this.Cleanup();
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