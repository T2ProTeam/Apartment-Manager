using AM.UI.Command.Furniture;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.Furniture;
using ViewModel.Room;

namespace AM.UI.ViewModelUI.Furnitures
{
    public class FurnitureAddVMUI : ViewModelBase
    {
        private readonly FurnitureStore _store;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewModelFactory;

        public ICommand AddSuccess { get; }
        public ICommand AddConFirm { get; }

        public ICommand Cancel { get; }

        public int _ID;

        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public string _Name;

        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "")]
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public bool IsValid
        {
            get
            {
                var context = new ValidationContext(this);
                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

                bool isValid = Validator.TryValidateObject(this, context, results, true);

                return isValid;
            }
        }

        public FurnitureAddVMUI(FurnitureStore store, INavigator navigator, IAparmentViewModelFactory viewModelFactory)
        {
            _store = store;
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            AddSuccess = new FurnitureAddCommand(this, store, viewModelFactory, navigator);
            AddConFirm = new RelayCommand(Create);
        }

        public void Create(object parameter)
        {
            AddSuccess.Execute(null);
        }
    }
}