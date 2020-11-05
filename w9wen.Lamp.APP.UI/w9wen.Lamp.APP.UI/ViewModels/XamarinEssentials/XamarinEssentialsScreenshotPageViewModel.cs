using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
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
            screenshotCommand ?? (screenshotCommand = new DelegateCommand(async () => await ScreenshotExecute(), () => Screenshot.IsCaptureSupported));

        #endregion Command

        #region Constructor

        public XamarinEssentialsScreenshotPageViewModel(
           INavigationService navigationService,
           IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            this.Title = "Xamarin Essentials: 螢幕快取";
            this.BoaderColor = Color.Blue;
        }

        #endregion Constructor

        #region Methods

        private async Task ScreenshotExecute()
        {
            var mediaFile = await Screenshot.CaptureAsync().ConfigureAwait(false);
            var stream = await mediaFile.OpenReadAsync(ScreenshotFormat.Png);

            this.Image = ImageSource.FromStream(() => stream);

            if (this.BoaderColor == Color.Blue)
            {
                this.BoaderColor = Color.Red;
            }
            else
            {
                this.BoaderColor = Color.Blue;
            }
        }

        #endregion Methods
    }
}