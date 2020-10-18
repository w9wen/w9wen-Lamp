using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;

namespace w9wen.Lamp.APP.UI.ViewModels
{
    public class CloudOcrPageViewModel : ViewModelBase
    {
        public DelegateCommand TakePhotoCommand { get; }

        public CloudOcrPageViewModel(INavigationService navigationService,
                                     IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            this.TakePhotoCommand = new DelegateCommand(
              async () => await TakePhotoExecuteAsync().ConfigureAwait(false));
        }

        #region Methods

        private async Task TakePhotoExecuteAsync()
        {
            await CrossMedia.Current.Initialize().ConfigureAwait(false);

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await this.PageDialogService.DisplayAlertAsync(
                   "資產專案",
                   ":( 不支援.",
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
        }

        #endregion Methods
    }
}