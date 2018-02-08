namespace Parents.Views.Health
{
    using global::Parents.Services;
    using global::Parents.ViewModels;
    using SkiaSharp;
    using System;
    using Xamarin.Forms;
    //using Entry = Microcharts.Entry;

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

    }
}