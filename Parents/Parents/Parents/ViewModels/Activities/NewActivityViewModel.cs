﻿using GalaSoft.MvvmLight.Command;
using Parents.Helpers;
using Parents.Models;
using Parents.Models.ActivitiesManagement.Helpers;
using Parents.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Parents.ViewModels.Activities
{
    public class NewActivityViewModel : INotifyPropertyChanged
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
        //ObservableCollection<ActivityType> _activityType;
        //List<ActivityType> activityTypes;
        string _activityDescription;
        string _activityRemarks;
        DateTime _activityDateStart;
        DateTime _activityDateEnd;
        DateTime _activityTimeStart;
        DateTime _activityTimeEnd;
        bool _isRunning;
        bool _isEnabled;
        bool _status;
        string _activityType;

        ImageSource _imageSource;
        MediaFile file;

        #endregion

        #region Properties
        public ImageSource ImageSource
        {
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ImageSource)));
                }
            }
            get
            {
                return _imageSource;
            }
        }


        public bool Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Status)));
                }
            }
        }

        public DateTime ActivityTimeStart
        {

            get
            {
                return _activityTimeStart;
            }
            set
            {
                if (_activityTimeStart != value)
                {
                    _activityTimeStart = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityTimeStart)));
                }
            }

        }

        public DateTime ActivityTimeEnd
        {

            get
            {
                return _activityTimeEnd;
            }
            set
            {
                if (_activityTimeEnd != value)
                {
                    _activityTimeEnd = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityTimeEnd)));
                }
            }

        }

        public string ActivityType
        {

            get
            {
                return _activityType;
            }
            set
            {
                if (_activityType != value)
                {
                    _activityType = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityType)));
                }
            }

        }

        //public ObservableCollection<ActivityType> ActType
        //{
        //    get { return _activityType; }
        //    set
        //    {
        //        if (_activityType != value)
        //        {
        //            _activityType = value;
        //            PropertyChanged?.Invoke(
        //                this,
        //                new PropertyChangedEventArgs(nameof(ActType)));
        //        }
        //    }
        //}

        public string ActivityRemarks
        {

            get
            {
                return _activityRemarks;
            }
            set
            {
                if (_activityRemarks != value)
                {
                    _activityRemarks = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityRemarks)));
                }
            }

        }

        public string ActivityDescription
        {

            get
            {
                return _activityDescription;
            }
            set
            {
                if (_activityDescription != value)
                {
                    _activityDescription = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityDescription)));
                }
            }

        }

        public DateTime ActivityDateStart
        {

            get
            {
                return _activityDateStart;
            }
            set
            {
                if (_activityDateStart != value)
                {
                    _activityDateStart = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityDateStart)));
                }
            }

        }

        public DateTime ActivityDateEnd
        {

            get
            {
                return _activityDateEnd;
            }
            set
            {
                if (_activityDateEnd != value)
                {
                    _activityDateEnd = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityDateEnd)));
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

        public string Image { get; set; }
        #endregion

        #region Constructors
        public NewActivityViewModel()
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            IsEnabled = true; //bool are disabled by default. This will enable buttons

            ActivityDateStart = DateTime.Now;
            ActivityDateEnd = DateTime.Now;

            Status = false;

            Image = "no_image";

            //LoadActivityTypes();
        }
        #endregion

        #region Commands
        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable &&
                CrossMedia.Current.IsTakePhotoSupported)
            {
                var source = await dialogService.ShowImageOptions();

                if (source == "Cancel")
                {
                    file = null;
                    return;
                }

                if (source == "From Camera")
                {
                    file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            Directory = "Sample",
                            Name = "test.jpg",
                            PhotoSize = PhotoSize.Small,
                        }
                    );
                }
                else
                {
                    file = await CrossMedia.Current.PickPhotoAsync();
                }
            }
            else
            {
                file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
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
            if (string.IsNullOrEmpty(ActivityDescription))
            {
                await dialogService.ShowMessage("Error", "Please insert Activity description");
                return;
            }

            if (string.IsNullOrEmpty(ActivityType) || ActivityType == "SELECT ACTIVITY TYPE...")
            {
                await dialogService.ShowMessage("Error", "Please select a valid actvity type");
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

            byte[] imageArray = null;
            if (file != null)
            {
                imageArray = FilesHelper.ReadFully(file.GetStream());
                file.Dispose();
            }

            var activity = new Activity
            {
                ActivityDescription = ActivityDescription,
                ActivityDateStart = ActivityDateStart,
                ActivityDateEnd = ActivityDateEnd,
                ChildrenActivityType = ActivityType,
                ActivityEndTime = ActivityTimeEnd.ToString(),
                ActivityStartTime = ActivityTimeStart.ToString(),
                ActivityRemarks = ActivityRemarks,
                Status = Status,
                relatedChildrenIdentitiCard = "10788194",
                ImageArray = imageArray
            };

            var mainViewModel = MainViewModel.GetInstance();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Post(
                "http://api.parents.outstandservices.pt",
                "/api",
                "/Activities",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                activity);


            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            activity = (Activity)response.Result;
            var activityViewModel = ActivitiesViewModel.GetInstance();
            activityViewModel.Add(activity);

            await navigationService.Back();

            IsRunning = false;
            IsEnabled = true;


        }
        #endregion

        #region Methods
        //async void LoadActivityTypes()
        //{


        //    var connection = await apiService.CheckConnection();
        //    if (!connection.IsSuccess)
        //    {
        //        await dialogService.ShowMessage("Error", connection.Message);
        //        return;
        //    }

        //    var mainViewModel = MainViewModel.GetInstance();

        //    var response = await apiService.GetList<ActivityType>(
        //       "http://api.parents.outstandservices.pt",
        //        "/api",
        //        "/ActivitiesTypes",
        //        mainViewModel.Token.TokenType,
        //        mainViewModel.Token.AccessToken);

        //    if (!response.IsSuccess)
        //    {
        //        await dialogService.ShowMessage("Error", connection.Message);
        //        return;
        //    }

        //    activityTypes = (List<ActivityType>)response.Result;

        //    ActType = new ObservableCollection<ActivityType>(activityTypes.OrderBy(c => c.ActivityTypeDescription));

        //}
        #endregion
    }
}
