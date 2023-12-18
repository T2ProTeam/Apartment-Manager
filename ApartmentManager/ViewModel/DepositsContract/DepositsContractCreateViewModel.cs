using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.People;
using ViewModel.Room;

namespace ViewModel.DepositsContract
{
    public class DepositsContractCreateViewModel
    {
        public int ID { get; set; }
        public RoomForCombobox Room { get; set; }

        public CustomerForCombobox Customer { get; set; }
        public DateTime DepositsDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Money { get; set; }
    }
}