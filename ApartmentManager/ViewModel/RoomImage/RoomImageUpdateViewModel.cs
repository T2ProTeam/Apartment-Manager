using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RoomImage
{
    public class RoomImageUpdateViewModel
    {
        public int ID { get; set; }
        public int IDroom { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "")]
        public string Name { get; set; }

        public string Url { get; set; }
    }
}