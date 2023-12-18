using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RoomImage
{
    public class RoomImageCreateViewModel
    {
        public int ID { get; set; }
        public int IDroom { get; set; }

        public string FileName { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

    }
}
