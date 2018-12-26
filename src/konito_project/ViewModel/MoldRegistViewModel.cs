﻿using konito_project.Commands;
using konito_project.Images;
using konito_project.Model;
using konito_project.Utils;
using konito_project.WorkBook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace konito_project.ViewModel {
    public class MoldRegistViewModel : ViewModelBase {
        private RegistMode mode;
        private MoldWorkBook workBook = new MoldWorkBook();
        private OpenFileDialog dialog = Utils.Dialogs.OpenDialogImage_JpgPng;

        public Mold CurrentMold { get; private set; }
        public ImageSource Image { get; private set; } = ImageManager.DEFAULT_IMAGE;
        public ObservableCollection<Client> ClientList { get; private set; }
        public ObservableCollection<Employee> EmployeeList { get; private set; }
        public Client SelectedClient { get; set; }
        public Employee SelectedEmployee { get; set; }
        public ICommand SaveCommand => new ActionCommand(SaveMoldData);
        public ICommand RegistMoldImageCommand => new ActionCommand(RegistMoldImage);

        public MoldRegistViewModel(): base() {
            mode = RegistMode.Append;
            CurrentMold = new Mold() {
                DesignedDate = DateTime.Today,
                LogsticalDate = DateTime.Today,
                Prototype = DateTime.Today,
                UpdatedPrototype = DateTime.Today
            };
        }

        public MoldRegistViewModel(string productCode): base() {
            mode = RegistMode.Edit;
            CurrentMold = workBook.GetDataByStrKeyOrNull(productCode);
        }

        protected override void InitWorkbook() {
            ClientWorkBook clientWorkBook = new ClientWorkBook();
            EmployeeWorkBook empWorkBook = new EmployeeWorkBook();

            ClientList = new ObservableCollection<Client>(clientWorkBook.GetAllRecords().OrderBy(x => x.CompanyName));
            EmployeeList = new ObservableCollection<Employee>(empWorkBook.GetAllRecords().OrderBy(x => x.DisplayName));
        }

        private void SaveMoldData() {
            CurrentMold.ArchitectEmployeeId = SelectedEmployee?.Id ?? 0;
            CurrentMold.RequestedClientId = SelectedClient?.Id ?? 0;

            ValidateErrorHandler validate = CurrentMold.ValidateRequired();

            if (validate.HasProblem == true) {
                System.Windows.MessageBox.Show($"{validate.ErrMsg}를(을) 입력해주세요!", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (workBook.FindDataByStrKeyOrDefault(CurrentMold.ProductCode) != null) {
                System.Windows.MessageBox.Show($"'{CurrentMold.ProductCode}'은 이미 등록된 제번입니다!", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CurrentMold.LastestUpdatedTime = DateTime.Now;

            switch (mode) {
                case RegistMode.Append:
                    CurrentMold.CreatedTime = DateTime.Now;
                    workBook.AddRecord(CurrentMold);
                    System.Windows.MessageBox.Show("신규 금형이 등록되었습니다!", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case RegistMode.Edit:
                    System.Windows.MessageBox.Show("금형 정보가 수정되었습니다!", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }

            UtilCommands.CloseCommand.Execute(null);
        }

        private void RegistMoldImage() {
            if (dialog.ShowDialog() != DialogResult.OK) {
                return;
            }

            int imgId = ImageManager.AddImage(dialog.FileName);
            CurrentMold.ImgId = imgId;

            Image = ImageManager.GetImage(imgId);
            NotifyChanged(nameof(Image));
        }
    }
}
