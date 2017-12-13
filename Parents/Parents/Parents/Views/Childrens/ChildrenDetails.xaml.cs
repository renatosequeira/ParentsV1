namespace Parents.Views.Childrens
{
    using global::Parents.Models;
    using global::Parents.ViewModels;
    using global::Parents.ViewModels.Activities;
    using global::Parents.Views.Activities;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChildrenDetails : ContentPage
	{
		public ChildrenDetails ()
		{
			InitializeComponent ();
            
        }

        private void ChildrensListButton_Clicked(object sender, EventArgs e)
        {
            PlaceHolder.Content = null;

            this.Navigation.PopAsync();
            var childrensList = new ChildrensList();
            PlaceHolder.Content = childrensList.Content;
        }

        private void ActivitiesButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new ActivitiesView());
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {

        }

    }
}