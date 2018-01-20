using Collector2.Models;
using Collector2.UWP.Interface;
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
        private const string BASEURI = "https://collectorv2.azurewebsites.net/api/";

        private ObservableCollection<Software> _softwareList;
        private RelayCommand _getSoftwareList;
        private readonly INavigationService _navigationService;

        public SoftwarePageViewModel(INavigationService navigationService)
        {
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
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BASEURI);
                        var json = await client.GetStringAsync("Softwares");
                        Software[] softwares = JsonConvert.DeserializeObject<Software[]>(json);
                        foreach (var h in softwares)
                        {
                            SoftwareList.Add(h);
                        }
                    }
                });
            }
        }

        public void Activate(object parameter)
        {
            if(GetSoftwareList.CanExecute(SoftwareList))
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
