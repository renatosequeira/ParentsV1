using GalaSoft.MvvmLight.Command;
using Parents.Services;
using System.Windows.Input;
using Parents.ViewModels;
using Parents.ViewModels.Locations;
using System.Threading.Tasks;

namespace Parents.Models.Sistema
{
    public class Menu
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Attrbuttes
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Constructors
        public Menu()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand NavigateCommand {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        async void Navigate()
        {
            switch (PageName)
            {
                case "LoginView":
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    navigationService.SetMainPage("LoginView");
                    break;

                case "LocationsView":
                    MainViewModel.GetInstance().Locations = new LocationsViewModel();
                    await navigationService.NavigateOnMaster("LocationsView");
                    break;
                default:
                    break;
            }
        }


        #endregion
    }
}
