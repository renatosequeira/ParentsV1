namespace Parents
{
    using Parents.Models;
    using Parents.Services;
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

        public static void LoginFacebookSuccess(FacebookResponse profile)
        {
            Current.MainPage = new MasterView();
        } 
        #endregion

    }
}
