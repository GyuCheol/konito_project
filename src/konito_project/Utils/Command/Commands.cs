using konito_project.Classess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace konito_project.Utils.Command {
    public static class Commands {
        public static readonly WindowHandlerCommand CloseCommand = new WindowHandlerCommand(CloseWindowOrCurWnd);
        public static readonly ActionCommand OpenDbDirCommand = new ActionCommand(() => Process.Start($"{AppDomain.CurrentDomain.BaseDirectory}/db/"));

        private static void CloseWindowOrCurWnd(Window w) {
            if(w == null) {
                Application.Current.Windows.OfType<Window>().First(x => x.IsActive).Close();
                return;
            }

            w.Close();
        }

    }
}
