using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace konito_project.ViewModel {
    public abstract class ViewModelBase {

        public ViewModelBase() {
            InitWorkbook();
            InitCmd();
        }

        protected abstract void InitWorkbook();

        protected abstract void InitCmd();

    }
}
