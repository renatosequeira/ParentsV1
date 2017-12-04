namespace Parents.ViewModels.School
{
    using Parents.Models;
    using Parents.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

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
        bool _isRefreshing;
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
            IsRefreshing = true;
            disciplines.Add(discipline);
            DisciplinesList = new ObservableCollection<Discipline>(
                disciplines.OrderBy(c => c.DisciplineDescription));
            IsRefreshing = false;
        }

        async void LoadDisciplines()
        {
            IsRefreshing = true;

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

            IsRefreshing = false;
        }

        public void UpdateDiscipline(Discipline discipline)
        {
            IsRefreshing = true;
            var oldDiscipline = disciplines.Where(c => c.DisciplineId == discipline.DisciplineId).FirstOrDefault();
            oldDiscipline = discipline;

            DisciplinesList = new ObservableCollection<Discipline>(
                disciplines.OrderBy(c => c.DisciplineDescription));
            IsRefreshing = false;
        }

        public async Task DeleteDiscipline(Discipline discipline)
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
                "/Disciplines",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                discipline);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            disciplines.Remove(discipline);

            DisciplinesList = new ObservableCollection<Discipline>(
                disciplines.OrderBy(c => c.DisciplineDescription));
            IsRefreshing = false;
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

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadDisciplines);
            }
        }
        #endregion
    }
}
