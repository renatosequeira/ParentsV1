namespace Parents.ViewModels.Health.HelperPages
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Parents.Models.HealthManagement;
    using Parents.Resources;
    using Parents.Services;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Forms;

    public class AddChildrenWeightHelperViewModel : INotifyPropertyChanged
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
        ChildrenWeight childrenWeight;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties
        public string InsertedWeight { get; set; }

        public double WeightValue { get; set; }

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
        public AddChildrenWeightHelperViewModel()
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            //InsertedWeight = childrenWeight.WeightVaue.ToString();
            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand CancelCommand
        {
            get
            {
                return new RelayCommand(Cancel);
            }
        }

        private async void Cancel()
        {
            bool response = await dialogService.ShowConfirm(AppResources.Confirm, AppResources.CancelAction);

            if (!response)
            {
                return;
            }

            await PopupNavigation.PopAsync();
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        async void Save()
        {
            if (string.IsNullOrEmpty(InsertedWeight))
            {
                await dialogService.ShowMessage("Error", "Please insert Children Weight");
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

            childrenWeight.WeightVaue = Convert.ToDouble(InsertedWeight);


            var mainViewModel = MainViewModel.GetInstance();

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Put(
                urlAPI,
                "/api",
                "/ChildrensWeight",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                childrenWeight);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            var childrensWeightViewModel = ChildrenWeightViewModel.GetInstance();
            childrensWeightViewModel.UpdateChildrenWeight(childrenWeight);

            //await navigationService.BackOnMaster();

            await PopupNavigation.PopAsync();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
