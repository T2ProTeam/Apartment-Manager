using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RentalContract
{
    public class RentalContractVm
    {
        public int ID { get; set; }
        public string RoomName { get; set; }
        public string LeaderName { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomMoney { get; set; }
        public int ElectricMoney { get; set; }
        public int WaterMoney { get; set; }
        public int ServiceMoney { get; set; }

        public Active Active { get; set; }
    }
}