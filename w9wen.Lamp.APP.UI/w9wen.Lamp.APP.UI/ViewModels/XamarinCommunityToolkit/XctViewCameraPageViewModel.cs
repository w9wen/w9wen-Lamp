using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace w9wen.Lamp.APP.UI.ViewModels
{
    public class XctViewCameraPageViewModel : BindableBase
    {
        private ImageSource captureImageSource;

        public ImageSource CaptureImageSource
        {
            get { return captureImageSource; }
            set { SetProperty(ref captureImageSource, value); }
        }

        private DelegateCommand<MediaCapturedEventArgs> mediaCapturedCommand;

        public DelegateCommand<MediaCapturedEventArgs> MediaCapturedCommand =>
            mediaCapturedCommand ?? (mediaCapturedCommand = new DelegateCommand<MediaCapturedEventArgs>(async (a) => await ExecuteMediaCapturedCommand(a)));

        private async Task ExecuteMediaCapturedCommand(MediaCapturedEventArgs e)
        {
            if (e != null &&
                e.Image != null)
            {
                this.CaptureImageSource = e.Image;
            }
        }

        public XctViewCameraPageViewModel()
        {
        }
    }
}