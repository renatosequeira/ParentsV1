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
    public partial class ActivitiesInstitutionTypeListView : ContentPage
    {
        public ActivitiesInstitutionTypeListView()
        {
            InitializeComponent();
        }

        private void ActivityList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ActivityInstitutionTypeList.SelectedItem = null;
        }
    }
}