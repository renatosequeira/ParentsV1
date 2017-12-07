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
            }

        }

        public async Task Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    
    }
}
