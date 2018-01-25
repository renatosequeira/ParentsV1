using GalaSoft.MvvmLight.Command;
using Parents.Services;
using System.Windows.Input;
using Parents.ViewModels;
using Parents.ViewModels.Locations;
using Parents.ViewModels.Sistema;

namespace Parents.Models.Sistema
{
    public class Menu
    {
        #region Services
        NavigationService navigationService;
        DataService dataService;
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
            dataService = new DataService();
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
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token.IsRemembered = false;
                    dataService.Update(mainViewModel.Token);
                    mainViewModel.Login = new LoginViewModel();
                    navigationService.SetMainPage(PageName);
                    break;
                case "LocationsView":
                    MainViewModel.GetInstance().Locations = new LocationsViewModel();
                    await navigationService.NavigateOnMaster(PageName);
                    break;
                case "SyncView":
                    MainViewModel.GetInstance().Sync = new SyncViewModel();
                    await navigationService.NavigateOnMaster(PageName);
                    break;
                case "MyProfileView":
                    MainViewModel.GetInstance().MyProfile = new MyProfileViewModel();
                    await navigationService.NavigateOnMaster(PageName);
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
