using Parents.Models.ActivitiesManagement.Helpers;
using Parents.ViewModels;
using Parents.ViewModels.Activities;
using Parents.Views.Activities.Helpers;
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
    public partial class ActivityRepeatHelperPageView : PopupPage
    {

        public ActivityRepeatHelperPageView()
        {
            var bc = new NewActivityViewModel();
            BindingContext = bc;

            InitializeComponent();


            activityRepeatMenu.ItemsSource = new List<ActivityRepeat>
            {
                new ActivityRepeat
                {
                    RepeatName = "Once"
                },
                new ActivityRepeat
                {
                    RepeatName = "Daily"
                },
                new ActivityRepeat
                {
                    RepeatName = "Weekly"
                },
                new ActivityRepeat
                {
                    RepeatName = "Monthly"
                },
                new ActivityRepeat
                {
                    RepeatName = "Yearly"
                },
                new ActivityRepeat
                {
                    RepeatName = "Custom"
                }
            };

        }

        private void repeatPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //Application.Current.Properties["selectedRepeatMode"] = activityRepeatMenu.SelectedItem;
            await PopupNavigation.PopAsync();
        }

        private void repeatPicker_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //if (repeatPicker.SelectedItem.ToString() == "Custom")
            //{
            //    customRepeats.IsVisible = true;
            //}
        }


        private void activityRepeatMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            activityRepeatMenu.SelectedItem = null;
        }

        private async void activityRepeatMenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            Application.Current.Properties["selectedRepeatMode"] = activityRepeatMenu.SelectedItem;
            Application.Current.Properties["numberOfRepetitions"] = repetitionTimes.Text;

            var bc = new NewActivityViewModel();
            BindingContext = bc;

            var _selectedItem = e.Item as ActivityRepeat;
            string selectedItem = _selectedItem.RepeatName;
            string _repetitions = "";

            if (string.IsNullOrEmpty(repetitionTimes.Text))
            {
                _repetitions = "1";
            }
            else
            {
                _repetitions = repetitionTimes.Text;
            }

            MessagingCenter.Send(this, "eventRecurrency", selectedItem);
            MessagingCenter.Send(this, "eventNumberOfOccurencies", _repetitions);

           await PopupNavigation.PopAsync();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {

        }

        private async void btnOK_Clicked(object sender, EventArgs e)
        {
            string _repetitions = "";

            if (string.IsNullOrEmpty(repetitionTimes.Text))
            {
                _repetitions = "1";
            }
            else
            {
                _repetitions = repetitionTimes.Text;
            }

            MessagingCenter.Send(this, "eventNumberOfOccurencies", _repetitions);
            await PopupNavigation.PopAsync();
        }
    }
}