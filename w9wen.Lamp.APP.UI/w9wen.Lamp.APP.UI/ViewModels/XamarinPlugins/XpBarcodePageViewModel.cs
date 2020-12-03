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
using ZXing.Net.Mobile.Forms;

namespace w9wen.Lamp.APP.UI.ViewModels
{
    public class XpBarcodePageViewModel : ViewModelBase
    {
        private DelegateCommand<object> barcodeScanCommand;

        public DelegateCommand<object> BarcodeScanCommand =>
            barcodeScanCommand ?? (barcodeScanCommand = new DelegateCommand<object>(async (scanResult) => await ExecuteBarcodeScanCommand(scanResult)));

        private async Task ExecuteBarcodeScanCommand(object scanResult)
        {
            if (scanResult != null)
            {
            }
        }

        private void ScanPage_OnScanResult(ZXing.Result result)
        {
        }

        #region Constructor

        public XpBarcodePageViewModel(INavigationService navigationService,
                                      IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
        }

        #endregion Constructor
    }
}