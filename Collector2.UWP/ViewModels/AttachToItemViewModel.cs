using Collector2.Models;
using Collector2.UWP.Helpers;
using Collector2.UWP.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.ViewModels
{
    public class AttachToItemViewModel : ViewModelBase
    {
        private const string SOFTWARE = "Software";
        private const string HARDWARE = "Hardware";

        INavigationService _navigationService;
       

        public AttachToItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Softwares = new ObservableCollection<Software>();
            Hardwares = new ObservableCollection<Hardware>();

            if (IsInDesignMode)
            {
                TypeSelected = SOFTWARE;
            }
        }


        public ObservableCollection<string> TypeSelections
        {
            get
            {
                return new ObservableCollection<string>()
                {
                    SOFTWARE,
                    HARDWARE
                };
            }
        }


        private string _typeSelected;

        public string TypeSelected
        {
            get
            {
                return _typeSelected;
            }
            set
            {
                _typeSelected = value;
            }
        }

        private RelayCommand _typeSelectionCommand;

        public RelayCommand TypeSelectionCommand
        {

            get
            {
                return (_typeSelectionCommand = new RelayCommand(async () =>
                {
                    switch (TypeSelected)
                    {
                        case SOFTWARE:
                            Softwares.Clear();
                            await DatabaseHelper.GetAllObjectsAsync(Softwares, "Softwares");
                            ListPage = typeof(AttachToSoftwareListPage);
                            break;
                        case HARDWARE:
                            Hardwares.Clear();
                            await DatabaseHelper.GetAllObjectsAsync(Hardwares, "Hardwares");
                            ListPage = typeof(AttachToHardwareListPage);
                            break;
                    }
                }));
            }
        }

        private Software _software;

        public Software Software
        {
            get
            {
                return _software;
            }
            set
            {
                _software = value;
                RaisePropertyChanged(nameof(Software));
            }
        }

        private RelayCommand _attachToSoftwareCommand;

        public RelayCommand AttachToSoftwareCommand 
        {
            get
            {
                return (_attachToSoftwareCommand = new RelayCommand(async () =>
                {
                    var imgId = ItemSelectionHelper.GetCurrentItemImage().ItemImageId;
                    var itemId = Software.ItemId;
                    var result = await DatabaseHelper.AttachOrDetachImageToItemAsync(imgId, itemId);

                    if (result == System.Net.HttpStatusCode.OK)
                    {
                        _navigationService.NavigateTo("UnattachedImagesPage");
                    }
                    //StatusBarHelper.Instance.StatusBarMessage = $"ItemImageId: {imgId}, ItemId: {itemId}";
                    StatusBarHelper.Instance.StatusBarMessage = result.ToString();
                }));
            }
        }

        private Type _listPage;

        public Type ListPage
        {
            get { return _listPage; }
            set
            {
                _listPage = value;
                RaisePropertyChanged(nameof(ListPage));
            }
        }


        private ObservableCollection<Software> _softwares;

        public ObservableCollection<Software> Softwares
        {
            get
            {
                return _softwares;
            }
            set
            {
                _softwares = value;
                RaisePropertyChanged(nameof(Softwares));
            }
        }

        private ObservableCollection<Hardware> _hardwares { get; set; }

        public ObservableCollection<Hardware> Hardwares
        {
            get { return _hardwares; }
            set
            {
                _hardwares = value;
                RaisePropertyChanged(nameof(Hardwares));
            }
        }
    }
}
