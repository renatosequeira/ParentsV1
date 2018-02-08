namespace Parents.ViewModels.Health
{
    using GalaSoft.MvvmLight.Command;
    using Services;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Parents.Models.HealthManagement;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class NewWeightViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Attributes
        ObservableCollection<ChildrenWeight> _childrenWeight;
        List<ChildrenWeight> childrensWeight;

        public double _weightValue { get; set; }
        public string _weightUnit { get; set; }
        public int _childrenId { get; set; }
        public DateTime _registryDate { get; set; }
        public double _oldWeightValue { get; set; }
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties
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

        public DateTime RegistryDate
        {
            get
            {
                return _registryDate;
            }
            set
            {
                if (_registryDate != value)
                {
                    _registryDate = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(RegistryDate)));
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

        public string WeightUnit
        {
            get
            {
                return _weightUnit;
            }
            set
            {
                if (_weightUnit != value)
                {
                    _weightUnit = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(WeightUnit)));
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
        public NewWeightViewModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        async void Save()
        {
            
            double lastWeight = await GetLastWeight();

            if (WeightVaue == 0)
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
                ChildrenId = 2,
                WeightUnit = "Kg",
                WeightVaue = WeightVaue,
                RegistryDate = DateTime.Now,
                OldWeightValue = WeightVaue - lastWeight

            };

            var mainViewModel = MainViewModel.GetInstance();

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Post(
                urlAPI,
                "/api",
                "/ChildrensWeight",
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

            await navigationService.BackOnMaster();

            IsRunning = false;
            IsEnabled = true;
        }

        public async Task<double> GetLastWeight()
        {
            double lastWeight = 0;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                //childrensWeight = dataService.Get<ChildrenWeight>(false);
                ////childrens = dataService.GetChildren();

                //if (childrensWeight.Count == 0)
                //{
                //    await dialogService.ShowMessage(
                //        "Error",
                //        "No children weights available offline.");
                //    return;
                //}
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
