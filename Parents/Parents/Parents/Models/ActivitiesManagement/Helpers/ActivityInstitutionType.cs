using GalaSoft.MvvmLight.Command;
using Parents.Services;
using Parents.ViewModels;
using Parents.ViewModels.Activities.Helpers.ActivitiesInstitutionType;
using SQLite;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Parents.Models.ActivitiesManagement.Helpers
{
    public class ActivityInstitutionType
    {
        #region Properties
        [PrimaryKey]
        public int ActivityInstitutionTypeId { get; set; }

        public string ActivityInstitutionTypeDescription { get; set; }

        public string userId { get; set; }
        #endregion

        #region Services
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Constructors
        public ActivityInstitutionType()
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
                return new RelayCommand(DeleteActivityInstitutionType);
            }
        }

        async void DeleteActivityInstitutionType()
        {
            var response = await dialogService.ShowConfirm("Confirm", "Are you sure to delete this record?");

            if (!response)
            {
                return;
            }

            await ActivitiesInstitutionTypeViewModel.GetInstance().DeleteActivityInstitutionType(this);
        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(EditActivityInstitutionType);
            }
        }

        async void EditActivityInstitutionType()
        {
            MainViewModel.GetInstance().EditActivitiesInstitutionType = new EditActivitiesInstitutionTypeViewModel(this);
            await navigationService.NavigateOnMaster("EditActivityInstitutionType");

        }

        public ICommand SelectActivityInstitutionType
        {
            get
            {
                return new RelayCommand(_SelectActivityInstitutionType);
            }
        }

        async void _SelectActivityInstitutionType()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditActivitiesInstitutionType = new EditActivitiesInstitutionTypeViewModel(this);
            await navigationService.NavigateOnMaster("DetailsActivityInstitutionType");

        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return ActivityInstitutionTypeId;
        }
        #endregion
    }
}
