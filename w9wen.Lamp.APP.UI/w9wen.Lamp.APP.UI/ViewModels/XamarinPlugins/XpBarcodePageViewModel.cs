using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace w9wen.Lamp.APP.UI.ViewModels
{
    public class XpBarcodePageViewModel : ViewModelBase
    {
        private Result scanResult;

        public Result ScanResult
        {
            get { return scanResult; }
            set { SetProperty(ref scanResult, value); }
        }

        private DelegateCommand<object> barcodeScanCommand;

        public DelegateCommand<object> BarcodeScanCommand =>
            barcodeScanCommand ?? (barcodeScanCommand = new DelegateCommand<object>(async (e) => await ExecuteBarcodeScanCommand(e)));

        private async Task ExecuteBarcodeScanCommand(object e)
        {
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