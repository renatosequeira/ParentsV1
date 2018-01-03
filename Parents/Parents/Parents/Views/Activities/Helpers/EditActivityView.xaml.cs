using Parents.Resources;
using Parents.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.Activities.Helpers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditActivityView : ContentPage
    {
        #region Attributtes
        string selectedItem;
        #endregion

        #region Services
        NavigationService navigationService;
        #endregion

        public EditActivityView()
        {
            InitializeComponent();

            navigationService = new NavigationService();
            
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            selectedItem = ActivityImage.Source.ToString();

            MessagingCenter.Send(this, "activityImageForMaximization", selectedItem);

            await navigationService.OpenPopup("maximizedActivityPage");
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            this.BusyIndicator.Title = AppResources.Saving;
        }

        private void ClickGestureRecognizer_Clicked(object sender, EventArgs e)
        {
            DescriptionLabel.IsEnabled = true;
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            DescriptionLabel.IsEnabled = true;
            lblPrivacy.IsEnabled = true;
            
        }

    }
}