using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace konito_project.Commands {
    public class ActionParamCommand<T> : ICommand where T : class {
        public event EventHandler CanExecuteChanged;

        private Action<T> action;

        public ActionParamCommand(Action<T> action) {
            this.action = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) {
            action(parameter as T);
        }
    }
}
