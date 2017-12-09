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
    public partial class ActivityTypeListView : ContentPage
    {
        public ActivityTypeListView()
        {
            InitializeComponent();
        }

        private void ActivityInstitutionTypeList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ActivityTypeList.SelectedItem = null;
        }
    }
}