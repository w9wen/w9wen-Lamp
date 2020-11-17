using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace w9wen.Lamp.APP.UI.ViewModels
{
    public class XeGeolocationPageViewModel : ViewModelBase
    {
        private CancellationTokenSource cts;

        public XeGeolocationPageViewModel(INavigationService navigationService,
                                          IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
        }

        private DelegateCommand startGetGeolocationCommand;

        public DelegateCommand StartGetGeolocationCommand =>
            startGetGeolocationCommand ?? (startGetGeolocationCommand = new DelegateCommand(async () => await ExecuteStartGetGeolocationCommand()));

        private async Task ExecuteStartGetGeolocationCommand()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest()
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30),
                    });
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            base.OnDisappearing();
        }

        private async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}