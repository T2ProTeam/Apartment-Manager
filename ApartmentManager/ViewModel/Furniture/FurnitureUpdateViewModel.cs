using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Furniture
{
    public class FurnitureUpdateViewModel
    {
        public int ID { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "")]
        public string Name { get; set; }

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