namespace Parents.ViewModels.Activities.Helpers.ActivitiesInstitutionType
{
    using GalaSoft.MvvmLight.Command;
    using Parents.Models.ActivitiesManagement.Helpers;
    using Parents.Services;
    using System.ComponentModel;
    using System.Windows.Input;
    using System;

    public class EditActivitiesInstitutionTypeViewModel : INotifyPropertyChanged
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
        ActivityInstitutionType activityInstitutionType;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties

        public string ActivityInstitutionTypeDescription
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
        public EditActivitiesInstitutionTypeViewModel(ActivityInstitutionType activityInstitutionType)
        {
            this.activityInstitutionType = activityInstitutionType;
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            ActivityInstitutionTypeDescription = activityInstitutionType.ActivityInstitutionTypeDescription;
            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SwitchPrivacyCommand
        {
            get
            {
                return new RelayCommand(SwitchPrivacy);
            }
        }

        async void SwitchPrivacy()
        {
            await dialogService.ShowMessage("teste", "teste");
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(ActivityInstitutionTypeDescription))
            {
                await dialogService.ShowMessage("Error", "Institution type description is missing!");
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

            activityInstitutionType.ActivityInstitutionTypeDescription = ActivityInstitutionTypeDescription;

            var mainViewModel = MainViewModel.GetInstance();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Put(
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

            var activityInstitutionTypeViewModel = ActivitiesInstitutionTypeViewModel.GetInstance();
            activityInstitutionTypeViewModel.UpdateActivityInstitutionType(activityInstitutionType);

            await navigationService.BackOnMaster();

            IsRunning = false;
            IsEnabled = true;

        }
        #endregion
    }
}
