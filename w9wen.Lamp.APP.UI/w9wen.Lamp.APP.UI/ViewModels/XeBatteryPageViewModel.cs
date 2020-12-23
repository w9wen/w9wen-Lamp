using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace w9wen.Lamp.APP.UI.ViewModels
{
    public class XeBatteryPageViewModel : BindableBase
    {
        #region Full Properties

        private string batteryState;

        public string BatteryState
        {
            get { return batteryState; }
            set { SetProperty(ref batteryState, value); }
        }

        private string batteryChargeLevel;

        public string BatteryChargeLevel
        {
            get { return batteryChargeLevel; }
            set { SetProperty(ref batteryChargeLevel, value); }
        }

        private string batteryPowerSource;

        public string BatteryPowerSource
        {
            get { return batteryPowerSource; }
            set { SetProperty(ref batteryPowerSource, value); }
        }

        #endregion Full Properties

        public XeBatteryPageViewModel()
        {
            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
        }

        private async void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            this.BatteryState = e.State.ToString();
            this.BatteryChargeLevel = (e.ChargeLevel * 100).ToString() + "%";
            this.BatteryPowerSource = e.PowerSource.ToString();
        }
    }
}