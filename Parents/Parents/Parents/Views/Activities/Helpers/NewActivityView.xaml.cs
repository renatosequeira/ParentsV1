using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Parents.Services;
using Parents.Views.Activities.HelpersPages;
using Parents.Models.ActivitiesManagement.Helpers;
using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using System.Collections.ObjectModel;
using Parents.Resources;

namespace Parents.Views.Activities.Helpers
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewActivityView : ContentPage
    {
        private IList<ActivitiesPriority> _activityPriority;

        #region Services
        NavigationService navigationService;
        #endregion

        #region Attributtes
        public static string type { get; set; }
        public static string priority { get; set; }
        private string _repeat { get; set; }
        #endregion

        #region Constructors
        public NewActivityView()
        {

            InitializeComponent();

            acvtivityTypeList.Text = "SELECT ACTIVITY TYPE...";
            acvtivityPriorityLabel.Text = "SELECT ACTIVITY PRIORITY...";
            lblPriority.Text = "PUBLIC";
            lblRepeat.Text = "SELECT REPEAT...";
            navigationService = new NavigationService();


            MessagingCenter.Subscribe<ActivityRepeatHelperPageView, string>(this, "eventRecurrency", (s, a) => {
                lblRepeat.Text = a.ToString();
            });

        }
        #endregion

        private async void btn1_Clicked(object sender, System.EventArgs e)
        {
            await navigationService.Navigate("ActivityTypeHelperPage");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            type = acvtivityTypeList.Text;
            priority = acvtivityPriorityLabel.Text;

            string activityTypeResponse = "";
            string activityPriorityResponse = "";


            string activityTypeProperty = null;
            string activityPriorityProperty = null;
            string repeatMode = null;
            string numberOfRepetitions = null;

            try
            {
                numberOfRepetitions = Application.Current.Properties["numberOfRepetitions"] as string;
            }
            catch (Exception)
            {
                numberOfRepetitions = null;
            }

            try
            {
                activityTypeProperty = Application.Current.Properties["activityTypeProperty"] as string;
            }
            catch (Exception)
            {
                activityTypeProperty = null;
            }

            try
            {
                repeatMode = Application.Current.Properties["selectedRepeatMode"] as string;
            }
            catch (Exception)
            {

                repeatMode = null;
            }

            if (!string.IsNullOrEmpty(repeatMode))
            {
                repeatMode = Application.Current.Properties["selectedRepeatMode"] as string;

                switch (repeatMode)
                {
                    case "Once":
                        lblRepeat.Text = "ONCE";
                        break;

                    case "Daily":
                        lblRepeat.Text = "DAILY";
                        break;

                    case "Weekly":
                        lblRepeat.Text = "WEEKLY";
                        break;

                    case "Monthly":
                        lblRepeat.Text = "MONTHLY";
                        break;

                    case "Yearly":
                        lblRepeat.Text = "YEARLY";
                        break;

                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(activityTypeProperty))
            {
                activityTypeResponse = Application.Current.Properties["activityTypeProperty"] as string;

                switch (activityTypeProperty)
                {
                    case "Anniversary":
                        activityTypeImage.Source = "ic_birthday";
                        break;

                    case "Event":
                        activityTypeImage.Source = "ic_event";
                        break;

                    case "School":
                        activityTypeImage.Source = "ic_school_brown";
                        break;

                    case "Workgroup":
                        activityTypeImage.Source = "ic_school_brown";
                        break;

                    case "Study Trips":
                        activityTypeImage.Source = "ic_school_brown";
                        break;

                    case "Parents Meeting":
                        activityTypeImage.Source = "ic_school_brown";
                        break;

                    case "Sports":
                        activityTypeImage.Source = "ic_soccer_brown";
                        break;

                    case "Others":
                        activityTypeImage.Source = "ic_question";
                        break;

                    default:
                        activityTypeImage.Source = "ic_question";
                        break;
                }
            }
            else
            {
                activityTypeResponse = type;
            }

            try
            {
                numberOfRepetitions = Application.Current.Properties["numberOfRepetitions"] as string;
            }
            catch (Exception)
            {

                numberOfRepetitions = null;
            }

            try
            {
                activityPriorityProperty = Application.Current.Properties["activityPriorityProperty"] as string;
            }
            catch (Exception)
            {

                activityPriorityProperty = null;
            }

            if (!string.IsNullOrEmpty(activityPriorityProperty))
            {
                activityPriorityResponse = Application.Current.Properties["activityPriorityProperty"] as string;

                switch (activityPriorityResponse)
                {
                    case "Low":
                        activityPriorityImage.Source = "ic_priority_low";
                        acvtivityPriorityLabel.TextColor = Color.Gray;
                        break;

                    case "Medium":
                        activityPriorityImage.Source = "ic_priority_medium";
                        acvtivityPriorityLabel.TextColor = Color.Gray;
                        break;

                    case "High":
                        activityPriorityImage.Source = "ic_priority_high";
                        acvtivityPriorityLabel.TextColor = Color.Gray;
                        break;

                    case "Immediate":
                        activityPriorityImage.Source = "ic_priority_immediate";
                        acvtivityPriorityLabel.TextColor = Color.Red;
                        break;

                    default:
                        activityPriorityImage.Source = "ic_priority_simple";
                        break;
                }
            }
            else
            {
                activityPriorityResponse = priority;
            }

            acvtivityTypeList.Text = activityTypeResponse.ToUpper();
            acvtivityPriorityLabel.Text = activityPriorityResponse.ToUpper();
            ResetKeys();
        }

        private void ResetKeys()
        {
            Application.Current.Properties["activityTypeProperty"] = null;
            Application.Current.Properties["activityPriorityProperty"] = null;
        }

        private void priorityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var priority = priorityList.Items[priorityList.SelectedIndex];
            //var pri = _activityPriority.Single(ap => ap.PriorityDescription == priority);
        }

        #region ExternalTables
        private IList<ActivitiesPriority> GetActivityPriority()
        {
            return new List<ActivitiesPriority>
            {
                new ActivitiesPriority
                {
                    PriorityDescription = "Low",
                    PriorityImage = "ic_priority_low"
                },

                 new ActivitiesPriority
                {
                    PriorityDescription = "Medium",
                    PriorityImage = "ic_priority_medium"
                },

                new ActivitiesPriority
                {
                    PriorityDescription = "High",
                    PriorityImage = "ic_priority_high"
                },

                new ActivitiesPriority
                {
                    PriorityDescription = "Immediate",
                    PriorityImage = "ic_priority_immediate"
                },
            };
        }
        #endregion

        private void prioritySwitch_Toggled(object sender, ToggledEventArgs e)
        {
            bool prioritySwitchIsEnabled = e.Value;

            if (prioritySwitchIsEnabled)
            {
                lblPriority.Text = "PRIVATE";
                privacyImage.Source = "ic_private";
            }
            else
            {
                lblPriority.Text = "PUBLIC";
                privacyImage.Source = "ic_public";
            }
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {

        }

        private void decriptionEntry_Focused(object sender, FocusEventArgs e)
        {

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await navigationService.Navigate("ActivityPriorityHelperPage");
        }

        private void ClearPriorityOption_Tapped(object sender, EventArgs e)
        {
            acvtivityPriorityLabel.Text = "SELECT ACTIVITY PRIORITY...";
            activityPriorityImage.Source = "ic_priority_simple";
        }

        private void ClearActivityType_Tapped(object sender, EventArgs e)
        {
            acvtivityTypeList.Text = "SELECT ACTIVITY TYPE...";
            activityTypeImage.Source = "ic_question";
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            decriptionEntry.Text = "";
        }

        private void ClearEndDateAndTime_Tapped(object sender, EventArgs e)
        {
            DateTime InitialDate = startDate.Date;

            endDate.Date = InitialDate.AddDays(1);
            endTime.Time = DateTime.Now.TimeOfDay;
        }

        private void ClearStartDateAndTime_Tapped(object sender, EventArgs e)
        {
            startDate.Date = DateTime.Now;
            startTime.Time = DateTime.Now.TimeOfDay;
        }

        private void ActivityRemarksClear_Tapped(object sender, EventArgs e)
        {
            activityRemarksEntry.Text = "";
        }

        private void ClearActivityLocation_Tapped(object sender, EventArgs e)
        {
            activityLocation.Text = "SELECT EVENT LOCATION...";
        }

        private async void SelectActivityLocation_Tapped(object sender, EventArgs e)
        {
            await navigationService.Navigate("ActivityLocationHelperPage");
        }

        private void statusSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            bool statusSwitchEnabled = e.Value;

            if (statusSwitchEnabled)
            {
                lblStatus.Text = "CLOSED";
                statusImage.Source = "status_completed";
            }
            else
            {
                lblStatus.Text = "ON GOING";
                statusImage.Source = "status_ongoing";
            }
        }

        private void allDaySwitch_Toggled(object sender, ToggledEventArgs e)
        {
            bool isChecked = e.Value;

            if (isChecked)
            {
                startTime.Time = TimeSpan.Parse("00:00");
                endTime.Time = TimeSpan.Parse("23:59");

                startTime.IsVisible = false;
                endTime.IsVisible = false;

                dateTimeSeparator1.IsVisible = false;
                dateTimeSeparator2.IsVisible = false;
                timeDemoImage1.IsVisible = false;
                timeDemoImage2.IsVisible = false;
            }
            else
            {
                TimeSpan time1 = TimeSpan.FromHours(1);
                startTime.IsVisible = true;
                endTime.IsVisible = true;
                startTime.Time = DateTime.Now.TimeOfDay;
                endTime.Time = DateTime.Now.TimeOfDay.Add(time1);

                dateTimeSeparator1.IsVisible = true;
                dateTimeSeparator2.IsVisible = true;
                timeDemoImage1.IsVisible = true;
                timeDemoImage2.IsVisible = true;
            }
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {

        }

        private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var popup = new ActivityRepeatHelperPageView();
            await Navigation.PushPopupAsync(popup);
        }

        private void ClearActivityRepead_Tapped(object sender, EventArgs e)
        {
            lblRepeat.Text = "SELECT REPEAT...";
        }

        private async void OpenActivityRepeatHelperPage_Tapped(object sender, EventArgs e)
        {
            var popup = new ActivityRepeatHelperPageView();
            await Navigation.PushPopupAsync(popup);

        }

        private ObservableCollection<string> Events { get; set; } = new ObservableCollection<string>();

        private void startDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            if(endDate.Date < startDate.Date)
                endDate.Date = startDate.Date;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //vwLoading.IsVisible = true;
            //lblProgressStatus.Text = "Saving";
            //actIndicator.Color = Color.Red;

            //contentView.IsVisible = true;

            //this.BusyIndicator.IsVisible = true;
            //this.BusyIndicator.IsBusy = true;
            this.BusyIndicator.Title = AppResources.Saving;
        }
    }
}