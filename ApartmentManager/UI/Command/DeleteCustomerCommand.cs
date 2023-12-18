using Accessibility;
using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI;
using AM.UI.ViewModelUI.Factory;
using Microsoft.IdentityModel.Tokens;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Command
{
    public class DeleteCustomerCommand : AsyncCommandBase
    {
        private readonly CustomerVMUI _customer;
        private readonly ApartmentStore _people;
        private readonly INavigator _renavigator;
        private readonly IAparmentViewModelFactory _factory;

        public DeleteCustomerCommand(CustomerVMUI customer, ApartmentStore people, IAparmentViewModelFactory aparmentViewModelFactory, INavigator renavigator)
        {
            _customer=customer;
            _people=people;
            _renavigator=renavigator;
            _factory=aparmentViewModelFactory;
            _customer.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var result = await _people.Delete(_customer.ID);
            if (result == true)
            {
                new MessageBoxCustom("Delete Susscced", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _renavigator.CurrentViewModel = _factory.CreateViewModel(ViewType.Customer);
            }
            else
                new MessageBoxCustom("Delete Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}