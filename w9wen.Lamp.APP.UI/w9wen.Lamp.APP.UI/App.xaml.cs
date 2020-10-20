using Prism;
using Prism.Ioc;
using w9wen.Lamp.APP.UI.Services;
using w9wen.Lamp.APP.UI.ViewModels;
using w9wen.Lamp.APP.UI.ViewModels.Profile;
using w9wen.Lamp.APP.UI.Views;
using w9wen.Lamp.APP.UI.Views.Profile;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace w9wen.Lamp.APP.UI
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        public static string ServerUrl { get; internal set; }

        #region Properties

        public static string BaseImageUrl { get; }
            = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

        #endregion Properties

        protected override async void OnInitialized()
        {
            InitializeComponent();

            ServerUrl = "https://w9wen-lamp.azurewebsites.net/api/";

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<OcrHomePage, OcrHomePageViewModel>();
            containerRegistry.RegisterForNavigation<CloudOcrPage, CloudOcrPageViewModel>();

            containerRegistry.RegisterForNavigation<ChatProfilePage, ChatProfileViewModel>();

            containerRegistry.Register<IOcrService, OcrService>();
        }
    }
}