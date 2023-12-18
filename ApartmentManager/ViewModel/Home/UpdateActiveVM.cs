using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ViewModel.Home
{
    public class UpdateActiveVM
    {
        public int ID { get; set; }
        public int IDRTC { get; set; }
        public int ElectricQuantity { get; set; }
        public Active Active { get; set; }
        public DateTime PayDate { get; set; }
        public int TotalMoney { get; set; }
        public bool IsActive { get; set; }
    }
}