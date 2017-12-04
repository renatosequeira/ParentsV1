using Parents.Models.Settings;
using Parents.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.Settings
{

    #region Constructors
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisciplinesSettingsView : ContentPage
    {
        #region Services
        NavigationService navigationService;
        #endregion

        public DisciplinesSettingsView()
        {
            InitializeComponent();
            navigationService = new NavigationService();
        }

        private void settingsMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            settingsMenu.SelectedItem = null;
        }

        private async void settingsMenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var menuItem = e.Item as SettingsMenu;
            await navigationService.Navigate(menuItem.MenuName);
        }
    } 
    #endregion
}