using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.DepositsContract
{
    public class DepositsContractVm
    {
        public int ID { get; set; }
        public string RoomName { get; set; }

        public string LeaderName { get; set; }
        public DateTime DepositsDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Money { get; set; }
    }
}