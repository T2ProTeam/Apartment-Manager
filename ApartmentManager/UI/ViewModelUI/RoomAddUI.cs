using AM.UI.Utilities;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Dtos;
using ViewModel.People;

namespace AM.UI.ViewModelUI
{
    public class RoomAddUI : ViewModelBase
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set {
                _ID = value;
                  OnPropertyChanged(nameof(ID));           
            
                }
        }

    }
}
