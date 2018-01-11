namespace Parents.ViewModels.Activities.Helpers.ActivitiesInstitutionType
{
    using GalaSoft.MvvmLight.Command;
    using Parents.Models.ActivitiesManagement.Helpers;
    using Parents.Services;
    using System.ComponentModel;
    using System.Windows.Input;

    public class NewActivityInstitutionTypeViewModel : INotifyPropertyChanged
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
        string _activityInstitutionTypeDescription;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties
        public string ActivityInstitutionTypeDescription
        {
            get
            {
                return _activityInstitutionTypeDescription;
            }
            set
            {
                if (_activityInstitutionTypeDescription != value)
                {
                    _activityInstitutionTypeDescription = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityInstitutionTypeDescription)));
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
        public NewActivityInstitutionTypeViewModel()
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            IsEnabled = true; //bool are disabled by default. This will enable buttons

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
            if (string.IsNullOrEmpty(ActivityInstitutionTypeDescription))
            {
                await dialogService.ShowMessage("Error", "Please insert Activity Family description");
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

            var activityInstitutionType = new ActivityInstitutionType
            {
                ActivityInstitutionTypeDescription = ActivityInstitutionTypeDescription
            };

            var mainViewModel = MainViewModel.GetInstance();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Post(
                "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesInstitutionTypes",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                activityInstitutionType);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            activityInstitutionType = (ActivityInstitutionType)response.Result;
            var activityInstitutionTypeViewModel = ActivitiesInstitutionTypeViewModel.GetInstance();
            activityInstitutionTypeViewModel.Add(activityInstitutionType);

            await navigationService.BackOnMaster();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
