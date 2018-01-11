using Parents.Models.Settings;
using Parents.Services;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EducationSettingsView : ContentPage
    {
        #region Services
        NavigationService navigationService;
        #endregion

        public EducationSettingsView()
        {
            InitializeComponent();
            navigationService = new NavigationService();

            settingsMenu.ItemsSource = new List<SettingsMenu>
            {
                new SettingsMenu
                {
                    MenuName="Disciplines",
                    MenuDescription = "Disciplines configuration",
                    MenuImageSource = "settings_disciplines"
                },
                new SettingsMenu
                {
                    MenuName ="Exams",
                    MenuImageSource ="settings_Exams",
                    MenuDescription ="School exams family configuration"
                },
                new SettingsMenu
                {
                    MenuName ="Academic Year",
                    MenuDescription ="Academic Year configuration",
                    MenuImageSource ="settings_academic_year"
                }
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