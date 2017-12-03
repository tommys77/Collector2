using Collector2.UWP.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.ViewModels
{
    public class SoftwarePageViewModel : ViewModelBase, INavigable
    {
        private readonly INavigationService _navigationService;

        public SoftwarePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void Activate(object parameter)
        {
            //Todo: Get everything that needs to be redone on activation here
        }

        public void Deactivate(object parameter)
        {
            //Todo: Get everything that needs to be redone on deactivation here        
        }
    }
}
