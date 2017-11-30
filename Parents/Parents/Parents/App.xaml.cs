namespace Parents
{
    using Parents.Services;
    using Parents.Views;
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

                BarBackgroundColor = Color.FromHex("#5A392B"),
                BarTextColor = Color.FromHex("#FFFFFF")
            };

        } 
        #endregion

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

        
    }
}
