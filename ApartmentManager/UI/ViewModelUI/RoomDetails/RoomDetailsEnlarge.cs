using AM.UI.State.Navigators;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RoomDetails;

namespace AM.UI.ViewModelUI.RoomDetails
{
    public class RoomDetailsEnlarge : ViewModelBase
    {
        private readonly IAparmentViewModelFactory _ViewModelFactory;
        private readonly INavigator _navigator;
        private readonly RoomDetailsImage _roomDetails;

        public RoomDetailsImage RoomDetails
        {
            get => _roomDetails; 
            set
            {
                RoomDetails = value;
                OnPropertyChanged(nameof(RoomDetailsImage));
            }
        }
        public RoomDetailsEnlarge(IAparmentViewModelFactory viewModelFactory, INavigator navigator, RoomDetailsImage roomDetails)
        {
            _ViewModelFactory = viewModelFactory;
            _navigator = navigator;
            _roomDetails = roomDetails;
        }

    }
}
