namespace Parents.Views.Health.HelperPages
{
    using System;
    using global::Parents.Services;
    using Rg.Plugins.Popup.Pages;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Forms;

    public partial class AddChildrenWeightHelperPage : PopupPage
    {
        #region Services
        DialogService dialogService;
        #endregion

        #region Properties
        public string currentWeight { get; set; }
        #endregion

        #region Constructors
        public AddChildrenWeightHelperPage()
        {

            InitializeComponent();

            dialogService = new DialogService();

        }
        #endregion

        #region ClickEvents
        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            double value = Double.Parse(weight.Text);

            if(value > 100){
                bool result = await dialogService.ShowConfirm("Confirm weight","Are you sure weight is right?");

                if(!result){
                    return;
                }
            }
            MessagingCenter.Send(this, "insertedWeightFromPopup", weight.Text);
            await PopupNavigation.PopAsync();
        }
        #endregion
    }
}
