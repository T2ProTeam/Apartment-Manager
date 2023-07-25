using AM.UI.View.Rooms;
using AM.UI.ViewModelUI;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.View.Homes;
using ViewModel.Room;

namespace AM.UI.Utilities
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationServices _navigationServices;
        public NavigateCommand(NavigationServices navigationServices)
        {
            _navigationServices = navigationServices;
        }
        public override void Execute(object? parameter)
        {
            _navigationServices.Navigate();
        }
    }
}
