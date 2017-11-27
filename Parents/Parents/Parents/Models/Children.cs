namespace Parents.Models
{
    using GalaSoft.MvvmLight.Command;
    using Parents.ViewModels;
    using Parents.Views.Childrens;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

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
        public object ChildrenFamilyDoctor { get; set; }
        public object ChildrenEmail { get; set; }
        public object ChildrenMobile { get; set; }
        public object ChildrenAddress { get; set; }
        public object CurrentSchool { get; set; }
        public object SchoolContact { get; set; }
        public object FirstParentId { get; set; }
        public object SecondParentId { get; set; }
        public object BloodInformationDescription { get; set; }
        public string ChildrenImage { get; set; }
        public string ChildrenSex { get; set; }

        public string ChildrenImageFullPath
        {
            get
            {
                return string.Format("http://parents.outstandservices.pt/Content/Images/{0}", ChildrenImage.Substring(1));
            }

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

            await Application.Current.MainPage.Navigation.PushAsync(new ChildrenDetails());
        }
        #endregion
    }
}
