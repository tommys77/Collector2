using Collector2.Models;
using Collector2.Models.ViewModels;
using Collector2.UWP.Config;
using Collector2.UWP.Helpers;
using Collector2.UWP.Repository;
using Collector2.UWP.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.ViewModels
{
    public class AttachToItemViewModel : ViewModelBase
    {
        private const string SOFTWARE = "Software";
        private const string HARDWARE = "Hardware";

        private SoftwareRepository _softwareRepository = new SoftwareRepository();

        private static string Root;

        INavigationService _navigationService;


        public AttachToItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Root = CollectorConfig.ApiRoot;

            Softwares = new ObservableCollection<SoftwareViewModel>();
            Hardwares = new ObservableCollection<Hardware>();

            if (IsInDesignMode)
            {
                TypeSelected = SOFTWARE;

                for (var i = 0; i < 10; i++)
                {
                    Softwares.Add(
                       new SoftwareViewModel
                       {
                           Title = "Title " + i,
                           HardwareSpec = new HardwareSpec
                           {
                               HardwareSpecName = "Commodore Amiga 500"
                           }
                       });
                }
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
                            Softwares = await _softwareRepository.GetAllAsync();
                            ListPage = typeof(AttachToSoftwareListPage);
                            break;
                        case HARDWARE:
                            Hardwares.Clear();
                            await GenericDbAccess.GetAllObjectsAsync(Hardwares, "Hardwares");
                            ListPage = typeof(AttachToHardwareListPage);
                            break;
                    }
                }));
            }
        }

        private SoftwareViewModel _software;

        public SoftwareViewModel Software
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

                    var repository = new ItemImageRepository();
                    await repository.AttachOrDetachImageToItem(imgId, itemId);

                    if (repository.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        _navigationService.NavigateTo("UnattachedImagesPage");
                    }
                    else StatusBarHelper.Instance.StatusBarMessage = repository.StatusCode.ToString();
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


        private ObservableCollection<SoftwareViewModel> _softwares;

        public ObservableCollection<SoftwareViewModel> Softwares
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
