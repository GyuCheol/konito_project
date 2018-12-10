using konito_project.WorkBook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace konito_project.ViewModel {
    public abstract class ViewModelBase: INotifyPropertyChanged {
        // Child class may use this event.
        #pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelBase() {
            InitWorkbook();
            InitCmd();
            
        }

        protected void NotifyChanged(string propName) {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        protected abstract void InitWorkbook();

        protected abstract void InitCmd();

    }
}
