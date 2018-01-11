using Parents.Models.Settings;
using Parents.Services;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApplicationSettingCoreView : ContentPage
    {
        #region Services
        NavigationService navigationService;
        #endregion

        public ApplicationSettingCoreView()
        {
            InitializeComponent();
            navigationService = new NavigationService();

            settingsMenu.ItemsSource = new List<SettingsMenu>
            {
                new SettingsMenu{
                    MenuName = "Activities",
                    MenuImageSource ="settings_activities",
                    MenuDescription = "Child activities configuration"},
                new SettingsMenu{
                    MenuName = "Domestic",
                    MenuImageSource ="settings_domestic",
                    MenuDescription = "Child domestic aspects management"},

                new SettingsMenu{
                    MenuName = "Health",
                    MenuImageSource ="settings_health",
                    MenuDescription = "Child health management"},

                new SettingsMenu{
                    MenuName = "Parental",
                    MenuImageSource ="settings_parental",
                    MenuDescription = "Parental management"},

                new SettingsMenu{
                    MenuName = "Education",
                    MenuImageSource ="settings_school",
                    MenuDescription = "Children education"},

                new SettingsMenu{
                    MenuName = "Tasks",
                    MenuImageSource ="settings_task",
                    MenuDescription = "Child related tasks configuration"}
            };
        }

        private void settingsMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            settingsMenu.SelectedItem = null;
        }

        private async void settingsMenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var menuItem = e.Item as SettingsMenu;
            await navigationService.NavigateOnMaster(menuItem.MenuName);
        }
    }
}