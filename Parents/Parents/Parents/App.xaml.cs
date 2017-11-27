namespace Parents
{
    using Parents.Services;
    using Parents.Views;
    using Xamarin.Forms;


    public partial class App : Application
    {
        #region Services
        ApiService apiService;

        DialogService dialogService;

        #endregion

        public App()
        {
            InitializeComponent();

            apiService = new ApiService();
            dialogService = new DialogService();

            MainPage = new NavigationPage(new LoginView())
            {

                BarBackgroundColor = Color.FromHex("#7DBEA5"),
                BarTextColor = Color.FromHex("#5A392B")
            };

        }

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
