using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.People
{
    public class PeopleCreateViewModel
    {
        public int ID { get; set; }
        public int IDRental { get; set; }
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string IDCard { get; set; }
        public string Address { get; set; }
    }
}