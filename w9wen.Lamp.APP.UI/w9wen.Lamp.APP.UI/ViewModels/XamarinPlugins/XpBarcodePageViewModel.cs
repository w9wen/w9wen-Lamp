using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using ZXing;

namespace w9wen.Lamp.APP.UI.ViewModels
{
    public class XpBarcodePageViewModel : ViewModelBase
    {
        private bool isAnalyzing;

        public bool IsAnalyzing
        {
            get { return isAnalyzing; }
            set { SetProperty(ref isAnalyzing, value); }
        }

        private bool isScanning;

        public bool IsScanning
        {
            get { return isScanning; }
            set { SetProperty(ref isScanning, value); }
        }

        private bool isTorchOn;

        public bool IsTorchOn
        {
            get { return isTorchOn; }
            set { SetProperty(ref isTorchOn, value); }
        }

        private DelegateCommand torchOnCommand;

        public DelegateCommand TorchOnCommand =>
            torchOnCommand ?? (torchOnCommand = new DelegateCommand(ExecuteTorchOnCommand));

        private void ExecuteTorchOnCommand()
        {
            this.IsTorchOn = !this.IsTorchOn;
        }

        private DelegateCommand beginScan;

        public DelegateCommand BeginScan =>
            beginScan ?? (beginScan = new DelegateCommand(ExecuteBeginScan));

        private void ExecuteBeginScan()
        {
            this.IsAnalyzing = true;
            this.IsScanning = true;
            //this.IsTorchOn = true;
        }

        private DelegateCommand<object> barcodeScanCommand;

        public DelegateCommand<object> BarcodeScanCommand =>
            barcodeScanCommand ?? (barcodeScanCommand = new DelegateCommand<object>(async (e) => await ExecuteBarcodeScanCommand(e)));

        private async Task ExecuteBarcodeScanCommand(object e)
        {
            var result = e as Result;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                this.IsAnalyzing = false;
                this.IsScanning = false;
                //this.IsTorchOn = false;
                await PageDialogService.DisplayAlertAsync(App.Title,
                                                          $"Format:{result.BarcodeFormat}, {Environment.NewLine}Text: { result.Text}",
                                                          App.Confirmed).ConfigureAwait(false);
            });
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