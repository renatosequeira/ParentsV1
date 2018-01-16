using Parents.Renderers;
using Parents.ViewModels.Activities.HelperPages;

namespace Parents.Views.Activities.HelpersPages
{
    public partial class SearchPageView : SearchPage
    {
		public SearchPageView ()
		{
            BindingContext = new SearchPageViewModel();
            InitializeComponent ();
		}
	}
}