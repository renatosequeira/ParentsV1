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
        DataService dataService;
        #endregion

        #region Attributes
        ObservableCollection<ActivityParents> _activities;
        ObservableCollection<ActivityParents> _anniversaries;
        ObservableCollection<ActivityParents> _events;
        ObservableCollection<ActivityParents> _school;
        List<ActivityParents> activities;

        bool _isRefreshing;
        string childrenIdentityCard;
        int childrenId;
        string _selectedFilter;
        string _childrenName;
        string _selectedFilterForAnniversaries;
        string _selectedFilrerForEvents;
        string _selectedFiltersForSchool;
        string _filter;
        #endregion

        #region Properties
        public String Filter
        {
            get
            {

                return _filter;

            }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    Search();
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Filter)));
                }
            }
        }

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

        public String SelectedFilterForSchool
        {
            get
            {

                return _selectedFiltersForSchool;

            }
            set
            {
                if (_selectedFiltersForSchool != value)
                {
                    _selectedFiltersForSchool = value;
                    OpenFilteredActivitiesList(SelectedFilterForEvents);
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SelectedFilterForSchool)));
                }
            }
        }

        public ObservableCollection<ActivityParents> ActivitiesList
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

        public ObservableCollection<ActivityParents> AnniversariesList
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

        public ObservableCollection<ActivityParents> EventsList
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

        public ObservableCollection<ActivityParents> SchoolList
        {
            get
            {

                return _school;

            }
            set
            {
                if (_school != value)
                {
                    _school = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SchoolList)));
                }
            }
        }

        public ObservableCollection<ActivityParents> FilterOpenedActivities
        {
            get
            {
                ObservableCollection<ActivityParents> openedActivities = new ObservableCollection<ActivityParents>();

                if (_activities != null)

                {
                    List<ActivityParents> entities = (from e in _activities
                                               where e.Status.Equals(0)
                                               select e).ToList<ActivityParents>();
                    if (entities != null && entities.Any())
                    {
                        openedActivities = new ObservableCollection<ActivityParents>(entities);
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
            dataService = new DataService();

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

            #region LISTS LOADING
            OpenFilteredActivitiesList(SelectedFilter);
            OpenFilteredAnniversariesList(SelectedFilterForAnniversaries);
            OpenFilteredEventsList(SelectedFilterForEvents);
            OpenFilteredSchoolList(SelectedFilterForSchool);
            #endregion
        }

        #endregion

        #region Methods
        public void Add(ActivityParents activity)
        {
            IsRefreshing = true;
            activities.Add(activity);
            ActivitiesList = new ObservableCollection<ActivityParents>(
                activities.OrderBy(c => c.ActivityDateStart));
            IsRefreshing = false;
        }

        async void LoadActivities()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                activities = dataService.Get<ActivityParents>(false);

                if (activities.Count == 0)
                {
                    await dialogService.ShowMessage("Error", "No offline data is available");
                    return;
                }
            }
            else
            {
                var mainViewModel = MainViewModel.GetInstance();

                var response = await apiService.GetList<ActivityParents>(
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

                activities = (List<ActivityParents>)response.Result;
                SaveActivitiesOnDB();
            }
            
            Search();
            IsRefreshing = false;
        }

        void SaveActivitiesOnDB()
        {
            dataService.DeleteAll<ActivityParents>();
            dataService.Save(activities);
            //foreach (var activity in activities)
            //{
            //    if (!activity.Status)
            //    {
            //        dataService.Save(activities);
            //    }
            //}
        }

        async void LoadActivitiesForSpecificChildren()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                activities = dataService.Get<ActivityParents>(false);

                if(activities.Count == 0)
                {
                    await dialogService.ShowMessage("Error", "No offline Activities");
                }
            }
            else
            {

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetListForSpecificChildren<ActivityParents>(
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

            activities = (List<ActivityParents>)response.Result;
               // SaveActivitiesOnDB();

            }

            ActivitiesList = new ObservableCollection<ActivityParents>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        }

        #region COMPLETED ACTIVITIES FOR SPECIFIC CHILDREN
        async void LoadOnGoingActivitiesForSpecificChildren()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                activities = dataService.Get<ActivityParents>(false);
                if (activities.Count == 0)
                {
                    await dialogService.ShowMessage("Error", "No activities available offline");
                    return;
                }
            }
            else
            {
                var mainViewModel = MainViewModel.GetInstance();

                var response = await apiService.GetOnGoingActivitiesListForSpecificChildren<ActivityParents>(
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

                activities = (List<ActivityParents>)response.Result;
                SaveActivitiesOnDB();
            }

            ActivitiesList = new ObservableCollection<ActivityParents>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        }

        async void LoadCompletedActivitiesForSpecificChildren()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                activities = dataService.Get<ActivityParents>(false);

                if (activities.Count == 0)
                {
                    await dialogService.ShowMessage("Error", "No offline completed activities available.");
                }
            }
            else
            {

                var mainViewModel = MainViewModel.GetInstance();

                var response = await apiService.GetOnGoingActivitiesListForSpecificChildren<ActivityParents>(
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

                activities = (List<ActivityParents>)response.Result;

                //SaveActivitiesOnDB();
            }

            ActivitiesList = new ObservableCollection<ActivityParents>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        } 
        #endregion

        #region ANNIVERSARIES
        async void LoadOnGoingAnniversaries()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            
            if (!connection.IsSuccess)
            {
                
                activities = dataService.Get<ActivityParents>(false);

                if(activities.Count == 0)
                {
                    await dialogService.ShowMessage("Error", "No activities available offline");
                    return;
                }
            }
            else
            {

                var mainViewModel = MainViewModel.GetInstance();

                var response = await apiService.GetActivityTypesListForSpecificChildren<ActivityParents>(
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

                activities = (List<ActivityParents>)response.Result;
                //SaveActivitiesOnDB();
            }

            AnniversariesList = new ObservableCollection<ActivityParents>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        }

        async void LoadCompletedAnniversaries()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                activities = dataService.Get<ActivityParents>(false);

                if(activities.Count== 0)
                {
                    await dialogService.ShowMessage("Error", "No offline anniversaries");
                }
            }
            else
            {
            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetActivityTypesListForSpecificChildren<ActivityParents>(
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

            activities = (List<ActivityParents>)response.Result;

               // SaveActivitiesOnDB();
            }

            AnniversariesList = new ObservableCollection<ActivityParents>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

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
                activities = dataService.Get<ActivityParents>(false);

                if (activities.Count == 0)
                {
                    await dialogService.ShowMessage("Error", "No events available offline");
                }
            }
            else
            {

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetActivityTypesListForSpecificChildren<ActivityParents>(
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

            activities = (List<ActivityParents>)response.Result;

                //SaveActivitiesOnDB();
            }

            EventsList = new ObservableCollection<ActivityParents>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        }

        async void LoadCompletedEvents()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                activities = dataService.Get<ActivityParents>(false);

                if(activities.Count== 0)
                {
                    await dialogService.ShowMessage("Error","No completed events available offline");
                }
            }
            else
            {

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetActivityTypesListForSpecificChildren<ActivityParents>(
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

            activities = (List<ActivityParents>)response.Result;
               // SaveActivitiesOnDB();

            }

            EventsList = new ObservableCollection<ActivityParents>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        }
        #endregion

        #region SCHOOL
        async void LoadOnGoingSchool()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                activities = dataService.Get<ActivityParents>(false);

                if(activities.Count== 0)
                {
                    await dialogService.ShowMessage("Error", "No On-Going School Activities available offline");

                }
            }
            else
            {


            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetActivityTypesListForSpecificChildren<ActivityParents>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesForCurrentChildren",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken, childrenId, false, "SCHOOL");


            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            activities = (List<ActivityParents>)response.Result;

                //SaveActivitiesOnDB();
            }

            SchoolList = new ObservableCollection<ActivityParents>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        }

        async void LoadCompletedSchool()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                activities = dataService.Get<ActivityParents>(false);

                if(activities.Count == 0)
                {
                    await dialogService.ShowMessage("Error", "No completed School Actvities available offline");
                }
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }
            else
            {
                var mainViewModel = MainViewModel.GetInstance();

                var response = await apiService.GetActivityTypesListForSpecificChildren<ActivityParents>(
                   "http://api.parents.outstandservices.pt",
                    "/api",
                    "/ActivitiesForCurrentChildren",
                    mainViewModel.Token.TokenType,
                    mainViewModel.Token.AccessToken, childrenId, true, "SCHOOL");


                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage("Error", connection.Message);
                    return;
                }

                activities = (List<ActivityParents>)response.Result;

            }

            SchoolList = new ObservableCollection<ActivityParents>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentityCard));

            IsRefreshing = false;
        }
        #endregion

        public void UpdateActivity(ActivityParents activity)
        {
            IsRefreshing = true;
            var oldActivity = activities.Where(c => c.ActivityId == activity.ActivityId).FirstOrDefault();
            oldActivity = activity;

            ActivitiesList = new ObservableCollection<ActivityParents>(
                activities.OrderBy(c => c.ActivityDateStart));
            IsRefreshing = false;
        }

        public async Task DeleteActivity(ActivityParents activity)
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

            ActivitiesList = new ObservableCollection<ActivityParents>(
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

        public async Task ReloadSchool()
        {
            OpenFilteredSchoolList(SelectedFilterForSchool);
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

        private void OpenFilteredSchoolList(string selectedFilter)
        {
            switch (selectedFilter)
            {
                case "Show On Going School Activities":
                    LoadOnGoingSchool();
                    break;

                case "Show Completed School Activities":
                    LoadCompletedSchool();
                    break;

                case "Clear Filters":
                    LoadOnGoingSchool();
                    break;
                default:
                    LoadOnGoingSchool();
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
        public ICommand SearchViewCommand
        {
            get
            {
                return new RelayCommand(OpenSearchWindow);
            }
        }

        async void OpenSearchWindow()
        {
            await navigationService.NavigateOnMaster("OpenActivitiesFullListWindow");
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        void Search()
        {
            IsRefreshing = true;

            if (string.IsNullOrEmpty(Filter))
            {
                ActivitiesList = new ObservableCollection<ActivityParents>(
                    activities.OrderBy(c => c.ActivityDescription));
            }
            else
            {
                ActivitiesList = new ObservableCollection<ActivityParents>(activities
                    .Where(c => c.ActivityDescription.ToLower().Contains(Filter.ToLower()))
                    .OrderBy(c => c.ActivityDescription));
            }

            IsRefreshing = false;
        }

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
