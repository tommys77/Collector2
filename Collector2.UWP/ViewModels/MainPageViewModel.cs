using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using Windows.UI.Xaml;

namespace Collector2.UWP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        private bool isPaneOpen;

        public bool IsPaneOpen
        {
            get {
                return isPaneOpen;
            }
            set {
                if(!Equals(IsPaneOpen, value))
                {
                    isPaneOpen = value;
                    RaisePropertyChanged();
                }
            }
        }

        public void OpenPane()
        {
            IsPaneOpen = !IsPaneOpen;
        }
    }
}
