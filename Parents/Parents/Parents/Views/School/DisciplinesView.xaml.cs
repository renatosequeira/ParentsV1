using Parents.ViewModels;
using Parents.ViewModels.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.School
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisciplinesView : ContentPage
    {
        public DisciplinesView()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DisciplinesList.SelectedItem = null;
        }
    }
}