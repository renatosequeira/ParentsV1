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
    using Plugin.Toasts;
    using Rg.Plugins.Popup.Extensions;
    using Plugin.Share.Abstractions;
    using Plugin.Share;

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

        bool _editEnabled;
        #endregion

        #region Properties
        public bool EditEnabled
        {
            get
            {
                return _editEnabled;
            }
            set
            {
                if (_editEnabled != value)
                {
                    _editEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(EditEnabled)));
                }
            }
        }

        public string ActivityPriorityImage
        {
            get
            {
                try
                {
                    string ap = Application.Current.Properties["activityPriorityProperty"] as string;
                    ActivityPriority = ap.ToUpper();
                }
                catch (Exception)
                {
                    ActivityPriority = ActivityPriority;
                }
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
                try
                {
                    string ac = Application.Current.Properties["activityTypeProperty"] as string;
                    ChildrenActivityType = ac.ToUpper();
                }
                catch (Exception)
                {
                    ChildrenActivityType = ChildrenActivityType;
                }
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

                    case "SCHOOL":
                        ActivityTypeImage = "ic_school_brown";
                        break;

                    case "WORKGROUP":
                        ActivityTypeImage = "ic_school_brown";
                        break;

                    case "STUDY TRIPS":
                        ActivityTypeImage = "ic_school_brown";
                        break;

                    case "PARENTS MEETING":
                        ActivityTypeImage = "ic_school_brown";
                        break;

                    case "SPORTS":
                        ActivityTypeImage = "ic_soccer_brown";
                        break;

                    case "OTHERS":
                        ActivityTypeImage = "ic_question";
                        break;

                    default:
                        ActivityTypeImage = "ic_question";
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

            ActivityDescription = _activity.ActivityDescription;
            ActivityDateStart = _activity.ActivityDateStart;
            ActivityDateEnd = _activity.ActivityDateEnd;
            ChildrenActivityType = _activity.ChildrenActivityType;
            ActivityPriority = _activity.ActivityPriority;
            ActivityPrivacy = _activity.ActivityPrivacy;
            Status = _activity.Status;
            ImageSource = _activity.ActivityImageFullPath;

            IsEnabled = true;
            IsRunning = false;

            EditEnabled = false;
        }

        #endregion

        #region Commands
        public ICommand DeleteImageCommand
        {
            get
            {
                return new RelayCommand(DeleteImage);

            }
        }

        async void DeleteImage()
        {
            var response = await dialogService.ShowConfirm("Delete Image", "Are you sure to delete image?");

            if (!response)
            {
                return;
            }

            ImageSource = "no_image";
        }

        public ICommand ShareCommand
        {
            get
            {
                return new RelayCommand(Share);

            }
        }

        async void Share()
        {
            ShareMessage msg = new ShareMessage();
            msg.Title = String.Format("Share {0}", ActivityDescription);
            msg.Text = String.Format("{0}\n\nStart: {1:MM/dd/yyyy}\nFinish: {2:MM/dd/yyyy}", ActivityDescription, ActivityDateStart, ActivityDateEnd);

            var opt = new ShareOptions();

            await CrossShare.Current.Share(msg, opt);
        }

        public ICommand EditItemsCommand
        {
            get
            {
                return new RelayCommand(EditItems);

            }
        }

        async void EditItems()
        {
            if (EditEnabled)
            {
                EditEnabled = false;

            }
            else
            {
                EditEnabled = true;
            }



        }


        public ICommand StatusChangeCommand
        {
            get
            {
                return new RelayCommand(StatusChange);

            }
        }

        async void StatusChange()
        {
            if (Status) //completed
            { //passa para público
                Status = false;
                ActivityStatusImage = "status_ongoing";

            }
            else //opened
            { //passa para privado
                Status = true;
                ActivityStatusImage = "status_completed";
            }
        }

        public ICommand ActivityTypeChangeCommand
        {
            get
            {
                return new RelayCommand(TypeChange);
            }
        }

        async void TypeChange()
        {
            await navigationService.Navigate("ActivityTypeHelperPage");
        }

        public ICommand PriorityChangeCommand
        {
            get
            {
                return new RelayCommand(PriorityChange);
            }
        }

        async void PriorityChange()
        {
            await navigationService.Navigate("ActivityPriorityHelperPage");
        }

        public ICommand PrivacyChangeCommand
        {
            get
            {
                return new RelayCommand(PrivacyChange);

            }
        }

        async void PrivacyChange()
        {
            if (ActivityPrivacy) //privado
            { //passa para público
                ActivityPrivacy = false;
                ActivityPrivacyImage = "ic_public";

                await Application.Current.MainPage.Navigation.PushPopupAsync(new PrivacyChangedHelperPopupPage());

                //var notificator = DependencyService.Get<IToastNotificator>();


                //var options = new NotificationOptions()
                //{
                //    Title = "Privacy changed",
                //    Description = String.Format("Privacy changed to {0}", PrivacyName(ActivityPrivacy)),

                //};

                //var result = await notificator.Notify(options);
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
            //MessagingCenter.Send(this, "activityImageForMaximization", _selectedImage);

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
            //activity.ChildrenActivityType = CheckNewActivityTypeByImage(ActivityTypeImage);
            activity.ChildrenActivityType = ChildrenActivityType;
            activity.ActivityRemarks = ActivityRemarks;
            activity.Status = Status;
            activity.relatedChildrenIdentitiCard = childIdentityCard;
            activity.ImageArray = imageArray;
            activity.ActivityPrivacy = ActivityPrivacy;
            activity.ActivityPriority = CheckNewActivityPriority(ActivityPriorityImage);
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

            await activityViewModel.ReloadActivities();
            await activityViewModel.ReloadAnniversaries();
            await activityViewModel.ReloadEvents();
            await activityViewModel.ReloadSchool();

            IsRunning = false;
            IsEnabled = true;

            ResetKeys();
            await navigationService.Back();
        }


        #endregion

        #region Methods
        private string PrivacyName(bool activityPrivacy)
        {
            string name;

            if (activityPrivacy)
            {
                name = "Private";
            }
            else
            {
                name = "Public";
            }

            return name;
        }

        private string CheckNewActivityPriority(string activityPriorityImage)
        {
            string result = "";

            switch (activityPriorityImage)
            {
                case "ic_priority_low":
                    result = "LOW";
                    break;

                case "ic_priority_medium":
                    result = "MEDIUM";
                    break;

                case "ic_priority_high":
                    result = "HIGH";
                    break;

                case "ic_priority_immediate":
                    result = "IMMEDIATE";
                    break;
            }

            return result;
        }

        private string CheckNewActivityTypeByImage(string activityTypeImage)
        {
            string result = "";

            switch (activityTypeImage)
            {
                case "ic_birthday":
                    result = "ANNIVERSARY";
                    break;

                case "ic_event":
                    result = "EVENT";
                    break;

                case "ic_school_brown":
                    result = "SCHOOL";
                    break;

                    //case "ic_school_brown":
                    //    result = "Workgroup";
                    //    break;

                    //case "ic_school_brown":
                    //    result = "Study Trips";
                    //    break;

                    //case "Parents Meeting":
                    //    imgActivityType.Source = "ic_school_brown";
                    //    break;

                    //case "Sports":
                    //    imgActivityType.Source = "ic_soccer_brown";
                    //    break;

                    //case "Others":
                    //    imgActivityType.Source = "ic_question";
                    //    break;

            }

            return result;
        }

        void ResetKeys()
        {
            Application.Current.Properties["activityTypeProperty"] = null;
            Application.Current.Properties["activityPriorityProperty"] = null;
            GC.Collect();
        }
        #endregion
    }
}
