using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace konito_project.Classess {

    public class WindowHandlerCommand : ICommand {
        public event EventHandler CanExecuteChanged;

        private Action<Window> action;

        public WindowHandlerCommand(Action<Window> action) {
            this.action = action;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            action(parameter as Window);
        }
    }

}
