namespace Parents.ViewModels.AppCore
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Parents.ViewModels.Activities;
    using Parents.ViewModels.Activities.Helpers.ActivitiesInstitutionType;
    using Parents.ViewModels.Activities.Helpers.ActivityType;
    using Parents.ViewModels.Activities.Helpers.Peridiocity;
    using Parents.ViewModels.School;
    using Services;
    using ViewModels;

    public class NewParentViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties
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

        public string ParentFirstName { get; set; }
        public string ParentMIddleName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentIdentityCard { get; set; }
        public DateTime ParentBirthDate { get; set; }
        public string ParentEmail { get; set; }
        public string ParentMobile { get; set; }
        public string ParentAddress { get; set; }
        public string ParentImage { get; set; }
        public int UserType { get; set; }
        public string Password { get; set; }
        public string Confirm
        {
            get;
            set;
        }
        public string ParentalType { get; set; }
        #endregion

        #region Constructors
        public NewParentViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

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
            if (string.IsNullOrEmpty(ParentFirstName))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must enter a first name.");
                return;
            }

            if (string.IsNullOrEmpty(ParentLastName))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must enter a last name.");
                return;
            }

            if (string.IsNullOrEmpty(ParentEmail))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must enter a email.");
                return;
            }

            if (!RegexUtilities.IsValidEmail(ParentEmail))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must enter a valid email.");
                return;
            }

            if (ParentBirthDate == null)
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must enter a birth date.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must enter a password.");
                return;
            }

            if (Password.Length < 6)
            {
                await dialogService.ShowMessage(
                    "Error",
                    "The password must have at least 6 characters length.");
                return;
            }

            if (string.IsNullOrEmpty(Confirm))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must enter a password confirm.");
                return;
            }

            if (!Password.Equals(Confirm))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "The password and confirm, does not match.");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var parent = new Parent
            {
                UserType = 1,
                ParentAddress = ParentAddress,
                ParentBirthDate = ParentBirthDate,
                ParentEmail = ParentEmail,
                ParentFirstName = ParentFirstName,
                ParentIdentityCard = ParentIdentityCard,
                ParentImage = ParentImage,
                ParentLastName = ParentLastName,
                ParentMiddleName = ParentMIddleName,
                ParentMobile = ParentMobile,
                Password = "123456",
            };

            var response = await apiService.Post(
                "http://api.parents.outstandservices.pt",
                "/api",
                "/Parents",
                parent);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                    "Error",
                    response.Message);
                return;
            }

            var response2 = await apiService.GetToken(
                "http://api.parents.outstandservices.pt",
                ParentEmail,
                Password);

            if (response2 == null)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                    "Error",
                    "The service is not available, please try latter.");
                Password = null;
                return;
            }

            if (string.IsNullOrEmpty(response2.AccessToken))
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                    "Error",
                    response2.ErrorDescription);
                Password = null;
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = response2;
            mainViewModel.Parents = new ParentsViewModel();
            mainViewModel.Childrens = new ChildrensViewModel();
            mainViewModel.Disciplines = new DisciplinesViewModel();
            mainViewModel.ActivityFamily = new ActivityFamilyViewModel();
            mainViewModel.ActivitiesInstitutionType = new ActivitiesInstitutionTypeViewModel();
            mainViewModel.ActivityPeridiocity = new ActivityPeridiocityViewModel();
            mainViewModel.ActivityType = new ActivityTypeViewModel();
            await navigationService.Back();
            await navigationService.Navigate("HomeView");

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
