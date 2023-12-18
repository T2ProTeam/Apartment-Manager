using AM.UI.Command;
using AM.UI.Command.Furniture;
using AM.UI.Command.LoadDataBase;
using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI.Factory;
using AM.UI.ViewModelUI.Furnitures;
using Data.Entity;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using ViewModel.Dtos;
using ViewModel.Furniture;
using ViewModel.People;
using ViewModel.Room;
using AM.UI.Mediator;
using AM.UI.ViewModelUI.RoomDetails;

namespace AM.UI.ViewModelUI
{
    public class FurnitureHomeVMUI : Utilities.ViewModelBase
    {
        private ObservableCollection<FurnitureVm> _fur;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _ViewModelFactory;
        private readonly FurnitureStore _apartmentStore;
        public IFurniture _ifur;
        public IEnumerable<FurnitureVm> Fur => _fur;

        public ICommand TransferDataCommand { get; private set; }

        //Command
        public ICommand LoadDataBase { get; }

        public ICommand FurnitureNav { get; }

        public ICommand FurnitureUpdateNav { get; }
        public ICommand DeleteConFirm { get; }

        //Properties
        public bool _IsLoading;

        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                _IsLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private string _search;

        public string search
        {
            get { return _search; }
            set
            {
                _search = value;
                ChangedString(nameof(search));
            }
        }

        private bool _isText;

        public bool IsText
        {
            get { return _isText; }
            set
            {
                _isText = value;
                OnPropertyChanged(nameof(IsText));
            }
        }

        public bool HasData => _fur.Any();

        public string _MessageError;

        public string MessageError
        {
            get { return _MessageError; }
            set
            {
                _MessageError = value;
                OnPropertyChanged(nameof(MessageError));
            }
        }

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

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private FurnitureVm _SelectFurniture;

        public FurnitureVm SelectFurniture
        {
            get { return _SelectFurniture; }
            set
            {
                _SelectFurniture = value;
                OnPropertyChanged(nameof(SelectFurniture));
            }
        }

        public bool HasMessageError => !string.IsNullOrEmpty(MessageError);

        public FurnitureHomeVMUI(IFurniture ifur, INavigator navigator, IAparmentViewModelFactory ViewModelFactory, FurnitureStore apartmentStore)
        {
            _ifur = ifur;
            _navigator = navigator;
            _ViewModelFactory = ViewModelFactory;
            _apartmentStore = apartmentStore;
            _fur = new ObservableCollection<FurnitureVm>();
            LoadDataBase = new LoadFurnitureView(this, apartmentStore);
            LoadDataBase.Execute(null);
            FurnitureNav = new UpdateCurrentViewModelCommand(navigator, ViewModelFactory);
            FurnitureUpdateNav = new RelayCommand(FurnitureUpdateNavDef);
            DeleteConFirm = new FurnitureDeleteCommand(this, apartmentStore, navigator, ViewModelFactory);
            _apartmentStore.FurnitureAdd += Store_Add;
        }

        public void FurnitureUpdateNavDef(object parameter)
        {
            if (parameter is FurnitureVm fur)
            {
                _navigator.CurrentViewModel = new FurnitureUpdateVMUI(_ifur, fur, _navigator, _ViewModelFactory, _apartmentStore);
            }
        }

        public void UpdateData(List<FurnitureVm> data)
        {
            foreach (var furvm in data)
            {
                _fur.Add(furvm);
            }
        }

        private void Store_Add(FurnitureVm data)
        {
            _fur.Add(data);
        }

        private void ChangedString(string _Search)
        {
            IsText = false;

            if (search.Equals(""))
            {
                if (_fur.Any())
                    _fur.Clear();
                LoadDataBase.Execute(null);
                IsText = true;
            }
            else
            {
                if (int.TryParse(search, out int intValue))
                {
                    IsLoading = true;
                    if (_fur.Any())
                        _fur.Clear();
                    LoadDataBase.Execute(null);
                    ObservableCollection<FurnitureVm> find = new ObservableCollection<FurnitureVm>();
                    foreach (FurnitureVm item in _fur)
                        if (item.ID == intValue)
                            find.Add(item);
                    if (_fur.Any())
                        _fur.Clear();
                    foreach (FurnitureVm item in find)
                    {
                        _fur.Add(item);
                    }

                    IsLoading = false;
                }
                else if (_fur.Any()) _fur.Clear();
            }
            OnPropertyChanged(nameof(search));
        }

        /*public void UpdateSelectedFurniture(FurnitureVm data)
        {
            Mediators.Instance.Send("FurnitureSelected", data);
        }*/
    }
}