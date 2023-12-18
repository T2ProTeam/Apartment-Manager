using AM.UI.State;
using AM.UI.State.Store;
using AM.UI.ViewModelUI;
using AM.UI.ViewModelUI.DepositContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Command.LoadDataBase
{
    public class LoadDepositContractview : AsyncCommandBase
    {
        private readonly DepositContractHomeVMUI _deposithomevm;
        private readonly DepositContractStore _deposit;

        public LoadDepositContractview(DepositContractHomeVMUI deposithomevm, DepositContractStore deposit)
        {
            _deposithomevm = deposithomevm;
            _deposit = deposit;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _deposithomevm.IsLoading = true;
            try
            {
                await _deposit.LoadDepositsContract();
                _deposithomevm.UpdateData(_deposit.depositsvm);
            }
            catch (Exception)
            {
                _deposithomevm.MessageError = "Error to Load DepositContract Database";
            }
            _deposithomevm.IsLoading = false;
        }
    }
}
