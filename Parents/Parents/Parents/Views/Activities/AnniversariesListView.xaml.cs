using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Parents.Models;
using Parents.ViewModels;

namespace Parents.Views.Activities
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnniversariesListView : ContentPage
    {
        public AnniversariesListView()
        {

            InitializeComponent();

        }

        private void btnAddAnniversary_Clicked(object sender, EventArgs e)
        {

        }

        private async void buttonAdd_Clicked(object sender, EventArgs e)
        {
            bool b1 = btnAddAnniversary.IsVisible;

            if (!b1)
            {

                buttonAdd.Image = "add_opened_orange";
                contents.Opacity = 0.3;

                await buttonAdd.TranslateTo(-80, 0, 250, Easing.Linear);

                buttonAdd.Rotation = 0;
                await buttonAdd.RotateTo(225, 250);

                btnAddAnniversary.IsVisible = true;
                await btnAddAnniversary.FadeTo(1, 1000, Easing.SinIn);

            }
            else
            {
                buttonAdd.Image = "add_closed_orange";
                contents.Opacity = 1;
                buttonAdd.Rotation = 270;
                await buttonAdd.RotateTo(0, 250);
                await btnAddAnniversary.FadeTo(0, 500, Easing.SinOut);
                btnAddAnniversary.IsVisible = false;
                await buttonAdd.TranslateTo(0, 0, 250, Easing.Linear);
            }
        }

        private void AnniversariesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void AnniversariesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}