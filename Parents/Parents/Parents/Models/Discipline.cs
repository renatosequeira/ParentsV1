using GalaSoft.MvvmLight.Command;
using Parents.Services;
using Parents.ViewModels;
using Parents.ViewModels.School;
using Parents.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Parents.Models
{
    public class Discipline
    {
        #region Properties

        public int DisciplineId { get; set; }

        public string DisciplineDescription { get; set; }

        public string DisciplineRemarks { get; set; }

        public string userId { get; set; }
        #endregion

        #region Services
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Constructors
        public Discipline()
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
                return new RelayCommand(DeleteDiscipline);
            }
        }

        async void DeleteDiscipline()
        {
            var response = await dialogService.ShowConfirm("Confirm", "Are you sure to delete this record?");

            if (!response)
            {
                return;
            }

            await DisciplinesViewModel.GetInstance().DeleteDiscipline(this);
        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(EditDiscipline);
            }
        }

        async void EditDiscipline()
        {
            MainViewModel.GetInstance().EditDiscipline = new EditDisciplineViewModel(this);
            await navigationService.NavigateOnMaster("EditDiscipline");
        }

        public ICommand SelectDiscipline
        {
            get
            {
                return new RelayCommand(ViewDiscipline);
            }
        }

        async void ViewDiscipline()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditDiscipline = new EditDisciplineViewModel(this);
            await navigationService.NavigateOnMaster("DisciplinesView");
        }

       
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return DisciplineId;
        }
        #endregion

    
    }
}
