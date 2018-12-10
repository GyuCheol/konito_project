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
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace konito_project.ViewModel {
    public class EmployeeRegistViewModel : ViewModelBase {
        private static readonly ImageSource DEFAULT_IMAGE = new BitmapImage(new Uri("/konito_project;component/Assets/no_image.png", UriKind.RelativeOrAbsolute));

        private RegistMode CurrentMode;
        private OpenFileDialog dialog;
        
        public IEnumerable<string> Position => new string[] { "인턴", "사원", "대리", "과장", "차장", "부장", "팀장", "대표" };

        public Employee CurrentEmployee { get; private set; }

        public ImageSource Image { get; private set; }

        public ICommand ImageRegisterCommand { get; private set; }

        public EmployeeRegistViewModel(): base() {
            CurrentMode = RegistMode.Append;
            CurrentEmployee = new Employee() {
                Id = 0,
                Position = Position.First()
            };

            Image = DEFAULT_IMAGE;
        }
        
        private void RegistNewImage() {
            dialog.ShowDialog();

            if (string.IsNullOrEmpty(dialog.FileName))
                return;

            int imgId = ImageManager.AddImage(dialog.FileName);
            CurrentEmployee.ImgId = imgId;
            
            Image = ImageManager.GetImage(imgId);
            NotifyChanged(nameof(Image));
        }

        public EmployeeRegistViewModel(int empId): base() {
            CurrentMode = RegistMode.Edit;

            throw new NotImplementedException();
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
