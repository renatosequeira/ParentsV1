using Parents.Models.ActivitiesManagement.Helpers;
using Parents.ViewModels.Activities;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.Activities.HelpersPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitiesFilterOptionsHelperPageView : PopupPage
    {
        #region Attributes
        string selectedItem;
        #endregion

        public ActivitiesFilterOptionsHelperPageView()
        {
            InitializeComponent();

            activityFilterOptions.ItemsSource = new List<FilterOptions>
            {
                new FilterOptions
                {
                    FilterImage = "ic_filter_40_DarkGray",
                    FilterName = "Show On Going Activities"
                },
                new FilterOptions
                {
                    FilterImage = "ic_filter_40_DarkGray_1",
                    FilterName = "Show Completed Activities"
                },
                new FilterOptions
                {
                    FilterImage = "ic_clear_filter_40_DarkGray",
                    FilterName = "Clear Filters"
                },
            };
        }

        private async void activityFilterOptions_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var bc = new ActivitiesViewModel();
            BindingContext = bc;

            var _selectedItem = e.Item as FilterOptions;
            selectedItem = _selectedItem.FilterName;

            MessagingCenter.Send(this, "selectedFilter", selectedItem);
            
            await PopupNavigation.PopAsync();
        }
    }
}