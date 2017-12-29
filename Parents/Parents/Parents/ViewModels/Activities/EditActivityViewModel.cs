namespace Parents.ViewModels.Activities
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Parents.Helpers;
    using Plugin.Media.Abstractions;
    using Services;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EditActivityViewModel : INotifyPropertyChanged
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
        Activity activity;

        string _activityDescription;
        string _activityRemarks;
        DateTime _activityDateStart;
        DateTime _activityDateEnd;
        TimeSpan _activityTimeStart;
        TimeSpan _activityTimeEnd;

        bool _isRunning;
        bool _isEnabled;
        bool _isVisible;

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
        public EditActivityViewModel(Activity _activity)
        {
            this.activity = _activity;
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            ActivityDescription = _activity.ActivityDescription;
            ActivityDateStart = _activity.ActivityDateStart;
            ActivityDateEnd = _activity.ActivityDateEnd;
            ChildrenActivityType = _activity.ChildrenActivityType;
            ActivityPriority = _activity.ActivityPriority;
            ActivityPrivacy = _activity.ActivityPrivacy;
            Status = _activity.Status;
        }
      
        #endregion

        #region Commands
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

                        var activity = new Activity
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
                }
            }

            await navigationService.Back();
        }
        #endregion
    }
}
