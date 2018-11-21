using konito_project.Classess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Utils.Command {
    public static class Commands {
        public static readonly WindowHandlerCommand CloseCommand = new WindowHandlerCommand(w => w.Close());
        public static readonly ButtonCommand OpenDbDirCommand = new ButtonCommand(() => Process.Start($"{AppDomain.CurrentDomain.BaseDirectory}/db/"));
    }
}
