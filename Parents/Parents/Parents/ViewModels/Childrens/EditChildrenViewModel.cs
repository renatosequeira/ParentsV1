namespace Parents.ViewModels.Childrens
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using System;
    using Services;
    using Parents.Models;

    public class EditChildrenViewModel : INotifyPropertyChanged
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
        Children children;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties

        public string ChildrenFirstName
        {
            get;
            set;
        }

        public string ChildrenLastName
        {
            get;
            set;
        }

        public string ChildrenIdentityCard
        {
            get;
            set;
        }

        public string ChildrenBirthDate
        {
            get;
            set;
        }

        public string ChildrenSex
        {
            get;
            set;
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
        public EditChildrenViewModel(Children children)
        {
            this.children = children;
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            ChildrenFirstName = children.ChildrenFirstName;
            ChildrenLastName = children.ChildrenLastName;
            ChildrenIdentityCard = children.ChildrenIdentityCard;
            ChildrenSex = children.ChildrenSex;

            IsEnabled = true;
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

            children.ChildrenFirstName = ChildrenFirstName;
            children.ChildrenLastName = ChildrenLastName;
            children.ChildrenIdentityCard = ChildrenIdentityCard;
            children.ChildrenSex = ChildrenSex;
            children.ChildrenBirthDate = DateTime.Today;
            children.BloodInformationDescription = "";
            //children.BoodInformationId = 1;
            children.ChildrenAddress = "";
            children.ChildrenEmail = "";
            children.ChildrenFamilyDoctor = "";
            children.ChildrenImage = "";
            children.ChildrenMiddleName = "";
            children.ChildrenMobile = "";
            children.CurrentSchool = "";
            //children.MatrimonialStateId = 1;
            children.SchoolContact = "";
            

            var mainViewModel = MainViewModel.GetInstance();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Put(
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

            var childrensViewModel = ChildrensViewModel.GetInstance();
            childrensViewModel.UpdateChildren(children);

            await navigationService.BackOnMaster();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
