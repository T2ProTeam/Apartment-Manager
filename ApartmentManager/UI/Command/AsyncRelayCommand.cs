using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Command
{
    public class AsyncRelayCommand : AsyncCommandBase
    {
        public readonly Func<Task> _callback;
        public AsyncRelayCommand(Func<Task> callback)
        {
            _callback = callback;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            await _callback();
        }
    }
}
