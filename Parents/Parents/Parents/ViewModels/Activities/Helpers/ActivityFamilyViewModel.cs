namespace Parents.ViewModels.Activities
{
    using Models.ActivitiesManagement.Helpers;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Parents.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class ActivityFamilyViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Attributes
        ObservableCollection<ActivityFamily> _activitiesFamily;
        List<ActivityFamily> activitiesFamily;
        bool _isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<ActivityFamily> ActivitiesFamilyList
        {
            get
            {
                return _activitiesFamily;
            }
            set
            {
                if (_activitiesFamily != value)
                {
                    _activitiesFamily = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivitiesFamilyList)));
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
        public ActivityFamilyViewModel()
        {
            instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();

            LoadActivitiesFamily();
        }
        #endregion

        #region Methods
        public void Add(ActivityFamily activityFamily)
        {
            IsRefreshing = true;
            activitiesFamily.Add(activityFamily);
            ActivitiesFamilyList = new ObservableCollection<ActivityFamily>(
                activitiesFamily.OrderBy(c => c.ActivityFamilyDescription));
            IsRefreshing = false;
        }

        async void LoadActivitiesFamily()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<ActivityFamily>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesFamily",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            activitiesFamily = (List<ActivityFamily>)response.Result;

            ActivitiesFamilyList = new ObservableCollection<ActivityFamily>(activitiesFamily.OrderBy(c => c.ActivityFamilyDescription));

            IsRefreshing = false;
        }

        public void UpdateActivityFamily(ActivityFamily activityFamily)
        {
            IsRefreshing = true;
            var oldActivityFamily = activitiesFamily.Where(c => c.ActivityFamilyId == activityFamily.ActivityFamilyId).FirstOrDefault();
            oldActivityFamily = activityFamily;

            ActivitiesFamilyList = new ObservableCollection<ActivityFamily>(
                activitiesFamily.OrderBy(c => c.ActivityFamilyDescription));
            IsRefreshing = false;
        }

        public async Task DeleteActivityFamily(ActivityFamily activityFamily)
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
                "/ActivitiesFamily",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                activityFamily);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            activitiesFamily.Remove(activityFamily);

            ActivitiesFamilyList = new ObservableCollection<ActivityFamily>(
                activitiesFamily.OrderBy(c => c.ActivityFamilyDescription));
            IsRefreshing = false;
        }
        #endregion

        #region Sigleton
        static ActivityFamilyViewModel instance;

        public static ActivityFamilyViewModel GetInstance()
        {
            if (instance == null)
            {
                return new ActivityFamilyViewModel();
            }

            return instance;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadActivitiesFamily);
            }
        }
        #endregion
    }
}
