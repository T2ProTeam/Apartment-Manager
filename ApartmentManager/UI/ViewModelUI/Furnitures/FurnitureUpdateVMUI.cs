using AM.UI.Command.Furniture;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;
using ViewModel.Furniture;
using ViewModel.Room;

namespace AM.UI.ViewModelUI.Furnitures
{
    public class FurnitureUpdateVMUI : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewModelFactory;
        private readonly IFurniture _ifur;
        private readonly FurnitureStore _apartmentStore;
        private  FurnitureVm _furnitureViewModel;

        public ICommand UpdateSuccess { get; }
        public ICommand UpdateConFirm { get; }


        public FurnitureUpdateVMUI(IFurniture ifur, FurnitureVm furnitureViewModel, INavigator navigator, IAparmentViewModelFactory viewModelFactory, FurnitureStore apartmentStore)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _ifur = ifur;
            _furnitureViewModel = furnitureViewModel;
            _apartmentStore = apartmentStore;
            UpdateSuccess = new FurnitureUpdateCommand(this, apartmentStore);
            UpdateConFirm = new RelayCommand(UpdateConfirm);


        }


        public void UpdateConfirm(object parameter)
        {
            UpdateSuccess.Execute(null);
        }


        public FurnitureVm Furniture
        {
            get { return _furnitureViewModel; }
            set
            {
                Furniture = value;
                OnPropertyChanged(nameof(FurnitureVm));
            }
        }

    }
}
