using Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.People
{
    public class PeopleUpdateViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Tên là bắt buộc.")]
        [RegularExpression("^0[0-9]{10}$", ErrorMessage = "Số cần bắt đầu bằng 0 và phải có 11 chữ số.")]
        public string PhoneNumber { get; set; }

        [StringLength(100, ErrorMessage = "Email phải có từ 1 đến 100 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "")]
        public string IDCard { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "")]
        public string Address { get; set; }

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