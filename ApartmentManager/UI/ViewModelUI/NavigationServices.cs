using AM.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.ViewModelUI
{
    public class NavigationServices
    {
        private readonly NavigationVM _navigationVM;
        private readonly Func<ViewModelBase> _createViewModel;
        public NavigationServices(NavigationVM navigation,Func<ViewModelBase> createViewModel)
        {
            _navigationVM = navigation;
            _createViewModel = createViewModel;
        }
        public void Navigate()
        {
            _navigationVM.CurrentView = _createViewModel();
        }
    }
}
