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
    using Parents.Views.Sistema;
    using Parents.Views.Activities;
    using Parents.Views.Locations;

    public class NavigationService
    {
        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
                case "LoginView":
                    Application.Current.MainPage = new NavigationPage(new LoginView());
                    break;
                case "MasterView":
                    Application.Current.MainPage = new MasterView();
                    break;
            }
        }

        public async Task NavigateOnMaster(string pageName)
        {
            App.Master.IsPresented = false;

            switch (pageName)
            {
                case "HomeView":
                    await App.Navigator.PushAsync(new HomeView());
                    break;

                case "ChildrenDetails":
                    await App.Navigator.PushAsync(new ChildrenDetails());
                    break;

                case "ParentsView":
                    await App.Navigator.PushAsync(new ParentsView());
                    break;

                case "ChildrensList":
                    await App.Navigator.PushAsync(new ChildrensList());
                    break;

                case "NewChildrenView":
                    await App.Navigator.PushAsync(new NewChildrenView());
                    break;
                case "DisciplinesView":
                    await App.Navigator.PushAsync(new DisciplineDetailsViews());
                    break;
                case "NewDisciplineView":
                    await App.Navigator.PushAsync(new NewDisciplineView());
                    break;
                case "SettingsView":
                    await App.Navigator.PushAsync(new SettingsHomeView());
                    break;
                case "Application Core":
                    await App.Navigator.PushAsync(new ApplicationSettingCoreView());
                    break;
                case "Education":
                    await App.Navigator.PushAsync(new EducationSettingsView());
                    break;
                case "System":
                    await App.Navigator.PushAsync(new SystemSettingsView());
                    break;
                case "Disciplines":
                    await App.Navigator.PushAsync(new DisciplinesView());
                    break;
                case "EditDiscipline":
                    await App.Navigator.PushAsync(new EditDisciplineView());
                    break;
                case "Activities Family":
                    await App.Navigator.PushAsync(new ActivitiesFamilyListView());
                    break;
                case "Activities":
                    await App.Navigator.PushAsync(new ActivitiesSettingsView());
                    break;
                case "NewActivityFamily":
                    await App.Navigator.PushAsync(new NewActivityFamilyView());
                    break;
                case "EditActivityFamilyViewModel":
                    await App.Navigator.PushAsync(new EditActivityFamilyView());
                    break;
                case "Activities Family Details":
                    await App.Navigator.PushAsync(new DetailsActivityFamilyView());
                    break;
                case "Activity Institution Type":
                    await App.Navigator.PushAsync(new ActivitiesInstitutionTypeListView());
                    break;
                case "EditActivityInstitutionType":
                    await App.Navigator.PushAsync(new EditActivityInstitutionTypeView());
                    break;
                case "NewActivityInstitutionType":
                    await App.Navigator.PushAsync(new NewActivitiesInstitutionTypeView());
                    break;
                case "DetailsActivityInstitutionType":
                    await App.Navigator.PushAsync(new DetailsActivitiesInstitutionTypeView());
                    break;
                case "NewActicityPeridiocity":
                    await App.Navigator.PushAsync(new NewActivityPeridiocityView());
                    break;
                case "EditActivityPeridiocity":
                    await App.Navigator.PushAsync(new EditActivityPeridiocityView());
                    break;
                case "DetailsActivityPeridiocity":
                    await App.Navigator.PushAsync(new ActivityPeridiocityDetailsView());
                    break;
                case "Activity Peridiocity":
                    await App.Navigator.PushAsync(new ActivityPeridiocityListView());
                    break;
                case "Activity Type":
                    await App.Navigator.PushAsync(new ActivityTypeListView());
                    break;
                case "EditActivityType":
                    await App.Navigator.PushAsync(new EditActivityTypeView());
                    break;
                case "NewActicityType":
                    await App.Navigator.PushAsync(new NewActivityTypeView());
                    break;
                case "AddAnniversaryActivity":
                    await App.Navigator.PushAsync(new NewActivityView());
                    break;

                case "ActivityTypeHelperPage":
                    await App.Navigator.PushAsync(new ActivityTypeHelperPageView());
                    break;

                case "ActivityPriorityHelperPage":
                    await App.Navigator.PushAsync(new ActivityPriorityHelperPageView());
                    break;

                case "ActivityLocationHelperPage":
                    await App.Navigator.PushAsync(new ActivityLocationHelperPageView());
                    break;

                case "Activity Details":
                    await App.Navigator.PushAsync(new EditActivityView());
                    break;

                case "ActivitiesListView":
                    await App.Navigator.PushAsync(new ActivitiesView());
                    break;

                case "LocationsView":
                    await App.Navigator.PushAsync(new LocationsView());
                    break;

                case "OpenActivitiesFullListWindow":
                    await App.Navigator.PushAsync(new ActivitiesListView());
                    break;
                case "SyncView":
                    await App.Navigator.PushAsync(new SyncView());
                    break;
                case "MyProfileView":
                    await App.Navigator.PushAsync(new MyProfileView());
                    break;
            }

        }

        public async Task NavigateOnLogin(string pageName)
        {
            switch (pageName)
            {
                case "NewParentView":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewParentView());
                    break;

                case "NewUserView":
                    await App.Navigator.PushAsync(new NewUserView());
                    break;

                case "LoginFacebookView":
                    await App.Navigator.PushAsync(new LoginFacebookView());
                    break;

                //case "PasswordRecoveryView":
                //    await App.Navigator.PushAsync(new PasswordRecoveryView());
                //    break;
                case "PasswordRecoveryView":
                    await Application.Current.MainPage.Navigation.PushAsync(new PasswordRecoveryView());
                    break;
            }
        }

        public async Task BackOnMaster()
        {
            await App.Navigator.PopAsync();
        }

        public async Task BackOnLogin()
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
