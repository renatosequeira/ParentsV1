using Parents.Models;
using Parents.Services;
using Parents.ViewModels;
using Parents.ViewModels.Activities;
using Parents.Views.Activities.HelpersPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.Activities
{
	public partial class ActivitiesListView : ContentPage
	{
        #region Services
        NavigationService navigationService;
        #endregion

        #region Constructor
        public ActivitiesListView()
        {
            InitializeComponent();
            navigationService = new NavigationService();

        }
        #endregion

        #region ClickEvents
        private void btnOtherActvity_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnAddAnniversary_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.NewActivity = new NewActivityViewModel();

            await navigationService.NavigateOnMaster("AddAnniversaryActivity");
            
            buttonAdd.Image = "add_closed_orange";
            contents.Opacity = 1;

            buttonAdd.Rotation = 270;
            await buttonAdd.RotateTo(0, 250);

            await menuLabel.FadeTo(0, 0, Easing.SinOut);
            menuLabel.IsVisible = false;

            await btnAddAnniversary.FadeTo(0, 0, Easing.SinOut);
            btnAddAnniversary.IsVisible = false;

            await btnAddEvent.FadeTo(0, 0, Easing.SinOut);
            btnAddEvent.IsVisible = false;

            await btnAddSchoolActivity.FadeTo(0, 0, Easing.SinOut);
            btnAddSchoolActivity.IsVisible = false;

            await btnAddSportActivity.FadeTo(0, 0, Easing.SinOut);
            btnAddSportActivity.IsVisible = false;

            await btnOtherActvity.FadeTo(0, 0, Easing.SinOut);
            btnOtherActvity.IsVisible = false;

            await buttonAdd.TranslateTo(0, 0, 250, Easing.Linear);
        }

        private void btnAddEvent_Clicked(object sender, EventArgs e)
        {

        }

        private void btnAddSchoolActivity_Clicked(object sender, EventArgs e)
        {

        }

        private async void buttonAdd_Clicked(object sender, EventArgs e)
        {
            bool b1 = btnAddAnniversary.IsVisible;

            if (!b1)
            {

                buttonAdd.Image = "add_opened_orange";
                contents.Opacity = 0.1;

                await buttonAdd.TranslateTo(-80, 0, 250, Easing.Linear);

                buttonAdd.Rotation = 0;
                await buttonAdd.RotateTo(225, 250);

                menuLabel.IsVisible = true;
                await menuLabel.FadeTo(1, 0, Easing.Linear);

                btnAddAnniversary.IsVisible = true;
                await btnAddAnniversary.FadeTo(1, 0, Easing.Linear);

                btnAddEvent.IsVisible = true;
                await btnAddEvent.FadeTo(1, 0, Easing.Linear);

                btnAddSchoolActivity.IsVisible = true;
                await btnAddSchoolActivity.FadeTo(1, 0, Easing.Linear);

                btnAddSportActivity.IsVisible = true;
                await btnAddSportActivity.FadeTo(1, 0, Easing.Linear);

                btnOtherActvity.IsVisible = true;
                await btnOtherActvity.FadeTo(1, 0, Easing.Linear);
            }
            else
            {
                buttonAdd.Image = "add_closed_orange";
                contents.Opacity = 1;

                buttonAdd.Rotation = 270;
                await buttonAdd.RotateTo(0, 250);

                await menuLabel.FadeTo(0, 0, Easing.SinOut);
                menuLabel.IsVisible = false;

                await btnAddAnniversary.FadeTo(0, 0, Easing.SinOut);
                btnAddAnniversary.IsVisible = false;

                await btnAddEvent.FadeTo(0, 0, Easing.SinOut);
                btnAddEvent.IsVisible = false;

                await btnAddSchoolActivity.FadeTo(0, 0, Easing.SinOut);
                btnAddSchoolActivity.IsVisible = false;

                await btnAddSportActivity.FadeTo(0, 0, Easing.SinOut);
                btnAddSportActivity.IsVisible = false;

                await btnOtherActvity.FadeTo(0, 0, Easing.SinOut);
                btnOtherActvity.IsVisible = false;

                await buttonAdd.TranslateTo(0, 0, 250, Easing.Linear);
            }
        }

        private void btnAddSportActivity_Clicked(object sender, EventArgs e)
        {

        }

        private void ActivitiesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ActivitiesList.SelectedItem = null;
        }

        private void CheckBox_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {


        }

        private void ActivitiesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        #endregion

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchPageView());
        }
    }
}