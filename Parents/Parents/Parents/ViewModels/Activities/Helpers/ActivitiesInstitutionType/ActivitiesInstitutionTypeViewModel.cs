namespace Parents.ViewModels.Activities.Helpers.ActivitiesInstitutionType
{
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Models.ActivitiesManagement.Helpers;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class ActivitiesInstitutionTypeViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Attributes
        ObservableCollection<ActivityInstitutionType> _activitiesInstitutionType;
        List<ActivityInstitutionType> activitiesInstitutionType;
        bool _isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<ActivityInstitutionType> ActivitiesInstitutionTypeList
        {
            get
            {
                return _activitiesInstitutionType;
            }
            set
            {
                if (_activitiesInstitutionType != value)
                {
                    _activitiesInstitutionType = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivitiesInstitutionTypeList)));
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
        public ActivitiesInstitutionTypeViewModel()
        {
            instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();

            LoadActivitiesInstitutionTypes();
        }
        #endregion

        #region Methods
        public void Add(ActivityInstitutionType activityInstitutionType)
        {
            IsRefreshing = true;
            activitiesInstitutionType.Add(activityInstitutionType);
            ActivitiesInstitutionTypeList = new ObservableCollection<ActivityInstitutionType>(
                activitiesInstitutionType.OrderBy(c => c.ActivityInstitutionTypeDescription));
            IsRefreshing = false;
        }

        async void LoadActivitiesInstitutionTypes()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<ActivityInstitutionType>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/ActivitiesInstitutionTypes",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            activitiesInstitutionType = (List<ActivityInstitutionType>)response.Result;

            ActivitiesInstitutionTypeList = new ObservableCollection<ActivityInstitutionType>(activitiesInstitutionType.OrderBy(c => c.ActivityInstitutionTypeDescription));

            IsRefreshing = false;
        }

        public void UpdateActivityInstitutionType(ActivityInstitutionType activityInstitutionType)
        {
            IsRefreshing = true;
            var oldActivityInstitutionType = activitiesInstitutionType.Where(c => c.ActivityInstitutionTypeId == activityInstitutionType.ActivityInstitutionTypeId).FirstOrDefault();
            oldActivityInstitutionType = activityInstitutionType;

            ActivitiesInstitutionTypeList = new ObservableCollection<ActivityInstitutionType>(
                activitiesInstitutionType.OrderBy(c => c.ActivityInstitutionTypeDescription));
            IsRefreshing = false;
        }

        public async Task DeleteActivityInstitutionType(ActivityInstitutionType activityInstitutionType)
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
                "/ActivitiesInstitutionTypes",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                activityInstitutionType);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            activitiesInstitutionType.Remove(activityInstitutionType);

            ActivitiesInstitutionTypeList = new ObservableCollection<ActivityInstitutionType>(
                activitiesInstitutionType.OrderBy(c => c.ActivityInstitutionTypeDescription));
            IsRefreshing = false;
        }
        #endregion

        #region Sigleton
        static ActivitiesInstitutionTypeViewModel instance;

        public static ActivitiesInstitutionTypeViewModel GetInstance()
        {
            if (instance == null)
            {
                return new ActivitiesInstitutionTypeViewModel();
            }

            return instance;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadActivitiesInstitutionTypes);
            }
        }
        #endregion
    }
}
