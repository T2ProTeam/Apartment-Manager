using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Bill : DomainObject
    {
        public int IDRTC { get; set; }
        public int ElectricQuantity { get; set; }
        public Active Active { get; set; }
        public DateTime PayDate { get; set; }
        public double TotalMoney { get; set; }

        public RentalContract? RentalContract { get; set; }
        public ICollection<PaymentExtension> PaymentExtensions { get; } = new List<PaymentExtension>();
    }
}