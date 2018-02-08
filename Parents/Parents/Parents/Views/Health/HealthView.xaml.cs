using Parents.ViewModels;
using Parents.ViewModels.Health;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views.Health
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HealthView : TabbedPage
    {
        public HealthView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //var mainViewModel = MainViewModel.GetInstance();
            //mainViewModel.NewChildrenWeight = new NewWeightViewModel();
            //mainViewModel.ChildrenWeight = new ChildrenWeightViewModel();
        }
    }
}