using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.People;

namespace ViewModel.Room
{
    public class RoomCreateViewModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Staked { get; set; }
    }
}