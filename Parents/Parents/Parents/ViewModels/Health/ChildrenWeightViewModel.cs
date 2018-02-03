using GalaSoft.MvvmLight.Command;
using Parents.Models.HealthManagement;
using Parents.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
        #endregion

        #region Properties

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

        public void UpdateChildren(ChildrenWeight childrenWeight)
        {
            IsRefreshing = true;
            var oldChildrenWeight = childrensWeight.Where(c => c.ChildrenWeightId == childrenWeight.ChildrenWeightId).FirstOrDefault();
            oldChildrenWeight = childrenWeight;

            ChildrensWeightList = new ObservableCollection<ChildrenWeight>(
                childrensWeight.OrderBy(c => c.RegistryDate));
            IsRefreshing = false;
        }

        public async Task DeleteChildren(ChildrenWeight childrenWeight)
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
                    "/Childrens",
                    mainViewModel.Token.TokenType,
                    mainViewModel.Token.AccessToken);

                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage(
                        "Error",
                        response.Message);
                    return;
                }

                childrensWeight = (List<ChildrenWeight>)response.Result;

                SaveChildrenWeightOnDB();
            }

            ChildrensWeightList = new ObservableCollection<ChildrenWeight>(childrensWeight.OrderBy(c => c.RegistryDate));
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

        public ICommand NewChildrenCommand
        {
            get
            {
                return new RelayCommand(GoNewChildren);
            }
        }

        async void GoNewChildren()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.NewChildrenWeight = new NewWeightViewModel();  //Liga o objecto NewChildren a um viewmodel
            await dialogService.ShowMessage("INFO", "To be implemented");
            //await navigationService.NavigateOnMaster("NewChildrenView");
        }
        #endregion
    }
}
