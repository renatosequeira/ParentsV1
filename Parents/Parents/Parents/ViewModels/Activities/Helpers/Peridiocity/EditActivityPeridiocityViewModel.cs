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
    public class EditActivityPeridiocityViewModel : INotifyPropertyChanged
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
        ActivityPeridiocity activityPeridiocity;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties

        public string ActivityPeriodicityDescription
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
        public EditActivityPeridiocityViewModel(ActivityPeridiocity activityPeridiocity)
        {
            this.activityPeridiocity = activityPeridiocity;
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            ActivityPeriodicityDescription = activityPeridiocity.ActivityPeriodicityDescription;
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
            if (string.IsNullOrEmpty(ActivityPeriodicityDescription))
            {
                await dialogService.ShowMessage("Error", "Peridiocity description is missing!");
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

            activityPeridiocity.ActivityPeriodicityDescription = ActivityPeriodicityDescription;

            var mainViewModel = MainViewModel.GetInstance();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Put(
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

            var activityPeridiocityViewModel = ActivityPeridiocityViewModel.GetInstance();
            activityPeridiocityViewModel.UpdateActivityPeridiocity(activityPeridiocity);

            await navigationService.Back();

            IsRunning = false;
            IsEnabled = true;

        }
        #endregion
    }
}
