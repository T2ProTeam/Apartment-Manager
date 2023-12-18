using AM.UI.State.Navigators;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.ViewModelUI.Room
{
    public class RoomDeleteVMUI : ViewModelBase
    {
        private readonly IRoom _iroom;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _modelFactory;
        public RoomDeleteVMUI(IRoom iroom, INavigator navigator, IAparmentViewModelFactory modelFactory)
        {
            _iroom = iroom;
            _navigator = navigator;
            _modelFactory = modelFactory;
        }
    }
}
