using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI;
using AM.UI.ViewModelUI.Furnitures;
using AM.UI.ViewModelUI.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.Furniture;

namespace AM.UI.Command.Furniture
{
    public class FurnitureUpdateCommand : AsyncCommandBase
    {

        private readonly FurnitureUpdateVMUI _furniturevmui;
        private readonly FurnitureStore _apartmentstore;

        public ICommand FurnitureHomeNav { get; }

        public FurnitureUpdateCommand(FurnitureUpdateVMUI furniturevmui, FurnitureStore apartmentstore)
        {
            _furniturevmui = furniturevmui;
            _apartmentstore = apartmentstore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            FurnitureUpdateViewModel furniture = new FurnitureUpdateViewModel
            {
                ID = _furniturevmui.Furniture.ID,
                Name = _furniturevmui.Furniture.Name,
            };
            var result = await _apartmentstore.UpdateFurniture(furniture);
            if (result != null)
            {
                new MessageBoxCustom("Update Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                FurnitureHomeNav.Execute(ViewType.Furnitures);
            }
            else
            {
                new MessageBoxCustom("Update Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }
    }
}

