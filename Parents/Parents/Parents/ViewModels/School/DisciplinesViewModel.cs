namespace Parents.ViewModels.School
{
    using Parents.Models;
    using Parents.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System;
    using System.Linq;

    public class DisciplinesViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Attributes
        ObservableCollection<Discipline> _disciplines;
        List<Discipline> disciplines;
        #endregion

        #region Properties
        public ObservableCollection<Discipline> DisciplinesList
        {
            get
            {
                return _disciplines;
            }
            set
            {
                if (_disciplines != value)
                {
                    _disciplines = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(DisciplinesList)));
                }
            }
        }
        #endregion

        #region Constructors
        public DisciplinesViewModel()
        {
            instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();

            LoadDisciplines();
        }


        #endregion

        #region Methods
        public void Add(Discipline discipline)
        {
            //IsRefreshing = true;
            disciplines.Add(discipline);
            DisciplinesList = new ObservableCollection<Discipline>(
                disciplines.OrderBy(c => c.DisciplineDescription));
            //IsRefreshing = false;
        }

        async void LoadDisciplines()
        {
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Discipline>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/Disciplines",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            disciplines = (List<Discipline>)response.Result;

            DisciplinesList = new ObservableCollection<Discipline>(disciplines.OrderBy(c => c.DisciplineDescription));

        }


        #endregion

        #region Sigleton
        static DisciplinesViewModel instance;

        public static DisciplinesViewModel GetInstance()
        {
            if (instance == null)
            {
                return new DisciplinesViewModel();
            }

            return instance;
        }
        #endregion
    }
}
