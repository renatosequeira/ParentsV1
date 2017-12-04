namespace Parents.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Services;
    using Parents.ViewModels.Childrens;
    using Parents.Models;
    using Parents.ViewModels.School;
    using Parents.ViewModels.Settings;

    public class MainViewModel
    {
        #region Services
        NavigationService navigationService;
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
        //public SettingsHomeViewModel SettingsHome { get; set; }
        //public ApplicationSettingsCoreViewModel ApplicationSettingsCore { get; set; }


        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();
            navigationService = new NavigationService();
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
            await navigationService.Navigate("NewDisciplineView");
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
            await navigationService.Navigate("HomeView");
        }

        async void GoNewChildren()
        {
            NewChildren = new NewChildrenViewModel();  //Liga o objecto NewChildren a um viewmodel
            await navigationService.Navigate("NewChildrenView");
        }
        #endregion

    }
}
