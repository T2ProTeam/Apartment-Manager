using AM.UI.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Utilities
{
    public class RelayAsyncCommand : AsyncCommandBase
    {
        private readonly Func<Task> _callback;

        public RelayAsyncCommand(Func<Task> callback)
        {
            _callback=callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _callback();
        }
    }
}