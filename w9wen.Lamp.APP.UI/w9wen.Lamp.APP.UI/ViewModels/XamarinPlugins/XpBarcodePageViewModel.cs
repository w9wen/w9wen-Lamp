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
        private DelegateCommand barcodeScanCommand;

        private ZXingScannerPage scanPage;

        public DelegateCommand BarcodeScanCommand =>
            barcodeScanCommand ?? (barcodeScanCommand = new DelegateCommand(async () => await ExecuteBarcodeScanCommand()));

        private async Task ExecuteBarcodeScanCommand()
        {
            scanPage = new ZXingScannerPage();

            scanPage.OnScanResult += ScanPage_OnScanResult; ;
        }

        private void ScanPage_OnScanResult(ZXing.Result result)
        {
            scanPage.IsScanning = false;
        }

        #region Constructor

        public XpBarcodePageViewModel(INavigationService navigationService,
                                      IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
        }

        #endregion Constructor
    }
}