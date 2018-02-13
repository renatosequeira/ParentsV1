namespace Parents.Views
{
    using global::Parents.Resources;
    using Plugin.LocalNotifications;
    using Services;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

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
            contentView.IsVisible = false;
            BusyIndicator.IsBusy = false;
        }
        #endregion

        #region ButtonControls
        private async void btnParents_Clicked(object sender, EventArgs e)
        {


            await navigationService.NavigateOnMaster("ParentsView");
        }

        private async void btnChildrens_Clicked(object sender, EventArgs e)
        {
            this.BusyIndicator.Title = AppResources.Opening;

            BusyIndicator.IsBusy = true;
            contentView.IsVisible = true;
            await Task.Delay(2000);
            contentView.IsVisible = false;
            BusyIndicator.IsBusy = false;

            //await navigationService.NavigateOnMaster("ChildrensList");

        }
        #endregion

        private async void btn4_Clicked(object sender, EventArgs e)
        {
            await navigationService.NavigateOnMaster("SettingsView");
        }

        private void btn3_Clicked(object sender, EventArgs e)
        {
            CrossLocalNotifications.Current.Show("Testando as Notificações", "Agora va ler mais artigos do Bertuzzi no Medium", 1, DateTime.Now.AddSeconds(5));
        }
    }
}