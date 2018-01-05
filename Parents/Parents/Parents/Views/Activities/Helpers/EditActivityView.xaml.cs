using Parents.Resources;
using Parents.Services;
using Parents.ViewModels.Activities.HelperPages;
using Parents.Views.Activities.HelpersPages;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Rg.Plugins.Popup.Extensions;
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
        string selectedPriority;
        string selectedActivityType;
        #endregion

        #region Services
        NavigationService navigationService;
        #endregion

        public EditActivityView()
        {
            InitializeComponent();

            navigationService = new NavigationService();

            middleSectionBox.BackgroundColor = Color.White;

        }

        private async void MaximizeImage_Clicked(object sender, EventArgs e)
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
            priorityBox.IsEnabled = true;
            privacyBox.IsEnabled = true;
            typeBox.IsEnabled = true;
            statusBox.IsEnabled = true;
            middleSectionBox.BackgroundColor = Color.FromHex("#F1E0B1");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            #region Priority
            try
            {
                selectedPriority = Application.Current.Properties["activityPriorityProperty"] as string;
            }
            catch (Exception)
            {

                selectedPriority = null;
            }

            switch (selectedPriority)
            {
                case "Low":
                    imgPriority.Source = "ic_priority_low";
                    break;

                case "Medium":
                    imgPriority.Source = "ic_priority_medium";
                    break;

                case "High":
                    imgPriority.Source = "ic_priority_high";
                    break;

                case "Immediate":
                    imgPriority.Source = "ic_priority_immediate";
                    break;
            }
            #endregion

            #region ActivitType
            try
            {
                selectedActivityType = Application.Current.Properties["activityTypeProperty"] as string;
            }
            catch (Exception)
            {
                selectedActivityType = null;
            }

            if (!string.IsNullOrEmpty(selectedActivityType))
            {
                selectedActivityType = Application.Current.Properties["activityTypeProperty"] as string;

                switch (selectedActivityType)
                {
                    case "Anniversary":
                        imgActivityType.Source = "ic_birthday";
                        lblAC.Text = "ANNIVERSARY";
                        break;

                    case "Event":
                        imgActivityType.Source = "ic_event";
                        lblAC.Text = "EVENT";
                        break;

                    case "School":
                        imgActivityType.Source = "ic_school_brown";
                        lblAC.Text = "SCHOOL";
                        break;

                    case "Workgroup":
                        imgActivityType.Source = "ic_school_brown";
                        lblAC.Text = "WORKGROUP";
                        break;

                    case "Study Trips":
                        imgActivityType.Source = "ic_school_brown";
                        lblAC.Text = "STUDY TRIPS";
                        break;

                    case "Parents Meeting":
                        imgActivityType.Source = "ic_school_brown";
                        lblAC.Text = "PARENTS MEETING";
                        break;

                    case "Sports":
                        imgActivityType.Source = "ic_soccer_brown";
                        lblAC.Text = "SPORTS";
                        break;

                    case "Others":
                        imgActivityType.Source = "ic_question";
                        lblAC.Text = "OTHERS";
                        break;

                    default:
                        imgActivityType.Source = "ic_question";
                        break;
                }
            }


            //string r;

            //MessagingCenter.Subscribe<ActivityImageMaximizedHelperPageView, string>(this, "newActivityImage", (s, a) =>
            //{
            //    r = a.ToString();
            //});

            #endregion



            #region Methods
            void ResetKeys()
            {
                Application.Current.Properties["activityTypeProperty"] = null;
                Application.Current.Properties["activityPriorityProperty"] = null;
                GC.Collect();
            }
            #endregion
        }


    }
}