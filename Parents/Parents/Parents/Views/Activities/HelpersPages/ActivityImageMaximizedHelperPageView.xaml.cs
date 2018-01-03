using Parents.Services;
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
	public partial class ActivityImageMaximizedHelperPageView : PopupPage
    {
        #region Services
        NavigationService navigationService;
        #endregion

        public ActivityImageMaximizedHelperPageView()
		{
            InitializeComponent();
            navigationService = new NavigationService();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            
            //await PopupNavigation.PopAsync();
            CloseAllPopup();

            
        }

        private async void CloseAllPopup()
        {
            //await navigationService.ClosePopup();
            await Application.Current.MainPage.Navigation.PopAllPopupAsync();
        }
    }
}