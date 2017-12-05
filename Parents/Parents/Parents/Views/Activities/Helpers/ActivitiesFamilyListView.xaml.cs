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
    public partial class ActivitiesFamilyListView : ContentPage
    {
        public ActivitiesFamilyListView()
        {
            InitializeComponent();
        }

        private void DisciplinesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ActivityList.SelectedItem = null;

        }
    }
}