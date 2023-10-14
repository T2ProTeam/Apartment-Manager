﻿using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RentalContract;

namespace ViewModel.Bill
{
    public class BillCreateViewModel
    {
        public int ID { get; set; }
        public RentalContractForCombobox Rental { get; set; }
        public int ElectricQuantity { get; set; }
        public Active Active { get; set; }
        public DateTime PayDate { get; set; }
        public int TotalMoney { get; set; }
    }
}
