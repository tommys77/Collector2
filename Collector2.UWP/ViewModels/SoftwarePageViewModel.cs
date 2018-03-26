using Collector2.Models;
using Collector2.Models.ViewModels;
using Collector2.UWP.Config;
using Collector2.UWP.Helpers;
using Collector2.UWP.Interface;
using Collector2.UWP.Repository;
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
    public class SoftwarePageViewModel : ViewModelBase, INavigable
    {
        private string Root;

        private ObservableCollection<SoftwareViewModel> _softwareList;

        private RelayCommand _getSoftwareList;

        private readonly INavigationService _navigationService;
        private SoftwareRepository _softwareRepository = new SoftwareRepository();

        public SoftwarePageViewModel(INavigationService navigationService)
        {
            Root = CollectorConfig.ApiRoot;
            _navigationService = navigationService;
            
            if (this.IsInDesignMode)
            {
                SoftwareList = new ObservableCollection<SoftwareViewModel>()
                {
                    new SoftwareViewModel { Title = "Monkey Island 2"},
                    new SoftwareViewModel { Title = "Sensible World of Soccer 96/97"},
                    new SoftwareViewModel { Title = "Soccer Kid" },
                    new SoftwareViewModel { Title = "Moonstone" }
                };
            }
        }

        public ObservableCollection<SoftwareViewModel> SoftwareList
        {
            get { return _softwareList; }
            set
            {
                _softwareList = value;
                RaisePropertyChanged(nameof(SoftwareList));
            }
        }

        public RelayCommand GetSoftwareList
        {
            get
            {
                SoftwareList = new ObservableCollection<SoftwareViewModel>();
                return _getSoftwareList = new RelayCommand(async () =>
                {
                    SoftwareList = await _softwareRepository.GetAllAsync();
                });



            }
        }

        private SoftwareViewModel _selectedSoftware;

        public SoftwareViewModel SelectedSoftware
        {
            get { return _selectedSoftware; }
            set
            {
                _selectedSoftware = value;
                RaisePropertyChanged(nameof(SelectedSoftware));
            }
        }

        public RelayCommand GoToDetailsPage
        {
            get
            {
                return (new RelayCommand(() =>
                {
                    ItemSelectionHelper.SetCurrentSoftware(SelectedSoftware);
                    _navigationService.NavigateTo("SoftwareDetailsPage");
                }));
            }
        }

        public void Activate(object parameter)
        {
            if (GetSoftwareList.CanExecute(SoftwareList))
            {
                GetSoftwareList.Execute(SoftwareList);
            }
        }

        public void Deactivate(object parameter)
        {
            //Todo: Get everything that needs to be redone on deactivation here        
        }
    }
}
