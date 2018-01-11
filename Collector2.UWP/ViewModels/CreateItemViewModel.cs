using Collector2.UWP.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.ViewModels
{
    public class CreateItemViewModel : ViewModelBase
    {

        private const string SOFTWARE = "Software";
        private const string HARDWARE = "Hardware";

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

        private Type _formPage;

        public Type FormPage
        {
            get
            {
                return _formPage;
            }
            set
            {
                _formPage = value;
                RaisePropertyChanged(nameof(FormPage));
            }
        }

        private RelayCommand _typeSelectCommand;

        public RelayCommand TypeSelectionCommand
        {
            get
            {
                return (_typeSelectCommand = new RelayCommand(() =>
                {
                    switch (TypeSelected)
                    {
                        case SOFTWARE:
                            FormPage = typeof(NewSoftwarePage);
                            break;
                        case HARDWARE:
                            break;
                        default:
                            break;
                    }
                }));
            }
        }
    }
}
