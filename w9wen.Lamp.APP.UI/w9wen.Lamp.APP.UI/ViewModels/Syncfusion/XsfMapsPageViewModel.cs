using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;
using w9wen.Lamp.APP.UI.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace w9wen.Lamp.APP.UI.ViewModels
{
    public class XsfMapsPageViewModel : ViewModelBase
    {
        #region Full properties

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
        private readonly IGeolocationService geolocationService;

        public double CurrentLongitude
        {
            get { return currentLongitude; }
            set { SetProperty(ref currentLongitude, value); }
        }

        #endregion Full properties

        #region Delegate - Track location

        private DelegateCommand trackLocationCommand;

        public DelegateCommand TrackLocationCommand =>
            trackLocationCommand ?? (trackLocationCommand = new DelegateCommand(async () => await ExecuteTrackLocationCommand()));

        /// <summary>
        /// Track location command.
        /// </summary>
        /// <returns>Task</returns>
        private async Task ExecuteTrackLocationCommand()
        {
            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var location = await this.geolocationService.GetGeolocationAsync().ConfigureAwait(false);
                    this.CurrentLongitude = location.Longitude;
                    this.CurrentLatitude = location.Latitude;
                });
                return true;
            });
        }

        #endregion Delegate - Track location

        #region Constructor

        public XsfMapsPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            IGeolocationService geolocationService) : base(navigationService, pageDialogService)
        {
            this.geolocationService = geolocationService;
        }

        #endregion Constructor

        #region Override

        public override async void OnAppearing()
        {
            //base.OnAppearing();
            var location = await this.geolocationService.GetGeolocationAsync();
            if (location != null)
            {
                this.CurrentLatitude = location.Latitude;
                this.CurrentLongitude = location.Longitude;
            }
        }

        #endregion Override
    }
}