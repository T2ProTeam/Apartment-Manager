using Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.People;
using ViewModel.Room;

namespace ViewModel.RentalContract
{
    public class RentalContractUpdateViewModel
    {
        public int ID { get; set; }

        public RoomForCombobox RoomCombobox { get; set; }
        public CustomerForCombobox CustomerCombobox { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ElectricQuantity must be a positive number.")]
        public int RoomMoney { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ElectricQuantity must be a positive number.")]
        public int ElectricMoney { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ElectricQuantity must be a positive number.")]
        public int WaterMoney { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ElectricQuantity must be a positive number.")]
        public int ServiceMoney { get; set; }

        public Active Active { get; set; }

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