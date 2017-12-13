using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parents.ViewModels.Activities;
using System.Globalization;
using Xamarin.Forms;

namespace Parents.Models
{
    public class Activity
    {
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

                    default:
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
                if (convertedDate == "Tomorrow")
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
    }
}
