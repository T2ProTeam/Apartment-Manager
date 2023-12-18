using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.People;
using ViewModel.Room;

namespace ViewModel.DepositsContract
{
    public class DepositsContractUpdateViewModel
    {
        public int ID { get; set; }
        public RoomForCombobox Room { get; set; }
        public CustomerForCombobox Customer { get; set; }
        public DateTime DepositsDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ElectricQuantity must be a positive number.")]
        public int Money { get; set; }

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