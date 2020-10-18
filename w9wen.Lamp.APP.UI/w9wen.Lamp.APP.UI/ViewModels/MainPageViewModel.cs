using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace w9wen.Lamp.APP.UI.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService,
                                 IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "Main Page";
        }
    }
}