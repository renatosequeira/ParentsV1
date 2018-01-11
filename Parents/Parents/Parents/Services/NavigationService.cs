namespace Parents.Services
{
    using Views;
    using Views.Childrens;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using System;
    using Views.School;
    using Views.Settings;
    using Views.Settings.Activities;
    using Parents.Views.Activities.Helpers;
    using Parents.Views.Activities.Helpers.ActivitiesInstitutionType;
    using Parents.Views.Activities.Helpers.Peridiocity;
    using Parents.Views.Activities.Helpers.ActivityType;
    using Parents.Views.Activities.HelpersPages;
    using Rg.Plugins.Popup.Extensions;
    using Rg.Plugins.Popup.Services;
    using Parents.Views.Parents;

    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
                case "HomeView":
                    await Application.Current.MainPage.Navigation.PushAsync(new HomeView());
                    break;

                case "ChildrenDetails":
                    await Application.Current.MainPage.Navigation.PushAsync(new ChildrenDetails());
                    break;

                case "ParentsView":
                    await Application.Current.MainPage.Navigation.PushAsync(new ParentsView());
                    break;

                case "ChildrensList":
                    await Application.Current.MainPage.Navigation.PushAsync(new ChildrensList());
                    break;

                case "NewChildrenView":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewChildrenView());
                    break;
                case "DisciplinesView":
                    await Application.Current.MainPage.Navigation.PushAsync(new DisciplineDetailsViews());
                    break;
                case "NewDisciplineView":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewDisciplineView());
                    break;
                case "SettingsView":
                    await Application.Current.MainPage.Navigation.PushAsync(new SettingsHomeView());
                    break;
                case "Application Core":
                    await Application.Current.MainPage.Navigation.PushAsync(new ApplicationSettingCoreView());
                    break;
                case "Education":
                    await Application.Current.MainPage.Navigation.PushAsync(new EducationSettingsView());
                    break;
                case "System":
                    await Application.Current.MainPage.Navigation.PushAsync(new SystemSettingsView());
                    break;
                case "Disciplines":
                    await Application.Current.MainPage.Navigation.PushAsync(new DisciplinesView());
                    break;
                case "EditDiscipline":
                    await Application.Current.MainPage.Navigation.PushAsync(new EditDisciplineView());
                    break;
                case "Activities Family":
                    await Application.Current.MainPage.Navigation.PushAsync(new ActivitiesFamilyListView());
                    break;
                case "Activities":
                    await Application.Current.MainPage.Navigation.PushAsync(new ActivitiesSettingsView());
                    break;
                case "NewActivityFamily":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewActivityFamilyView());
                    break;
                case "EditActivityFamilyViewModel":
                    await Application.Current.MainPage.Navigation.PushAsync(new EditActivityFamilyView());
                    break;
                case "Activities Family Details":
                    await Application.Current.MainPage.Navigation.PushAsync(new DetailsActivityFamilyView());
                    break;
                case "Activity Institution Type":
                    await Application.Current.MainPage.Navigation.PushAsync(new ActivitiesInstitutionTypeListView());
                    break;
                case "EditActivityInstitutionType":
                    await Application.Current.MainPage.Navigation.PushAsync(new EditActivityInstitutionTypeView());
                    break;
                case "NewActivityInstitutionType":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewActivitiesInstitutionTypeView());
                    break;
                case "DetailsActivityInstitutionType":
                    await Application.Current.MainPage.Navigation.PushAsync(new DetailsActivitiesInstitutionTypeView());
                    break;
                case "NewActicityPeridiocity":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewActivityPeridiocityView());
                    break;
                case "EditActivityPeridiocity":
                    await Application.Current.MainPage.Navigation.PushAsync(new EditActivityPeridiocityView());
                    break;
                case "DetailsActivityPeridiocity":
                    await Application.Current.MainPage.Navigation.PushAsync(new ActivityPeridiocityDetailsView());
                    break;
                case "Activity Peridiocity":
                    await Application.Current.MainPage.Navigation.PushAsync(new ActivityPeridiocityListView());
                    break;
                case "Activity Type":
                    await Application.Current.MainPage.Navigation.PushAsync(new ActivityTypeListView());
                    break;
                case "EditActivityType":
                    await Application.Current.MainPage.Navigation.PushAsync(new EditActivityTypeView());
                    break;
                case "NewActicityType":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewActivityTypeView());
                    break;
                case "AddAnniversaryActivity":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewActivityView());
                    break;

                case "ActivityTypeHelperPage":
                    await Application.Current.MainPage.Navigation.PushAsync(new ActivityTypeHelperPageView());
                    break;

                case "ActivityPriorityHelperPage":
                    await Application.Current.MainPage.Navigation.PushAsync(new ActivityPriorityHelperPageView());
                    break;

                case "ActivityLocationHelperPage":
                    await Application.Current.MainPage.Navigation.PushAsync(new ActivityLocationHelperPageView());
                    break;

                case "Activity Details":
                    await Application.Current.MainPage.Navigation.PushAsync(new EditActivityView());
                    break;

                case "NewUserView":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewUserView());
                    break;

                case "NewParentView":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewParentView());
                    break;
            }

        }

        public async Task Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task OpenPopup(string pageName)
        {
            switch (pageName)
            {
                case "maximizedActivityPage":
                    await Application.Current.MainPage.Navigation.PushAsync(new ActivityImageMaximizedHelperPageView());
                    break;

                case "Activity Filters":
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ActivitiesFilterOptionsHelperPageView());
                    break;
            }

        }

        public async Task ClosePopup()
        {
            await Application.Current.MainPage.Navigation.PopAllPopupAsync();
        }
    
    }
}
