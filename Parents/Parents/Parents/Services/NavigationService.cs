namespace Parents.Services
{
    using Parents.Views;
    using Parents.Views.Childrens;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using System;
    using Parents.Views.School;

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
                    await Application.Current.MainPage.Navigation.PushAsync(new DisciplinesView());
                    break;
                case "NewDisciplineView":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewDisciplineView());
                    break;
            }

        }

        public async Task Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
