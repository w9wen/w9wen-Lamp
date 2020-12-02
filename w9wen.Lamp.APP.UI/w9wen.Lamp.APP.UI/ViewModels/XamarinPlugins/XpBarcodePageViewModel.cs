using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;

namespace w9wen.Lamp.APP.UI.ViewModels
{
    public class XpBarcodePageViewModel : ViewModelBase
    {
        private DelegateCommand barcodeScanCommand;

        public DelegateCommand BarcodeScanCommand =>
            barcodeScanCommand ?? (barcodeScanCommand = new DelegateCommand(async () => await ExecuteBarcodeScanCommand()));

        private async Task ExecuteBarcodeScanCommand()
        {
#if __ANDROID__
// Initialize the scanner first so it can track the current context
MobileBarcodeScanner.Initialize (Application);
#endif
            var scanner = new MobileBarcodeScanner();
            var result = await scanner.Scan();
        }

        #region Constructor

        public XpBarcodePageViewModel(INavigationService navigationService,
                                      IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
        }

        #endregion Constructor
    }
}