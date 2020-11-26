using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace w9wen.Lamp.APP.UI.ViewModels
{
    public class XsfMapsPageViewModel : ViewModelBase
    {
        private string currentPosition;

        public string CurrentPosition
        {
            get { return currentPosition; }
            set { SetProperty(ref currentPosition, value); }
        }

        private double currentLatitude;

        public double CurrentLatitude
        {
            get { return currentLatitude; }
            set { SetProperty(ref currentLatitude, value); }
        }

        private double currentLongitude;

        public double CurrentLongitude
        {
            get { return currentLongitude; }
            set { SetProperty(ref currentLongitude, value); }
        }

        public XsfMapsPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
        }

        public override async void OnAppearing()
        {
            //base.OnAppearing();
            var location = await GetGeolocationAsync();
            if (location != null)
            {
                this.CurrentLatitude = location.Latitude;
                this.CurrentLongitude = location.Longitude;
            }
        }

        private async Task<Location> GetGeolocationAsync()
        {
            try
            {
                //var location = await Geolocation.GetLastKnownLocationAsync();
                //if (location == null)
                //{
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest()
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30),
                });
                //}
                return location;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}