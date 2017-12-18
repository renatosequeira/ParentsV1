using Parents.Models.ActivitiesManagement.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Globalization;

namespace Parents.Views.Activities.HelpersPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityTypeHelperPageView : ContentPage
    {
        private string _teste;

        public string Teste
        {
            get
            {
                return _teste;
            }

            set
            {
                _teste = value;
            }
        }

        public ActivityTypeHelperPageView()
        {
            InitializeComponent();

        }


        public ListView ActivityTypes
        {
            get
            {
                return ActivityTypeList;
            }
        }

        private void ActivityTypeList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var activityType = e.Item as ActivityType;
            Application.Current.Properties["activityTypeProperty"] = activityType.ActivityTypeDescription;

            Teste = (e.Item as ActivityType).ActivityTypeDescription;
            Navigation.PopAsync();
        }
    }
}