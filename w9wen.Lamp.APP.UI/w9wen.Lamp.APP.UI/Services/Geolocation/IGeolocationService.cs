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
    public interface IGeolocationService
    {
        /// <summary>
        /// Get the geolocation.
        /// </summary>
        /// <returns></returns>
        Task<Location> GetGeolocationAsync();
    }
}