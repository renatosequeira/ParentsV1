using Parents.Models;
using System.Collections.ObjectModel;

namespace Parents.ViewModels
{
    public class ParentsViewModel
    {
        #region Properties
        public ObservableCollection<Parent> Parents { get; set; }
        #endregion

        #region Constructors
        public ParentsViewModel()
        {
            LoadParents();
        }
        #endregion

        #region Methods
        private void LoadParents()
        {
            
        } 

        #endregion
    }
}
