using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.ViewModel {
    public class EmployeeRegistViewModel : ViewModelBase {

        private RegistMode CurrentMode;

        public EmployeeRegistViewModel(): base() {
            CurrentMode = RegistMode.Append;
        }

        public EmployeeRegistViewModel(int empId) {
            CurrentMode = RegistMode.Edit;
        }

        protected override void InitCmd() {
            
        }

        protected override void InitWorkbook() {
            
        }
    }
}
