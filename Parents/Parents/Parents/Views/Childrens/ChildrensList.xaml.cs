using Parents.Models;
using Parents.Services;
using Parents.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using Parents.Resources;

namespace Parents.Views.Childrens
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChildrensList : ContentPage
	{
        #region Services
        NavigationService navigationService;
        #endregion

        #region Constructors
        public ChildrensList()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Childrens = new ChildrensViewModel();

            
            InitializeComponent();

            navigationService = new NavigationService();
            contentView.IsVisible = false;
            BusyIndicator.IsBusy = false;

        } 
        #endregion

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            navigationService.BackOnMaster();
            return true;
        }

        private async void searchBar_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            //await searchBar.FadeTo(0.5,100,Easing.CubicIn);
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ChildrensCompletedList.SelectedItem = null;
            var child = e.Item as Children;
        }

        private void ChildrensCompletedList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ChildrensCompletedList.SelectedItem = null;
            var child = e.SelectedItem as Children;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            this.BusyIndicator.Title = AppResources.Opening;

            BusyIndicator.IsBusy = true;
            contentView.IsVisible = true;
            await Task.Delay(2000);
            contentView.IsVisible = false;
            BusyIndicator.IsBusy = false;
        }
    }
}