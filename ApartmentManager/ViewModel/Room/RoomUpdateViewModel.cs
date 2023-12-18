using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.People;

namespace ViewModel.Room
{
    public class RoomUpdateViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "")]

        [Range(1, int.MaxValue, ErrorMessage = "")]
        public int Quantity { get; set; }

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