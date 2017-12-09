using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.Activities.Helpers.ActivityType
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditActivityTypeView : ContentPage
    {
        public EditActivityTypeView()
        {
            InitializeComponent();
        }

        private void changePrivacy_Toggled(object sender, ToggledEventArgs e)
        {
            bool result = e.Value;
            string labelText = PrivacyName.Text;
            if (result)
            {
                PrivacyName.Text = labelText + " is Private";
            }
            else
            {
                PrivacyName.Text = labelText + " is Public";
            }
        }
    }
}