using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Mediator
{
    public class Mediators
    {
        private static Mediators _instance = new Mediators();
        public static Mediators Instance => _instance;

        private Dictionary<string, Action<object>> _actions = new Dictionary<string, Action<object>>();

        public void Register(string message, Action<object> action)
        {
            if (!_actions.ContainsKey(message))
                _actions[message] = action;
        }

        public void Send(string message, object parameter = null)
        {
            if (_actions.ContainsKey(message))
                _actions[message](parameter);
        }
    }
}
