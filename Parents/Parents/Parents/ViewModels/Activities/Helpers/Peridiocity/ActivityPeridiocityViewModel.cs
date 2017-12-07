namespace Parents.ViewModels.Activities.Helpers.Peridiocity
{
    using GalaSoft.MvvmLight.Command;
    using Parents.Models.ActivitiesManagement.Helpers;
    using Parents.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class ActivityPeridiocityViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Attributes
        ObservableCollection<ActivityPeridiocity> _activitiesPeridiocity;
        List<ActivityPeridiocity> activitiesPeridiocity;
        bool _isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<ActivityPeridiocity> ActivitiesPeridiocityList
        {
            get
            {
                return _activitiesPeridiocity;
            }
            set
            {
                if (_activitiesPeridiocity != value)
                {
                    _activitiesPeridiocity = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivitiesPeridiocityList)));
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
        public ActivityPeridiocityViewModel()
        {
            instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();

            LoadActivitiesPeridiocity();
        }
        #endregion

        #region Methods
        public void Add(ActivityPeridiocity activityPeridiocity)
        {
            IsRefreshing = true;
            activitiesPeridiocity.Add(activityPeridiocity);
            ActivitiesPeridiocityList = new ObservableCollection<ActivityPeridiocity>(
                activitiesPeridiocity.OrderBy(c => c.ActivityPeriodicityDescription));
            
            IsRefreshing = false;
        }

        async void LoadActivitiesPeridiocity()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<ActivityPeridiocity>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesPeriodicity",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            activitiesPeridiocity = (List<ActivityPeridiocity>)response.Result;

            ActivitiesPeridiocityList = new ObservableCollection<ActivityPeridiocity>(activitiesPeridiocity.OrderBy(c => c.ActivityPeriodicityDescription));

            IsRefreshing = false;
        }

        public void UpdateActivityPeridiocity(ActivityPeridiocity activityPeridiocity)
        {
            IsRefreshing = true;
            var oldActivityPeridiocity = activitiesPeridiocity.Where(c => c.ActivityPeriodicityId == activityPeridiocity.ActivityPeriodicityId).FirstOrDefault();
            oldActivityPeridiocity = activityPeridiocity;

            ActivitiesPeridiocityList = new ObservableCollection<ActivityPeridiocity>(
                activitiesPeridiocity.OrderBy(c => c.ActivityPeriodicityDescription));
            IsRefreshing = false;
        }

        public async Task DeleteActivityPeridiocity(ActivityPeridiocity activityPeridiocity)
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
                "/ActivitiesPeriodicity",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                activityPeridiocity);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            activitiesPeridiocity.Remove(activityPeridiocity);

            ActivitiesPeridiocityList = new ObservableCollection<ActivityPeridiocity>(
                activitiesPeridiocity.OrderBy(c => c.ActivityPeriodicityDescription));
            IsRefreshing = false;
        }
        #endregion

        #region Sigleton
        static ActivityPeridiocityViewModel instance;

        public static ActivityPeridiocityViewModel GetInstance()
        {
            if (instance == null)
            {
                return new ActivityPeridiocityViewModel();
            }

            return instance;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadActivitiesPeridiocity);
            }
        }
        #endregion
    }
}
