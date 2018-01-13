namespace Parents.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using System;
    using Services;
    using Parents.ViewModels.School;
    using Parents.ViewModels.Activities;
    using Parents.ViewModels.Activities.Helpers.ActivitiesInstitutionType;
    using Parents.ViewModels.Activities.Helpers.Peridiocity;
    using Parents.ViewModels.Activities.Helpers.ActivityType;
    using Parents.ViewModels.AppCore;
    using Xamarin.Forms;
    using Parents.Views;

    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        DialogService dialogService;
        ApiService apiService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        string _email;
        string _password;
        bool _isToggled;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties
        public string Email {
            get
            {
                return _email;
            }
            set
            {
                if(_email != value)
                {
                    _email = value;
                    PropertyChanged?.Invoke(
                        this, 
                        new PropertyChangedEventArgs(nameof(Email)));
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        }

        public bool IsToggled
        {
            get
            {
                return _isToggled;
            }
            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsToggled)));
                }
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            IsEnabled = true; //bool are disabled by default. This will enable buttons
            IsToggled = true;

            Email = "rds.516@gmail.com";
            Password = "123456";
        }
        #endregion

        #region Commands
        public ICommand LoginWithFacebookCommand1
        {
            get
            {
                return new RelayCommand(LoginWithFacebook1);
            }
        }

        async void LoginWithFacebook1()
        {
            //await navigationService.NavigateOnLogin("LoginFacebookView");
            await Application.Current.MainPage.Navigation.PushAsync(new LoginFacebookView());
        }


        public ICommand LoginWithFacebookCommand
        {
            get
            {
                return new RelayCommand(LoginWithFacebook);
            }
        }

        async void LoginWithFacebook()
        {
            await navigationService.NavigateOnLogin("LoginFacebookView");
        }


        public ICommand LoginCommand {
            get
            {
                return new RelayCommand(Login);
            }
        }

        async void Login()
        {
            //Validação do preenchimento dos campos email e password
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Error", "Please enter a valid Email!");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "Please enter a Password!");
                return;
            }

            //Activity Indicator ativo e botões inativos
            IsRunning = true;
            IsEnabled = false;

            //verificar se existe ligação à internet
            var connection = await apiService.CheckConnection();

            //se não houver ligação à internet, popup com erro e sai do método
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                IsRunning = false;
                IsEnabled = true;
                return;
            }

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.GetToken(urlAPI, Email, Password);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (response == null)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", "The service is not available. Please try again later");
                return;
            }

            if (String.IsNullOrEmpty(response.AccessToken))
            {
                await dialogService.ShowMessage("Error", response.ErrorDescription);
                IsRunning = false;
                IsEnabled = true;
                return;
            }

            //se nada acima se verificar, login tem sucesso

            //chamar o singleton - assegura-se que o objecto ParentsViewModel é instanciado antes de ser aberto
            var mainViewModel = MainViewModel.GetInstance();

            mainViewModel.Token = response; //guarda Token no mainviewmodel
            mainViewModel.Parents = new ParentsViewModel();
            mainViewModel.Childrens = new ChildrensViewModel();
            mainViewModel.Disciplines = new DisciplinesViewModel();
            mainViewModel.ActivityFamily = new ActivityFamilyViewModel();
            mainViewModel.ActivitiesInstitutionType = new ActivitiesInstitutionTypeViewModel();
            mainViewModel.ActivityPeridiocity = new ActivityPeridiocityViewModel();
            mainViewModel.ActivityType = new ActivityTypeViewModel();

            //mainViewModel.Activities = new ActivitiesViewModel();
            
            navigationService.SetMainPage("MasterView");
      
            Email = null;
            Password = null;

            IsRunning = false;
            IsEnabled = true;
        }

        public ICommand RegisterNewUserCommand
        {
            get
            {
                return new RelayCommand(RegisterNewUser);
            }
        }

        async void RegisterNewUser()
        {
            //MainViewModel.GetInstance().NewUser = new NewUserViewModel();
            //await navigationService.Navigate("NewParentView");
            MainViewModel.GetInstance().NewParent = new NewParentViewModel();
            await navigationService.NavigateOnLogin("NewParentView");
        }


        #endregion
    }
}
