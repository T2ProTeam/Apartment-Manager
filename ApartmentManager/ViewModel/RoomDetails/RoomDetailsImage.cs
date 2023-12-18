using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ViewModel.RoomDetails
{
    public class RoomDetailsImage
    {
        public int IDRoom { get; set; }
        public int IDImage { get; set; }
        public string Url { get; set; }

        public BitmapImage bitmapimage { get; set; }
    }
}
