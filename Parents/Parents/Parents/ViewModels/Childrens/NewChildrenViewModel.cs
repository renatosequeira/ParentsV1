namespace Parents.ViewModels.Childrens
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using System;
    using Services;
    using Parents.Models;

    public class NewChildrenViewModel : INotifyPropertyChanged
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
        string _childrenFirstName;
        string _childrenLastName;
        string _childrenIdentityCard;
        string _childrenBirthDate;
        string _childrenSex;

        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties

        public string ChildrenFirstName
        {
            get
            {
                return _childrenFirstName;
            }
            set
            {
                if (_childrenFirstName != value)
                {
                    _childrenFirstName = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrenFirstName)));
                }
            }
        }

        public string ChildrenLastName
        {
            get
            {
                return _childrenLastName;
            }
            set
            {
                if (_childrenLastName != value)
                {
                    _childrenLastName = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrenLastName)));
                }
            }
        }

        public string ChildrenIdentityCard
        {
            get
            {
                return _childrenIdentityCard;
            }
            set
            {
                if (_childrenIdentityCard != value)
                {
                    _childrenIdentityCard = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrenIdentityCard)));
                }
            }
        }

        public string ChildrenBirthDate
        {
            get
            {
                return _childrenBirthDate;
            }
            set
            {
                if (_childrenBirthDate != value)
                {
                    _childrenBirthDate = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrenBirthDate)));
                }
            }
        }

        public string ChildrenSex
        {
            get
            {
                return _childrenSex;
            }
            set
            {
                if (_childrenSex != value)
                {
                    _childrenSex = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrenSex)));
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
        public NewChildrenViewModel()
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            IsEnabled = true; //bool are disabled by default. This will enable buttons

        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        async void Save()
        {
            if (string.IsNullOrEmpty(ChildrenFirstName))
            {
                await dialogService.ShowMessage("Error", "Please insert Children First Name");
                return;
            }

            if (string.IsNullOrEmpty(ChildrenLastName))
            {
                await dialogService.ShowMessage("Error", "Please insert Children Last Name");
                return;
            }

            if (string.IsNullOrEmpty(ChildrenIdentityCard))
            {
                await dialogService.ShowMessage("Error", "Please insert Children ID card number");
                return;
            }

            if (string.IsNullOrEmpty(ChildrenSex))
            {
                await dialogService.ShowMessage("Error", "Please insert Children Gender");
                return;
            }

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

            var children = new Children
            {
                ChildrenFirstName = ChildrenFirstName,
                ChildrenLastName = ChildrenLastName,
                ChildrenIdentityCard = ChildrenIdentityCard,
                ChildrenSex = ChildrenSex,
                ChildrenBirthDate = DateTime.Today,
                BloodInformationDescription = "",
                BoodInformationId = 1,
                ChildrenAddress = "",
                ChildrenEmail = "",
                ChildrenFamilyDoctor = "",
                ChildrenImage = "",
                ChildrenMiddleName = "",
                ChildrenMobile = "",
                CurrentSchool ="",
                MatrimonialStateId = 1,
                SchoolContact = ""
            };

            var mainViewModel = MainViewModel.GetInstance();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Post(
                "http://api.parents.outstandservices.pt",
                "/api",
                "/Childrens",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                children);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            children = (Children)response.Result;
            var childrensViewModel = ChildrensViewModel.GetInstance();
            childrensViewModel.AddChildren(children);

            await navigationService.Back();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
