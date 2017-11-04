using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.ViewModels
{
    public class SoftwarePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public SoftwarePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
