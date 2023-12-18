using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.PaymentExtension
{
    public class PaymentExtensionVm
    {
        public int ID { get; set; }
        public int IDBill { get; set; }
        public string NameRoom { get; set; }
        public string NameLeader { get; set; }
        public double TotalMoney { get; set; }
        public DateTime Days { get; set; }
    }
}