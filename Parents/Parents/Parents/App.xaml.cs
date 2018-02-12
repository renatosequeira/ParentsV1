namespace Parents
{
    using Models;
    using Services;
    using ViewModels;
    using ViewModels.Activities;
    using ViewModels.Activities.Helpers.ActivitiesInstitutionType;
    using ViewModels.Activities.Helpers.ActivityType;
    using ViewModels.Activities.Helpers.Peridiocity;
    using ViewModels.School;
    using Views;
    using Views.Sistema;
    using System;
    using Xamarin.Forms;
    using PushNotification.Plugin;
    using Plugin.Connectivity;
    using Parents.ViewModels.Sistema;

    public partial class App : Application
    {
        #region Services
        ApiService apiService;
        NavigationService navigationService;
        DialogService dialogService;
        DataService dataService;
        AutomaticOfflineSyncService automaticOfflineSyncService;
        #endregion

        #region Properties
        public static NavigationPage Navigator
        {
            get;
            internal set;
        }

        public static MasterView Master { get; internal set; }
        #endregion

        #region Constructor
        public App()
        {
            InitializeComponent();

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            dataService = new DataService();
            automaticOfflineSyncService = new AutomaticOfflineSyncService();

            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;

            var appToken = dataService.First<TokenResponse>(false);

            if (appToken != null &&
                appToken.IsRemembered &&
                appToken.Expires > DateTime.Now)
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = appToken;
                //mainViewModel.Parents = new ParentsViewModel();
                //mainViewModel.Childrens = new ChildrensViewModel();
                //mainViewModel.Disciplines = new DisciplinesViewModel();
                //mainViewModel.ActivityFamily = new ActivityFamilyViewModel();
                //mainViewModel.ActivitiesInstitutionType = new ActivitiesInstitutionTypeViewModel();
                //mainViewModel.ActivityPeridiocity = new ActivityPeridiocityViewModel();
                mainViewModel.ActivityType = new ActivityTypeViewModel();
                //mainViewModel.Activities = new ActivitiesViewModel();

                navigationService.SetMainPage("MasterView");
            }
            else
            {
                navigationService.SetMainPage("LoginView");
            }


            //MainPage = new NavigationPage(new LoginView())
            //{
            //    //#5A392B
            //    BarBackgroundColor = Color.FromHex("#2C3E50"),
            //    BarTextColor = Color.FromHex("#FFFFFF")
            //};

        }

        private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected)
            {
                automaticOfflineSyncService.ActivitiesSynchronization();

            }
        }

        #endregion

        #region Methods

        protected override void OnStart()
        {
            //CrossPushNotification.Current.Unregister();
            //CrossPushNotification.Current.Register();
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static Action LoginFacebookFail
        {
            get
            {
                return new Action(() => Current.MainPage =
                                  new NavigationPage(new LoginView()));
            }
        }

        public async static void LoginFacebookSuccess(FacebookResponse profile)
        {
            if (profile == null)
            {
                Current.MainPage = new NavigationPage(new LoginView());
                return;
            }

            var apiService = new ApiService();
            var dialogService = new DialogService();

            var checkConnetion = await apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                await dialogService.ShowMessage("Error", checkConnetion.Message);
                return;
            }

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();
            var token = await apiService.LoginFacebook(
                urlAPI,
                "/api",
                "/Parents/LoginFacebook",
                profile);

            if (token == null)
            {
                await dialogService.ShowMessage(
                    "Error",
                    "Problem ocurred retrieving user information, try latter.");
                Current.MainPage = new NavigationPage(new LoginView());
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Parents = new ParentsViewModel();
            mainViewModel.Childrens = new ChildrensViewModel();
            mainViewModel.Disciplines = new DisciplinesViewModel();
            mainViewModel.ActivityFamily = new ActivityFamilyViewModel();
            mainViewModel.ActivitiesInstitutionType = new ActivitiesInstitutionTypeViewModel();
            mainViewModel.ActivityPeridiocity = new ActivityPeridiocityViewModel();
            mainViewModel.ActivityType = new ActivityTypeViewModel();
            Current.MainPage = new MasterView();
        }

        public void ResetAllKeys()
        {
            Application.Current.Properties["childrenId"] = null;
            GC.Collect();
        }

        
        #endregion

    }
}
