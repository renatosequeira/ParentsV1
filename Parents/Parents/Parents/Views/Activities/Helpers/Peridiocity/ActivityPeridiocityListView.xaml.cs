using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.Activities.Helpers.Peridiocity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityPeridiocityListView : ContentPage
    {
        public ActivityPeridiocityListView()
        {
            InitializeComponent();
        }

        private void ActivityInstitutionTypeList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ActivitiesPeridiocityList.SelectedItem = null;
        }
    }
}