using GalaSoft.MvvmLight.Command;
using Parents.Models;
using Parents.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Parents.ViewModels.School
{
    public class NewDisciplineViewModel : INotifyPropertyChanged
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
        string _disciplineDescription;
        string _disciplineRemarks;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties
        public string DisciplineDescription
        {
            get
            {
                return _disciplineDescription;
            }
            set
            {
                if (_disciplineDescription != value)
                {
                    _disciplineDescription = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(DisciplineDescription)));
                }
            }
        }

        public string DisciplineRemarks
        {
            get
            {
                return _disciplineRemarks;
            }
            set
            {
                if (_disciplineRemarks != value)
                {
                    _disciplineRemarks = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(DisciplineRemarks)));
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
        public NewDisciplineViewModel()
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
            if (string.IsNullOrEmpty(DisciplineDescription))
            {
                await dialogService.ShowMessage("Error", "Please insert discipline description");
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

            var discipline = new Discipline
            {
                DisciplineDescription = DisciplineDescription,
                DisciplineRemarks = DisciplineRemarks
            };

            var mainViewModel = MainViewModel.GetInstance();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Post(
                "http://api.parents.outstandservices.pt",
                "/api",
                "/Disciplines",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                discipline);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            discipline = (Discipline)response.Result;
            var disciplinesViewModel = DisciplinesViewModel.GetInstance();
            disciplinesViewModel.Add(discipline);

            await navigationService.BackOnMaster();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
