using GalaSoft.MvvmLight.Command;
using Parents.Services;
using Parents.ViewModels;
using Parents.ViewModels.Activities.Helpers.ActivityType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Parents.Models.ActivitiesManagement.Helpers
{
    public class ActivityType
    {
        public int ActivityTypeId { get; set; }

        public string ActivityTypeDescription { get; set; }

        public string userId { get; set; }

        public bool ActivityTypePrivacy { get; set; }

        public string TypeAlternateDescription
        {

            get
            {
                if (ActivityTypePrivacy)
                {
                    return "Private";
                }
                else
                {
                    return "Public";
                }

            }

        }

        #region Services
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Constructors
        public ActivityType()
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
                return new RelayCommand(DeleteActivityType);
            }
        }

        async void DeleteActivityType()
        {
            var response = await dialogService.ShowConfirm("Confirm", "Are you sure to delete this record?");

            if (!response)
            {
                return;
            }

            await ActivityTypeViewModel.GetInstance().DeleteActivityType(this);
        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(EditActivityType);
            }
        }

        async void EditActivityType()
        {
    
            if (!ActivityTypePrivacy)
            {
                await dialogService.ShowMessage("Error", "Registry is public and can't be edited");
                return;
            }

            MainViewModel.GetInstance().EditActivityType = new EditActivityTypeViewModel(this);
            await navigationService.NavigateOnMaster("EditActivityType");

        }

        public ICommand SelectActivityType
        {
            get
            {
                return new RelayCommand(SelectActivityTypeItem);
            }
        }

        async void SelectActivityTypeItem()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditActivityType = new EditActivityTypeViewModel(this);
            await navigationService.NavigateOnMaster("ActivityTypeDetails");

        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return ActivityTypeId;
        }
        #endregion
    }
}
