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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SystemSettingsView : ContentPage
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Constructors
        public SystemSettingsView()
        {
            InitializeComponent();
            navigationService = new NavigationService();

            systemMenu.ItemsSource = new List<SystemMenu>
            {
                new SystemMenu{Name = "Notifications", Status=false, Description="Application notifications" },
                new SystemMenu{Name = "Automatic Synchronization", Status=true, Description="Automatic synchronization when item is submitted" }
            };
        }
        #endregion

        private void systemMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void systemMenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}