using Collector2.Models;
using Collector2.Models.ViewModels;
using Collector2.UWP.Helpers;
using Collector2.UWP.Interface;
using Collector2.UWP.Repository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Collector2.UWP.ViewModels
{
    public partial class SDetailsViewModel : ViewModelBase, INavigable
    {
        private bool _isInEditMode;
        private ObservableCollection<Format> _allFormats;
        private ObservableCollection<Category> _categories;
        private ObservableCollection<HardwareSpec> _hardwareSpecs;

        public RelayCommand CancelChanges
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Current = new SoftwareViewModel(ItemSelectionHelper.GetCurrentSoftware());
                    if (IsInEditMode)
                    {
                        IsInEditMode = !IsInEditMode;
                    }
                });
            }
        }

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                RaisePropertyChanged(nameof(Categories));
            }
        }

        public ObservableCollection<HardwareSpec> HardwareSpecs
        {
            get { return _hardwareSpecs; }
            set
            {
                _hardwareSpecs = value;
                RaisePropertyChanged(nameof(HardwareSpecs));
            }
        }

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

        public ObservableCollection<Format> AllFormats
        {
            get { return _allFormats; }
            set
            {
                _allFormats = value;
                RaisePropertyChanged(nameof(AllFormats));
            }
        }

        public RelayCommand SetupEditMode
        {
            get
            {
                return new RelayCommand(async () =>
                { 
                    await LoadData();
                });
            }
        }

        public async Task LoadData()
        {
            AllFormats = new ObservableCollection<Format>();
            Categories = new ObservableCollection<Category>();
            HardwareSpecs = new ObservableCollection<HardwareSpec>();

            await GenericDbAccess.GetAllObjectsAsync(AllFormats, "Formats");
            await GenericDbAccess.GetAllObjectsAsync(Categories, "Categories");
            await GenericDbAccess.GetAllObjectsAsync(HardwareSpecs, "HardwareSpecs");
        }

        public RelayCommand EditModeCommand
        {
            get
            {
                return new RelayCommand(() =>
                    {
                        IsInEditMode = !IsInEditMode;
                    });
            }
        }

        public RelayCommand SaveChangesToDatabase
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SoftwareViewModel, Software>();
                    });

                    var mapper = config.CreateMapper();
                    var software = mapper.Map<SoftwareViewModel, Software>(Current);

                    var repository = new SoftwareRepository();
                    await repository.UpdateAsync(software);

                    if(repository.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        StatusBarHelper.Instance.StatusBarMessage = "Changes has been saved.";
                        IsInEditMode = false;
                        RefreshHelper.Instance.NeedRefresh = true;
                        Current.HasChanged = false;
                    }
                });
            }
        }
    }
}
