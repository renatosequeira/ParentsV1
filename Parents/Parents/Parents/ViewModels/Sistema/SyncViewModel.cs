namespace Parents.ViewModels.Sistema
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using System;
    using Services;
    using Parents.ViewModels.School;
    using Parents.ViewModels.Activities;
    using Parents.ViewModels.Activities.Helpers.ActivitiesInstitutionType;
    using Parents.ViewModels.Activities.Helpers.Peridiocity;
    using Parents.ViewModels.Activities.Helpers.ActivityType;
    using Parents.ViewModels.AppCore;
    using Xamarin.Forms;
    using Parents.Views;
    using Parents.Models;
    using System.Linq;

    public class SyncViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        DialogService dialogService;
        ApiService apiService;
        NavigationService navigationService;
        DataService dataService;
        #endregion

        #region Attributes
        string _message;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Message)));
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
        public SyncViewModel()
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();
            dataService = new DataService();

            Message = "Press sync button to start";

            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SyncCommand
        {
            get
            {
                return new RelayCommand(Synchronize);
            }
        }

        async void Synchronize()
        {
            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            //valida se existem atividades que devam ser atualizados para o webservice
            var activities = dataService.Get<ActivityParents>(false).Where(a => a.PendingToSave).ToList();

            if (activities.Count == 0)
            {
                await dialogService.ShowMessage("Error", "There are no activities to sync");
                IsRunning = false;
                IsEnabled = true;
                return;
            }

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();
            var mainViewModel = MainViewModel.GetInstance();

            foreach (var activity in activities)
            {
                var response = await apiService.Post(
                "http://api.parents.outstandservices.pt",
                "/api",
                "/Activities",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                activity);

                if (response.IsSuccess)
                {
                    activity.PendingToSave = false;
                    dataService.Update(activity);
                }
            }

            await dialogService.ShowMessage("Info", "Data synchronization finished");
            await navigationService.BackOnMaster();
        }
        #endregion
        
    }
}
