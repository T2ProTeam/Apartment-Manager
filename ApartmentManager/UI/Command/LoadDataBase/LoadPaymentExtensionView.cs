using AM.UI.State;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Command.LoadDataBase
{
    public class LoadPaymentExtensionView : AsyncCommandBase
    {
        private readonly PaymentExtensionHomeVMUI _paymenthomevm;
        private readonly PaymentExtensionStore _payment;

        public LoadPaymentExtensionView(PaymentExtensionHomeVMUI paymenthomevm, PaymentExtensionStore payment)
        {
            _paymenthomevm = paymenthomevm;
            _payment = payment;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _paymenthomevm.IsLoading = true;
            try
            {
                await _payment.LoadPaymentExtension();
                _paymenthomevm.UpdateData(_payment.paymentextensionvm);
            }
            catch (Exception)
            {
                _paymenthomevm.MessageError = "Error to Load PaymentExtension Database";
            }
            _paymenthomevm.IsLoading = false;
        }
    }
}