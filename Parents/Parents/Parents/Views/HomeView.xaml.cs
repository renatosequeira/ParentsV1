﻿namespace Parents.Views
{
    using Plugin.LocalNotifications;
    using Services;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Constructors
        public HomeView()
        {
            navigationService = new NavigationService();
            InitializeComponent();
        }
        #endregion

        #region ButtonControls
        private async void btnParents_Clicked(object sender, EventArgs e)
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new ParentsView());
            await navigationService.NavigateOnMaster("ParentsView");
        }

        private async void btnChildrens_Clicked(object sender, EventArgs e)
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new ChildrensList());
            await navigationService.NavigateOnMaster("ChildrensList");

        }
        #endregion

        private async void btn4_Clicked(object sender, EventArgs e)
        {
            await navigationService.NavigateOnMaster("SettingsView");
        }

        private void btn3_Clicked(object sender, EventArgs e)
        {
            CrossLocalNotifications.Current.Show("Testando as Notificações", "Agora va ler mais artigos do Bertuzzi no Medium", 1, DateTime.Now.AddSeconds(5));
        }
    }
}