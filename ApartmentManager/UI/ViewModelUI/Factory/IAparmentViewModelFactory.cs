using AM.UI.State.Navigators;
using AM.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.ViewModelUI.Factory
{
    public interface IAparmentViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}