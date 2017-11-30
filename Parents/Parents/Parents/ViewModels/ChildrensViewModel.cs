namespace Parents.ViewModels
{
    using Models;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Linq;
    using Services;

    public class ChildrensViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Attributes
        ObservableCollection<Children> _childrens;
        List<Children> childrens;
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
        #endregion

        #region Constructors
        public ChildrensViewModel()
        {
            instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();

            LoadChildrens();
        }
        #endregion

        #region Methods
        public void Add(Children children)
        {
            //IsRefreshing = true;
            childrens.Add(children);
            ChildrensList = new ObservableCollection<Children>(
                childrens.OrderBy(c => c.ChildrenFirstName));
            //IsRefreshing = false;
        }


        async void LoadChildrens()
        {
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Children>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/Childrens",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            childrens = (List<Children>)response.Result;

            ChildrensList = new ObservableCollection<Children>(childrens.OrderBy(c => c.ChildrenFirstName));
           
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
    }
}
