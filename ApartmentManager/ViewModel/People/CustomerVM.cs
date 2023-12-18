using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Dtos;

namespace ViewModel.People
{
    public class CustomerVM
    {
        public int ID { get; set; }
        public int IDRT { get; set; }
        public string RoomName { get; set; }
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string IDCard { get; set; }
        public string Address { get; set; }
    }
}