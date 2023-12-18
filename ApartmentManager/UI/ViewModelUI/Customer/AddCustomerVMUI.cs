using AM.UI.Command;
using AM.UI.Command.LoadDataBase.LoadCombobox;
using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using Azure.Core;
using Caliburn.Micro;
using Data.Enum;
using Services.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using ViewModel.People;
using ViewModel.RentalContract;

namespace AM.UI.ViewModelUI.Customer
{
    public class AddCustomerVMUI : ViewModelBase
    {
        private readonly IPeople _Ipeople;

        private int _IDRT;

        public int IDRT
        {
            get { return _IDRT; }
            set
            {
                _IDRT = value;
                OnPropertyChanged(nameof(IDRT));
            }
        }

        private ObservableCollection<Sex> _combosex;

        public IEnumerable<Sex> ComboSex => _combosex;

        private ObservableCollection<RentalContractForCombobox> _ListRT;

        public IEnumerable<RentalContractForCombobox> ListRT => _ListRT;
        private string _name;

        [Required(ErrorMessage = "Tên là bắt buộc.")]
        [RegularExpression("^[^0-9]*$", ErrorMessage = "Tên không được chứa số.")]
        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                Validation();
                OnPropertyChanged(nameof(name));
            }
        }

        private Sex _sex = Sex.Male;

        public Sex sex
        {
            get { return _sex; }
            set
            {
                _sex = value;
                OnPropertyChanged(nameof(sex));
            }
        }

        private DateTime _birthday = DateTime.Now;

        public DateTime birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(birthday));
            }
        }

        private string _phoneNumber;

        [RegularExpression("^0[0-9]{10}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(11, ErrorMessage = "Số điện thoại phải chứa đúng 11 ký tự.")]
        public string phoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value; Validation();
                OnPropertyChanged(nameof(phoneNumber));
            }
        }

        private string _email;

        //saiiiiiiiii
        [StringLength(100, ErrorMessage = "Email phải có từ 1 đến 100 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email không hợp lệ.")]
        public string email
        {
            get { return _email; }
            set
            {
                _email = value; Validation();
                OnPropertyChanged(nameof(email));
            }
        }

        private string _idcard;

        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "")]
        public string idcard
        {
            get { return _idcard; }
            set
            {
                _idcard = value; Validation();
                OnPropertyChanged(nameof(idcard));
            }
        }

        private string _address;

        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "")]
        public string address
        {
            get { return _address; }
            set
            {
                _address = value; Validation();
                OnPropertyChanged(nameof(address));
            }
        }

        private bool _flag;

        public bool flag
        {
            get { return _flag; }
            set
            {
                _flag = value;
                OnPropertyChanged(nameof(flag));
            }
        }

        private void Validation()
        {
            var validationContext = new ValidationContext(this, null, null) { MemberName = nameof(idcard) };
            var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            flag = Validator.TryValidateObject(this, validationContext, validationResults, true);
            OnPropertyChanged(nameof(flag));
        }

        public ICommand Cancel { get; }
        public ICommand Confirm { get; }
        public ICommand Succeccd { get; }
        public ICommand LoadComboboxRental { get; }

        public AddCustomerVMUI(IPeople people, INavigator navigator, IAparmentViewModelFactory factory, ApartmentStore apartmentStore, ComboboxStore comboboxStore)
        {
            _ListRT = new ObservableCollection<RentalContractForCombobox>();
            _combosex = new ObservableCollection<Sex> { Sex.Male, Sex.Female };
            _Ipeople = people;

            Cancel = new UpdateCurrentViewModelCommand(navigator, factory);
            Succeccd = new AddCusomerCommand(this, apartmentStore, navigator, factory);
            Confirm = new RelayAsyncCommand(Addcustomer);
        }

        public async Task Addcustomer()
        {
            Succeccd.Execute(null);
        }

        public void AddCombobox(List<RentalContractForCombobox> data)
        {
            data.ForEach(x => _ListRT.Add(x));
        }
    }
}