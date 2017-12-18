using Parents.Models.ActivitiesManagement.Helpers;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.Activities.HelpersPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityPriorityHelperPageView : ContentPage
    {
        private string _valor;

        public string Valor
        {
            get
            {
                return _valor;
            }

            set
            {
                _valor = value;
            }
        }

        public ActivityPriorityHelperPageView()
        {
            InitializeComponent();

            priorityList.ItemsSource = new List<ActivitiesPriority>
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

        private void priorityList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            priorityList.SelectedItem = null;
        }

        private void priorityList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var activityType = e.Item as ActivitiesPriority;
            Application.Current.Properties["activityPriorityProperty"] = activityType.PriorityDescription;

            Valor = (e.Item as ActivitiesPriority).PriorityDescription;
            Navigation.PopAsync();
        }
    }

}