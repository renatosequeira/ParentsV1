using Parents.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.Activities.Helpers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditActivityFamilyView : ContentPage
    {
        #region Services
        DialogService dialogService;
        #endregion

        #region Properties
        bool currentState;
        #endregion

        #region Constructors
        public EditActivityFamilyView()
        {
            InitializeComponent();
            dialogService = new DialogService();
            currentState = changePrivacy.IsToggled;
        }
    
        #endregion

        async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            //var response = await dialogService.ShowConfirm("Change privacy", "Are you sure to change turn this Family State?");

            //if (currentState)
            //{
            //    if (response)
            //    {
            //        changePrivacy.IsToggled = true;
            //    }
            //    else
            //    {
            //        changePrivacy.IsToggled = false;
                   
            //    }


            //}

        }

    }
}