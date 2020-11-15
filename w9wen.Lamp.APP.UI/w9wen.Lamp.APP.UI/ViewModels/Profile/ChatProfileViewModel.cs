using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using w9wen.Lamp.APP.UI.Services;
using w9wen.Lamp.BE;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
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

        #endregion Fields

        private AssetEntity assetItem;

        public AssetEntity AssetItem
        {
            get { return assetItem; }
            set { SetProperty(ref assetItem, value); }
        }

        private Image captureImage;

        public Image CaptureImage
        {
            get { return captureImage; }
            set { SetProperty(ref captureImage, value); }
        }

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

            //this.TakePhotoCommand = new DelegateCommand(
            //  async () => await TakePhotoExecuteAsync().ConfigureAwait(false));
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

        private DelegateCommand<MediaCapturedEventArgs> mediaCapturedCommand;

        public DelegateCommand<MediaCapturedEventArgs> MediaCapturedCommand =>
            mediaCapturedCommand ?? (mediaCapturedCommand = new DelegateCommand<MediaCapturedEventArgs>(async (a) => await ExecuteMediaCapturedCommand(a)));

        private async Task ExecuteMediaCapturedCommand(MediaCapturedEventArgs e)
        {
            this.IsBusy = true;

            var streamList = new List<Stream>();
            if (e.Image != null)
            {
                if (this.CaptureImage == null)
                {
                    this.CaptureImage = new Image();
                }
                this.CaptureImage.Source = e.Image;
                FileStream fileStream = new FileStream(e.Path, FileMode.Open);

                streamList.Add(fileStream);
            }

            var responseResult = await this.ocrService.GetItemAsync(streamList);

            var assetItem = responseResult.Result;

            this.AssetItem = assetItem;

            if (this.AssetItem.AssetName == null)
            {
                await this.PageDialogService.DisplayAlertAsync(App.Title, "查無資產編號", App.Confirmed);
            }

            this.IsBusy = false;
        }

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
        //private async Task TakePhotoExecuteAsync()
        //{
        //    await CrossMedia.Current.Initialize().ConfigureAwait(false);

        //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        //    {
        //        await this.PageDialogService.DisplayAlertAsync(
        //           "資產專案",
        //           ":( 不支援相機功能.",
        //           "確認").ConfigureAwait(false);

        //        return;
        //    }

        //    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
        //    {
        //        Directory = "Assets",
        //        SaveToAlbum = true,
        //        CompressionQuality = 75,
        //        CustomPhotoSize = 50,
        //        PhotoSize = PhotoSize.MaxWidthHeight,
        //        MaxWidthHeight = 2000,
        //        DefaultCamera = CameraDevice.Rear
        //    });

        //    this.IsBusy = true;

        //    var streamList = new List<Stream>();
        //    if (file != null)
        //    {
        //        streamList.Add(file.GetStream());
        //    }

        //    var responseResult = await this.ocrService.GetItemAsync(streamList);

        //    var assetItem = responseResult.Result;

        //    this.AssetItem = assetItem;

        //    if (this.AssetItem.AssetName == null)
        //    {
        //        await this.PageDialogService.DisplayAlertAsync(App.Title, "查無資產編號", App.Confirmed);
        //    }

        //    this.IsBusy = false;
        //}

        #endregion Methods
    }
}