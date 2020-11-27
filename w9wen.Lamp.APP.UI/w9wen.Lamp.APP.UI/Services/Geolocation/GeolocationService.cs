using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace w9wen.Lamp.APP.UI.Services
{
    /// <summary>
    /// Geolocation service.
    /// </summary>
    public class GeolocationService : IGeolocationService
    {
        /// <summary>
        /// Get the geolocation.
        /// </summary>
        /// <returns></returns>
        public async Task<Location> GetGeolocationAsync()
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