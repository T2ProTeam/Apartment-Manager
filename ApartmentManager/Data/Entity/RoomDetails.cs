﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class RoomDetails
    {
        public string IDFur { get; set; }
        public string IDRoom { get; set; }
        public Furniture? IDfur { get; set; }
        public Room? IDroom { get; set; }
    }
}
