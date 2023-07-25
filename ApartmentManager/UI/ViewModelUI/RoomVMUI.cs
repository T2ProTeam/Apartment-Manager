using AM.UI.Utilities;
using Data.Entity;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AM.UI.ViewModelUI
{
    public class RoomVMUI : ViewModelBase
    { 

        private ViewModelBase _currentViewModel;

        public ICommand AddCommand;

        public RoomVMUI(NavigationServices services)
        {
           AddCommand = new NavigateCommand(services);
        }


        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
            
        }
        public event Action CurrentViewModelChanged;
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }


    }
}
