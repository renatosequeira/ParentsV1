namespace Parents.ViewModels.Activities.Helpers.ActivityType
{
    using GalaSoft.MvvmLight.Command;
    using Models.ActivitiesManagement.Helpers;
    using Services;
    using System.ComponentModel;
    using System.Windows.Input;

    public class NewActivityTypeViewModel : INotifyPropertyChanged
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
        string _activityTypeDescription;
        bool _activityTypePrivacy;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties
        public bool ActivityTypePrivacy
        {
            get
            {
                return _activityTypePrivacy;
            }
            set
            {
                if (_activityTypePrivacy != value)
                {
                    _activityTypePrivacy = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityTypePrivacy)));
                }
            }
        }

        public string ActivityTypeDescription
        {

            get
            {
                return _activityTypeDescription;
            }
            set
            {
                if (_activityTypeDescription != value)
                {
                    _activityTypeDescription = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityTypeDescription)));
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
        public NewActivityTypeViewModel()
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            IsEnabled = true; //bool are disabled by default. This will enable buttons
            ActivityTypePrivacy = false;

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
            if (string.IsNullOrEmpty(ActivityTypeDescription))
            {
                await dialogService.ShowMessage("Error", "Please insert Activity Type description");
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

            var activityType = new ActivityType
            {
                ActivityTypeDescription = ActivityTypeDescription,
                ActivityTypePrivacy = ActivityTypePrivacy
            };

            var mainViewModel = MainViewModel.GetInstance();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Post(
                "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesTypes",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                activityType);


            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            activityType = (ActivityType)response.Result;
            var activityTypeViewModel = ActivityTypeViewModel.GetInstance();
            activityTypeViewModel.Add(activityType);

            await navigationService.Back();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
