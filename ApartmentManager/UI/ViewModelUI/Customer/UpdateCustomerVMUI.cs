using AM.UI.Command;
using AM.UI.Command.Customer;
using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.Utilities;
using AM.UI.View.Dialog;
using AM.UI.View.People;
using AM.UI.ViewModelUI.Factory;
using Data.Enum;
using Microsoft.EntityFrameworkCore.Metadata;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModel.People;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AM.UI.ViewModelUI.Customer
{
    public class UpdateCustomerVMUI : ViewModelBase
    {
        private readonly ApartmentStore _people;
        private PeopleUpdateViewModel _customerVM;
        private ObservableCollection<Sex> _combosex;

        public IEnumerable<Sex> ComboSex => _combosex;

        public Sex sex
        {
            get { return _customerVM.Sex; }
            set
            {
                _customerVM.Sex = value;
                OnChanged(nameof(sex));
            }
        }

        public string name
        {
            get { return _customerVM.Name; }
            set
            {
                _customerVM.Name = value;
                OnChanged(nameof(name));
            }
        }

        public string phoneNumber
        {
            get { return _customerVM.PhoneNumber; }
            set
            {
                _customerVM.PhoneNumber = value;
                OnPropertyChanged(nameof(phoneNumber));
                OnPropertyChanged(nameof(customerVM));
                // OnChanged(nameof(phoneNumber));
            }
        }

        public string email
        {
            get { return _customerVM.Email; }
            set
            {
                _customerVM.Email = value;
                OnChanged(nameof(email));
            }
        }

        public string idcard
        {
            get { return _customerVM.IDCard; }
            set
            {
                _customerVM.IDCard = value;
                OnChanged(nameof(idcard));
            }
        }

        public string address
        {
            get { return _customerVM.Address; }
            set
            {
                _customerVM.Address = value;
                OnChanged(nameof(address));
            }
        }

        public PeopleUpdateViewModel customerVM
        {
            get { return _customerVM; }
            set
            {
                _customerVM = value;
                OnPropertyChanged(nameof(customerVM));
            }
        }

        public ICommand Confirm { get; }
        public ICommand Cancel { get; }
        public ICommand Succeccd { get; }

        public UpdateCustomerVMUI(ApartmentStore people, CustomerVM customer, INavigator navigator, IAparmentViewModelFactory factory)
        {
            _combosex = new ObservableCollection<Sex> { Sex.Male, Sex.Female };
            customerVM = new PeopleUpdateViewModel
            {
                ID= customer.ID,
                Name = customer.Name,
                Sex= customer.Sex,
                Birthday= customer.Birthday,
                PhoneNumber = customer.PhoneNumber,
                Email= customer.Email,
                IDCard= customer.IDCard,
                Address= customer.Address,
            };

            _people = people;
            Cancel = new UpdateCurrentViewModelCommand(navigator, factory);
            Succeccd = new UpdateCustomerCommand(this, people, navigator, factory);
            Confirm = new RelayAsyncCommand(Updatecustomer);
        }

        private void OnChanged(string parameter)
        {
            MessageBox.Show(_customerVM.IsValid.ToString());
            OnPropertyChanged(nameof(parameter));
            OnPropertyChanged(nameof(customerVM));
        }

        public async Task Updatecustomer()
        {
            bool? Confirm = new MessageBoxCustom($"Do you want to Upate customer : {customerVM.ID} ", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (Confirm == true)
            {
                Succeccd.Execute(null);
            }
        }
    }
}