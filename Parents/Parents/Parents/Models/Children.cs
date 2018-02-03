namespace Parents.Models
{
    using GalaSoft.MvvmLight.Command;
    using Parents.Services;
    using Parents.ViewModels;
    using Parents.ViewModels.Activities;
    using Parents.ViewModels.Childrens;
    using SQLite;
    using SQLite.Net.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class Children
    {
        #region Attributtes
 
        [PrimaryKey]
        public int ChildrenId { get; set; }
        //public int BoodInformationId { get; set; }
        //public int MatrimonialStateId { get; set; }
        public string ChildrenFirstName { get; set; }
        public string ChildrenMiddleName { get; set; }
        public string ChildrenLastName { get; set; }
        public string ChildrenIdentityCard { get; set; }
        public DateTime ChildrenBirthDate { get; set; }
        public string ChildrenFamilyDoctor { get; set; }
        public string ChildrenEmail { get; set; }
        public string ChildrenMobile { get; set; }
        public string ChildrenAddress { get; set; }
        public string CurrentSchool { get; set; }
        public string SchoolContact { get; set; }
        public string FirstParentId { get; set; }
        public string SecondParentId { get; set; }
        public string BloodInformationDescription { get; set; }
        public string ChildrenImage { get; set; }
        public string ChildrenSex { get; set; }
        public byte[] ImageArray { get; set; }

        public bool PendingToSave { get; set; }
        public bool ChildWithHealthIssues { get; set; }
        public bool IsMale { get; set; }

        //public List<ActivityParents> Activities { get; set; }


        #endregion

        #region Services
        NavigationService navigationService;
        DialogService dialogService;
        ApiService apiService;
        DataService dataService;
        #endregion

        #region Properties
        
        public string ChildrenGenderImage
        {
            get
            {
                string image = "";

                if (IsMale)
                {
                    image = "ic_boy_80_gray_outline";
                }
                else
                {
                    image = "ic_girl_80_gray_outline";
                }
                
                return image;
            }
        }

        public string HealthStatusImage
        {

            get
            {
                string image = "ic_heart_80_green_outline";

                if (ChildWithHealthIssues)
                {
                    image = "ic_heart_80_red_outline";
                   
                }
                else
                {
                    image = "ic_heart_80_green_outline";
                }

                return image;
            }

        }

        public string ChildrenImageFullPath
        {

            get
            {
                if (string.IsNullOrEmpty(ChildrenImage))
                {
                    //return "no_image";
                    if (IsMale)
                    {
                        return "boy_avatar";
                    }
                    else
                    {
                        return "girl_avatar";
                    }
                }

                return string.Format(
                    "http://api.parents.outstandservices.pt/{0}",
                    ChildrenImage.Substring(1));
            }

        }

        public string ChildrenAge
        {
            
            get
            {
                DateTime birth = ChildrenBirthDate;
                DateTime today = DateTime.Today;
                var age = today.Year - birth.Year;
                if (birth > today.AddYears(-age)) age--;
                return age.ToString();
            }

        }

        public string ChildFullName
        {
            get
            {
                string fullName = string.Format("{0} {1}", ChildrenFirstName, ChildrenLastName);
                return fullName.ToUpper();
            }

        }
        #endregion

        #region Constructors
        public Children()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            apiService = new ApiService();
            dataService = new DataService();

            ChildWithHealthIssues = false;
        }
        #endregion

        #region Commands
        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(DeleteChildren);
            }
        }

        async void DeleteChildren()
        {
            var response = await dialogService.ShowConfirm("Confirm", "Are you sure to delete this record?");

            if (!response)
            {
                return;
            }

            await ChildrensViewModel.GetInstance().DeleteChildren(this);
        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(EditChildren);
            }
        }

        async void EditChildren()
        {
            MainViewModel.GetInstance().EditChildren = new EditChildrenViewModel(this);
            await navigationService.NavigateOnMaster("ChildrenDetails");
        }

        public ICommand SelectChildrenCommand {
            get
            {
                return new RelayCommand(SelectChildren);
            }
        }

        async void SelectChildren()
        {
            Application.Current.Properties["childrenIdentityCard"] = ChildrenIdentityCard;
            Application.Current.Properties["childrenId"] = ChildrenId;
            MessagingCenter.Send(this, "childrenName", ChildrenFirstName);

            var mainViewModel = MainViewModel.GetInstance();

            mainViewModel.Activities = new ActivitiesViewModel();


            mainViewModel.EditChildren = new EditChildrenViewModel(this);
            await navigationService.NavigateOnMaster("ChildrenDetails");

        }

        public ICommand AddActivityCommand
        {
            get
            {
                return new RelayCommand(AddActivity);
            }
        }

        async void AddActivity()
        {
            Application.Current.Properties["childrenIdentityCard"] = ChildrenIdentityCard;
            Application.Current.Properties["childrenId"] = ChildrenId;
            MessagingCenter.Send(this, "childrenName", ChildrenFirstName);

            var mainViewModel = MainViewModel.GetInstance();

            mainViewModel.NewActivity = new NewActivityViewModel();

            await navigationService.NavigateOnMaster("AddAnniversaryActivity");

        }

        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return ChildrenId;
        }

       
        #endregion

    }
}
