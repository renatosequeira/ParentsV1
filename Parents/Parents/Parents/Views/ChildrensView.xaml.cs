namespace Parents.Views
{
    using Views.Childrens;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChildrensView : ContentPage
	{

		public ChildrensView ()
		{

			InitializeComponent ();

            
        }

        private void btnMyChildrens_Clicked()
        {
            var page = new ChildrensList();
            PlaceHolder.Content = page.Content;
        }

        private void FAB_Clicked(object sender, EventArgs e)
        {

        }

        private void ToolbarItem_Activated(object sender, EventArgs e)
        {

        }
    }
}