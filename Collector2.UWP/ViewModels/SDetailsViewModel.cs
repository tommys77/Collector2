using Collector2.Models;
using Collector2.UWP.Helpers;
using Collector2.UWP.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.ViewModels
{
    public partial class SDetailsViewModel : ViewModelBase, INavigable
    {

        private string _title;
        private string _year;
        private string _category;
        private string _hardware;
        private bool _isInEditMode;
        private bool _isInViewMode;
        private ItemImage _selectedImage;

        private List<ItemImage> _images;

        private RelayCommand _editModeBtn;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public ItemImage SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
                RaisePropertyChanged(nameof(SelectedImage));
            }
        }

        public List<ItemImage> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                RaisePropertyChanged(nameof(Images));
            }
        }

        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
                RaisePropertyChanged(nameof(Year));
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                RaisePropertyChanged(nameof(Category));
            }
        }

        public string Format { get; private set; }

        public string Hardware
        {
            get { return _hardware; }
            set
            {
                _hardware = value;
                RaisePropertyChanged(nameof(Hardware));
            }
        }

        public int FormatCount { get; private set; }

        public bool IsInEditMode
        {
            get
            {
                return _isInEditMode;
            }
            set
            {
                if (!IsInEditMode.Equals(value))
                {
                    _isInEditMode = value;
                    IsInViewMode = !value;
                    RaisePropertyChanged(nameof(IsInEditMode));
                }
            }
        }

        public bool IsInViewMode
        {
            get { return _isInViewMode; }
            set
            {
                _isInViewMode = value;
                RaisePropertyChanged(nameof(IsInViewMode));
            }
        }

        public SDetailsViewModel()
        {
            IsInViewMode = true;
            if (IsInDesignMode)
            {
                IsInEditMode = true;
                IsInViewMode = true;
            }
        }

        public RelayCommand EditModeCommand
        {
            get
            {
                return _editModeBtn
                    ?? (_editModeBtn = new RelayCommand(() =>
                      {
                          IsInEditMode = !IsInEditMode;
                      }));
            }
        }

        public void Activate(object parameter)
        {
            var _current = ItemSelectionHelper.GetCurrentSoftware();

            if (_current != null)
            {
                Title = _current.Title;
                Year = _current.YearOfRelease.ToString();
                Hardware = _current.HardwareSpec.HardwareSpecName;
                Category = _current.Category.CategoryName;
                Format = _current.Format.FormatName;
                FormatCount = _current.FormatCount;

                SelectedImage = _current.ItemImages.First();

                Images = _current.ItemImages;

            }
        }

        public void Deactivate(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
