namespace Parents.Views.Settings
{
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Models.Settings;
    using Services;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsHomeView : ContentPage
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Constructor
        public SettingsHomeView()
        {
            InitializeComponent();
            navigationService = new NavigationService();

            settingsMenu.ItemsSource = new List<SettingsMenu>
            {
                new SettingsMenu{
                    MenuName = "Application Core",
                    MenuImageSource ="settings_core",
                    MenuDescription = "Parents application configuration"},

                new SettingsMenu{
                    MenuName = "Security",
                    MenuImageSource ="settings_security",
                    MenuDescription = "Parents application security configuration"},

                new SettingsMenu{
                    MenuName = "System",
                    MenuImageSource ="settings_icon",
                    MenuDescription = "Parents system configuration"}
            };
        }
        #endregion

        #region Methods
        private void settingsMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            settingsMenu.SelectedItem = null;
        }
        #endregion

        private async void settingsMenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var menuItem = e.Item as SettingsMenu;
            await navigationService.Navigate(menuItem.MenuName);
        }
    }
}