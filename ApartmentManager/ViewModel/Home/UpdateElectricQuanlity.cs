using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Home
{
    public class UpdateElectricQuanlity
    {
        public int Id { get; set; }
        public int Quality { get; set; }
        public int ElectricMoneyDefualt { get; set; }
        public int RoomMoney { get; set; }
        public int WaterMoney { get; set; }
        public int ServiceMoney { get; set; }
        public int TotalMoney { get; set; }
        public Active Active { get; set; }
    }
}