using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace konito_project.Commands {

    public class ActionCommand : ICommand {
        public event EventHandler CanExecuteChanged;

        private Action action;

        public bool CanExecute(object parameter) {
            return true;
        }

        public ActionCommand(Action action) {
            this.action = action;
        }

        public void Execute(object parameter) {
            action();
        }
    }
}
