using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.ViewModels
{
    public class AttachToItemViewModel : ViewModelBase
    {
        private string _status = "This is the Attach View";

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged(nameof(Status));
            }
        }
        public AttachToItemViewModel()
        {
        }

    }
}
