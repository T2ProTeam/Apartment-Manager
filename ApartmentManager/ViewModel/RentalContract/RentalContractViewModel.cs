﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RentalContract
{
    public class RentalContractViewModel
    {
        public int ID { get; set; }
        public int IDroom { get; set; }
        public int IDLeader { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomMoney { get; set; }
        public int ElectricMoney { get; set; }
        public int WaterMoney { get; set; }
        public int ServiceMoney { get; set; }
    }
}
