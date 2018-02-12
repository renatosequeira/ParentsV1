namespace Parents.Models.HealthManagement
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Parents.Services;
    using Parents.ViewModels;
    using Parents.ViewModels.Health;
    using Parents.ViewModels.Health.HelperPages;

    public class ChildrenWeight
    {
        #region Services
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Attributtes
        public int ChildrenWeightId { get; set; }

        public int ChildrenId { get; set; }

        public double WeightVaue { get; set; }

        public string WeightUnit { get; set; }

        public DateTime RegistryDate { get; set; }

        public double OldWeightValue { get; set; }

        public bool IsVisible { get; set; }

        string _selectedItem;
        #endregion

        #region Constructors
        public ChildrenWeight()
        {
            dialogService = new DialogService();
            navigationService = new NavigationService();
        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return ChildrenWeightId;
        }

        public string WeightDifference
        {
            get
            {
                double dif = (WeightVaue - OldWeightValue);
                double difRounded = Math.Round(dif, 2);
                return difRounded.ToString();
            }
        }

        public string DifferenceImage
        {
            get
            {
                string img = "";
                double _weightDifference = double.Parse(WeightDifference);

                if (_weightDifference > 0)
                {
                    img = "ic_up";
                }
                else if (_weightDifference < 0)
                {
                    img = "ic_down";
                }
                else
                {
                    img = "ic_equal";
                }

                return img;
            }
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

            await ChildrenWeightViewModel.GetInstance().DeleteChildrenWeight(this);
        }

        public ICommand SelectWeightCommand
        {
            get
            {
                return new RelayCommand(SelectWeight);
            }
        }

        async void SelectWeight()
        {

            //Application.Current.Properties["childrenIdentityCard"] = ChildrenIdentityCard;
            //Application.Current.Properties["childrenId"] = ChildrenId;
            //MessagingCenter.Send(this, "childrenName", ChildrenFirstName);

            //var mainViewModel = MainViewModel.GetInstance();
            //mainViewModel.EditChildrenWeightHelper = new EditChildrenWeightHelperViewModel(this);

            //await navigationService.NavigateOnMaster("EditChildrenWeightHelperView");
            
        }
        #endregion
    }
}
