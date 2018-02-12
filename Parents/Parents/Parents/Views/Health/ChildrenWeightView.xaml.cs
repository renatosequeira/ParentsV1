namespace Parents.Views.Health
{
    using global::Parents.Models.HealthManagement;
    using global::Parents.Services;
    using global::Parents.ViewModels;
    using global::Parents.ViewModels.Health;
    using global::Parents.ViewModels.Health.HelperPages;
    using global::Parents.Views.Activities.HelpersPages;
    using global::Parents.Views.Health.HelperPages;
    using Rg.Plugins.Popup.Extensions;
    using System;
    using Xamarin.Forms;

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
        } 
        #endregion
        
        private void weightEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string r = e.NewTextValue;
            //double valor = double.Parse(r);
            //weightSlider.Value = valor;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //string r = e.NewTextValue;
            //double valor = double.Parse(r);
            //weightSlider.Value = valor;
        }


        private void ClickGestureRecognizer_Clicked(object sender, EventArgs e)
        {
            var maincv = MainViewModel.GetInstance();
            var list = maincv.ChildrenWeight.ChildrensWeightList;
            
        }

        private async void ChildrensWeightList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as ChildrenWeight;

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditChildrenWeightHelper = new EditChildrenWeightHelperViewModel(content);

            var popup = new EditChildrenWeightHelperPage();
            await Navigation.PushPopupAsync(popup);
        }
    }
}