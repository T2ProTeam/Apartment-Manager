using AM.UI.State;
using AM.UI.ViewModelUI;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Dtos;
using ViewModel.People;

namespace AM.UI.Command
{
    public class LoadCustomerView : AsyncCommandBase
    {
        private readonly CustomerVMUI _customer;
        private readonly ApartmentStore _people;

        public LoadCustomerView(CustomerVMUI customer, ApartmentStore people)
        {
            _customer=customer;
            _people=people;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _customer.IsLoading =true;
            try
            {
                await _people.Load();

                _customer.UpdateData(_people.customervm);
            }
            catch (Exception)
            {
                _customer.ErrorMessage = "Failed to load Customer";
            }
            _customer.IsLoading =false;
        }
    }
}