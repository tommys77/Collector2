using Collector2.Models;
using Collector2.Models.DTO;
using Collector2.UWP.Helpers;
using Collector2.UWP.Interface;
using Collector2.UWP.Repository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Collector2.UWP.ViewModels
{
    public class NewSoftwareViewModel : ViewModelBase, INavigable
    {

        private const string Root = "http://collectorv2.azurewebsites.net/api/";

        private string _newFormatName;
        private string _openCloseFormatCreation;
        private string _newFormatStatus;
        private string _softwareTitle;
        private string _softwareRequirements;
        private string _softwareType;
        private int _softwareFormatCount;
        private int _softwareYearOfRelease;

        private Category _softwareCategory;
        private Format _softwareFormat;
        private HardwareSpec _softwareHardwareSpec;

        private bool _isNewFormatOpen = false;

        private ObservableCollection<Format> _formats;
        private ObservableCollection<int> _years;
        private ObservableCollection<Category> _categories;
        private ObservableCollection<HardwareSpec> _hardwareSpecs;

        private RelayCommand _newFormatCommand;
        private RelayCommand _loadFormatsCommand;
        private RelayCommand _createFormatCommand;
        private RelayCommand _loadCategoriesCommand;
        private RelayCommand _loadHardwareSpecsCommand;
        private RelayCommand _saveToDatabase;

        private readonly INavigationService _navigationService;

        public NewSoftwareViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            if (this.IsInDesignMode)
            {
                IsNewFormatOpen = true;
            }
        }

        public string NewFormatName
        {
            get { return _newFormatName; }
            set
            {
                if (!value.Equals(_newFormatName))
                {
                    _newFormatName = value;
                    RaisePropertyChanged(nameof(NewFormatName));
                }
            }
        }

        public string OpenCloseFormatCreation
        {
            get { return _openCloseFormatCreation; }
            set
            {
                _openCloseFormatCreation = value;
                RaisePropertyChanged(nameof(OpenCloseFormatCreation));
            }
        }

        public string NewFormatStatus
        {
            get { return _newFormatStatus; }
            set
            {
                _newFormatStatus = value;
                RaisePropertyChanged(nameof(NewFormatStatus));
            }
        }

        public string SoftwareTitle
        {
            get { return _softwareTitle; }
            set
            {
                _softwareTitle = value;
                RaisePropertyChanged(nameof(SoftwareTitle));
            }
        }

        public string SoftwareRequirements
        {
            get { return _softwareRequirements; }
            set
            {
                _softwareRequirements = value;
                RaisePropertyChanged(nameof(SoftwareRequirements));
            }
        }

        public string SoftwareType
        {
            get { return _softwareType; }
            set
            {
                _softwareType = value;
                RaisePropertyChanged(nameof(SoftwareType));
            }
        }

        public int SoftwareFormatCount
        {
            get { return _softwareFormatCount; }
            set
            {
                _softwareFormatCount = value;
                RaisePropertyChanged(nameof(SoftwareFormatCount));
            }
        }

        public int SoftwareYearOfRelease
        {
            get { return _softwareYearOfRelease; }
            set
            {
                _softwareYearOfRelease = value;
                RaisePropertyChanged(nameof(SoftwareYearOfRelease));
            }
        }

        public bool IsNewFormatOpen
        {
            get
            {
                return _isNewFormatOpen;
            }
            set
            {
                _isNewFormatOpen = value;
                RaisePropertyChanged(nameof(IsNewFormatOpen));
            }
        }

        public Category SoftwareCategory
        {
            get { return _softwareCategory; }
            set
            {
                _softwareCategory = value;
                RaisePropertyChanged(nameof(SoftwareCategory));
            }
        }

        public Format SoftwareFormat
        {
            get { return _softwareFormat; }
            set
            {
                _softwareFormat = value;
                RaisePropertyChanged(nameof(SoftwareFormat));
            }
        }

        public HardwareSpec SoftwareHardwareSpec
        {
            get { return _softwareHardwareSpec; }
            set
            {
                _softwareHardwareSpec = value;
                RaisePropertyChanged(nameof(SoftwareHardwareSpec));
            }
        }

        public ObservableCollection<Format> Formats
        {
            get { return _formats; }
            set
            {
                _formats = value;
                RaisePropertyChanged(nameof(Formats));
            }
        }

        public ObservableCollection<int> Years
        {
            get
            {
                return _years;
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

        public RelayCommand NewFormatCommand
        {
            get
            {
                return (_newFormatCommand = new RelayCommand(() =>
               {
                   IsNewFormatOpen = !IsNewFormatOpen;
                   if (!IsNewFormatOpen)
                   {
                       NewFormatStatus = "";
                   }
                   SetFormatCreationButtonContent();
               }));
            }
        }

        public RelayCommand LoadFormatsCommand
        {
            get
            {
                Formats = new ObservableCollection<Format>();
                return (_loadFormatsCommand = new RelayCommand(async () =>
                {
                    using (var client = new HttpClient())
                    {
                        Formats.Clear();
                        await GenericDbAccess.GetAllObjectsAsync(Formats, "Formats");
                    }
                }));
            }
        }

        public RelayCommand LoadHardwareSpecsCommand
        {
            get
            {
                HardwareSpecs = new ObservableCollection<HardwareSpec>();
                return _loadHardwareSpecsCommand = new RelayCommand(async () =>
                {
                    HardwareSpecs.Clear();
                    await GenericDbAccess.GetAllObjectsAsync(HardwareSpecs, "HardwareSpecs");
                });
            }
        }

        public RelayCommand CreateFormatCommand
        {
            get
            {
                if (NewFormatName != null || NewFormatName != "")
                {
                    return (_createFormatCommand = new RelayCommand(async () =>
                    {
                        var format = new Format { FormatName = NewFormatName };

                        using (HttpClient client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var uri = Root + "Formats";
                            var json = JsonConvert.SerializeObject(format);
                            var content = new StringContent(json, Encoding.UTF8, "application/json");
                            var response = await client.PostAsync(uri, content);

                            if (response.IsSuccessStatusCode)
                            {
                                SetFormatCreationButtonContent();

                                LoadFormatsCommand.Execute(Formats);

                                IsNewFormatOpen = !IsNewFormatOpen;
                            }

                            else NewFormatStatus = response.ToString();

                        }
                    }));
                }
                else
                {
                    return (_createFormatCommand = new RelayCommand(() =>
                    {
                        NewFormatStatus = "Invalid name";
                    }));
                }
            }
        }

        public RelayCommand LoadCategoriesCommand
        {
            get
            {
                Categories = new ObservableCollection<Category>();
                return _loadCategoriesCommand = new RelayCommand(async () =>
               {
                   using (var client = new HttpClient())
                   {
                       Categories.Clear();
                       client.BaseAddress = new Uri(Root);
                       var json = await client.GetStringAsync("Categories");
                       Category[] categories = JsonConvert.DeserializeObject<Category[]>(json);
                       foreach (var c in categories)
                       {
                           Categories.Add(c);
                       }
                   }
               });
            }
        }

        public RelayCommand SaveToDatabase
        {
            get
            {
                return _saveToDatabase = new RelayCommand(async () =>
                {
                    var imgId = ItemSelectionHelper.GetCurrentItemImage().ItemImageId;
                    var software = new Software
                    {
                        Title = SoftwareTitle,
                        YearOfRelease = SoftwareYearOfRelease,
                        FormatId = SoftwareFormat.FormatId,
                        FormatCount = SoftwareFormatCount,
                        HardwareSpecId = SoftwareHardwareSpec.HardwareSpecId,
                        CategoryId = SoftwareCategory.CategoryId,
                        SoftwareType = SoftwareType,
                        Condition = SoftwareCondition,
                        Requirements = SoftwareRequirements
                    };

                    var repository = new SoftwareRepository();
                    await repository.AddAsync(software, imgId);

                    //"SoftwareType": "sample string 2",
                    //"Title": "sample string 3",
                    //"YearOfRelease": 4,
                    //"FormatCount": 5,
                    //"Requirements": "sample string 6",
                    //"Condition": "sample string 7",
                    //"FormatId": 8,
                    //"CategoryId": 9,
                    //"HardwareSpecId": 10,

                    if (repository.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        _navigationService.NavigateTo("UnattachedImagesPage");
                    }

                    StatusBarHelper.Instance.StatusBarMessage = repository.StatusCode.ToString();
                });
            }
        }

        private string _softwareCondition;

        public string SoftwareCondition
        {
            get { return _softwareCondition; }
            set
            {
                _softwareCondition = value;
                RaisePropertyChanged(nameof(SoftwareCondition));
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

        public void SetFormatCreationButtonContent()
        {
            switch (OpenCloseFormatCreation)
            {
                case "+":
                    OpenCloseFormatCreation = "-";
                    break;

                case "-":
                    OpenCloseFormatCreation = "+";
                    break;

                default:
                    OpenCloseFormatCreation = "+";
                    break;
            }
        }

        private void Setup()
        {
            SetFormatCreationButtonContent();
            SoftwareYearOfRelease = 1977;
            PopulateComboBoxes();
        }

        private void PopulateComboBoxes()
        {
            _years = new ObservableCollection<int>();
            for (var i = 1977; i <= DateTime.Now.Year; i++)
            {
                _years.Add(i);
            }
            if (LoadFormatsCommand.CanExecute(Formats))
            {
                LoadFormatsCommand.Execute(Formats);
            }

            if (LoadCategoriesCommand.CanExecute(Categories))
            {
                LoadCategoriesCommand.Execute(Categories);
            }

            if (LoadHardwareSpecsCommand.CanExecute(HardwareSpecs))
            {
                LoadHardwareSpecsCommand.Execute(HardwareSpecs);
            }
        }

    }
}