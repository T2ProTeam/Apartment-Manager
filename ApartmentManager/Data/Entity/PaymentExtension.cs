using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class PaymentExtension : DomainObject
    {
        public int IDBill { get; set; }
        public DateTime Days { get; set; }
        public Bill? Bill { get; set; }
    }
}