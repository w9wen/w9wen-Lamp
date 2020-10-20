using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using w9wen.Lamp.APP.UI.Services;

namespace w9wen.Lamp.APP.UI.ViewModels
{
    public class CloudOcrPageViewModel : ViewModelBase
    {
        private readonly IOcrService ocrService;

        public DelegateCommand TakePhotoCommand { get; }

        #region Constructor

        public CloudOcrPageViewModel(INavigationService navigationService,
                                     IPageDialogService pageDialogService,
                                     IOcrService ocrService) : base(navigationService, pageDialogService)
        {
            this.ocrService = ocrService;

            this.TakePhotoCommand = new DelegateCommand(
              async () => await TakePhotoExecuteAsync().ConfigureAwait(false));
        }

        #endregion Constructor

        #region Methods

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
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });

            var streamList = new List<Stream>();
            if (file != null)
            {
                streamList.Add(file.GetStream());
            }

            var result = await this.ocrService.GetItemAsync(streamList);
        }

        #endregion Methods
    }
}