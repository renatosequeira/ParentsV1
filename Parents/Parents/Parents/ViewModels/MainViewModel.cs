namespace Parents.ViewModels
{
    using Models;
    using Parents.ViewModels.Childrens;
    using Refractored.FabControl;
    using Xamarin.Forms;

    public class MainViewModel
    {
        #region Properties

        public LoginViewModel Login { get; set; }
        public ParentsViewModel Parents { get; set; }
        public ChildrensViewModel Childrens { get; set; }
        public ChildrenDetailsViewModel ChildrenDetails { get; set; }
        public TokenResponse Token { get; set; } //permite que o token esteja disponível durante a execução do programa


        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();
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

    }
}
