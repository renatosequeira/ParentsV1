using Parents.Models;
using Parents.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Parents.Services
{
    public class AutomaticOfflineSyncService
    {
        #region Services
        ApiService apiService;
        DataService dataService;
        #endregion

        #region Constructors
        public AutomaticOfflineSyncService()
        {
            apiService = new ApiService();
            dataService = new DataService();
        }
        #endregion

        #region Methods
        public async void ActivitiesSynchronization()
        {
            //valida se existem atividades que devam ser atualizados para o webservice
            var activities = dataService.Get<ActivityParents>(false).Where(a => a.PendingToSave).ToList();

            if (activities.Count == 0)
            {
                return;
            }

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();
            var mainViewModel = MainViewModel.GetInstance();

            foreach (var activity in activities)
            {
                var response = await apiService.Post(
                "http://api.parents.outstandservices.pt",
                "/api",
                "/Activities",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                activity);

                if (response.IsSuccess)
                {
                    activity.PendingToSave = false;
                    dataService.Update(activity);
                }
            }
        }
        #endregion
    }
}
