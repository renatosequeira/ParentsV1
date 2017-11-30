using GalaSoft.MvvmLight.Command;
using Parents.Services;
using Parents.ViewModels;
using Parents.ViewModels.School;
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
        #endregion

        #region Services
        NavigationService navigationService;
        #endregion

        #region Constructors
        public Discipline()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
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
            mainViewModel.Disciplines = new DisciplinesViewModel();
            await navigationService.Navigate("DisciplinesView");
        }
        #endregion
    }
}
