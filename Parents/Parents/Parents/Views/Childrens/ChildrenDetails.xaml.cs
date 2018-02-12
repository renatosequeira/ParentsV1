namespace Parents.Views.Childrens
{
    using global::Parents.Models;
    using global::Parents.Resources;
    using global::Parents.ViewModels;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;


    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChildrenDetails : ContentPage
	{

        public ChildrenDetails ()
		{
			InitializeComponent ();
            MainScroll.ParallaxView = HeaderView;
            contentView.IsVisible = false;
            BusyIndicator.IsBusy = false;

        }

        private void btnAddEvent_Clicked(object sender, EventArgs e)
        {

        }


        private void btnChangeProfilePicture_Clicked(object sender, EventArgs e)
        {

        }


        private void btnEdit_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            bool homeButtonGroup = btnAddActivityPicture.IsVisible;

            if (!homeButtonGroup)
            {
                btnAddActivityPicture.IsVisible = true;
                btnAddHealthEventPicture.IsVisible = true;
                btnAddSchoolEventPicture.IsVisible = true;
                btnAddWeightPicture.IsVisible = true;
                btnAddHeightPicture.IsVisible = true;

                await bodySection.FadeTo(0.1, 100, Easing.Linear);
                await btnAddActivityPicture.FadeTo(1, 0, Easing.Linear);
                await btnAddHealthEventPicture.FadeTo(1, 0, Easing.Linear);
                await btnAddSchoolEventPicture.FadeTo(1, 0, Easing.Linear);
                await btnAddWeightPicture.FadeTo(1, 0, Easing.Linear);
                await btnAddHeightPicture.FadeTo(1, 0, Easing.Linear);

                btnAdd.BackgroundColor = Color.FromHex("#3F5765");
                btnAdd.TextColor = Color.White;
            }
            else
            {

                await btnAddActivityPicture.FadeTo(0, 0, Easing.Linear);
                await btnAddHealthEventPicture.FadeTo(0, 0, Easing.Linear);
                await btnAddSchoolEventPicture.FadeTo(0, 0, Easing.Linear);
                await btnAddWeightPicture.FadeTo(0, 0, Easing.Linear);
                await btnAddHeightPicture.FadeTo(0, 0, Easing.Linear);
                await bodySection.FadeTo(1, 100, Easing.Linear);

                btnAddActivityPicture.IsVisible = false;
                btnAddHealthEventPicture.IsVisible = false;
                btnAddSchoolEventPicture.IsVisible = false;
                btnAddWeightPicture.IsVisible = false;
                btnAddHeightPicture.IsVisible = false;

                btnAdd.BackgroundColor = Color.FromHex("#99E758");
                btnAdd.TextColor = Color.White;
            }
        }

        private async void btnMore_Clicked(object sender, EventArgs e)
        {
            bool b1 = btnAddEvent.IsVisible;

            if (!b1)
            {
                btnAddEvent.IsVisible = true;
                btnChangeBannerPicture.IsVisible = true;

                await btnAddEvent.FadeTo(1, 0, Easing.Linear);
                await btnChangeBannerPicture.FadeTo(1, 500, Easing.Linear);
            }
            else
            {
                await btnChangeBannerPicture.FadeTo(0, 0, Easing.Linear);
                await btnAddEvent.FadeTo(0, 0, Easing.Linear);

                btnChangeBannerPicture.IsVisible = false;
                btnAddEvent.IsVisible = false;
            }
        }

        private void btnAddWeightPicture_Clicked(object sender, EventArgs e)
        {
            btnAddWeightPicture.IsVisible = false;

        }

        private async void btnOpenHealthView_Clicked(object sender, EventArgs e)
        {
            this.BusyIndicator.Title = AppResources.Opening;

            BusyIndicator.IsBusy = true;
            contentView.IsVisible = true;
            await Task.Delay(2000);
            contentView.IsVisible = false;
            BusyIndicator.IsBusy = false;
        }

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    Application.Current.Properties["childrenId"] = null;
        //}
    }
}