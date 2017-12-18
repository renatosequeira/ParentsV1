namespace Parents.ViewModels.Activities
{
    using GalaSoft.MvvmLight.Command;
    using Parents.Models;
    using Parents.Services;
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
        #endregion

        #region Attributes
        ObservableCollection<Activity> _activities;
        List<Activity> activities;
        bool _isRefreshing;
        string childrenIdentity;
        #endregion

        #region Properties
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
            instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();

            if (Application.Current.Properties.ContainsKey("childrenIdentityCard"))
            {
                var id = Application.Current.Properties["childrenIdentityCard"] as string;
                childrenIdentity = id;
            }

            LoadActivities();
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



            ActivitiesList = new ObservableCollection<Activity>(activities.OrderBy(c => c.ActivityDateEnd).Where(ch => ch.relatedChildrenIdentitiCard == childrenIdentity));
            //ActivitiesList = new ObservableCollection<Activity>(activities.OrderBy(c => c.ActivityDateStart));
            IsRefreshing = false;
        }

        public void UpdateActivityFamily(Activity activity)
        {
            IsRefreshing = true;
            var oldActivity = activities.Where(c => c.ActivityId == activity.ActivityId).FirstOrDefault();
            oldActivity = activity;

            ActivitiesList = new ObservableCollection<Activity>(
                activities.OrderBy(c => c.ActivityDateStart));
            IsRefreshing = false;
        }

        public async Task DeleteActivityFamily(Activity activity)
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
                return new RelayCommand(LoadActivities);
            }
        }
        #endregion
    }
}
