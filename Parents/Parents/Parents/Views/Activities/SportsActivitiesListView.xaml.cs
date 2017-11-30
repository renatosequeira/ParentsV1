using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.Activities
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SportsActivitiesListView : ContentPage
    {
        public SportsActivitiesListView()
        {
            InitializeComponent();
        }

        private async void btnAddSportActivity_Clicked(object sender, EventArgs e)
        {

        }

        private async void buttonAdd_Clicked(object sender, EventArgs e)
        {
            bool b1 = btnAddSportActivity.IsVisible;

            if (!b1)
            {

                buttonAdd.Image = "add_opened_orange";
                contents.Opacity = 0.3;

                await buttonAdd.TranslateTo(-80, 0, 250, Easing.Linear);

                buttonAdd.Rotation = 0;
                await buttonAdd.RotateTo(225, 250);
                
                btnAddSportActivity.IsVisible = true;
                await btnAddSportActivity.FadeTo(1, 1000, Easing.SinIn);

            }
            else
            {
                buttonAdd.Image = "add_closed_orange";
                contents.Opacity = 1;
                buttonAdd.Rotation = 270;
                await buttonAdd.RotateTo(0, 250);
                await btnAddSportActivity.FadeTo(0, 500, Easing.SinOut);
                btnAddSportActivity.IsVisible = false;
                await buttonAdd.TranslateTo(0, 0, 250, Easing.Linear);
            }
        }

    }
}