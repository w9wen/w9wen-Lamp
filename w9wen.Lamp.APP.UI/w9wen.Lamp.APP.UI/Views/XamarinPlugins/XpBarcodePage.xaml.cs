using Xamarin.Forms;

namespace w9wen.Lamp.APP.UI.Views
{
    public partial class XpBarcodePage : ContentPage
    {
        public XpBarcodePage()
        {
            InitializeComponent();
        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            if (result != null)
            {
            }
        }
    }
}