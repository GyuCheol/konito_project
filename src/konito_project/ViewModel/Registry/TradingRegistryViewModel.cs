using konito_project.Commands;
using konito_project.Model;
using konito_project.Utils;
using konito_project.WorkBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace konito_project.ViewModel.Registry {

    public class TradingRegistryViewModel : ViewModelBase {

        public ICommand SaveCommand => new ActionCommand(Save);

        public AccountType CurrentAccountType { get; set; }
        public Trading CurrentTrading { get; set; }
        public bool IsTax { get; set; } = true;
        public bool IsSmallTax { get; set; }

        //View 표시 전용
        public double DisplayTax { get { return actualTax * 100; } set { actualTax = value / 100; } }
        
        //실제 저장될 Data 
        private double actualTax = 0.1;

        private RegistMode currentMode;

        public TradingRegistryViewModel(int year, int month, AccountType acType) {
            currentMode = RegistMode.Append;
            CurrentAccountType = acType;
            CurrentTrading = new Trading() {
                Date = new DateTime(year, month, 1),
                TaxType = TaxType.Tax,
                AccountType = acType
            };
        }

        public TradingRegistryViewModel(Trading tr, AccountType acType) {
            currentMode = RegistMode.Edit;
            CurrentAccountType = acType;
            CurrentTrading = tr;
            IsTax = (CurrentTrading.TaxType == TaxType.Tax) ? true : false;
            actualTax = tr.Tax;
        }

        private void Save() {
            ValidateErrorHandler errChecker = CurrentTrading.ValidateRequired();
            CurrentTrading.TaxType = (IsTax) ? TaxType.Tax : TaxType.TaxSmall;

            if (errChecker.HasProblem) {
                MessageBox.Show($"{errChecker.ErrMsg}를(을) 입력해주세요!", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            TradingWorkBook wb = new TradingWorkBook(CurrentTrading.Date.Year, CurrentAccountType, CurrentTrading.Date.Month);
            CurrentTrading.Tax = actualTax;

            if (currentMode == RegistMode.Append) {
                CurrentTrading.Id = wb.GetNewRecordId();
                wb.AddRecord(CurrentTrading);
            }
            else {
                wb.EditRecordById(CurrentTrading, CurrentTrading.Id);
            }

            System.Windows.MessageBox.Show("저장이 완료되었습니다!", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
            UtilCommands.CloseCommand.Execute(null);

        }

    }

}
