using Collector2.UWP.Interface;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.ViewModels
{
    public class NewSoftwareViewModel : ViewModelBase, INavigable
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

        private ObservableCollection<int> _years;

        public ObservableCollection<int> Years
        {
            get
            {
                return _years;
            }
        }

        private int _selectedYear;

        public int SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                _selectedYear = value;
                RaisePropertyChanged(nameof(SelectedYear));
            }
        }

        public void Setup()
        {
            SelectedYear = 1977;
            _years = new ObservableCollection<int>();
            for (var i = 1977; i <= 2018; i++)
            {
                _years.Add(i);
            }
        }

        public void Activate(object parameter)
        {
            Setup();
        }

        public void Deactivate(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
