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
                    MenuDescription = "Activities family definitions"},
                new SettingsMenu
                {
                    MenuName = "Activity Institution Type",
                    MenuImageSource = "ic_soccer_stadium_brown",
                    MenuDescription = "Places where activities might occur."
                },
                new SettingsMenu
                {
                    MenuName="Activity Peridiocity",
                    MenuDescription = "Settings for Activities occurence",
                    MenuImageSource = "ic_activities_peridiocity"
                },
                new SettingsMenu
                {
                    MenuName="Activity Type",
                    MenuDescription = "Study trips, Workgroups, Parents Meetings",
                    MenuImageSource = "ic_study_brown"
                }
            };
        }

        private void settingsMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            activitySettingsMenu.SelectedItem = null;
        }

        private async void settingsMenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var menuItem = e.Item as SettingsMenu;
            await navigationService.Navigate(menuItem.MenuName);
        }
    }
}