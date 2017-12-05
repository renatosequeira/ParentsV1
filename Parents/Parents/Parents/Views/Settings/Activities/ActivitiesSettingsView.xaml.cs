namespace Parents.Views.Settings.Activities
{
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Models.Settings;
    using Services;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitiesSettingsView : ContentPage
    {
        #region Services
        NavigationService navigationService;
        #endregion

        public ActivitiesSettingsView()
        {
            InitializeComponent();
            navigationService = new NavigationService();

            activitySettingsMenu.ItemsSource = new List<SettingsMenu>
            {
                new SettingsMenu{
                    MenuName = "Activities Family",
                    MenuImageSource ="ic_settings_family",
                    MenuDescription = "Activities family definitions"}     
            };
        }

        private void settingsMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            activitySettingsMenu.SelectedItem = null;
        }

        private async void settingsMenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await navigationService.Navigate("Activities Family");
        }
    }
}