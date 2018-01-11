using GalaSoft.MvvmLight.Command;
using Parents.Models.ActivitiesManagement.Helpers;
using Parents.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace Parents.ViewModels.Activities.Helpers
{
    public class EditActivityFamilyViewModel : INotifyPropertyChanged
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
        ActivityFamily activityFamily;
        bool _privacy;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties
        public bool Privacy
        {
            get
            {
                return _privacy;
            }
            set
            {
                if (_privacy != value)
                {
                    _privacy = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Privacy)));
                }
            }
        }

        public string ActivityFamilyDescription
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
        public EditActivityFamilyViewModel(ActivityFamily _activityFamily)
        {
            this.activityFamily = _activityFamily;   
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            ActivityFamilyDescription = _activityFamily.ActivityFamilyDescription;
            Privacy = _activityFamily.Privacy;
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
            if (string.IsNullOrEmpty(ActivityFamilyDescription))
            {
                await dialogService.ShowMessage("Error", "Activity Family is missing.");
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

            activityFamily.ActivityFamilyDescription = ActivityFamilyDescription;
            activityFamily.Privacy = Privacy;

            var mainViewModel = MainViewModel.GetInstance();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Put(
                "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesFamily",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                activityFamily);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            var activityFamilyViewModel = ActivityFamilyViewModel.GetInstance();
            activityFamilyViewModel.UpdateActivityFamily(activityFamily);

            await navigationService.BackOnMaster();

            IsRunning = false;
            IsEnabled = true;

        }
        #endregion
    }
}
