namespace Parents.Views.Health
{
    using global::Parents.Models.HealthManagement;
    using global::Parents.Services;
    using global::Parents.ViewModels;
    using global::Parents.ViewModels.Health.HelperPages;
    using global::Parents.Views.Health.HelperPages;
    using Rg.Plugins.Popup.Extensions;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    public partial class ChildrenWeightView : ContentPage
	{
        #region Services
        ApiService apiService;
        DialogService dialogService;
        DataService dataService;
        NavigationService navigationService;
        #endregion

        #region Constructors
        public ChildrenWeightView()
        {
            InitializeComponent();

            apiService = new ApiService();
            dialogService = new DialogService();
            dataService = new DataService();

            MessagingCenter.Subscribe<AddChildrenWeightHelperPage, string>(this, "insertedWeightFromPopup", (s, a) => {
                weightLabel.Text = a.ToString();
            });
        } 
        #endregion

        async void ChildrensWeightList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as ChildrenWeight;

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditChildrenWeightHelper = new EditChildrenWeightHelperViewModel(content);

            var popup = new EditChildrenWeightHelperPage();
            await Navigation.PushPopupAsync(popup);
        }

        async void AddWeight_Clicked(object sender, System.EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.AddChildrenWeightHelper = new AddChildrenWeightHelperViewModel();

            var popup = new AddChildrenWeightHelperPage();
            await Navigation.PushPopupAsync(popup);

        }
    }
}