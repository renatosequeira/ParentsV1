using GalaSoft.MvvmLight.Command;
using Parents.Services;
using Parents.ViewModels;
using Parents.ViewModels.Activities;
using Parents.ViewModels.Activities.Helpers;
using SQLite;
using SQLite.Net.Attributes;
using System.Windows.Input;

namespace Parents.Models.ActivitiesManagement.Helpers
{
    public class ActivityFamily
    {
        #region Properties
        [PrimaryKey]
        public int ActivityFamilyId { get; set; }

        public string ActivityFamilyDescription { get; set; }

        public string ActivityFamilyDescriptionPrivate { get; set; }

        public string userId { get; set; }

        public bool Privacy { get; set; }

        public string FamilyItemColor
        {
            get
            {
                if (Privacy)
                {
                    return "Blue";
                }
                else
                {
                    return "White";
                }
            }
        }
        #endregion

        #region Services
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Constructors
        public ActivityFamily()
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
                return new RelayCommand(DeleteActivityFamily);
            }
        }

        async void DeleteActivityFamily()
        {
            var response = await dialogService.ShowConfirm("Confirm", "Are you sure to delete this record?");

            if (!response)
            {
                return;
            }

            await ActivityFamilyViewModel.GetInstance().DeleteActivityFamily(this);
        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(EditActivityFamily);
            }
        }

        async void EditActivityFamily()
        {
            MainViewModel.GetInstance().EditActivityFamily = new EditActivityFamilyViewModel(this);
            await navigationService.NavigateOnMaster("EditActivityFamilyViewModel");

        }

        public ICommand SelectActivityFamily
        {
            get
            {
                return new RelayCommand(SelectActivityFamilyItem);
            }
        }

        async void SelectActivityFamilyItem()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditActivityFamily = new EditActivityFamilyViewModel(this);
            await navigationService.NavigateOnMaster("Activities Family Details");

        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return ActivityFamilyId;
        }
        #endregion
    }
}
