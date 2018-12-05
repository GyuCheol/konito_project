using konito_project.Commands;
using konito_project.WorkBook;
using konito_project.Images;
using konito_project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace konito_project.ViewModel {
    public class EmployeeRegistViewModel : ViewModelBase {
        private RegistMode CurrentMode;
        private OpenFileDialog dialog;

        public IEnumerable<string> Position => new string[] { "인턴", "사원", "대리", "과장", "차장", "부장", "팀장", "대표" };
        
        public Employee CurrentEmployee { get; private set; }

        public ICommand ImageRegisterCommand { get; private set; }

        public EmployeeRegistViewModel(): base() {
            CurrentMode = RegistMode.Append;
            CurrentEmployee = new Employee() {
                Id = 0
            };

            SelectedPosition = Position.First();
        }

        public string SelectedPosition { get; set; }

        private void RegistNewImage() {
            dialog.ShowDialog();

            if (string.IsNullOrEmpty(dialog.FileName))
                return;

            ImageManager.AddImage(dialog.FileName);
        }

        public EmployeeRegistViewModel(int empId) {
            CurrentMode = RegistMode.Edit;
        }

        protected override void InitCmd() {
            ImageRegisterCommand = new ActionCommand(RegistNewImage);
        }

        protected override void InitWorkbook() {
            dialog = new OpenFileDialog();

            dialog.Multiselect = false;
            dialog.Filter = "Image files|*.jpg;*.png";
        }
    }
}
