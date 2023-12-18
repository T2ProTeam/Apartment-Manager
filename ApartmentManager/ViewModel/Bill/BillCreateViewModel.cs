using Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RentalContract;
using ViewModel.Room;

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