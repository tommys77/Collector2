using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.ViewModels
{
    public class NewSoftwareViewModel : ViewModelBase
    {
        private bool _isNewFormatOpen = false;

        public bool IsNewFormatOpen
        {
            get { return _isNewFormatOpen; }
            set
            {
                _isNewFormatOpen = value;
                RaisePropertyChanged(nameof(IsNewFormatOpen));
            }
        }

    }
}
