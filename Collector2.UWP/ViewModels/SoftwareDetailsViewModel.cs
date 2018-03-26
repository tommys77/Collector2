using Collector2.Models;
using Collector2.UWP.Interface;
using Collector2.UWP.Views.PivotViews;
using GalaSoft.MvvmLight;
using System;

namespace Collector2.UWP.ViewModels
{
    public class SoftwareDetailsViewModel : ViewModelBase, INavigable
    {

        private Type _details;
        private Type _reviews;
        private Type _comments;

        public SoftwareDetailsViewModel()
        {
            if (IsInDesignMode)
            {
                // Design mode code
            }
        }

        public Type Details
        {
            get { return _details; }
            set
            {
                _details = value;
                RaisePropertyChanged(nameof(Details));
            }
        }

        public Type Reviews
        {
            get { return _reviews; }
            set
            {
                _reviews = value;
                RaisePropertyChanged(nameof(Details));
            }
        }

        public Type Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                RaisePropertyChanged(nameof(Comments));
            }
        }

        public void Activate(object parameter)
        {
            Details = typeof(SDetails);
        }

        public void Deactivate(object parameter)
        {

        }
    }
}