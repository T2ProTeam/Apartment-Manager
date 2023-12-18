using AM.UI.Utilities;
using System;

namespace AM.UI.State.Navigators
{
    public class Navigator : INavigator
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                // _currentViewModel?.Dispose();

                _currentViewModel = value;
                StateChanged?.Invoke();
            }
        }

        private ViewModelBase _CurrentHomeViewModel;

        public ViewModelBase CurrentHomeViewModel
        {
            get
            {
                return _CurrentHomeViewModel;
            }
            set
            {
                //  _CurrentHomeViewModel?.Dispose();

                _CurrentHomeViewModel = value;
                StateChanged?.Invoke();
            }
        }

        // private ComboboxBase _CurrentCombobox;

        public event Action StateChanged;

        /*public ComboboxBase CurrentCombobox
        {
            get { return _CurrentCombobox; }
            set
            {
                _CurrentCombobox = value;
                StateChanged?.Invoke();
            }
        }

        public object LoadCurrentComboBox(ComboBoxType ComboboxType)
        {
            throw new NotImplementedException();
        }*/
    }
}