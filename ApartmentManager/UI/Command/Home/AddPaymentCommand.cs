using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.PaymentExtension;

namespace AM.UI.Command.Home
{
    public class AddPaymentCommand : AsyncCommandBase
    {
        private readonly HomeBillListingViewModel viewModel;
        private readonly HomeStore homeStore;

        public AddPaymentCommand(HomeBillListingViewModel viewModel, HomeStore homeStore)
        {
            this.viewModel=viewModel;
            this.homeStore=homeStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            InputMessage message = new InputMessage();
            message.ShowDialog();
            if (message.Input != DateTime.MinValue)
            {
                PaymentExtensionCreateViewModel request = new PaymentExtensionCreateViewModel
                {
                    IDBill = viewModel.selectItem.ID,
                    Days = message.Input,
                };
                int result = await homeStore.AddPayment(request);
                if (result ==1)
                {
                    viewModel.LoadDatabase.Execute(null);
                    new MessageBoxCustom("Add Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
                else if (result==0)
                {
                    new MessageBoxCustom("Add Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                }
            }
        }
    }
}