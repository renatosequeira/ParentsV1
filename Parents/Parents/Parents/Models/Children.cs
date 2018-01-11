namespace Parents.Models
{
    using GalaSoft.MvvmLight.Command;
    using Parents.Services;
    using Parents.ViewModels;
    using Parents.ViewModels.Activities;
    using Parents.ViewModels.Activities.HelperPages;
    using Parents.ViewModels.Activities.Helpers.ActivityType;
    using Parents.ViewModels.Childrens;
    using Parents.Views.Activities;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

    public class Children
    {
        #region Properties
 
        public int ChildrenId { get; set; }
        public int ParentId { get; set; }
        public int BoodInformationId { get; set; }
        public int MatrimonialStateId { get; set; }
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

        public List<Activity> Activities { get; set; }

        public string ChildrenImageFullPath
        {

            get
            {
                if (string.IsNullOrEmpty(ChildrenImage))
                {
                    return "no_image";
                    //return null;
                }

                return string.Format(
                    "http://parents.outstandservices.pt/{0}",
                    ChildrenImage.Substring(1));
            }

        }
        #endregion

        #region Services
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Constructors
        public Children()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
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
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return ChildrenId;
        }
        #endregion

    }
}
