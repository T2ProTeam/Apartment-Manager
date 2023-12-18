using Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RentalContract;

namespace ViewModel.Bill
{
    public class BillUpdateViewModel
    {
        public int ID { get; set; }
        public RentalContractForCombobox Rental { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ElectricQuantity must be a positive number.")]
        public int ElectricQuantity { get; set; }

        public Active Active { get; set; }
        public DateTime PayDate { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "ElectricQuantity must be a positive number.")]
        public double TotalMoney { get; set; }

        public bool IsValid
        {
            get
            {
                var context = new ValidationContext(this);
                var results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(this, context, results, true);

                return isValid;
            }
        }
    }
}