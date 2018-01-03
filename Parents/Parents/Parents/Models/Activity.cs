using System;
using Parents.ViewModels.Activities;
using System.Globalization;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Parents.Services;
using Parents.ViewModels;
using Parents.Resources;
using Parents.ViewModels.Activities.HelperPages;

namespace Parents.Models
{
    public class Activity
    {
        #region Services
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Properties
        public int ActivityId { get; set; }
        public int ChildrenId { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime ActivityDateStart { get; set; }
        public DateTime ActivityDateEnd { get; set; }
        public string ActivityRemarks { get; set; }
        public string ActivityAddress { get; set; }
        public string Image { get; set; }
        public string userId { get; set; }
        public bool ActivityPrivacy { get; set; }
        public string relatedChildrenIdentitiCard { get; set; }
        public string invitedUserId { get; set; }
        public bool invitationAcknowledged { get; set; }
        public bool Status { get; set; }
        public string ChildrenActivityType { get; set; }
        public string ChildrenActivityFamily { get; set; }
        public string ActivityPriority { get; set; }
        public byte[] ImageArray { get; set; }

        public TimeSpan ActivityTimeStart { get; set; }
        public TimeSpan ActivityTimeEnd { get; set; }

        public string ActivityRepeat { get; set; }
        public string EventId { get; set; }

        public string ActivityImageFullPath
        {

            get
            {
                if (string.IsNullOrEmpty(Image))
                {
                    return "no_image";
                    //return null;
                }

                return string.Format(
                    "http://api.parents.outstandservices.pt/{0}",
                    Image.Substring(1));
            }

        }

        public string convertedDate
        {
            get
            {
                string LimitDateResult = "";

                DateTime today = DateTime.Now;
                DateTime limit = ActivityDateEnd;

                var limitHours = limit;
                var todayHours = today;

                TimeSpan timeDif = limitHours- todayHours;

                int contadorDias = timeDif.Days;
                int contadorHoras = timeDif.Hours; //diferença de horas até ao fim do dia

               
                if (contadorDias >= 0 && contadorDias <=1 && contadorHoras > 0 && contadorHoras < 24)
                {
                    LimitDateResult = "Tomorrow";
                }

                if (contadorDias < 0 && contadorHoras < 0)
                {
                    LimitDateResult = "Overdue";
                }

                if (contadorDias == 0)
                {
                    if(today.Day == limit.Day)
                    {
                        LimitDateResult = "Today";
                    }

                }

                if (contadorDias > 2)
                {
                    LimitDateResult = ActivityDateEnd.ToString("dd-MMMM-yyyy", CultureInfo.InvariantCulture);
                }

                return LimitDateResult;
            }
        }

        public string activityTypeImage
        {
            get
            {
                string type = ChildrenActivityType;
                string actImage = "";

                switch (type)
                {
                    case "Sports":
                        actImage = "ic_soccer_stadium_brown";
                        break;

                    case "Anniversary":
                        actImage = "anniversary_orange";
                        break;

                    case "Study Trips":
                        actImage = "settings_school";
                        break;

                    case "Workgroup":
                        actImage = "settings_school";
                        break;

                    case "Events":
                        actImage = "event_orange";
                        break;

                    case "Parents Meeting":
                        actImage = "settings_school";
                        break;

                    default:
                        break;
                }
                return actImage;
            }
        }

        public string activityIlustrativeImage
        {
            get
            {
                string type = ChildrenActivityType;
                string actImage = "";

                switch (type)
                {
                    case "Sports":
                        actImage = "activity_sports";
                        break;

                    case "Anniversary":
                        actImage = "anniversary_baloons";
                        break;

                    case "Study Trips":
                        actImage = "activity_castle";
                        break;

                    case "Workgroup":
                        actImage = "activity_castle";
                        break;

                    case "Events":
                        actImage = "activity_events";
                        break;

                    case "Parents Meeting":
                        actImage = "activity_parents_meeting";
                        break;

                    default:
                        actImage = "no_image";
                        break;
                }
                return actImage;
            }
        }

        public string EndDateColor
        {
            get
            {
                string color = "#666";
                if (convertedDate == "Tomorrow" || convertedDate == "Today")
                {
                    color = "#7DBEA5";
                }else if(convertedDate == "Overdue")
                {
                    color = "#F26C1A";
                }
                else
                {
                    color = "#666";
                }

                return color;
            }
        }
        #endregion

        #region Constructors
        public Activity()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            ActivityDateStart = DateTime.Today;
        }
        #endregion

        #region Commands
        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(DeleteActivity);
            }
        }

        async void DeleteActivity()
        {
            var response = await dialogService.ShowConfirm("Confirm", "Are you sure to delete this record?");

            if (!response)
            {
                return;
            }

            await ActivitiesViewModel.GetInstance().DeleteActivity(this);
        }

        public ICommand SelectActivityCommand
        {
            get
            {
                return new RelayCommand(SelectActivityItem);
            }
        }

        async void SelectActivityItem()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditActivity = new EditActivityViewModel(this);
            mainViewModel.ActivityImageMaximizedHelper = new ActivityImageMaximizedHelperPageViewModel(this);
            await navigationService.Navigate("Activity Details");

        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return ActivityId;
        }
        #endregion
    }
}
