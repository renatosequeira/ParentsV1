namespace Parents.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Services;
    using ViewModels.Childrens;
    using Models;
    using ViewModels.School;
    using ViewModels.Settings;
    using ViewModels.Activities;
    using ViewModels.Activities.Helpers;
    using Views.Activities.Helpers;
    using Parents.ViewModels.Activities.Helpers.ActivitiesInstitutionType;
    using Parents.ViewModels.Activities.Helpers.Peridiocity;
    using System;
    using Parents.ViewModels.Activities.Helpers.ActivityType;
    using System.Collections.ObjectModel;
    using Parents.ViewModels.Activities.HelperPages;
    using Parents.ViewModels.AppCore;
    using Parents.Models.Sistema;

    public class MainViewModel
    {
        #region Services
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Properties
        public LoginViewModel Login { get; set; }
        public ParentsViewModel Parents { get; set; }
        public ChildrensViewModel Childrens { get; set; }
        public ChildrenDetailsViewModel ChildrenDetails { get; set; }
        public TokenResponse Token { get; set; } //permite que o token esteja disponível durante a execução do programa
        public EditChildrenViewModel EditChildren { get; set; }
        public HomePageViewModel HomePage { get; set; }
        public NewChildrenViewModel NewChildren { get; set; }
        public DisciplinesViewModel Disciplines { get; set; }
        public NewDisciplineViewModel NewDiscipline { get; set; }
        public EditDisciplineViewModel EditDiscipline { get; set; }
        public ActivityFamilyViewModel ActivityFamily { get; set; }
        public NewActivityFamilyViewModel NewActvityFamily { get; set; }
        public EditActivityFamilyViewModel EditActivityFamily { get; set; }
        public ActivitiesInstitutionTypeViewModel ActivitiesInstitutionType { get; set; }
        public EditActivitiesInstitutionTypeViewModel EditActivitiesInstitutionType { get; set; }
        public NewActivityInstitutionTypeViewModel NewActivityInstitutionType { get; set; }
        public ActivityPeridiocityViewModel ActivityPeridiocity { get; set; }
        public EditActivityPeridiocityViewModel EditActivityPeridiocity { get; set; }
        public NewActivityPeridiocityViewModel NewActivityPeridiocity { get; set; }
        public ActivityTypeViewModel ActivityType { get; set; }
        public NewActivityTypeViewModel NewActivityType { get; set; }
        public EditActivityTypeViewModel EditActivityType { get; set; }
        public ActivitiesViewModel Activities { get; set; }
        public Children Children { get; set; } //objetivo é nao perder de vista o objecto Children
        public NewActivityViewModel NewActivity { get; set; }
        public EditActivityViewModel EditActivity { get; set; }
        public ActivityImageMaximizedHelperPageViewModel ActivityImageMaximizedHelper { get; set; }
        public NewUserViewModel NewUser { get; set; }
        public NewParentViewModel NewParent { get; set; }
        public ObservableCollection<Menu> MyMenu { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();
            navigationService = new NavigationService();
            dialogService = new DialogService();
            LoadMenu();
        }
        #endregion

        #region Singleton
        static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if(instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Commands
        public ICommand FilterActivities
        {
            get
            {
                return new RelayCommand(GoToFilterActivities);
            }
        }

        async void GoToFilterActivities()
        {
            //NewActivityType = new NewActivityTypeViewModel();
            //await navigationService.Navigate("NewActicityType");
            await dialogService.ShowMessage("Under development", "Filtering function is under development");
        }

        public ICommand NewActivityTypeCommand
        {
            get
            {
                return new RelayCommand(GoToNewActivityTypeCommand);
            }
        }

        async void GoToNewActivityTypeCommand()
        {
            NewActivityType = new NewActivityTypeViewModel();
            await navigationService.NavigateOnMaster("NewActicityType");
        }

        public ICommand NewActivityPericiocityCommand
        {
            get
            {
                return new RelayCommand(GoToNewActivityPericiocityCommand);
            }
        }

        async void GoToNewActivityPericiocityCommand()
        {
            NewActivityPeridiocity = new NewActivityPeridiocityViewModel();
            await navigationService.NavigateOnMaster("NewActicityPeridiocity");
        }

        public ICommand NewActivityInstitutionTypeCommand
        {
            get
            {
                return new RelayCommand(GoToNewActivityInstitutionTypeCommand);
            }
        }

        async void GoToNewActivityInstitutionTypeCommand()
        {
            NewActivityInstitutionType = new NewActivityInstitutionTypeViewModel();
            await navigationService.NavigateOnMaster("NewActivityInstitutionType");
        }

        public ICommand NewActivityFamilyCommand
        {
            get
            {
                return new RelayCommand(GoToNewActivityFamily);
            }
        }

        async void GoToNewActivityFamily()
        {
            NewActvityFamily = new NewActivityFamilyViewModel();
            await navigationService.NavigateOnMaster("NewActivityFamily");
        }

        public ICommand NewDisciplineCommand
        {
            get
            {
                return new RelayCommand(GoToNewDiscipline);
            }
        }

        async void GoToNewDiscipline()
        {
            NewDiscipline = new NewDisciplineViewModel();
            await navigationService.NavigateOnMaster("NewDisciplineView");
        }

        public ICommand NewChildrenCommand
        {
            get
            {
                return new RelayCommand(GoNewChildren);
            }
        }

        public ICommand HomeViewCommand
        {
            get
            {
                return new RelayCommand(GoHome);
            }
        }

        async void GoHome()
        {
            await navigationService.NavigateOnMaster("HomeView");
        }

        async void GoNewChildren()
        {
            NewChildren = new NewChildrenViewModel();  //Liga o objecto NewChildren a um viewmodel
            await navigationService.NavigateOnMaster("NewChildrenView");
        }

        public ICommand GoToActivitiesCommand
        {
            get
            {
                return new RelayCommand(OpenActivities);
            }
        }

        async void OpenActivities()
        {
            await navigationService.NavigateOnMaster("ActivitiesListView");
        }

        public ICommand BackCommand
        {
            get
            {
                return new RelayCommand(GoBack);
            }
        }

        async void GoBack()
        {
            await navigationService.BackOnMaster();
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            MyMenu = new ObservableCollection<Menu>();

            MyMenu.Add(new Menu
            {
                Icon = "ic_settings",
                PageName = "MyProfileView",
                Title = "My Profile",
            });

            MyMenu.Add(new Menu
            {
                Icon = "ic_map",
                PageName = "LocationsView",
                Title = "Locations",
            });

            MyMenu.Add(new Menu
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginView",
                Title = "Close sesion",
            });

        }
        #endregion
    }
}
