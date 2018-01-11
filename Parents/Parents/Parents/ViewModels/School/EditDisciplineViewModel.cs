namespace Parents.ViewModels.Settings
{
    using GalaSoft.MvvmLight.Command;
    using Parents.Models;
    using Parents.Services;
    using System.ComponentModel;
    using System.Windows.Input;
    using System;
    using Parents.ViewModels.School;

    public class EditDisciplineViewModel : INotifyPropertyChanged
    {

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        DialogService dialogService;
        ApiService apiService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        Discipline discipline;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties

        public string DisciplineDescription
        {
            get;
            set;
        }

        public string DisciplineRemarks
        {
            get;
            set;
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
        public EditDisciplineViewModel(Discipline discipline)
        {
            this.discipline = discipline;
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            DisciplineDescription = discipline.DisciplineDescription;
            DisciplineRemarks = discipline.DisciplineRemarks;
            
            IsEnabled = true;
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

        private async void Save()
        {
            if (string.IsNullOrEmpty(DisciplineDescription))
            {
                await dialogService.ShowMessage("Error", "Discipline description is missing");
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

            discipline.DisciplineDescription = DisciplineDescription;
            discipline.DisciplineRemarks = DisciplineRemarks;

            var mainViewModel = MainViewModel.GetInstance();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Put(
                "http://api.parents.outstandservices.pt",
                "/api",
                "/Disciplines",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                discipline);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            var disciplineViewModel = DisciplinesViewModel.GetInstance();
            disciplineViewModel.UpdateDiscipline(discipline);

            await navigationService.BackOnMaster();

            IsRunning = false;
            IsEnabled = true;

        }
        #endregion
    }
}
