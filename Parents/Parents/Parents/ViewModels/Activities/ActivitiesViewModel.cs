namespace Parents.ViewModels.Activities
{
    using GalaSoft.MvvmLight.Command;
    using Parents.Models;
    using Parents.Services;
    using Parents.Views.Activities;
    using Parents.Views.Activities.HelpersPages;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ActivitiesViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        ObservableCollection<Activity> _activities;
        ObservableCollection<Activity> _anniversaries;
        ObservableCollection<Activity> _events;
        List<Activity> activities;
        bool _isRefreshing;
        string childrenIdentityCard;
        int childrenId;
        string _selectedFilter;
        string _childrenName;
        string _selectedFilterForAnniversaries;
        string _selectedFilrerForEvents;

        #endregion

        #region Properties
        public String ChildrenName
        {
            get
            {

                return _childrenName;

            }
            set
            {
                if (_childrenName != value)
                {
                    _childrenName = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrenName)));
                }
            }
        }

        public String SelectedFilter
        {
            get
            {
              
                return _selectedFilter;

            }
            set
            {
                if (_selectedFilter != value)
                {
                    _selectedFilter = value;
                    OpenFilteredActivitiesList(SelectedFilter);
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SelectedFilter)));
                }
            }
        }

        public String SelectedFilterForAnniversaries
        {
            get
            {

                return _selectedFilterForAnniversaries;

            }
            set
            {
                if (_selectedFilterForAnniversaries != value)
                {
                    _selectedFilterForAnniversaries = value;
                    OpenFilteredAnniversariesList(SelectedFilterForAnniversaries);
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SelectedFilterForAnniversaries)));
                }
            }
        }

        public String SelectedFilterForEvents
        {
            get
            {

                return _selectedFilrerForEvents;

            }
            set
            {
                if (_selectedFilrerForEvents != value)
                {
                    _selectedFilrerForEvents = value;
                    OpenFilteredActivitiesList(SelectedFilterForEvents);
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SelectedFilterForEvents)));
                }
            }
        }

        public ObservableCollection<Activity> ActivitiesList
        {
            get
            {
              
                return _activities;

            }
            set
            {
                if (_activities != value)
                {
                    _activities = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivitiesList)));
                }
            }
        }

        public ObservableCollection<Activity> AnniversariesList
        {
            get
            {
              
                return _anniversaries;

            }
            set
            {
                if (_anniversaries != value)
                {
                    _anniversaries = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(AnniversariesList)));
                }
            }
        }

        public ObservableCollection<Activity> EventsList
        {
            get
            {

                return _events;

            }
            set
            {
                if (_events != value)
                {
                    _events = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(EventsList)));
                }
            }
        }

        public ObservableCollection<Activity> FilterOpenedActivities
        {
            get
            {
                ObservableCollection<Activity> openedActivities = new ObservableCollection<Activity>();

                if (_activities != null)

                {
                    List<Activity> entities = (from e in _activities
                                               where e.Status.Equals(0)
                                               select e).ToList<Activity>();
                    if (entities != null && entities.Any())
                    {
                        openedActivities = new ObservableCollection<Activity>(entities);
                    }
                }

                return openedActivities;

            }
            set
            {
                if (_activities != value)
                {
                    _activities = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(AnniversariesList)));
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
        public ActivitiesViewModel()
        {
            MessagingCenter.Subscribe<ActivitiesFilterOptionsHelperPageView, string>(this, "selectedFilter", (s, a) => {
                SelectedFilter = a.ToString();
            });

            MessagingCenter.Subscribe<ActivitiesListView, string>(this, "childrenName", (s, a) => {
                ChildrenName = a.ToString();
            });

            instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            if (Application.Current.Properties.ContainsKey("childrenIdentityCard"))
            {
                var idCard = Application.Current.Properties["childrenIdentityCard"] as string;
                childrenIdentityCard = idCard;
            }

            if (Application.Current.Properties.ContainsKey("childrenId"))
            {
                int id = Convert.ToInt32(Application.Current.Properties["childrenId"]);
                childrenId = Convert.ToInt32(id);
            }

            OpenFilteredActivitiesList(SelectedFilter);
            OpenFilteredAnniversariesList(SelectedFilterForAnniversaries);
            OpenFilteredEventsList(SelectedFilterForEvents);
        }

        #endregion

        #region Methods
        public void Add(Activity activity)
        {
            IsRefreshing = true;
            activities.Add(activity);
            ActivitiesList = new ObservableCollection<Activity>(
                activities.OrderBy(c => c.ActivityDateStart));
            IsRefreshing = false;
        }

        async void LoadActivities()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Activity>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/Activities",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);


            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            activities = (List<Activity>)response.Result;

            ActivitiesList = new ObservableCollection<Activity>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        }

        async void LoadActivitiesForSpecificChildren()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetListForSpecificChildren<Activity>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesForCurrentChildren",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken, childrenId);


            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            activities = (List<Activity>)response.Result;

            ActivitiesList = new ObservableCollection<Activity>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        }

        async void LoadOnGoingActivitiesForSpecificChildren()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetOnGoingActivitiesListForSpecificChildren<Activity>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesForCurrentChildren",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken, childrenId, false);


            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            activities = (List<Activity>)response.Result;

            ActivitiesList = new ObservableCollection<Activity>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        }

        async void LoadCompletedActivitiesForSpecificChildren()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetOnGoingActivitiesListForSpecificChildren<Activity>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesForCurrentChildren",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken, childrenId, true);


            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            activities = (List<Activity>)response.Result;

            ActivitiesList = new ObservableCollection<Activity>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        }

        #region ANNIVERSARIES
        async void LoadOnGoingAnniversaries()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetActivityTypesListForSpecificChildren<Activity>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesForCurrentChildren",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken, childrenId, false, "ANNIVERSARY");


            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            activities = (List<Activity>)response.Result;

            AnniversariesList = new ObservableCollection<Activity>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        }

        async void LoadCompletedAnniversaries()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetActivityTypesListForSpecificChildren<Activity>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesForCurrentChildren",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken, childrenId, true, "ANNIVERSARY");


            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            activities = (List<Activity>)response.Result;

            AnniversariesList = new ObservableCollection<Activity>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        }
        #endregion

        #region EVENTS
        async void LoadOnGoingEvents()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetActivityTypesListForSpecificChildren<Activity>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesForCurrentChildren",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken, childrenId, false, "EVENT");


            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            activities = (List<Activity>)response.Result;

            EventsList = new ObservableCollection<Activity>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        }

        async void LoadCompletedEvents()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetActivityTypesListForSpecificChildren<Activity>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesForCurrentChildren",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken, childrenId, true, "EVENT");


            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            activities = (List<Activity>)response.Result;

            EventsList = new ObservableCollection<Activity>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        } 
        #endregion

        public void UpdateActivity(Activity activity)
        {
            IsRefreshing = true;
            var oldActivity = activities.Where(c => c.ActivityId == activity.ActivityId).FirstOrDefault();
            oldActivity = activity;

            ActivitiesList = new ObservableCollection<Activity>(
                activities.OrderBy(c => c.ActivityDateStart));
            IsRefreshing = false;
        }

        public async Task DeleteActivity(Activity activity)
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
                "/Activities",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                activity);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            activities.Remove(activity);

            ActivitiesList = new ObservableCollection<Activity>(
                activities.OrderBy(c => c.ActivityDateStart));
            IsRefreshing = false;
        }

        public async Task ReloadActivities()
        {
            LoadActivities();
        }

        public async Task FilterActivities()
        {
            OpenFilteredActivitiesList(SelectedFilter);
        }

        public async Task ReloadAnniversaries()
        {
            OpenFilteredAnniversariesList(SelectedFilterForAnniversaries);
        }

        public async Task ReloadEvents()
        {
            OpenFilteredEventsList(SelectedFilterForEvents);
        }

        private void OpenFilteredActivitiesList(string selectedFilter)
        {
            switch (selectedFilter)
            {
                case "Show On Going Activities":
                    LoadOnGoingActivitiesForSpecificChildren();
                    break;

                case "Show Completed Activities":
                    LoadCompletedActivitiesForSpecificChildren();
                    break;

                case "Clear Filters":
                    LoadActivitiesForSpecificChildren();
                    break;
                default:
                    LoadOnGoingActivitiesForSpecificChildren();
                    break;
            }
        }

        private void OpenFilteredAnniversariesList(string selectedFilter)
        {
            switch (selectedFilter)
            {
                case "Show On Going Anniversaries":
                    LoadOnGoingAnniversaries();
                    break;

                case "Show Completed Anniversaries":
                    LoadCompletedAnniversaries();
                    break;

                case "Clear Filters":
                    LoadOnGoingAnniversaries();
                    break;
                default:
                    LoadOnGoingAnniversaries();
                    break;
            }
        }

        private void OpenFilteredEventsList(string selectedFilter)
        {
            switch (selectedFilter)
            {
                case "Show On Going Events":
                    LoadOnGoingEvents();
                    break;

                case "Show Completed Events":
                    LoadCompletedEvents();
                    break;

                case "Clear Filters":
                    LoadOnGoingEvents();
                    break;
                default:
                    LoadOnGoingEvents();
                    break;
            }
        }
        #endregion

        #region Sigleton
        static ActivitiesViewModel instance;

        public static ActivitiesViewModel GetInstance()
        {

            return instance;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                //return new RelayCommand(LoadActivities);
                return new RelayCommand(LoadActivitiesForSpecificChildren);
            }
        }

        public ICommand RefreshAnniversariesCommand
        {
            get
            {
                return new RelayCommand(LoadOnGoingActivitiesForSpecificChildren);
            }
        }

        public ICommand FilterActivitiesCommand
        {
            get
            {
                return new RelayCommand(_FilterActivities);
            }
        }

        async void _FilterActivities()
        {

            await FilterActivities();
        }

        public ICommand OpenFilterOptionsPageHelperCommand
        {
            get
            {
                return new RelayCommand(OpenFilterOptionsPage);
            }
        }

        async void OpenFilterOptionsPage()
        {
            await navigationService.OpenPopup("Activity Filters");
        }
        #endregion
    }
}
