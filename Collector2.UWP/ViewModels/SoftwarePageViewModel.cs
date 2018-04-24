using Collector2.Models;
using Collector2.Models.ViewModels;
using Collector2.UWP.Config;
using Collector2.UWP.Helpers;
using Collector2.UWP.Interface;
using Collector2.UWP.Repository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.ViewModels
{
    public class SoftwarePageViewModel : ViewModelBase, INavigable
    {
        private string Root;

        private ObservableCollection<SoftwareViewModel> _softwareList;
        public ObservableCollection<ItemImage> MainImages { get; set; }

        private ItemImage _selectedImage;


        private RelayCommand _getSoftwareList;

        private readonly INavigationService _navigationService;
        private SoftwareRepository _softwareRepository = new SoftwareRepository();

        public SoftwarePageViewModel(INavigationService navigationService)
        {
            Root = CollectorConfig.ApiRoot;
            _navigationService = navigationService;
            ShowListView = true;

            if (GetSoftwareList.CanExecute(SoftwareList))
            {
                StatusBarHelper.Instance.StatusBarMessage = "Please wait, loading collection from database...";
                GetSoftwareList.Execute(SoftwareList);
            }


            if (this.IsInDesignMode)
            {
                SoftwareList = new ObservableCollection<SoftwareViewModel>()
                {
                    new SoftwareViewModel { Title = "Monkey Island 2"},
                    new SoftwareViewModel { Title = "Sensible World of Soccer 96/97"},
                    new SoftwareViewModel { Title = "Soccer Kid" },
                    new SoftwareViewModel { Title = "Moonstone" }
                };
            }
        }

        public ObservableCollection<SoftwareViewModel> SoftwareList
        {
            get { return _softwareList; }
            set
            {
                _softwareList = value;
                RaisePropertyChanged(nameof(SoftwareList));
            }
        }

        public RelayCommand GetSoftwareList
        {
            get
            {
                SoftwareList = new ObservableCollection<SoftwareViewModel>();
                MainImages = new ObservableCollection<ItemImage>();
                
                return _getSoftwareList = new RelayCommand(async () =>
                {
                    SoftwareList = await _softwareRepository.GetAllAsync();

                    foreach (var s in SoftwareList)
                    {
                        if (s.Title.StartsWith("The "))
                        {
                            s.Title = s.Title.Substring(4) + ", The";
                        }
                    }

                    var mainImages = from s in SoftwareList
                                     from i in s.ItemImages
                                     where i.IsMainImage == true
                                     orderby s.Title ascending
                                     select i;

                    foreach (var i in mainImages)
                    {
                        MainImages.Add(i);
                    }
                    StatusBarHelper.Instance.StatusBarMessage = $"You have {MainImages.Count()} software items in your collection";
                });
            }
        }

        private SoftwareViewModel _selectedSoftware;

        public SoftwareViewModel SelectedSoftware
        {
            get { return _selectedSoftware; }
            set
            {
                _selectedSoftware = value;
                RaisePropertyChanged(nameof(SelectedSoftware));
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

        private void FindAndSetSelectedSoftware(int imgId)
        {
            var query = from s in SoftwareList
                        from i in s.ItemImages
                        where i.ItemImageId == imgId
                        select s;

            SelectedSoftware = query.First();
        }

        private bool _showGridView;
        public bool ShowGridView
        {
            get { return _showGridView; }
            set
            {
                _showGridView = value;
                ShowListView = !value;
                RaisePropertyChanged(nameof(ShowGridView));
            }
        }

        private bool _showListView;
        public bool ShowListView
        {
            get { return _showListView; }
            set
            {
                _showListView = value;
                RaisePropertyChanged(nameof(ShowListView));
            }
        }

        public RelayCommand GridViewImages
        {
            get
            {
                return (new RelayCommand(() =>
                {
                    ShowGridView = !ShowGridView;
                }));
            }
        }


        public RelayCommand GoToDetailsPage
        {
            get
            {
                return (new RelayCommand(() =>
                {
                    if (SelectedImage != null)
                    {
                        FindAndSetSelectedSoftware(SelectedImage.ItemImageId);
                        ItemSelectionHelper.SetCurrentSoftware(SelectedSoftware);
                        _navigationService.NavigateTo("SoftwareDetailsPage");
                    }
                    //if (SelectedSoftware != null)
                    //{
                    //    ItemSelectionHelper.SetCurrentSoftware(SelectedSoftware);
                    //    _navigationService.NavigateTo("SoftwareDetailsPage");
                    //} 
                }));
            }
        }


        public void Activate(object parameter)
        {
            GoBackHelper.Instance.CanGoBack = false;
            if(RefreshHelper.Instance.NeedRefresh)
            {
                StatusBarHelper.Instance.StatusBarMessage = "Collection has changed, reloading...";
                GetSoftwareList.Execute(SoftwareList);
                RefreshHelper.Instance.NeedRefresh = false;
                
            }
        }

        public void Deactivate(object parameter)
        {
            //Todo: Get everything that needs to be redone on deactivation here        
        }
    }
}
