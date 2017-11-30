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
	public partial class ActivitiesListView : ContentPage
	{
		public ActivitiesListView ()
		{
			InitializeComponent ();
		}

        private void btnOtherActvity_Clicked(object sender, EventArgs e)
        {

        }

        private void btnAddAnniversary_Clicked(object sender, EventArgs e)
        {

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
                contents.Opacity = 0.3;

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
    }
}