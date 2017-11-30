namespace Parents.Models
{
    using GalaSoft.MvvmLight.Command;
    using Parents.Services;
    using Parents.ViewModels;
    using Parents.ViewModels.Childrens;
    using System;
    using System.Windows.Input;

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
        #endregion

        #region Constructors
        public Children()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand SelectChildrenCommand {
            get
            {
                return new RelayCommand(SelectChildren);
            }
        }

        async void SelectChildren()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Childrens = new ChildrensViewModel();
            await navigationService.Navigate("ChildrenDetails");
        }
        #endregion

       // #region Methods
       //public override int GetHashCode()
       //  {
       //     return ChildrenId;
       //}
    //#endregion

    }
}
