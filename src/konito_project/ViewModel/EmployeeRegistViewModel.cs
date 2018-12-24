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
using konito_project.Utils;
using System.Windows;

namespace konito_project.ViewModel {
    public class EmployeeRegistViewModel : ViewModelBase {
        private static readonly ImageSource DEFAULT_IMAGE = new BitmapImage(new Uri("/konito_project;component/Assets/no_image.png", UriKind.RelativeOrAbsolute));

        private RegistMode CurrentMode;
        private OpenFileDialog dialog;
        private EmployeeWorkBook workbook = new EmployeeWorkBook();
        
        public IEnumerable<string> Position => new string[] { "인턴", "사원", "대리", "과장", "차장", "부장", "팀장", "대표" };

        public Employee CurrentEmployee { get; private set; }

        public ImageSource Image { get; private set; }

        public ICommand ImageRegisterCommand => new ActionCommand(RegistNewImage);
        public ICommand SaveCommand => new ActionCommand(ClickSaveCommand);

        public EmployeeRegistViewModel(): base() {
            CurrentMode = RegistMode.Append;
            CurrentEmployee = new Employee() {
                Id = workbook.GetNewRecordId(),
                Position = Position.First(),
                BirthDate = DateTime.Now,
                EnteredDate = DateTime.Now
            };

            Image = DEFAULT_IMAGE;
        }
        
        public EmployeeRegistViewModel(int empId): base() {
            CurrentMode = RegistMode.Edit;

            throw new NotImplementedException();
        }
        
        protected override void InitWorkbook() {
            dialog = new OpenFileDialog();

            dialog.Multiselect = false;
            dialog.Filter = "Image files|*.jpg;*.png";
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

        private void ClickSaveCommand() {
            ValidateErrorHandler validate = CurrentEmployee.ValidateRequired();

            if (validate.HasProblem) {
                System.Windows.MessageBox.Show($"{validate.ErrMsg}를(을) 입력해주세요!", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CurrentEmployee.LastestUpdatedTime = DateTime.Now;

            switch (CurrentMode) {
                case RegistMode.Append:
                    CurrentEmployee.CreatedTime = DateTime.Now;
                    workbook.AddRecord(CurrentEmployee);
                    System.Windows.MessageBox.Show("신규 임직원이 등록되었습니다!", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case RegistMode.Edit:
                    System.Windows.MessageBox.Show("임직원 정보가 수정되었습니다!", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }

            UtilCommands.CloseCommand.Execute(null);
        }
    }
}
