namespace Parents.ViewModels.Locations
{
    using Services;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Xamarin.Forms.Maps;
    using Models.Sistema;
    using System.Collections.Generic;
    using System;

    public class LocationsViewModel
    {
        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Properties
        public ObservableCollection<Pin> Pins { get; set; }
        #endregion

        #region Constructors
        public LocationsViewModel()
        {
            instance = this;
            apiService = new ApiService();
            dialogService = new DialogService();
        }
        #endregion

        #region Singleton
        static LocationsViewModel instance;

        public static LocationsViewModel GetInstance()
        {
            if (instance == null)
            {
                return new LocationsViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        public async Task LoadPins()
        {
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Location>(
               "http://api.parents.outstandservices.pt",
                "/api",
                "/Locations",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);


            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var locations = (List<Location>)response.Result;
            Pins = new ObservableCollection<Pin>();

            foreach (var location in locations)
            {
                Pins.Add(new Pin
                {
                    Address = location.Address,
                    Label = location.Description,
                    Position = new Position(location.Latitude, location.Longitude),
                    Type = PinType.Generic
                });
            }
        }
        #endregion
    }
}
