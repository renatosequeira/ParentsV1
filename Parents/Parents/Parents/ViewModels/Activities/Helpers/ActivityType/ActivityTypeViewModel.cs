namespace Parents.ViewModels.Activities.Helpers.ActivityType
{
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models.ActivitiesManagement.Helpers;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class ActivityTypeViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
# endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Attributes
        ObservableCollection<ActivityType> _activityTypes;
        List<ActivityType> activityTypes;
        bool _isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<ActivityType> ActivityTypesList
        {
            get
            {
                return _activityTypes;
            }
            set
            {
                if (_activityTypes != value)
                {
                    _activityTypes = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityTypesList)));
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
        public ActivityTypeViewModel()
        {
            instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();

            LoadActivityTypes();
        }
        #endregion

        #region Methods
        public void Add(ActivityType activityType)
        {
            IsRefreshing = true;
            activityTypes.Add(activityType);
            ActivityTypesList = new ObservableCollection<ActivityType>(
                activityTypes.OrderBy(c => c.ActivityTypeDescription));
            IsRefreshing = false;
        }

        async void LoadActivityTypes()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<ActivityType>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesTypes",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            activityTypes = (List<ActivityType>)response.Result;

            ActivityTypesList = new ObservableCollection<ActivityType>(activityTypes.OrderBy(c => c.ActivityTypeDescription));

            IsRefreshing = false;
        }

        public void UpdateActivityType(ActivityType activityType)
        {
            IsRefreshing = true;
            var oldActivityType = activityTypes.Where(c => c.ActivityTypeId == activityType.ActivityTypeId).FirstOrDefault();
            oldActivityType = activityType;

            ActivityTypesList = new ObservableCollection<ActivityType>(
                activityTypes.OrderBy(c => c.ActivityTypeDescription));
            IsRefreshing = false;
        }

        public async Task DeleteActivityType(ActivityType activityType)
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
                "/ActivitiesTypes",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                activityType);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            activityTypes.Remove(activityType);

            ActivityTypesList = new ObservableCollection<ActivityType>(
                activityTypes.OrderBy(c => c.ActivityTypeDescription));
            IsRefreshing = false;
        }
        #endregion

        #region Sigleton
        static ActivityTypeViewModel instance;

        public static ActivityTypeViewModel GetInstance()
        {
            if (instance == null)
            {
                return new ActivityTypeViewModel();
            }

            return instance;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadActivityTypes);
            }
        }
        #endregion
    }
}
