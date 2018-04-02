using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Collector2.Models.ViewModels
{
    public class SoftwareViewModel : INotifyPropertyChanged
    {
        public SoftwareViewModel() { }

        public int CategoryId { get; set; }
        public int FormatId { get; set; }
        public int HardwareSpecId { get; set; }

        public SoftwareViewModel(SoftwareViewModel model)
        {
            ItemId = model.ItemId;
            Title = model.Title;
            SoftwareType = model.SoftwareType;
            YearOfRelease = model.YearOfRelease;
            FormatCount = model.FormatCount;
            Requirements = model.Requirements;
            Condition = model.Condition;

            ItemImages = model.ItemImages;
            HardwareSpec = model.HardwareSpec;
            Category = model.Category;
            Format = model.Format;

            HasChanged = false;
        }

        #region Properties and private fields

        private int _itemId;
        public int ItemId
        {
            get { return _itemId; }
            set
            {
                _itemId = value;
                HasChanged = true;
                NotifyPropertyChanged(nameof(ItemId));
            }
        }

        private string _softwareType;
        public string SoftwareType
        {
            get { return _softwareType; }
            set
            {
                _softwareType = value;
                HasChanged = true;
                NotifyPropertyChanged(nameof(SoftwareType));
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                HasChanged = true;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        private int _yearOfRelease;
        public int YearOfRelease
        {
            get { return _yearOfRelease; }
            set
            {
                _yearOfRelease = value;
                HasChanged = true;
                NotifyPropertyChanged(nameof(YearOfRelease));
            }
        }

        private int _formatCount;
        public int FormatCount
        {
            get { return _formatCount; }
            set
            {
                _formatCount = value;
                HasChanged = true;
                NotifyPropertyChanged(nameof(FormatCount));
            }
        }

        private string _requirements;
        public string Requirements
        {
            get { return _requirements; }
            set
            {
                _requirements = value;
                HasChanged = true;
                NotifyPropertyChanged(nameof(Requirements));
            }
        }

        private string _condition;
        public string Condition
        {
            get { return _condition; }
            set
            {
                _condition = value;
                HasChanged = true;
                NotifyPropertyChanged(nameof(Condition));
            }
        }

        private List<ItemImage> _itemImages;
        public List<ItemImage> ItemImages
        {
            get { return _itemImages; }
            set
            {
                _itemImages = value;
                HasChanged = true;
                NotifyPropertyChanged(nameof(ItemImages));
            }
        }


       

        private HardwareSpec _hardwareSpec;
        public HardwareSpec HardwareSpec
        {
            get { return _hardwareSpec; }
            set
            {
                _hardwareSpec = value;
                HardwareSpecId = value.HardwareSpecId;
                HasChanged = true;
                NotifyPropertyChanged(nameof(HardwareSpec));
            }
        }

        private Category _category;
        public Category Category
        {
            get { return _category; }
            set
            {
                _category = value;
                CategoryId = value.CategoryId;
                HasChanged = true;
                NotifyPropertyChanged(nameof(Category));
            }
        }

        private Format _format;
        public Format Format
        {
            get { return _format; }
            set
            {
                _format = value;
                FormatId = value.FormatId;
                HasChanged = true;
                NotifyPropertyChanged(nameof(Format));
            }
        }

        private bool _hasChanged = false;
        public bool HasChanged
        {
            get { return _hasChanged; }
            private set
            {
                _hasChanged = value;
                NotifyPropertyChanged(nameof(HasChanged));
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
