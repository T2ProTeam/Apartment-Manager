using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI.Customer;
using AM.UI.ViewModelUI.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.People;

namespace AM.UI.Command.Customer
{
    public class UpdateCustomerCommand : AsyncCommandBase
    {
        private readonly UpdateCustomerVMUI _updateCustomerVMUI;
        private readonly ApartmentStore _aparmentstore;
        public ICommand NavCustomerHome { get; }

        public UpdateCustomerCommand(UpdateCustomerVMUI update, ApartmentStore apartmentStore, INavigator navigator, IAparmentViewModelFactory aparment)
        {
            _updateCustomerVMUI = update;
            _aparmentstore = apartmentStore;
            NavCustomerHome = new UpdateCurrentViewModelCommand(navigator, aparment);
            _updateCustomerVMUI.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            PeopleUpdateViewModel viewModel = new PeopleUpdateViewModel
            {
                ID = _updateCustomerVMUI.customerVM.ID,
               // IDroom =_updateCustomerVMUI.customerVM.IDroom,
                Name = _updateCustomerVMUI.customerVM.Name,
                Sex = _updateCustomerVMUI.customerVM.Sex,
                Birthday= _updateCustomerVMUI.customerVM.Birthday,
                PhoneNumber = _updateCustomerVMUI.customerVM.PhoneNumber,
                Email = _updateCustomerVMUI.customerVM.Email,
                IDCard = _updateCustomerVMUI.customerVM.IDCard,
                Address = _updateCustomerVMUI.customerVM.Address,
            };
            var result = await _aparmentstore.UpdateCustomer(viewModel);
            if (result !=null)
            {
                new MessageBoxCustom("Update Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                NavCustomerHome.Execute(ViewType.Customer);
            }
            else
                new MessageBoxCustom("Update Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}