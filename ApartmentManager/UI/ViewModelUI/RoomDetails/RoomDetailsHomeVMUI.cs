using AM.UI.Command;
using AM.UI.Command.LoadDataBase;
using AM.UI.Command.RoomImages;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI.Factory;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ViewModel.Furniture;
using ViewModel.Room;
using ViewModel.RoomDetails;
using System.Drawing;
using System.Windows.Controls;
using AM.UI.View.RoomDetails;
using Caliburn.Micro;
using Data.Enum;
using Data.Entity;
using Services.Interface;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Identity.Client;
using System.Windows.Media.Animation;
using AM.UI.Command.RoomFurniture;

namespace AM.UI.ViewModelUI.RoomDetails
{
    public class RoomDetailsHomeVMUI : ViewModelBase
    {
        private readonly RoomVm _LoadInformationRoom;
        private ObservableCollection<RoomDetailsFurniture> _roomdetails;
        private ObservableCollection<RoomDetailsImage> _roomdetailsimage;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _ViewModelFactory;
        private readonly RoomStore _apartmentStore;
        public ObservableCollection<string> ComboBoxItems { get; set; }

        //command
        public ICommand LoadDataBase { get; }

        public ICommand RoomDetailsAddImageNav { get; }

        public ICommand RoomDetailsNavToCustomerInfor { get; }

        public ICommand DeleteImageCommand { get; }
        public ICommand DeleteImageConfirmCommand { get; }

        public ICommand ShowLargeImageNav { get; }

        public ICommand AddFurnitureSuccess { get; }
        public ICommand AddFurnitureConFirm { get; }

        public ICommand DeleteFurnitureSuccess { get; }
        public ICommand DeleteFurnitureConFirm { get; }

        //Properties

        public RoomVm LoadInformationRoom
        {
            get { return _LoadInformationRoom; }
            set
            {
                LoadInformationRoom = value;
                OnPropertyChanged(nameof(RoomVm));
            }
        }

        public bool HasData => _roomdetailsimage.Any();

        private bool _FlagImage = false;

        public bool FlagImage
        {
            get { return _FlagImage; }
            set
            {
                _FlagImage = value;
                OnPropertyChanged(nameof(FlagImage));
            }
        }

        private bool _FlagFurniture = false;

        public bool FlagFurniture
        {
            get { return _FlagFurniture; }
            set
            {
                _FlagFurniture = value;
                OnPropertyChanged(nameof(FlagFurniture));
            }
        }

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

        public bool HasMessageError => !string.IsNullOrEmpty(MessageError);

        public ObservableCollection<FurnitureVm> _comboBoxfurniture;

        public IEnumerable<FurnitureVm> comboBoxfurniture => _comboBoxfurniture;

        public IEnumerable<RoomDetailsFurniture> RoomDetails => _roomdetails;

        public ObservableCollection<RoomDetailsImage> RoomDetailsImage => _roomdetailsimage;

        private RoomDetailsImage _RoomImages;

        public RoomDetailsImage RoomImages
        {
            get { return _RoomImages; }
            set
            {
                _RoomImages = value;
                OnPropertyChanged(nameof(RoomImages));
                SetFlagImage();
            }
        }

        private RoomDetailsFurniture _SelectListViewFurniture;

        public RoomDetailsFurniture SelectListViewFurniture
        {
            get { return _SelectListViewFurniture; }
            set
            {
                _SelectListViewFurniture = value;
                OnPropertyChanged(nameof(SelectListViewFurniture));
                SetFlagFurniture();
            }
        }

        private FurnitureVm _SelectedFurniture;

        public FurnitureVm SelectedFurniture
        {
            get { return _SelectedFurniture; }
            set
            {
                _SelectedFurniture = value;

                OnPropertyChanged(nameof(SelectedFurniture));
            }
        }

        public void SetFlagImage()
        {
            if (_RoomImages != null)
                _FlagImage = true;
            else
            {
                _FlagImage = false;
            }
            OnPropertyChanged(nameof(FlagImage));
        }

