using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Home
{
    public class HomeItemVM
    {
        public int ID { get; set; }
        public string NameRoom { get; set; }
        public int ElecQuality { get; set; }
        public int RoomMoney { get; set; }
        public int ElectricMoney { get; set; }
        public int WaterMoney { get; set; }
        public int ServiceMoney { get; set; }
        public DateTime PayDate { get; set; }
        public double TotalMoney { get; set; }
        public Active Active { get; set; }
        public bool IsActive { get; set; }
    }
}