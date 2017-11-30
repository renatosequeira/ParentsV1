namespace Parents.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Views;
    using Services;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class HomePageViewModel
    {
        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Commands
        public ICommand ChildrensListCommand
        {
            get
            {
                return new RelayCommand(Child);
            }
        }

        async void Child()
        {
            //verificar se existe ligação à internet
            var connection = await apiService.CheckConnection();

            //se não houver ligação à internet, popup com erro e sai do método
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }


            //se nada acima se verificar, login tem sucesso

            //chamar o singleton - assegura-se que o objecto ParentsViewModel é instanciado antes de ser aberto
            var mainViewModel = MainViewModel.GetInstance();

            mainViewModel.Parents = new ParentsViewModel();

            //await Application.Current.MainPage.Navigation.PushAsync(new ParentsView());
            await Application.Current.MainPage.Navigation.PushAsync(new HomeView());


        }
        #endregion
    }
}
