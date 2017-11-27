using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.Childrens
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChildrensList : ContentPage
	{
		public ChildrensList ()
		{
			InitializeComponent ();
		}

        private async void btnHomeButton_Clicked()
        {
            await Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new HomeView());
        }

        private void ToolbarItem_Activated(object sender, EventArgs e)
        {

        }
    }
}