using AM.UI.Command.RoomImages;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using ViewModel.Room;
using ViewModel.RoomDetails;
using ViewModel.RoomImage;

namespace AM.UI.ViewModelUI.RoomDetails
{
    public class RoomDetailsAddImageVMUI : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewModelFactory;
        private readonly RoomStore _apartmentStore;

        private ObservableCollection<RoomImageCreateViewModel> _tempimages;
        public IEnumerable<RoomImageCreateViewModel> tempimages => _tempimages;

        private RoomVm _detailsImage;

        public RoomVm detailsImage
        {
            get { return _detailsImage; }
            set
            {
                detailsImage = value;
                OnPropertyChanged(nameof(RoomVm));
            }
        }

        public ObservableCollection<RoomImageCreateViewModel> images
        {
            get { return _tempimages; }
            set
            {
                _tempimages = value;
                OnPropertyChanged(nameof(tempimages));
            }
        }

        public ICommand SelectedFile { get; }

        public ICommand SelectedFileSuccessCommand { get; }

        public ICommand SelectedFileConfirm { get; }

        public ICommand RemoveItem { get; }

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

        private int _IDroom;

        public int IDRoom
        {
            get { return _IDroom; }
            set
            {
                _IDroom = value;
                OnPropertyChanged(nameof(IDRoom));
            }
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _name;

        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        private string selectedFilePath;

        public string SelectedFilePath
        {
            get { return selectedFilePath; }
            set
            {
                if (selectedFilePath != value)
                {
                    selectedFilePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HasData => _tempimages.Any();

        public RoomDetailsAddImageVMUI(INavigator navigator, IAparmentViewModelFactory viewModelFactory, RoomStore apartmentStore, RoomVm image)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _apartmentStore = apartmentStore;
            _detailsImage = image;
            SelectedFile = new RelayCommand(SelectFile);
            RemoveItem = new RelayCommand(RemoveImageItem);
            _tempimages = new ObservableCollection<RoomImageCreateViewModel>();
            SelectedFileConfirm = new AddRoomImageCommand(this, navigator, viewModelFactory, apartmentStore);
            _tempimages.CollectionChanged += OnReservationsChanged;
        }

        public void RemoveImageItem(object parameter)
        {
            var item = _tempimages.FirstOrDefault(x => x.Name.Equals(name));
            _tempimages.Remove(item);
        }

        public void SelectFile(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Thiết lập các tùy chọn hộp thoại chọn tệp
            openFileDialog.Title = "Chọn Tệp";
            openFileDialog.Filter = "Tất cả các tệp|*.*"; // Có thể thay đổi bộ lọc tệp theo nhu cầu của bạn

            // Hiển thị hộp thoại chọn tệp và xử lý kết quả
            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                SelectedFilePath = openFileDialog.FileName;

                RoomImageCreateViewModel roomimage = new RoomImageCreateViewModel
                {
                    IDroom = detailsImage.ID,
                    Name = Name,
                    FileName = openFileDialog.SafeFileName,
                    Url = SelectedFilePath
                };
                _tempimages.Add(roomimage);
            }
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}