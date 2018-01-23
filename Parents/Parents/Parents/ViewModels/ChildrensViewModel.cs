namespace Parents.ViewModels
{
    using Models;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Linq;
    using Services;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class ChildrensViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        DataService dataService;
        #endregion

        #region Attributes
        ObservableCollection<Children> _childrens;
        List<Children> childrens;
        bool _isRefreshing;
        #endregion

        #region Properties       
        public ObservableCollection<Children> ChildrensList
        {
            get
            {
                return _childrens;
            }
            set
            {
                if(_childrens != value)
                {
                    _childrens = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrensList)));
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
        #endregion

        #region Constructors
        public ChildrensViewModel()
        {
            instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();
            dataService = new DataService();

            LoadChildrens();
        }
        #endregion

        #region Sigleton
        static ChildrensViewModel instance;

        public static ChildrensViewModel GetInstance()
        {
            if (instance == null)
            {
                return new ChildrensViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        public void AddChildren(Children children)
        {
            IsRefreshing = true;
            childrens.Add(children);
            ChildrensList = new ObservableCollection<Children>(
                childrens.OrderBy(c => c.ChildrenFirstName));
            IsRefreshing = false;
        }

        public void UpdateChildren(Children children)
        {
            IsRefreshing = true;
            var oldChildren = childrens.Where(c => c.ChildrenId == children.ChildrenId).FirstOrDefault();
            oldChildren = children;

            ChildrensList = new ObservableCollection<Children>(
                childrens.OrderBy(c => c.ChildrenFirstName));
            IsRefreshing = false;
        }

        public async Task DeleteChildren(Children children)
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

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Delete(
                "http://api.parents.outstandservices.pt",
                "/api",
                "/Childrens",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                children);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            childrens.Remove(children);

            ChildrensList = new ObservableCollection<Children>(
                childrens.OrderBy(c => c.ChildrenFirstName));
            IsRefreshing = false;
        }

        async void LoadChildrens()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                childrens = dataService.Get<Children>(false);
                //childrens = dataService.GetChildren();

                if (childrens.Count == 0)
                {
                    await dialogService.ShowMessage(
                        "Error",
                        "No childrens available offline.");
                    return;
                }               
            }
            else
            {
                var mainViewModel = MainViewModel.GetInstance();

                var urlAPI = Application.Current.Resources["URLAPI"].ToString();

                var response = await apiService.GetList<Children>(
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

                childrens = (List<Children>)response.Result;
                
                SaveChildrenOnDB();
            }

            ChildrensList = new ObservableCollection<Children>(childrens.OrderBy(c => c.ChildrenFirstName));
            //Search();
            IsRefreshing = false;
        }

        void SaveChildrenOnDB()
        {
            dataService.DeleteAll<Children>();
            foreach (var children in childrens)
            {
                //dataService.Insert(children);
                dataService.Save(childrens);
            }
        }

        //async void LoadChildrens()
        //{
        //    IsRefreshing = true;
        //    var connection = await apiService.CheckConnection();

        //    if (!connection.IsSuccess)
        //    {
        //        await dialogService.ShowMessage("Error", connection.Message);
        //        return;
        //    }

        //    var mainViewModel = MainViewModel.GetInstance();

        //    var response = await apiService.GetList<Children>(
        //       "http://api.parents.outstandservices.pt",
        //        "/api",
        //        "/Childrens",
        //        mainViewModel.Token.TokenType,
        //        mainViewModel.Token.AccessToken);

        //    if (!response.IsSuccess)
        //    {
        //        await dialogService.ShowMessage("Error", connection.Message);
        //        return;
        //    }

        //    childrens = (List<Children>)response.Result;

        //    ChildrensList = new ObservableCollection<Children>(childrens.OrderBy(c => c.ChildrenFirstName));

        //    IsRefreshing = false;
        //}

        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadChildrens);
            }
        }
        #endregion

    }
}
