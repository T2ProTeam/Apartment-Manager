using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Room;

namespace ViewModel.RoomImage
{
    public class CurrentRoomImageViewModel
    {
       public List<RoomImageCreateViewModel> Room { get; set; }
       public string NameFile { get; set; }
       public CurrentRoomImageViewModel()
        {
            Room = new List<RoomImageCreateViewModel>();
        }
    }
}
