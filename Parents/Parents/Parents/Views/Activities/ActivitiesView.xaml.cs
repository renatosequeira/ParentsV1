namespace Parents.Views.Activities
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActivitiesView : TabbedPage
	{
        public ActivitiesView ()
		{
			InitializeComponent ();
            BarBackgroundColor = Color.FromHex("#EE9D31");

            //masterPage = new ActivitiesView();
            //Master = masterPage;
            //Detail = new NavigationPage(new SchoolActivitiesListView());
        }
	}
}