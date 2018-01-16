namespace Parents
{
    using Parents.Models;
    using Parents.Services;
    using Parents.ViewModels;
    using Parents.ViewModels.Activities;
    using Parents.ViewModels.Activities.Helpers.ActivitiesInstitutionType;
    using Parents.ViewModels.Activities.Helpers.ActivityType;
    using Parents.ViewModels.Activities.Helpers.Peridiocity;
    using Parents.ViewModels.School;
    using Parents.Views;
    using Parents.Views.Sistema;
    using System;
    using Xamarin.Forms;


    public partial class App : Application
    {
        #region Services
        ApiService apiService;
        NavigationService navigationService;
        DialogService dialogService;

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

            MainPage = new NavigationPage(new LoginView())
            {
                //#5A392B
                BarBackgroundColor = Color.FromHex("#2C3E50"),
                BarTextColor = Color.FromHex("#FFFFFF")
            };

            // MainPage = new LoginFacebookView();
            
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
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

        #endregion

    }
}
