using AM.UI.State;
using AM.UI.State.Store;
using AM.UI.ViewModelUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AM.UI.Command.LoadDataBase
{
    public class LoadFurnitureView : AsyncCommandBase
    {
        private readonly FurnitureHomeVMUI _furniturehomevm;
        private readonly FurnitureStore _furniture;

        public LoadFurnitureView(FurnitureHomeVMUI furniturehomevm, FurnitureStore furniture)
        {
            _furniturehomevm = furniturehomevm;
            _furniture = furniture;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _furniturehomevm.IsLoading = true ;
            try
            {
               
                await _furniture.LoadFurniture();
                _furniturehomevm.UpdateData(_furniture.fuuniturevm);
            }
            catch (Exception)
            {
                _furniturehomevm.MessageError = "Error to Load Furniture Database";
            }
            _furniturehomevm.IsLoading = false;
        }
    }
}
