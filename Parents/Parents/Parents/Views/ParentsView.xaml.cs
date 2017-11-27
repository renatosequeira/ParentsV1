using Parents.Views.Parents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParentsView : ContentPage
    {
        public ParentsView()
        {
            InitializeComponent();

            
        }

        private void ParentsSearchIcon_Tapped(object sender, EventArgs e)
        {
            var page = new ParentsSearch();
            PlaceHolder.Content = page.Content;
        }

        private void ParentsInviteIcon_Tapped(object sender, EventArgs e)
        {
            var page = new ParentsInvite();
            PlaceHolder.Content = page.Content;
        }

        private void ParentsNotifyIcon_Tapped(object sender, EventArgs e)
        {
            var page = new ParentsNotify();
            PlaceHolder.Content = page.Content;
        }

        private void ParentsMessageIcon_Tapped(object sender, EventArgs e)
        {
            var page = new ParentsMessage();
            PlaceHolder.Content = page.Content;
        }
    }
}