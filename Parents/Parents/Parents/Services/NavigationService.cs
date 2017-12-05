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
            }

        }

        public async Task Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
