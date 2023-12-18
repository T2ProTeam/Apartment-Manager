using AM.UI.State;
using AM.UI.State.Store;
using AM.UI.ViewModelUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Command.LoadDataBase
{
    public class LoadBillView : AsyncCommandBase
    {
        private readonly BillHomeVMUI _billhomevm;
        private readonly BillStore _bill;

        public LoadBillView(BillHomeVMUI billhomevm, BillStore bill)
        {
            _billhomevm = billhomevm;
            _bill = bill;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _billhomevm.Isloading = true;
            try
            {
                await _bill.LoadBill();
                _billhomevm.UpdateData(_bill.billvm);
            }catch (Exception)
            {
                _billhomevm.MessageError = "Error to Load Bill Database";
            }
            _billhomevm.Isloading = false;
        }
    }
}
