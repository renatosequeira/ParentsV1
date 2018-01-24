using GalaSoft.MvvmLight.Command;
using Parents.Helpers;
using Parents.Models;
using Parents.Resources;
using Parents.Services;
using Parents.Views.Activities.HelpersPages;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.ComponentModel;
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
        DataService dataService;
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

        bool _mondaySelected;
        bool _tuesdaySelected;
        bool _wednesdaySelected;
        bool _thursdaySelected;
        bool _fridaySelected;
        bool _saturdaySelected;
        bool _sundaySelected;

        string _mondayImage;
        string _tuesdayImage;
        string _wednesdayImage;
        string _thrusdayImage;
        string _fridayImage;
        string _saturdayImage;
        string _sundayImage;

        string[] _selectedDays;

        ImageSource _imageSource;
        MediaFile file;

        #endregion

        #region Properties

        public bool MondaySelected
        {
            get
            {
                return _mondaySelected;
            }
            set
            {
                if (_mondaySelected != value)
                {
                    _mondaySelected = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(MondaySelected)));
                }
            }
        }

        public string MondayImage
        {
            get
            {
                return _mondayImage;
            }
            set
            {
                if (_mondayImage != value)
                {
                    _mondayImage = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(MondayImage)));
                }
            }
        }

        public bool TuesdaySelected
        {
            get
            {
                return _tuesdaySelected;
            }
            set
            {
                if (_tuesdaySelected != value)
                {
                    _tuesdaySelected = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(TuesdaySelected)));
                }
            }
        }

        public string TuesdayImage
        {
            get
            {
                return _tuesdayImage;
            }
            set
            {
                if (_tuesdayImage != value)
                {
                    _tuesdayImage = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(TuesdayImage)));
                }
            }
        }

        public bool WednesdaySelected
        {
            get
            {
                return _wednesdaySelected;
            }
            set
            {
                if (_wednesdaySelected != value)
                {
                    _wednesdaySelected = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(WednesdaySelected)));
                }
            }
        }

        public string WednesdayImage
        {
            get
            {
                return _wednesdayImage;
            }
            set
            {
                if (_wednesdayImage != value)
                {
                    _wednesdayImage = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(WednesdayImage)));
                }
            }
        }

        public bool ThursdaySelected
        {
            get
            {
                return _thursdaySelected;
            }
            set
            {
                if (_thursdaySelected != value)
                {
                    _thursdaySelected = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ThursdaySelected)));
                }
            }
        }

        public string ThursdayImage
        {
            get
            {
                return _thrusdayImage;
            }
            set
            {
                if (_thrusdayImage != value)
                {
                    _thrusdayImage = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ThursdayImage)));
                }
            }
        }

        public bool FridaySelected
        {
            get
            {
                return _fridaySelected;
            }
            set
            {
                if (_fridaySelected != value)
                {
                    _fridaySelected = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(FridaySelected)));
                }
            }
        }

        public string FridayImage
        {
            get
            {
                return _fridayImage;
            }
            set
            {
                if (_fridayImage != value)
                {
                    _fridayImage = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(FridayImage)));
                }
            }
        }

        public bool SaturdaySelected
        {
            get
            {
                return _saturdaySelected;
            }
            set
            {
                if (_saturdaySelected != value)
                {
                    _saturdaySelected = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SaturdaySelected)));
                }
            }
        }

        public string SaturdayImage
        {
            get
            {
                return _saturdayImage;
            }
            set
            {
                if (_saturdayImage != value)
                {
                    _saturdayImage = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SaturdayImage)));
                }
            }
        }

        public bool SundaySelected
        {
            get
            {
                return _sundaySelected;
            }
            set
            {
                if (_sundaySelected != value)
                {
                    _sundaySelected = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SundaySelected)));
                }
            }
        }

        public string SundayImage
        {
            get
            {
                return _sundayImage;
            }
            set
            {
                if (_sundayImage != value)
                {
                    _sundayImage = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SundayImage)));
                }
            }
        }

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

        public string GhostActivityRepeat
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
                        new PropertyChangedEventArgs(nameof(GhostActivityRepeat)));
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
            dataService = new DataService();

            IsEnabled = true; //bool are disabled by default. This will enable buttons

            TimeSpan time1 = TimeSpan.FromHours(1);

            ActivityDateStart = DateTime.Now;
            ActivityDateEnd = DateTime.Now;
            ActivityTimeStart = DateTime.Now.TimeOfDay;
            //ActivityTimeEnd = DateTime.Now.TimeOfDay.Add(time1);

            string endHours = ActivityDateStart.Hour.ToString();

            if (endHours == "23")
            {
                int currentMinute = DateTime.Now.Minute;
                int minuteResult = 59 - currentMinute;

                ActivityTimeEnd = TimeSpan.FromMinutes(minuteResult);
                ActivityDateEnd = ActivityDateStart.AddDays(1);
            }
            else
            {
                ActivityTimeEnd = DateTime.Now.TimeOfDay.Add(time1);
            }

            #region DaysOfWeekInicializer
            MondayImage = "ic_monday";
            TuesdayImage = "ic_tuesday";
            WednesdayImage = "ic_wednsday";
            ThursdayImage = "ic_thursday";
            FridayImage = "ic_friday";
            SaturdayImage = "ic_saturday";
            SundayImage = "ic_sunday";
            #endregion

            _selectedDays = new string[7];

            Status = false;

            string r;

            ImageSource = "no_image";

            MessagingCenter.Subscribe<ActivityRepeatHelperPageView, string>(this, "eventRecurrency", (s, a) => {
                r = a.ToString();
                ActivityRepeat = r;
                GhostActivityRepeat = r;
                _selectedDays[0] = r;

            });

            MessagingCenter.Subscribe<ActivityRepeatHelperPageView, string>(this, "eventNumberOfOccurencies", (s, a) => {
                EventRepetitions = a.ToString();
            });

        }
        #endregion

        #region Commands
        public ICommand ValidateWeekDaysCommand
        {
            get
            {
                return new RelayCommand(_validateWeekDays);
            }
        }

        private void _validateWeekDays()
        {
            ActivityRepeat = CheckSelectedDays(_selectedDays);
            GhostActivityRepeat = CheckSelectedDays(_selectedDays);
        }

        public ICommand MondaySelectedCommand
        {
            get
            {
                return new RelayCommand(_MondaySelected);
            }
        }

        private void _MondaySelected()
        {
            if (MondaySelected)
            {
                MondaySelected = false;
                MondayImage = "ic_monday";
                _selectedDays[0] = null;
            }
            else
            {
                MondaySelected = true;
                MondayImage = "ic_monday_selected1";
                _selectedDays[0] = "2ª, ";
            }

        }

        public ICommand TuesdaySelectedCommand
        {
            get
            {
                return new RelayCommand(_TuesdaySelected);
            }
        }

        private void _TuesdaySelected()
        {
            if (TuesdaySelected)
            {
                TuesdaySelected = false;
                TuesdayImage = "ic_tuesday";
                _selectedDays[1] = null;
            }
            else
            {
                TuesdaySelected = true;
                TuesdayImage = "ic_tuesday_selected";
                _selectedDays[1] = "3ª, ";
            }
        }

        public ICommand WednesdaySelectedCommand
        {
            get
            {
                return new RelayCommand(_WednesdaySelected);
            }
        }

        private void _WednesdaySelected()
        {
            if (WednesdaySelected)
            {
                WednesdaySelected = false;
                WednesdayImage = "ic_wednsday";
                _selectedDays[2] = null;
            }
            else
            {
                WednesdaySelected = true;
                WednesdayImage = "ic_wednsday_selected";
                _selectedDays[2] = "4ª, ";
            }
        }

        public ICommand ThursdaySelectedCommand
        {
            get
            {
                return new RelayCommand(_ThursdaySelected);
            }
        }

        private void _ThursdaySelected()
        {
            if (ThursdaySelected)
            {
                ThursdaySelected = false;
                ThursdayImage = "ic_thursday";
                _selectedDays[3] = null;
            }
            else
            {
                ThursdaySelected = true;
                ThursdayImage = "ic_thrusday_selected";
                _selectedDays[3] = "5ª, ";
            }
        }

        public ICommand FridaySelectedCommand
        {
            get
            {
                return new RelayCommand(_FridaySelected);
            }
        }

        private void _FridaySelected()
        {
            if (FridaySelected)
            {
                FridaySelected = false;
                FridayImage = "ic_friday";
                _selectedDays[4] = null;
            }
            else
            {
                FridaySelected = true;
                FridayImage = "ic_friday_selected";
                _selectedDays[4] = "6ª, ";
            }
        }

        public ICommand SaturdaySelectedCommand
        {
            get
            {
                return new RelayCommand(_SaturdayCommand);
            }
        }

        private void _SaturdayCommand()
        {
            if (SaturdaySelected)
            {
                SaturdaySelected = false;
                SaturdayImage = "ic_saturday";
                _selectedDays[5] = null;
            }
            else
            {
                SaturdaySelected = true;
                SaturdayImage = "ic_saturday_selected";
                _selectedDays[5] = "Sab ";
            }
        }

        public ICommand SundaySelectedCommand
        {
            get
            {
                return new RelayCommand(_SundaySelect);
            }
        }

        private void _SundaySelect()
        {
            if (SundaySelected)
            {
                SundaySelected = false;
                SundayImage = "ic_sunday";
                _selectedDays[6] = null;
            }
            else
            {
                SundaySelected = true;
                SundayImage = "ic_sunday_selected";
                _selectedDays[6] = "Dom ";
                //ActivityRepeat = CheckSelectedDays(_selectedDays);
            }
            //CheckSelectedDays(_selectedDays);
        }

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
                    DateTime data = DateTime.Now;
                    TimeSpan hora = data.TimeOfDay;

                    file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            Directory = "Parents",
                            Name = String.Format("Parents_{0:dd/MM/yyyy}_{1}.jpg", data, data.TimeOfDay),
                            PhotoSize = PhotoSize.Small,
                            SaveToAlbum=true                            
                        }
                    );
                }
                else
                {
                    file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                    });
                }
            }
            else
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
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

            byte[] imageArray = null;

            if (file != null)
            {
                imageArray = FilesHelper.ReadFully(file.GetStream());
                file.Dispose();
            }

            var mainViewModel = MainViewModel.GetInstance();

            string childIdCard = Application.Current.Properties["childrenIdentityCard"] as string;
            string childId = Application.Current.Properties["childrenId"] as string;

            var activity = new ActivityParents
            {
                ActivityDescription = ActivityDescription,
                ActivityDateStart = ActivityDateStart,
                ActivityDateEnd = ActivityDateEnd,
                ChildrenActivityType = ChildrenActivityType,
                ActivityRemarks = ActivityRemarks,
                Status = Status,
                relatedChildrenIdentitiCard = childIdCard,
                ImageArray = imageArray,
                ActivityPrivacy = ActivityPrivacy,
                ActivityPriority = ActivityPriority,
                ChildrenId = Convert.ToInt32(childId),
                ActivityTimeStart = ActivityTimeStart,
                ActivityTimeEnd = ActivityTimeEnd
            };

            //verificar se existe ligação à internet
            var connection = await apiService.CheckConnection();

            //se não houver ligação à internet, popup com erro e sai do método
            if (!connection.IsSuccess)
            {
                //await dialogService.ShowMessage("Error", connection.Message);
                //IsRunning = false;
                //IsEnabled = true;
                activity.PendingToSave = true;
                dataService.Insert(activity);
                await dialogService.ShowMessage("Information", "The product was saved locally. Don't forget to upload record when connection is established");
            }
            else
            {
                var urlAPI = Application.Current.Resources["URLAPI"].ToString();

                //se existir ligação à internet guarda token na variavel response
                var response = await apiService.Post(
                    urlAPI,
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

                activity = (ActivityParents)response.Result;
            }

            var activityViewModel = ActivitiesViewModel.GetInstance();
            activityViewModel.Add(activity);

            await navigationService.BackOnMaster();

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
            //ActivityRepeat = CheckSelectedDays(_selectedDays);

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

            int _eventRepetitions = Convert.ToInt32(EventRepetitions);

            bool _selectedMonday = MondaySelected;
            bool _selectedTuesday = TuesdaySelected;
            bool _selectedWednesday = WednesdaySelected;
            bool _selectedThursday = ThursdaySelected;
            bool _selectedFriday = FridaySelected;
            bool _selectedSaturday = SaturdaySelected;
            bool _selectedSunday = SundaySelected;

            string _mondayArray = _selectedDays[0];
            string _tuesdayArray = _selectedDays[1];
            string _wednesdayArray = _selectedDays[2];
            string _thursdayArray = _selectedDays[3];
            string _fridayArray = _selectedDays[4];
            string _saturdayArray = _selectedDays[5];
            string _sundayArray = _selectedDays[6];

            for (int k = 0; k < _selectedDays.Length; k++)
            {
                ActivityRepeat = _selectedDays[k];
                GhostActivityRepeat = _selectedDays[k];

                if (!string.IsNullOrEmpty(_selectedDays[k]))
                //if (true)
                {
                    for (int i = 0; i < _eventRepetitions; i++)
                    {
                        //int repeatIn = (int.Parse(RepeatMultiplicationFactor));

                        IsRunning = true;
                        IsEnabled = false;

                        byte[] imageArray = null;

                        try
                        {
                            if (file != null)
                            {
                                imageArray = FilesHelper.ReadFully(file.GetStream());
                                file.Dispose();
                            }
                        }
                        catch (Exception ex)
                        {
                            //await dialogService.ShowMessage("Images", "Only first event will have associated image. Please add images for recurreng events, manually.");
                        }

                        string childIdentityCard = Application.Current.Properties["childrenIdentityCard"] as string;
                        int childId = Convert.ToInt32(Application.Current.Properties["childrenId"]);

                        DateTime tempDateStart = ActivityDateStart;
                        DateTime tempDateEnd = ActivityDateEnd;
                        string dayOfWeek = ActivityDateStart.DayOfWeek.ToString();

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

                                if (ActivityRepeat.Contains("2ª"))
                                {
                                    if (dayOfWeek == "Monday")
                                    {
                                        tempDateStart = ActivityDateStart;
                                        tempDateEnd = ActivityDateEnd;
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Tuesday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(6);
                                        tempDateEnd = ActivityDateEnd.AddDays(6);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Wednesday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(5);
                                        tempDateEnd = ActivityDateEnd.AddDays(5);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Thursday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(4);
                                        tempDateEnd = ActivityDateEnd.AddDays(4);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Friday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(3);
                                        tempDateEnd = ActivityDateEnd.AddDays(3);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Saturday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(2);
                                        tempDateEnd = ActivityDateEnd.AddDays(2);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Sunday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(1);
                                        tempDateEnd = ActivityDateEnd.AddDays(1);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                }

                                if (ActivityRepeat.Contains("3ª"))
                                {
                                    if (dayOfWeek == "Monday") //o dia da semana correspondente à data que o user seleccionou, é uma segunda
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(1); //a data temporaria, tem é incrementada para corresponder a uma terça-feira
                                        tempDateEnd = ActivityDateEnd.AddDays(1);
                                        tempDateStart = tempDateStart.AddDays(7 * i); //7 dias depois da primeira data inserida, é inderida a segunda, e seguintes (i)
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Tuesday")
                                    {
                                        tempDateStart = ActivityDateStart;
                                        tempDateEnd = ActivityDateEnd;
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Wednesday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(6);
                                        tempDateEnd = ActivityDateEnd.AddDays(6);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Thursday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(5);
                                        tempDateEnd = ActivityDateEnd.AddDays(5);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Friday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(4);
                                        tempDateEnd = ActivityDateEnd.AddDays(4);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Saturday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(3);
                                        tempDateEnd = ActivityDateEnd.AddDays(3);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Sunday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(2);
                                        tempDateEnd = ActivityDateEnd.AddDays(2);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }

                                }

                                if (ActivityRepeat.Contains("4ª"))
                                {
                                    if (dayOfWeek == "Monday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(2); //a data temporaria, tem é incrementada para corresponder a uma terça-feira
                                        tempDateEnd = ActivityDateEnd.AddDays(2);
                                        tempDateStart = tempDateStart.AddDays(7 * i); //7 dias depois da primeira data inserida, é inderida a segunda, e seguintes (i)
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Tuesday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(1);
                                        tempDateEnd = ActivityDateEnd.AddDays(1);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Wednesday")
                                    {
                                        tempDateStart = ActivityDateStart;
                                        tempDateEnd = ActivityDateEnd;
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Thursday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(6);
                                        tempDateEnd = ActivityDateEnd.AddDays(6);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Friday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(5);
                                        tempDateEnd = ActivityDateEnd.AddDays(5);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Saturday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(4);
                                        tempDateEnd = ActivityDateEnd.AddDays(4);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Sunday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(3);
                                        tempDateEnd = ActivityDateEnd.AddDays(3);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                }

                                if (ActivityRepeat.Contains("5ª"))
                                {
                                    if (dayOfWeek == "Monday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(3); //a data temporaria, tem é incrementada para corresponder a uma terça-feira
                                        tempDateEnd = ActivityDateEnd.AddDays(3);
                                        tempDateStart = tempDateStart.AddDays(7 * i); //7 dias depois da primeira data inserida, é inderida a segunda, e seguintes (i)
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Tuesday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(2);
                                        tempDateEnd = ActivityDateEnd.AddDays(2);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Wednesday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(1);
                                        tempDateEnd = ActivityDateEnd.AddDays(1);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Thursday")
                                    {
                                        tempDateStart = ActivityDateStart;
                                        tempDateEnd = ActivityDateEnd;
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Friday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(6);
                                        tempDateEnd = ActivityDateEnd.AddDays(6);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Saturday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(5);
                                        tempDateEnd = ActivityDateEnd.AddDays(5);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Sunday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(4);
                                        tempDateEnd = ActivityDateEnd.AddDays(4);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                }

                                if (ActivityRepeat.Contains("6ª"))
                                {
                                    if (dayOfWeek == "Monday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(4); //a data temporaria, tem é incrementada para corresponder a uma terça-feira
                                        tempDateEnd = ActivityDateEnd.AddDays(4);
                                        tempDateStart = tempDateStart.AddDays(7 * i); //7 dias depois da primeira data inserida, é inderida a segunda, e seguintes (i)
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Tuesday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(3);
                                        tempDateEnd = ActivityDateEnd.AddDays(3);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Wednesday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(2);
                                        tempDateEnd = ActivityDateEnd.AddDays(2);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Thursday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(1);
                                        tempDateEnd = ActivityDateEnd.AddDays(1);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Friday")
                                    {
                                        tempDateStart = ActivityDateStart;
                                        tempDateEnd = ActivityDateEnd;
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Saturday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(6);
                                        tempDateEnd = ActivityDateEnd.AddDays(6);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Sunday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(5);
                                        tempDateEnd = ActivityDateEnd.AddDays(5);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                }

                                if (ActivityRepeat.Contains("Sab"))
                                {
                                    if (dayOfWeek == "Monday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(5); //a data temporaria, tem é incrementada para corresponder a uma terça-feira
                                        tempDateEnd = ActivityDateEnd.AddDays(5);
                                        tempDateStart = tempDateStart.AddDays(7 * i); //7 dias depois da primeira data inserida, é inderida a segunda, e seguintes (i)
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Tuesday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(4);
                                        tempDateEnd = ActivityDateEnd.AddDays(4);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Wednesday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(3);
                                        tempDateEnd = ActivityDateEnd.AddDays(3);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Thursday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(2);
                                        tempDateEnd = ActivityDateEnd.AddDays(2);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Friday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(1);
                                        tempDateEnd = ActivityDateEnd.AddDays(1);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Saturday")
                                    {
                                        tempDateStart = ActivityDateStart;
                                        tempDateEnd = ActivityDateEnd;
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Sunday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(6);
                                        tempDateEnd = ActivityDateEnd.AddDays(6);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                }

                                if (ActivityRepeat.Contains("Dom"))
                                {
                                    if (dayOfWeek == "Monday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(6); //a data temporaria, tem é incrementada para corresponder a uma terça-feira
                                        tempDateEnd = ActivityDateEnd.AddDays(6);
                                        tempDateStart = tempDateStart.AddDays(7 * i); //7 dias depois da primeira data inserida, é inderida a segunda, e seguintes (i)
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Tuesday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(5);
                                        tempDateEnd = ActivityDateEnd.AddDays(5);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Wednesday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(4);
                                        tempDateEnd = ActivityDateEnd.AddDays(4);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Thursday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(3);
                                        tempDateEnd = ActivityDateEnd.AddDays(3);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Friday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(2);
                                        tempDateEnd = ActivityDateEnd.AddDays(2);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Saturday")
                                    {
                                        tempDateStart = ActivityDateStart.AddDays(1);
                                        tempDateEnd = ActivityDateEnd.AddDays(1);
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                    if (dayOfWeek == "Sunday")
                                    {
                                        tempDateStart = ActivityDateStart;
                                        tempDateEnd = ActivityDateEnd;
                                        tempDateStart = tempDateStart.AddDays(7 * i);
                                        tempDateEnd = tempDateEnd.AddDays(7 * i);
                                    }
                                }
                                break;
                        }

                        var activity = new ActivityParents
                        {
                            ActivityDescription = ActivityDescription,
                            ActivityDateStart = tempDateStart,
                            ActivityDateEnd = tempDateEnd,
                            ChildrenActivityType = ChildrenActivityType,
                            ActivityRemarks = ActivityRemarks,
                            Status = Status,
                            relatedChildrenIdentitiCard = childIdentityCard,
                            ImageArray = imageArray,
                            ActivityPrivacy = ActivityPrivacy,
                            ActivityPriority = ActivityPriority,
                            ChildrenId = childId,
                            ActivityTimeStart = ActivityTimeStart,
                            ActivityTimeEnd = ActivityTimeEnd,
                            ActivityRepeat = ActivityRepeat
                        };

                        //verificar se existe ligação à internet
                        var connection = await apiService.CheckConnection();

                        //se não houver ligação à internet, popup com erro e sai do método
                        if (!connection.IsSuccess)
                        {
                            //await dialogService.ShowMessage("Error", connection.Message);

                            //IsRunning = false;
                            //IsEnabled = true;
                            activity.PendingToSave = true;
                            dataService.Insert(activity);
                            await dialogService.ShowMessage("Information", "The product was saved locally. Don't forget to upload record when connection is established");
                        }
                        else
                        {
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

                            activity = (ActivityParents)response.Result;
                        }
                       
                        var activityViewModel = ActivitiesViewModel.GetInstance();
                        activityViewModel.Add(activity);

                        IsRunning = false;
                        IsEnabled = true;

                        }
                    }
                }

            await navigationService.BackOnMaster();
            }

        private string CheckSelectedDays(string[] selectedDays)
        {
            //ver que dias estão seleccionados
            string segunda = selectedDays[0];
            string terca = selectedDays[1];
            string quarta = selectedDays[2];
            string quinta = selectedDays[3];
            string sexta = selectedDays[4];
            string sabado = selectedDays[5];
            string domingo = selectedDays[6];

            //imprimir resultados no ActivityRepeat
            if (string.IsNullOrEmpty(segunda) &&
                string.IsNullOrEmpty(terca) &&
                string.IsNullOrEmpty(quarta) &&
                string.IsNullOrEmpty(quinta) &&
                string.IsNullOrEmpty(sexta) &&
                string.IsNullOrEmpty(sabado) &&
                string.IsNullOrEmpty(domingo))
            {
                return ActivityRepeat;
            }
            else
            {
               
                return ActivityRepeat = String.Format("{0} {1} {2} {3} {4} {5} {6}",
                    segunda, terca, quarta, quinta, sexta, sabado, domingo);
            }

        }

        static bool IsNullOrEmpty(string[] myStringArray)
        {
            return myStringArray == null || myStringArray.Length < 1;
        }


        #endregion

        #region Methods
      
        #endregion
    }
    }

