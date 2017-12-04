using Parents.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.ViewModels.Settings
{
    public class SettingsHomeViewModel
    {
        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Constructors
        public SettingsHomeViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
        }
        #endregion

        #region Commands

        #endregion
    }
}
