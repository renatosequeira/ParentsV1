using GalaSoft.MvvmLight.Command;
using Parents.Models.HealthManagement;
using Parents.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Parents.ViewModels.Health
{
    public class ChildrenWeightViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        DataService dataService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        ObservableCollection<ChildrenWeight> _childrenWeight;
        List<ChildrenWeight> childrensWeight;

        bool _isRefreshing;
        bool _searchVisibility;
        double _selectedWeight;

        double _weightValue;
        double _oldWeightValue;

        double _maximumWeight;
        double _minimumWeight;

        int _childrenAge;

        bool _isRunning;
        bool _isEnabled;

        int _childrenId;

        string _emptyListImage;

        bool _showEmptyWeightMessage;
        bool _showList;

        string _insertedWeight;
        #endregion

        #region Properties

        public string InsertedWeight
        {
            get
            {

                return _insertedWeight;

            }
            set
            {
                if (_insertedWeight != value)
                {
                    _insertedWeight = value;
                    try
                    {
                        WeightVaue = Convert.ToDouble(value);
                    }
                    catch (Exception)
                    {

                        WeightVaue = 0;
                    }
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(InsertedWeight)));
                }
            }
        }
        
        public bool ShowList
        {
            get
            {
                return _showList;
            }
            set
            {
                if (_showList != value)
                {
                    _showList = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ShowList)));
                }
            }
        }

        public bool ShowEmptyWeightMessage
        {
            get
            {
                return _showEmptyWeightMessage;
            }
            set
            {
                if (_showEmptyWeightMessage != value)
                {
                    _showEmptyWeightMessage = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ShowEmptyWeightMessage)));
                }
            }
        }

        public string EmptyListImage
        {
            get
            {

                return _emptyListImage;

            }
            set
            {
                if (_emptyListImage != value)
                {
                    _emptyListImage = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(EmptyListImage)));
                }
            }
        }

        public int ChildrenId
        {
            get
            {

                return _childrenId;

            }
            set
            {
                if (_childrenId != value)
                {
                    _childrenId = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrenId)));
                }
            }
        }

        public int ChildrenAge
        {
            get
            {

                return _childrenAge;

            }
            set
            {
                if (_childrenAge != value)
                {
                    _childrenAge = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrenAge)));
                }
            }
        }

        public double MinimimWeight
        {
            get
            {

                return _minimumWeight;

            }
            set
            {
                if (_minimumWeight != value)
                {
                    _minimumWeight = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(MinimimWeight)));
                }
            }
        }

        public double MaximumWeight
        {
            get
            {

                return _maximumWeight;

            }
            set
            {
                if (_maximumWeight != value)
                {
                    _maximumWeight = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(MaximumWeight)));
                }
            }
        }

        public double OldWeightValue
        {
            get
            {

                return _oldWeightValue;

            }
            set
            {
                if (_oldWeightValue != value)
                {
                    _oldWeightValue = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(OldWeightValue)));
                }
            }
        }

        public double WeightVaue
        {
            get
            {

                return _weightValue;
                
            }
            set
            {
                if (_weightValue != value)
                {
                    _weightValue = value;
                    
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(WeightVaue)));
                }
            }
        }

        public double SelectedWeight
        {
            get
            {
                return _selectedWeight;
            }
            set
            {
                if (_selectedWeight != value)
                {
                    _selectedWeight = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SelectedWeight)));
                }
            }
        }

        public bool SearchVisibility
        {
            get
            {
                return _searchVisibility;
            }
            set
            {
                if (_searchVisibility != value)
                {
                    _searchVisibility = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SearchVisibility)));
                }
            }
        }
        
        public ObservableCollection<ChildrenWeight> ChildrensWeightList
        {
            get
            {
                return _childrenWeight;
            }
            set
            {
                if (_childrenWeight != value)
                {
                    _childrenWeight = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrensWeightList)));
                }
            }
        }

        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRefreshing)));
                }
            }
        }

        public string Image { get; set; }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }
        #endregion

        #region Constructors
        public ChildrenWeightViewModel()
        {
            instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();
            dataService = new DataService();
            navigationService = new NavigationService();
            _searchVisibility = false;

            LoadChildrenWeightList();

            ChildrenId = Convert.ToInt32(Application.Current.Properties["childrenId"]);
        }

        

        #endregion

        #region Sigleton
        static ChildrenWeightViewModel instance;

        public static ChildrenWeightViewModel GetInstance()
        {
            if (instance == null)
            {
                return new ChildrenWeightViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        public void AddChildrenWeight(ChildrenWeight childrenWeight)
        {
            IsRefreshing = true;
            childrensWeight.Add(childrenWeight);
            ChildrensWeightList = new ObservableCollection<ChildrenWeight>(
                childrensWeight.OrderBy(c => c.RegistryDate));
            IsRefreshing = false;
        }

        public void UpdateChildrenWeight(ChildrenWeight childrenWeight)
        {
            IsRefreshing = true;
            var oldChildrenWeight = childrensWeight.Where(c => c.ChildrenWeightId == childrenWeight.ChildrenWeightId).FirstOrDefault();
            oldChildrenWeight = childrenWeight;

            ChildrensWeightList = new ObservableCollection<ChildrenWeight>(
                childrensWeight.OrderBy(c => c.RegistryDate));
            IsRefreshing = false;
        }

        public async Task DeleteChildrenWeight(ChildrenWeight childrenWeight)
        {
            IsRefreshing = true;

            //verificar se existe ligação à internet
            var connection = await apiService.CheckConnection();

            //se não houver ligação à internet, popup com erro e sai do método
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);

                IsRefreshing = false;
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Delete(
                urlAPI,
                "/api",
                "/ChildrensWeight",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                childrenWeight);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            childrensWeight.Remove(childrenWeight);

            ChildrensWeightList = new ObservableCollection<ChildrenWeight>(
                childrensWeight.OrderBy(c => c.RegistryDate));
            IsRefreshing = false;
        }

        async void LoadChildrenWeightList()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                childrensWeight = dataService.Get<ChildrenWeight>(false);
                //childrens = dataService.GetChildren();

                if (childrensWeight.Count == 0)
                {
                    await dialogService.ShowMessage(
                        "Error",
                        "No children weights available offline.");
                    return;
                }
            }
            else
            {
                var mainViewModel = MainViewModel.GetInstance();

                var urlAPI = Application.Current.Resources["URLAPI"].ToString();

                var response = await apiService.GetList<ChildrenWeight>(
                    urlAPI,
                    "/api",
                    "/ChildrensWeight/WeightForSpecificChildren",
                    mainViewModel.Token.TokenType,
                    mainViewModel.Token.AccessToken, ChildrenId);

                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage(
                        "Error",
                        response.Message);
                    return;
                }

                childrensWeight = (List<ChildrenWeight>)response.Result;

                //SaveChildrenWeightOnDB();
            }

            ChildrensWeightList = new ObservableCollection<ChildrenWeight>(childrensWeight.OrderByDescending(c => c.RegistryDate));


            if (ChildrensWeightList.Count == 0)
            {
                ShowList = false;
                ShowEmptyWeightMessage = true;
                EmptyListImage = "ic_empty_list";
            }
            else
            {
                ShowList = true;
                ShowEmptyWeightMessage = false;
                EmptyListImage = "ic_empty_list";
            }

            try
            {
                OldWeightValue = Convert.ToDouble(ChildrensWeightList.FirstOrDefault().WeightVaue);
                InsertedWeight = ChildrensWeightList.FirstOrDefault().WeightVaue.ToString();
            }
            catch (Exception)
            {

                OldWeightValue = 0;
                InsertedWeight = "0";
            }
            //Search();
            IsRefreshing = false;
        }

        void SaveChildrenWeightOnDB()
        {
            dataService.DeleteAll<ChildrenWeight>();
            dataService.Save(childrensWeight);
        }

        #endregion

        #region Commands
        public ICommand HomeViewCommand
        {
            get
            {
                return new RelayCommand(GoHome);
            }
        }

        async void GoHome()
        {
            await navigationService.NavigateOnMaster("HomeView");
        }

        public ICommand SelectChildrenCommand
        {
            get
            {
                return new RelayCommand(SelectChildren);
            }
        }

        async void SelectChildren()
        {
            await navigationService.NavigateOnMaster("ChildrenDetails");
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadChildrenWeightList);
            }
        }

        public ICommand SearchViewCommand
        {
            get
            {
                return new RelayCommand(OpenSearch);
            }
        }

        private void OpenSearch()
        {
            if (SearchVisibility)
            {

                SearchVisibility = false;
            }
            else
            {
                SearchVisibility = true;
            }
        }

        public ICommand AddWeightCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        async void Save()
        {
            double lastWeight;
            try
            {
                lastWeight = await GetLastWeight();
            }
            catch (Exception ex)
            {
                lastWeight = 0;
            }

            if (string.IsNullOrEmpty(InsertedWeight))
            {
                await dialogService.ShowMessage("Error", "Please insert Weight value");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            //verificar se existe ligação à internet
            var connection = await apiService.CheckConnection();

            //se não houver ligação à internet, popup com erro e sai do método
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);

                IsRunning = false;
                IsEnabled = true;
                return;
            }

            var childrenWeight = new ChildrenWeight
            {
                ChildrenId = ChildrenId,
                WeightUnit = "Kg",
                WeightVaue = Convert.ToDouble(WeightVaue),
                RegistryDate = DateTime.Now,
                OldWeightValue = OldWeightValue

            };

            var mainViewModel = MainViewModel.GetInstance();

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Post(
                urlAPI,
                "/api",
                "/ChildrensWeight/WeightForSpecificChildren",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                childrenWeight);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            //await navigationService.BackOnMaster();
            LoadChildrenWeightList();

            IsRunning = false;
            IsEnabled = true;
        }

        public async Task<double> GetLastWeight()
        {
            double lastWeight = 0;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {

            }
            else
            {
                var mainViewModel = MainViewModel.GetInstance();

                var urlAPI = Application.Current.Resources["URLAPI"].ToString();

                var response = await apiService.GetList<ChildrenWeight>(
                    urlAPI,
                    "/api",
                    "/ChildrensWeight",
                    mainViewModel.Token.TokenType,
                    mainViewModel.Token.AccessToken);

                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage(
                        "Error",
                        response.Message);
                    return 0;
                }

                childrensWeight = (List<ChildrenWeight>)response.Result;

                //SaveChildrenWeightOnDB();
            }

            ChildrensWeightList = new ObservableCollection<ChildrenWeight>(childrensWeight.OrderBy(c => c.RegistryDate));
            lastWeight = ChildrensWeightList.LastOrDefault().WeightVaue;

            return lastWeight;
        }

       
        #endregion
    }
}
