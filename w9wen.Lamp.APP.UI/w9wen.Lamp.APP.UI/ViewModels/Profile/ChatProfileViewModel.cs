using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using w9wen.Lamp.APP.UI.Services;
using Xamarin.Forms.Internals;

namespace w9wen.Lamp.APP.UI.ViewModels.Profile
{
    /// <summary>
    /// ViewModel for profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ChatProfileViewModel : ViewModelBase
    {
        #region Fields

        private readonly IOcrService ocrService;
        private string assetsNo;

        #endregion Fields

        #region Properties

        public string AssetsNo
        {
            get { return assetsNo; }
            set { SetProperty(ref assetsNo, value); }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ProfileViewModel" /> class
        /// </summary>
        //public ChatProfileViewModel()
        //{
        //    this.EditCommand = new Command(this.EditButtonClicked);
        //    this.AvailableCommand = new Command(this.AvailableStatusClicked);
        //    this.NotificationCommand = new Command(this.NotificationOptionClicked);
        //}

        public ChatProfileViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            IOcrService ocrService) : base(navigationService, pageDialogService)
        {
            this.ocrService = ocrService;

            this.TakePhotoCommand = new DelegateCommand(
              async () => await TakePhotoExecuteAsync().ConfigureAwait(false));
        }

        #endregion Constructor

        #region Command

        public DelegateCommand TakePhotoCommand { get; }

        ///// <summary>
        ///// Gets or sets the command that is executed when the edit button is clicked.
        ///// </summary>
        //public Command EditCommand { get; set; }

        ///// <summary>
        ///// Gets or sets the command that is executed when the available status is clicked.
        ///// </summary>
        //public Command AvailableCommand { get; set; }

        ///// <summary>
        ///// Gets or sets the command that is executed when the notification option is clicked.
        ///// </summary>
        //public Command NotificationCommand { get; set; }

        #endregion Command

        #region Methods

        ///// <summary>
        ///// Invoked when the edit button is clicked.
        ///// </summary>
        ///// <param name="obj">The object</param>
        //private void EditButtonClicked(object obj)
        //{
        //    // Do something
        //}

        ///// <summary>
        ///// Invoked when the available status is clicked.
        ///// </summary>
        ///// <param name="obj">The object</param>
        //private async void AvailableStatusClicked(object obj)
        //{
        //    Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
        //    (obj as Grid).BackgroundColor = (Color)retVal;
        //    await Task.Delay(100);
        //    (obj as Grid).BackgroundColor = Color.Transparent;
        //}

        ///// <summary>
        ///// Invoked when the notification option is clicked.
        ///// </summary>
        ///// <param name="obj">The object</param>
        //private async void NotificationOptionClicked(object obj)
        //{
        //    Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
        //    (obj as Grid).BackgroundColor = (Color)retVal;
        //    await Task.Delay(100);
        //    (obj as Grid).BackgroundColor = Color.Transparent;
        //}
        private async Task TakePhotoExecuteAsync()
        {
            await CrossMedia.Current.Initialize().ConfigureAwait(false);

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await this.PageDialogService.DisplayAlertAsync(
                   "資產專案",
                   ":( 不支援相機功能.",
                   "確認").ConfigureAwait(false);

                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Assets",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Rear
            });

            this.IsBusy = true;

            var streamList = new List<Stream>();
            if (file != null)
            {
                streamList.Add(file.GetStream());
            }

            var result = await this.ocrService.GetItemAsync(streamList);

            this.AssetsNo = result.Result;

            this.IsBusy = false;
        }

        #endregion Methods
    }
}