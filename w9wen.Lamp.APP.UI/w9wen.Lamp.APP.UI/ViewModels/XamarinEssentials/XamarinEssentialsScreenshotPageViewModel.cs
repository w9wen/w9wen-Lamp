using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace w9wen.Lamp.APP.UI.ViewModels
{
    public class XamarinEssentialsScreenshotPageViewModel : ViewModelBase
    {
        private ImageSource image;

        public ImageSource Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }

        private Color boaderColor;

        public Color BoaderColor
        {
            get { return boaderColor; }
            set { SetProperty(ref boaderColor, value); }
        }

        #region Command

        private DelegateCommand screenshotCommand;

        public DelegateCommand ScreenshotCommand =>
            screenshotCommand ?? (screenshotCommand = new DelegateCommand(async () => await ScreenshotExecuteAsync(), () => Screenshot.IsCaptureSupported));

        private DelegateCommand emailScreenshotCommand;

        public DelegateCommand EmailScreenshotCommand =>
            emailScreenshotCommand ?? (emailScreenshotCommand = new DelegateCommand(async () => await EmailScreenshotExecuteAsync()));

        #endregion Command

        #region Constructor

        public XamarinEssentialsScreenshotPageViewModel(
           INavigationService navigationService,
           IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            this.Title = "Xamarin Essentials: Screenshot";
            this.BoaderColor = Color.Blue;
        }

        #endregion Constructor

        #region CommandExecute

        private async Task ScreenshotExecuteAsync()
        {
            var screenshotStream = await this.TakeScreenshotAsync().ConfigureAwait(false);

            this.Image = ImageSource.FromStream(() => screenshotStream);

            if (this.BoaderColor == Color.Blue)
            {
                this.BoaderColor = Color.Red;
            }
            else
            {
                this.BoaderColor = Color.Blue;
            }
        }

        private async Task EmailScreenshotExecuteAsync()
        {
            var screenshotStream = await this.TakeScreenshotAsync().ConfigureAwait(false);

            var filePath = Path.Combine(FileSystem.CacheDirectory, "Screenshot.jpg");

            using (var file = File.Create(filePath))
            {
                await screenshotStream.CopyToAsync(file);
            }

            await Email.ComposeAsync(new EmailMessage()
            {
                Attachments = { new EmailAttachment(filePath) },
            });
        }

        #endregion CommandExecute

        #region Methods

        private bool ScreenshotIsSupported()
        {
            var isSupported = Screenshot.IsCaptureSupported;
            return isSupported;
        }

        /// <summary>
        /// Capture the screen shot.
        /// </summary>
        /// <returns>The stream of the screen shot.</returns>
        private async Task<Stream> TakeScreenshotAsync()
        {
            var screenshotResult = await Screenshot.CaptureAsync().ConfigureAwait(false);
            var stream = await screenshotResult.OpenReadAsync(ScreenshotFormat.Png).ConfigureAwait(false);
            return stream;
        }

        #endregion Methods
    }
}