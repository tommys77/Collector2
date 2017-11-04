using Collector2.Models;
using Collector2.UWP.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace Collector2.UWP.ViewModels
{
    public class NewItemPageViewModel : ViewModelBase
    {

        public ItemImage SelectedItemImage { get; private set; }
        public NewItemPageViewModel()
        {
            SelectedItemImage = ItemSelectionHelper.CurrentItemImage;
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