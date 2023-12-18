using AM.UI.Command;
using AM.UI.Command.Room;
using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.View.Dialog;
using AM.UI.View.RoomDetails;
using AM.UI.View.Rooms;
using AM.UI.ViewModelUI.Factory;
using AM.UI.ViewModelUI.Room;
using AM.UI.ViewModelUI.RoomDetails;
using ClosedXML.Excel;
using Data;
using Data.Entity;
using Data.Relationships;
using MaterialDesignColors;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Identity.Client;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using ViewModel.DepositsContract;
using ViewModel.Dtos;
using ViewModel.Furniture;
using ViewModel.People;
using ViewModel.Room;
using ViewModel.RoomDetails;
using static System.Net.Mime.MediaTypeNames;

namespace AM.UI.ViewModelUI
{
    public class RoomHomeVMUI : ViewModelBase
    {
        private readonly IRoom _iroom;
        private ObservableCollection<RoomVm> _room;

        private readonly IAparmentViewModelFactory _viewModelFactory;
        private readonly RoomVm _RoomViewModel;
        private readonly ApartmentDbContextFactory _factory;
        private readonly RoomDetailsHomeVMUI _roomDetailsHomeVMUI;
        private readonly RoomStore _apartmentStore;
        private readonly ComboboxStore _ComboboxStore;

        private readonly INavigator _navigator;
        public ICommand RoomNavCommand { get; }
        public ICommand RoomUpdateNavCommand { get; }

        public ICommand RoomDetailsCommand { get; }

        public ICommand RoomDeleteCommand { get; }

        public ICommand RoomDeleteCommandConfirm { get; }

        public ICommand FindingRoomCommand { get; }

        public ICommand LoadDataBase { get; }

        public ICommand MouseEnterCommand { get; set; }

        public ICommand MouseLeaveCommand { get; set; }

        public ICommand ExportExcelCommand { get; set; }

        public string _Search = "...";

        public bool HasData => _room.Any();

        public int _IDFind;

