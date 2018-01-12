namespace Parents.Views.Locations
{
    using System;
    using System.Threading.Tasks;
    using global::Parents.ViewModels.Locations;
    using Services;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    public partial class LocationsView : ContentPage
    {
        #region Services
        GeolocatorService geolocatorService;
        #endregion


        #region Constructors
        public LocationsView()
        {
            InitializeComponent();

            geolocatorService = new GeolocatorService();

            MoveMapToCurrentPosition();
        }
        #endregion

        #region Methods
        async void MoveMapToCurrentPosition()
        {
            await geolocatorService.GetLocation();

            if (geolocatorService.Latitude != 0 ||
                geolocatorService.Longitude != 0)
            {
                var position = new Position(
                    geolocatorService.Latitude,
                    geolocatorService.Longitude);

                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                    position,
                    Distance.FromKilometers(.5)));
            }
            
            await LoadPins();
        }

        private async Task LoadPins()
        {
            var locationsViewModel = LocationsViewModel.GetInstance();

            await locationsViewModel.LoadPins();

            foreach (var pin in locationsViewModel.Pins)
            {
                MyMap.Pins.Add(pin);
            }
        }

        #endregion

    }
}