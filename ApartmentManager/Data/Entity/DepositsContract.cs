using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class DepositsContract : DomainObject
    {
        public int IDRoom { get; set; }

        public int IDLeader { get; set; }

        public DateTime DepositsDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Money { get; set; }
        public Room? Room { get; set; }
        public People? People { get; set; }
    }
}