using Collector2.Models;
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

        private ObservableCollection<Software> _softwareList;

        private RelayCommand _getSoftwareList;

        private readonly INavigationService _navigationService;
        private SoftwareRepository _softwareRepository = new SoftwareRepository();

        public SoftwarePageViewModel(INavigationService navigationService)
        {
            Root = CollectorConfig.ApiRoot;
            _navigationService = navigationService;
            
            if (this.IsInDesignMode)
            {
                SoftwareList = new ObservableCollection<Software>()
                {
                    new Software { Title = "Monkey Island 2"},
                    new Software { Title = "Sensible World of Soccer 96/97"},
                    new Software { Title = "Soccer Kid" },
                    new Software { Title = "Moonstone" }
                };
            }
        }

        public ObservableCollection<Software> SoftwareList
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
                SoftwareList = new ObservableCollection<Software>();
                return _getSoftwareList = new RelayCommand(async () =>
                {
                    SoftwareList = await _softwareRepository.GetAllAsync();
                });



            }
        }

        private Software _selectedSoftware;

        public Software SelectedSoftware
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
                    ItemSelectionHelper.CurrentSoftware = SelectedSoftware;
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
