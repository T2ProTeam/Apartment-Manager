using AM.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Room;

namespace AM.UI.ListingViewModel
{
    public class RoomListingViewModel : ViewModelBase
    {
        public ObservableCollection<RoomVm> _RoomViewModel;
        public IEnumerable<RoomVm> Rooms => _RoomViewModel;
        public RoomListingViewModel()
        {
            _RoomViewModel = new ObservableCollection<RoomVm>();
        }
    }
}
