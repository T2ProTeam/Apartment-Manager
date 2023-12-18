using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI;
using AM.UI.ViewModelUI.Customer;
using AM.UI.ViewModelUI.Factory;
using Data.Entity;
using Data.Enum;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ViewModel.People;

namespace AM.UI.Command
{
    public class AddCusomerCommand : AsyncCommandBase
    {
        private readonly AddCustomerVMUI _customer;
        private readonly ApartmentStore _people;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewModelFactory;

        public AddCusomerCommand(AddCustomerVMUI customer, ApartmentStore people, INavigator navigator, IAparmentViewModelFactory viewModelFactory)
        {
            _customer=customer;
            _people=people;
            _navigator=navigator;
            _viewModelFactory = viewModelFactory;
            _customer.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            PeopleCreateViewModel create = new PeopleCreateViewModel
            {
                IDRental = _customer.IDRT,
                Name = _customer.name,
                Sex = _customer.sex,
                Birthday= _customer.birthday,
                PhoneNumber = _customer.phoneNumber,
                Email = _customer.email,
                IDCard = _customer.idcard,
                Address = _customer.address,
            };
            var result = await _people.AddCustomer(create);
            if (result !=null)
            {
                new MessageBoxCustom("Add Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.Customer);
            }
            else
                new MessageBoxCustom("Add Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}