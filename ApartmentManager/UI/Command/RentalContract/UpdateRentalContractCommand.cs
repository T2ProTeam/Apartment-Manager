using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI.Factory;
using AM.UI.ViewModelUI.RentalContract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.RentalContract;

namespace AM.UI.Command.RentalContract
{
    public class UpdateRentalContractCommand : AsyncCommandBase
    {
        private readonly RentalContractUpdateVMUI _rental;
        private readonly RentalContractStore _store;

        public ICommand RentalHomeNav { get; }

        public UpdateRentalContractCommand(RentalContractUpdateVMUI rental, INavigator navigator, IAparmentViewModelFactory viewmodel, RentalContractStore store)
        {
            _rental = rental;
            _store = store;
            RentalHomeNav = new UpdateCurrentViewModelCommand(navigator, viewmodel);
            _rental.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            RentalContractUpdateViewModel rental = new RentalContractUpdateViewModel
            {
                ID = _rental.Rental.ID,
                RoomCombobox = _rental.SelectRoom,
                CustomerCombobox = _rental.SelectCustomer,
                ReceiveDate = _rental.Rental.ReceiveDate,
                CheckOutDate = _rental.Rental.CheckOutDate,
                RoomMoney = _rental.Rental.RoomMoney,
                ElectricMoney = _rental.Rental.ElectricMoney,
                WaterMoney = _rental.Rental.WaterMoney,
                ServiceMoney = _rental.Rental.ServiceMoney,
                Active = _rental.Active
            };
            var result = await _store.UpdateRentalContract(rental);
            if (result != null)
            {
                new MessageBoxCustom("Update Success", MessageType.Success, MessageButtons.Ok).ShowDialog();
                RentalHomeNav.Execute(ViewType.RentalContract);
            }
            else
            {
                new MessageBoxCustom("Update Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }

        public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}