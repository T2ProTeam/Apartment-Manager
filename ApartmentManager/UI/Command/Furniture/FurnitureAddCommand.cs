using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI.Factory;
using AM.UI.ViewModelUI.Furnitures;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Furniture;

namespace AM.UI.Command.Furniture
{
    public class FurnitureAddCommand : AsyncCommandBase
    {
        private readonly FurnitureStore _store;
        private readonly FurnitureAddVMUI _FurnitureAddvmui;
        private readonly IAparmentViewModelFactory _factory;
        private readonly INavigator _navigator;
        public FurnitureAddCommand(FurnitureAddVMUI furnitureAddVMUI, FurnitureStore store, IAparmentViewModelFactory factory, INavigator navigator)
        {
            _store = store;
            _factory = factory;
            _FurnitureAddvmui = furnitureAddVMUI;
            _navigator = navigator;
            _FurnitureAddvmui.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            FurnitureCreateViewModel model = new FurnitureCreateViewModel
            {
                ID = _FurnitureAddvmui.ID,
                Name = _FurnitureAddvmui.Name
            };
            var result = await _store.CreateFurniture(model);
            if (result != null)
            {
                new MessageBoxCustom("Add Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _navigator.CurrentViewModel = _factory.CreateViewModel(ViewType.Furnitures);
            }
            else
            {
                new MessageBoxCustom("Fail", MessageType.Success, MessageButtons.Ok).ShowDialog();
            }


        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}