        public void SetFlagFurniture()
        {
            if (_SelectListViewFurniture != null)
            {
                _FlagFurniture = true;
            }
            else
            {
                _FlagFurniture = false;
            }
            OnPropertyChanged(nameof(FlagFurniture));
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

        private int _IdDelete;

        public int IdDelete
        {
            get { return _IdDelete; }
            set
            {
                _IdDelete = value;
                OnPropertyChanged(nameof(IdDelete));
            }
        }

        private Furniture _Furniture;

        public Furniture Furniture
        {
            get { return _Furniture; }
            set
            {
                _Furniture = value;
                OnPropertyChanged(nameof(Furniture));
            }
        }

        public RoomDetailsHomeVMUI(RoomVm id, INavigator navigator, IAparmentViewModelFactory ViewModelFactory, RoomStore apartmentStore)
        {
            _navigator = navigator;
            _ViewModelFactory = ViewModelFactory;
            _apartmentStore = apartmentStore;
            _IdRoom = id;
            _roomdetails = new ObservableCollection<RoomDetailsFurniture>();
            _LoadInformationRoom = id;

            _roomdetailsimage = new ObservableCollection<RoomDetailsImage>();
            _comboBoxfurniture = new ObservableCollection<FurnitureVm>();
            LoadDataBase = new LoadRoomDetailsView(this, apartmentStore, id.ID);
            LoadDataBase.Execute(null);
            RoomDetailsNavToCustomerInfor = new RelayCommand(NavToCusInfor);

            DeleteImageCommand = new RelayCommand(DeleteImage);
            DeleteImageConfirmCommand = new DeleleRoomImageCommand(navigator, ViewModelFactory, apartmentStore, this);
            ShowLargeImageNav = new RelayCommand(ShowLargeImage);
            RoomDetailsAddImageNav = new RelayCommand(AddImage);

            AddFurnitureSuccess = new RoomFurnitureAddCommand(this, navigator, ViewModelFactory, apartmentStore);
            AddFurnitureConFirm = new RelayCommand(AddFurniture);
            DeleteFurnitureSuccess = new RoomFurnitureDeleteCommand(navigator, ViewModelFactory, apartmentStore, this);
            DeleteFurnitureConFirm = new RelayCommand(DeleteFurniture);

            _roomdetails.CollectionChanged += OnReservationsChanged;
            _apartmentStore.RoomImageDelete += DeleteImage_Store;
            _apartmentStore.RoomFurnitureCreate += AddFurniture_Store;
            _apartmentStore.RoomFurnitureDelete += DeleteFurniture_Store;

            _comboBoxfurniture.Add(new FurnitureVm { ID = 0, Name = "None" });
            _comboBoxfurniture.CollectionChanged += OnReservationsChanged;
        }

        public RoomVm _IdRoom;

        public void ShowLargeImage(object parameter)
        {
            new RoomDetailsShowLargeImage(BitmapImageFromFile(RoomImages.Url)).Show();
        }

        public static BitmapSource BitmapImageFromFile(string filepath)
        {
            BitmapImage bi = new BitmapImage();

            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad; //here
            bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache; //and here
            bi.UriSource = new Uri(filepath, UriKind.RelativeOrAbsolute);
            bi.EndInit();

            return bi;
        }

        public void DeleteImage(object parameter)
        {
            bool? Confirm = new MessageBoxCustom($"Do you want to delete Image :{RoomImages.IDImage} ", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (Confirm == true)
            {
                if (File.Exists(_RoomImages.Url))
                {
                    File.Delete(_RoomImages.Url);
                }
                DeleteImageConfirmCommand.Execute(null);
            }
        }

        public void AddImage(object parameter)
        {
            _navigator.CurrentViewModel = new RoomDetailsAddImageVMUI(_navigator, _ViewModelFactory, _apartmentStore, _LoadInformationRoom);
        }

        public void DeleteFurniture(object parameter)
        {
            bool? Confirm = new MessageBoxCustom($"Do you want to delete Furniture :{SelectListViewFurniture.IdFur} ", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (Confirm == true)
            {
                DeleteFurnitureSuccess.Execute(null);
            }
        }

        public void AddFurniture(object parameter)
        {
            AddFurnitureSuccess.Execute(null);
        }

        ///Store

        public async void DeleteFurniture_Store(int id)
        {
            var object1 = _roomdetails.FirstOrDefault(x => x.IdFur == id);
            if (object1 != null)
            {
                _roomdetails.Remove(object1);
            }
        }

        public async void DeleteImage_Store(int id)
        {
            var object1 = _roomdetailsimage.FirstOrDefault(x => x.IDImage == id);
            if (object1 != null)
            {
                _roomdetailsimage.Remove(object1);
            }
        }

        public void UpdateDataImage(List<RoomDetailsImage> data)
        {
            _roomdetailsimage.Clear();
            foreach (var roomdetail in data)
            {
                _roomdetailsimage.Add(roomdetail);
            }
        }

        public void NavToCusInfor(object parameter)
        {
            _navigator.CurrentViewModel = new RoomDetailsInformationCustomerVMUI(_navigator, _ViewModelFactory, _apartmentStore, _LoadInformationRoom);
        }

        public void AddFurniture_Store(RoomDetailsFurniture data)
        {
            _roomdetails.Add(data);
        }

        public void UpdateDataComboBoxFurniture(List<FurnitureVm> data)
        {
            data.ForEach(x => _comboBoxfurniture.Add(x));
        }

        public void UpdateDataFurniture(List<RoomDetailsFurniture> data)
        {
            foreach (var roomdetail in data)
            {
                _roomdetails.Add(roomdetail);
            }
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}