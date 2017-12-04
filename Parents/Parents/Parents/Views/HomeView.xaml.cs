namespace Parents.Views
{
    using Services;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Constructors
        public HomeView()
        {
            navigationService = new NavigationService();
            InitializeComponent();
        }
        #endregion

        #region ButtonControls
        private async void btnParents_Clicked(object sender, EventArgs e)
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new ParentsView());
            await navigationService.Navigate("ParentsView");
        }

        private async void btnChildrens_Clicked(object sender, EventArgs e)
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new ChildrensList());
            await navigationService.Navigate("ChildrensList");
        }
        #endregion

        private async void btn4_Clicked(object sender, EventArgs e)
        {
            await navigationService.Navigate("SettingsView");
        }
    }
}