using Prism.Navigation;
using Prism.Services;

namespace w9wen.Lamp.APP.UI.ViewModels
{
    public class XsfMapsPageViewModel : ViewModelBase
    {
        public XsfMapsPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
        }
    }
}