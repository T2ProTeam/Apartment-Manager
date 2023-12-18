using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.ViewModelUI.Factory;
using AM.UI.ViewModelUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM.UI.View.Dialog;
using System.ComponentModel;

namespace AM.UI.Command.Furniture
{
    public class FurnitureDeleteCommand : AsyncCommandBase
    {
        private readonly FurnitureHomeVMUI _furniturevmui;
        private readonly FurnitureStore _Store;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _factory;

        public FurnitureDeleteCommand(FurnitureHomeVMUI furniturevmui, FurnitureStore store, INavigator navigator, IAparmentViewModelFactory factory)
        {
            _furniturevmui = furniturevmui;
            _Store = store;
            _navigator = navigator;
            _factory = factory;
            _furniturevmui.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var result = await _Store.DeleteFurniture(_furniturevmui.SelectFurniture.ID);
            if (result == true)
            {
                new MessageBoxCustom("Delete Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _navigator.CurrentViewModel = _factory.CreateViewModel(ViewType.Furnitures);
            }
            else
            {
                new MessageBoxCustom("Delete Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }

        public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }

}
