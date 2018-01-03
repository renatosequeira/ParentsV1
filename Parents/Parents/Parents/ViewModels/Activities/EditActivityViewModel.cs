namespace Parents.ViewModels.Activities
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Parents.Helpers;
    using Parents.ViewModels.Activities.HelperPages;
    using Parents.Views.Activities.HelpersPages;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Services;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Renderers;

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

        ImageSource _imageSource;
        MediaFile file;

        string _activityTypeImage;
        string _statusImage;
        string _priorityImage;
        string _privacyImage;

        #endregion

        #region Properties
        public string ActivityPriorityImage
        {
            get
            {
                return _priorityImage;
            }
            set
            {
                if (_priorityImage != value)
                {
                    _priorityImage = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityPriorityImage)));
                }
            }
        }

        public string ActivityStatusImage
        {
            get
            {
                return _statusImage;
            }
            set
            {
                if (_statusImage != value)
                {
                    _statusImage = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityStatusImage)));
                }
            }
        }

        public string ActivityPrivacyImage
        {
            get
            {
                return _privacyImage;
            }
            set
            {
                if (_privacyImage != value)
                {
                    _privacyImage = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityPrivacyImage)));
                }
            }
        }

        public string ActivityTypeImage
        {
            get
            {
                return _activityTypeImage;
            }
            set
            {
                if (_activityTypeImage != value)
                {
                    _activityTypeImage = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityTypeImage)));
                }
            }
        }

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

                    ActivityPrivacyImage = "ic_private";

                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityPrivacy)));
                }
                else
                {
                    ActivityPrivacyImage = "ic_public";
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

                    switch (_priority)
                    {
                        case "LOW":
                            ActivityPriorityImage = "ic_priority_low";
                            break;
                        case "MEDIUM":
                            ActivityPriorityImage = "ic_priority_medium";
                            break;
                        case "HIGH":
                            ActivityPriorityImage = "ic_priority_high";
                            break;
                        case "IMMEDIATE":
                            ActivityPriorityImage = "ic_priority_immediate";
                            break;
                    }

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
                    ActivityStatusImage = "status_completed";
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Status)));
                }
                else
                {
                    ActivityStatusImage = "status_ongoing";
                }
            }
        }

        public string ChildrenActivityType
        {
            get
            {
                switch (_activityType)
                {
                    case "ANNIVERSARY":
                        ActivityTypeImage = "ic_birthday";
                        break;
                    case "EVENT":
                        ActivityTypeImage = "ic_event";
                        break;

                    default:
                        break;
                }
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

            IsEnabled = true;
            IsRunning = false;

            ActivityDescription = _activity.ActivityDescription;
            ActivityDateStart = _activity.ActivityDateStart;
            ActivityDateEnd = _activity.ActivityDateEnd;
            ChildrenActivityType = _activity.ChildrenActivityType;
            ActivityPriority = _activity.ActivityPriority;
            ActivityPrivacy = _activity.ActivityPrivacy;
            Status = _activity.Status;
            ImageSource = _activity.ActivityImageFullPath;
        }

        #endregion

        #region Commands
        public ICommand PrivacyChangeCommand
        {
            get
            {
                return new RelayCommand(PrivacyChange);
                
            }
        }

        void PrivacyChange()
        {
            if (ActivityPrivacy) //privado
            { //passa para público
                ActivityPrivacy = false;
                ActivityPrivacyImage = "ic_public";
                //DependencyService.Get<Toast>().Show("Changed");
            }
            else //publico
            { //passa para privado
                ActivityPrivacy = true;
                ActivityPrivacyImage = "ic_private";
                //DependencyService.Get<Toast>().Show("Changed 1");
            }
        }

        public ICommand MaximizeActivityPicture
        {
            get
            {
                return new RelayCommand(MaximizePicture);
            }
        }

        async void MaximizePicture()
        {
            //var mainViewModel = MainViewModel.GetInstance();
            //mainViewModel.ActivityImageMaximizedHelper = new ActivityImageMaximizedHelperPageViewModel();

            var _selectedImage = ImageSource;
            Application.Current.Properties["image"] = ImageSource;

            //gravar o endereço da imagem no messaging center
            MessagingCenter.Send(this, "activityImageForMaximization", _selectedImage);

            await navigationService.OpenPopup("maximizedActivityPage");
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

            activity.ActivityDescription = ActivityDescription;
            activity.ActivityDateStart = ActivityDateStart;
            activity.ActivityDateEnd = ActivityDateEnd;
            activity.ChildrenActivityType = ChildrenActivityType;
            activity.ActivityRemarks = ActivityRemarks;
            activity.Status = Status;
            activity.relatedChildrenIdentitiCard = childIdentityCard;
            activity.ImageArray = imageArray;
            activity.ActivityPrivacy = ActivityPrivacy;
            activity.ActivityPriority = ActivityPriority;
            activity.ChildrenId = childId;
            activity.ActivityTimeStart = ActivityTimeStart;
            activity.ActivityTimeEnd = ActivityTimeEnd;
            activity.ActivityRepeat = ActivityRepeat;

            var mainViewModel = MainViewModel.GetInstance();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Put(
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

            var activityViewModel = ActivitiesViewModel.GetInstance();
            activityViewModel.UpdateActivity(activity);

            IsRunning = false;
            IsEnabled = true;

            await navigationService.Back();
        }
        #endregion
    }
}
