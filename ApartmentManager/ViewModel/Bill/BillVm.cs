using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Bill
{
    public class BillVm
    {
        public int ID { get; set; }
        public int IDRTC { get; set; }
        public int ElectricQuantity { get; set; }
        public Active Active { get; set; }
        public DateTime PayDate { get; set; }
        public double TotalMoney { get; set; }
    }
}