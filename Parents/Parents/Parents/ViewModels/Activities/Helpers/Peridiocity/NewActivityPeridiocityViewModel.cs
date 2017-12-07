using GalaSoft.MvvmLight.Command;
using Parents.Models.ActivitiesManagement.Helpers;
using Parents.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Parents.ViewModels.Activities.Helpers.Peridiocity
{
    public class NewActivityPeridiocityViewModel : INotifyPropertyChanged
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
        string _activityPeriodicityDescription;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties
        public string ActivityPeriodicityDescription
        {
            get
            {
                return _activityPeriodicityDescription;
            }
            set
            {
                if (_activityPeriodicityDescription != value)
                {
                    _activityPeriodicityDescription = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityPeriodicityDescription)));
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
        public NewActivityPeridiocityViewModel()
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
            if (string.IsNullOrEmpty(ActivityPeriodicityDescription))
            {
                await dialogService.ShowMessage("Error", "Please insert Activity peridiocity description");
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

            var activityPeridiocity = new ActivityPeridiocity
            {
                ActivityPeriodicityDescription = ActivityPeriodicityDescription
            };

            var mainViewModel = MainViewModel.GetInstance();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Post(
                "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesPeriodicity",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                activityPeridiocity);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            activityPeridiocity = (ActivityPeridiocity)response.Result;
            var activityPeridiocityViewModel = ActivityPeridiocityViewModel.GetInstance();
            activityPeridiocityViewModel.Add(activityPeridiocity);

            await navigationService.Back();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
