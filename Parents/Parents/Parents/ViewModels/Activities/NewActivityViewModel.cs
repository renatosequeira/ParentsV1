using GalaSoft.MvvmLight.Command;
using Parents.Helpers;
using Parents.Models;
using Parents.Models.ActivitiesManagement.Helpers;
using Parents.Services;
using Parents.Views.Activities.HelpersPages;
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
        string _activityDescription;
        string _activityRemarks;
        DateTime _activityDateStart;
        DateTime _activityDateEnd;
        TimeSpan _activityTimeStart;
        TimeSpan _activityTimeEnd;
        bool _isRunning;
        bool _isEnabled;
        bool _status;
        string _activityType;
        string _priority;
        bool _activityPrivacy;
        bool _allDay;

        string _repeat;
        int _repeatMultiplicationFactor;
        string _eventRepetitions;

        ImageSource _imageSource;
        MediaFile file;

        #endregion

        #region Properties
        public int RepeatMultiplicationFactor
        {
            get
            {
                return _repeatMultiplicationFactor;
            }
            set
            {
                if (_repeatMultiplicationFactor != value)
                {
                    _repeatMultiplicationFactor = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(RepeatMultiplicationFactor)));
                }
            }
        }

        public string EventRepetitions
        {
            get
            {
                return _eventRepetitions;
            }
            set
            {
                if (_eventRepetitions != value)
                {
                    _eventRepetitions = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(EventRepetitions)));
                }
            }
        }

        public string ActivityRepeat
        {
            get
            {
                return _repeat;
            }
            set
            {
                if (_repeat != value)
                {
                    _repeat = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityRepeat)));
                }
            }
        }

        public bool allDay
        {
            get
            {
                return _allDay;
            }
            set
            {
                if (_allDay != value)
                {
                    _allDay = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(allDay)));
                }
            }
        }

        public TimeSpan ActivityTimeStart
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

        public TimeSpan ActivityTimeEnd
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

        public bool ActivityPrivacy
        {
            get
            {
                return _activityPrivacy;
            }
            set
            {
                if (_activityPrivacy != value)
                {
                    _activityPrivacy = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityPrivacy)));
                }
            }
        }

        public string ActivityPriority
        {
            get
            {
                return _priority;
            }
            set
            {
                if (_priority != value)
                {
                    _priority = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityPriority)));
                }
            }
        }

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

        public string ChildrenActivityType
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
                        new PropertyChangedEventArgs(nameof(ChildrenActivityType)));
                }
            }
        }

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

            TimeSpan time1 = TimeSpan.FromHours(1);

            ActivityDateStart = DateTime.Now;
            ActivityDateEnd = DateTime.Now;
            ActivityTimeStart = DateTime.Now.TimeOfDay;
            ActivityTimeEnd = DateTime.Now.TimeOfDay.Add(time1);


            Status = false;

            string r;

            ImageSource = "no_image";

            MessagingCenter.Subscribe<ActivityRepeatHelperPageView, string>(this, "eventRecurrency", (s, a) => {
                //ActivityRepeat = a.ToString();
                r = a.ToString();
                ActivityRepeat = r;
            });

            MessagingCenter.Subscribe<ActivityRepeatHelperPageView, string>(this, "eventNumberOfOccurencies", (s, a) => {
                EventRepetitions = a.ToString();
            });
        }
        #endregion

        #region Commands
        //public ICommand CalculateRepeatCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(CalculateRepeatedEvents);
        //    }
        //}

        //private void CalculateRepeatedEvents()
        //{
        //    switch (ActivityRepeat)
        //    {
        //        case "Once":
        //            EventRepetitions = "1";
        //            RepeatMultiplicationFactor = 0; //numero de dias na semana
        //            break;

        //        case "Daily":
        //            //EventRepetitions = 5;
        //            RepeatMultiplicationFactor = 1; //numero de dias na semana

        //            break;
        //        case "Weekly":
        //            //EventRepetitions = 5;
        //            RepeatMultiplicationFactor = 7; //numero de dias na semana


        //            break;
        //        default:
        //            RepeatMultiplicationFactor = 0;
        //            break;
        //    }

        //}

        public ICommand switchAllDayCommand
        {
            get
            {
                return new RelayCommand(SwitchToAllDay);
            }
        }

        private void SwitchToAllDay()
        {
            TimeSpan startTime = ActivityTimeStart;
            TimeSpan endTime = ActivityTimeEnd;

            if (allDay)
            {
                ActivityTimeStart = TimeSpan.Parse("00:00");
                ActivityTimeEnd = TimeSpan.Parse("23:59");
            }

        }

        public ICommand ClearStartDateAndTimeCommand
        {
            get
            {
                return new RelayCommand(ClearStartDate);
            }
        }

        private void ClearStartDate()
        {
            ActivityDateStart = DateTime.Now;
        }

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

            if (string.IsNullOrEmpty(ChildrenActivityType) || ChildrenActivityType == "SELECT ACTIVITY TYPE...")
            {
                await dialogService.ShowMessage("Error", "Please select a valid actvity type");
                return;
            }

            if (string.IsNullOrEmpty(ActivityPriority) || ActivityPriority == "SELECT ACTIVITY PRIORITY...")
            {
                await dialogService.ShowMessage("Error", "Please select a valid actvity priority");
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

            byte[] imageArray = null;

            if (file != null)
            {
                imageArray = FilesHelper.ReadFully(file.GetStream());
                file.Dispose();
            }

            string childId = Application.Current.Properties["childrenIdentityCard"] as string;

            var activity = new Activity
            {
                ActivityDescription = ActivityDescription,
                ActivityDateStart = ActivityDateStart,
                ActivityDateEnd = ActivityDateEnd,
                ChildrenActivityType = ChildrenActivityType,
                ActivityRemarks = ActivityRemarks,
                Status = Status,
                relatedChildrenIdentitiCard = childId,
                ImageArray = imageArray,
                ActivityPrivacy = ActivityPrivacy,
                ActivityPriority = ActivityPriority,
                ChildrenId = 2,
                ActivityTimeStart = ActivityTimeStart,
                ActivityTimeEnd = ActivityTimeEnd
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

        public ICommand SaveRepeatedEventsCommand
        {
            get
            {
                return new RelayCommand(SaveRepeatedEvents);
            }
        }

        async void SaveRepeatedEvents()
        {
            if (string.IsNullOrEmpty(ActivityDescription))
            {
                await dialogService.ShowMessage("Error", "Please insert Activity description");
                return;
            }

            if (string.IsNullOrEmpty(ChildrenActivityType) || ChildrenActivityType == "SELECT ACTIVITY TYPE...")
            {
                await dialogService.ShowMessage("Error", "Please select a valid actvity type");
                return;
            }

            if (string.IsNullOrEmpty(ActivityPriority) || ActivityPriority == "SELECT ACTIVITY PRIORITY...")
            {
                await dialogService.ShowMessage("Error", "Please select a valid actvity priority");
                return;
            }

            //CalculateRepeatedEvents();
            int _eventRepetitions = Convert.ToInt32(EventRepetitions);

            for (int i = 0; i < _eventRepetitions; i++)
            {
                //int repeatIn = (int.Parse(RepeatMultiplicationFactor));

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

                string childId = Application.Current.Properties["childrenIdentityCard"] as string;

                DateTime tempDateStart = ActivityDateStart;
                DateTime tempDateEnd = ActivityDateEnd;

                switch (ActivityRepeat)
                {
                    case "Once":
                        tempDateStart = ActivityDateStart;
                        tempDateEnd = ActivityDateEnd;
                        break;
                    case "Daily":
                        tempDateStart = ActivityDateStart.AddDays(1 * i);
                        tempDateEnd = ActivityDateEnd.AddDays(1 * i);
                        break;
                    case "Weekly":
                        tempDateStart = ActivityDateStart.AddDays(7 * i);
                        tempDateEnd = ActivityDateEnd.AddDays(7 * i);
                        break;
                    case "Monthly":
                        tempDateStart = ActivityDateStart.AddMonths(1 * i);
                        tempDateEnd = ActivityDateEnd.AddMonths(1 * i);
                        break;

                    case "Yearly":
                        tempDateStart = ActivityDateStart.AddYears(1 * i);
                        tempDateEnd = ActivityDateEnd.AddYears(1 * i);
                        break;
                    default:
                        break;
                }

                var activity = new Activity
                {
                    ActivityDescription = ActivityDescription,
                    ActivityDateStart = tempDateStart,
                    ActivityDateEnd = tempDateEnd,
                    ChildrenActivityType = ChildrenActivityType,
                    ActivityRemarks = ActivityRemarks,
                    Status = Status,
                    relatedChildrenIdentitiCard = childId,
                    ImageArray = imageArray,
                    ActivityPrivacy = ActivityPrivacy,
                    ActivityPriority = ActivityPriority,
                    ChildrenId = 2,
                    ActivityTimeStart = ActivityTimeStart,
                    ActivityTimeEnd = ActivityTimeEnd,
                    ActivityRepeat = ActivityRepeat
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

                //await navigationService.Back();

                IsRunning = false;
                IsEnabled = true;

            }

            await navigationService.Back();
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
