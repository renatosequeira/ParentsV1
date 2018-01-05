using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.Activities.HelpersPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityImageClickedHelperPage : PopupPage
    {
        public ActivityImageClickedHelperPage()
        {
            InitializeComponent();

            var options = new List<string>
            {
                "View Image",
                "Change Image",
                "Delete Image"
            };

            listViewOptions.ItemsSource = options;
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        private void OkButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}