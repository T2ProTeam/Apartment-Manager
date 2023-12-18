using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.People;
using ViewModel.Room;

namespace ViewModel.RentalContract
{
    public class RentalContractCreateViewModel
    {
        public int ID { get; set; }
        public RoomForCombobox RoomCombobox { get; set; }
        public CustomerForCombobox CustomerCombobox { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomMoney { get; set; }
        public int ElectricMoney { get; set; }
        public int WaterMoney { get; set; }
        public int ServiceMoney { get; set; }

        public Active Active { get; set; }
    }
}