        public int IDFind
        {
            get { return _IDFind; }
            set
            {
                _IDFind = value;
                OnPropertyChanged(nameof(IDFind));
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

        private string _messageError;

        public string MessageError
        {
            get { return _messageError; }
            set
            {
                _messageError = value;
                OnPropertyChanged(nameof(MessageError));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        private RoomVm _SelectRoom;

        public RoomVm SelectRoom
        {
            get { return _SelectRoom; }
            set
            {
                _SelectRoom = value;
                OnPropertyChanged(nameof(SelectRoom));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(MessageError);

        private bool _isText;

        public bool IsText
        {
            get
            {
                return _isText;
            }
            set
            {
                _isText = value;
                OnPropertyChanged(nameof(IsText));
            }
        }

        public IEnumerable<RoomVm> Room => _room;

        private bool _isLoading;

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private int _ID;

        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        private string _LeaderNameToToolTip;

        public string LeaderNameToToolTip
        {
            get { return _LeaderNameToToolTip; }
            set
            {
                _LeaderNameToToolTip = value;
                OnPropertyChanged(nameof(LeaderNameToToolTip));
            }
        }

        private bool _ShowComment = true;

        public bool ShowComment
        {
            get { return _ShowComment; }
            set
            {
                _ShowComment = value;
                OnPropertyChanged(nameof(ShowComment));
            }
        }

        private string _TextForMouseEnter;

        public string TextForMouseEnter
        {
            get { return _TextForMouseEnter; }
            set
            {
                _TextForMouseEnter = value;
                OnPropertyChanged(nameof(TextForMouseEnter));
            }
        }

        public RoomHomeVMUI(IRoom iroom, INavigator navigator, IAparmentViewModelFactory viewModelFactory, RoomStore apartmentStore, ApartmentDbContextFactory factory, RentalContractHomeVMUI rentalVMUI, ComboboxStore comboboxStore)
        {
            _iroom = iroom;
            _viewModelFactory = viewModelFactory;
            _navigator = navigator;
            _room = new ObservableCollection<RoomVm>();
            _factory = factory;
            _ComboboxStore = comboboxStore;
            LoadDataBase = new LoadRoomView(this, apartmentStore);
            LoadDataBase.Execute(null);
            RoomNavCommand = new UpdateCurrentViewModelCommand(navigator, viewModelFactory);
            _apartmentStore = apartmentStore;
            RoomUpdateNavCommand = new RelayCommand(DataRoomUpdate);
            RoomDeleteCommandConfirm = new RoomDeleteCommand(this, apartmentStore, navigator, viewModelFactory);
            RoomDetailsCommand = new RelayCommand(ShowRoomDetails);
            _room.CollectionChanged += OnReservationsChanged;
            _apartmentStore.RoomAdd += Store_Add;
            _apartmentStore.RoomDelete += Delete_Store;
            MouseEnterCommand = new RelayCommand(SetShowCommentTrue);
            MouseLeaveCommand = new RelayCommand(SetShowCommentFalse);

            // ExportExcelCommand = new RelayCommand(Export);
        }

        /*public void Export(object parameter)
        {
            DataTable RoomTable = convert.Convert(_room);
            ExportExcel(RoomTable);
        }*/

        public void SetShowCommentTrue(object parameter)
        {
            _TextForMouseEnter = "This is Details";
            _ShowComment = true;
        }

        public void SetShowCommentFalse(object parameter)
        {
            _ShowComment = false;
        }

        public void Test(object parameter)
        {
            MessageBox.Show(_SelectRoom.NameLeader);
        }

        private void Store_Add(RoomVm data)
        {
            _room.Add(data);
        }

        public void ShowRoomDetails(object parameter)
        {
            if (parameter is RoomVm room)
            {
                _navigator.CurrentViewModel = new RoomDetailsHomeVMUI(room, _navigator, _viewModelFactory, _apartmentStore);
            }
        }

        public async void Delete_Store(int id)
        {
            var object1 = _room.FirstOrDefault(x => x.ID == id);
            if (object1 != null)
            {
                _room.Remove(object1);
            }
        }

        public void UpdateData(List<RoomVm> data)
        {
            data.ForEach(x => _room.Add(x));
        }

        public void DataRoomUpdate(object parameter)
        {
            if (parameter is RoomVm r)
            {
                _navigator.CurrentViewModel = new RoomUpdateVMUI(_iroom, r, _navigator, _viewModelFactory, _apartmentStore, _ComboboxStore);
            }
        }

        public void UpdateRoomWhenCreateDeposit(DepositsContractVm data)
        {
            RoomVm room = new RoomVm()
            {
                NameLeader = data.LeaderName
            };
            var current = _room.ToList().FindIndex(x => x.NameLeader == data.LeaderName);
            if (current != -1)
            {
                _room[current].NameLeader = room.NameLeader;
            }
        }

        private void ChangedString(string _Search)
        {
            IsText = false;

            if (search.Equals(""))
            {
                if (_room.Any())
                    _room.Clear();
                LoadDataBase.Execute(null);
                IsText = true;
            }
            else
            {
                if (int.TryParse(search, out int intValue))
                {
                    IsLoading = true;
                    if (_room.Any())
                        _room.Clear();
                    LoadDataBase.Execute(null);
                    ObservableCollection<RoomVm> find = new ObservableCollection<RoomVm>();
                    foreach (RoomVm item in _room)
                        if (item.ID == intValue)
                            find.Add(item);
                    if (_room.Any())
                        _room.Clear();
                    foreach (RoomVm item in find)
                    {
                        _room.Add(item);
                    }

                    IsLoading = false;
                }
                else if (_room.Any()) _room.Clear();
            }
            OnPropertyChanged(nameof(search));
        }

        public void ExportExcel(DataTable RoomTable)
        {
            using (var Workbook = new XLWorkbook())
            {
                var Worksheet = Workbook.Worksheets.Add("Room");
                for (int i = 0; i < RoomTable.Columns.Count; i++)
                {
                    Worksheet.Cell(i, i + 1).Value = RoomTable.Columns[i].ColumnName;
                }
                for (int i = 0; i < RoomTable.Rows.Count; i++)
                {
                    for (int j = 0; i < RoomTable.Columns.Count; j++)
                    {
                        Worksheet.Cell(i + 2, j + 1).Value = (XLCellValue)RoomTable.Rows[i][j];
                    }
                }
                Workbook.SaveAs("Roomdata.xlsx");
            }
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }

        public RoomVm RoomV
        {
            get => _RoomViewModel;
            set
            {
                RoomV = value;
                OnPropertyChanged();
            }
        }
    }
}