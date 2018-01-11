namespace Parents.Models.ActivitiesManagement.Helpers
{
    using GalaSoft.MvvmLight.Command;
    using Parents.Services;
    using Parents.ViewModels;
    using Parents.ViewModels.Activities.Helpers.Peridiocity;
    using System.Windows.Input;

    public class ActivityPeridiocity
    {
        #region Properties
        public int ActivityPeriodicityId { get; set; }
        public string ActivityPeriodicityDescription { get; set; }
        public string userId { get; set; }
        #endregion

        #region Services
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Constructors
        public ActivityPeridiocity()
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
                return new RelayCommand(DeleteActivityPeridiocity);
            }
        }

        async void DeleteActivityPeridiocity()
        {
            var response = await dialogService.ShowConfirm("Confirm", "Are you sure to delete this record?");

            if (!response)
            {
                return;
            }

            await ActivityPeridiocityViewModel.GetInstance().DeleteActivityPeridiocity(this);
        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(EditActivityPeridiocity);
            }
        }

        async void EditActivityPeridiocity()
        {
            MainViewModel.GetInstance().EditActivityPeridiocity = new EditActivityPeridiocityViewModel(this);
            await navigationService.NavigateOnMaster("EditActivityPeridiocity");

        }

        public ICommand SelectActivityPeridiocity
        {
            get
            {
                return new RelayCommand(SelectActivityPeridiocityItem);
            }
        }

        async void SelectActivityPeridiocityItem()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditActivityPeridiocity = new EditActivityPeridiocityViewModel(this);
            await navigationService.NavigateOnMaster("DetailsActivityPeridiocity");

        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return ActivityPeriodicityId;
        }
        #endregion
    }
}
