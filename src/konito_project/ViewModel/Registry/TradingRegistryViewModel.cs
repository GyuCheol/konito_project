using konito_project.Commands;
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
using System.Windows.Input;

namespace konito_project.ViewModel.Registry {

    public class TradingRegistryViewModel : ViewModelBase {

        public ICommand SaveCommand => new ActionCommand(Save);

        public AccountType CurrentAccountType { get; set; }
        public Trading CurrentTrading { get; set; }
        public bool IsTax { get; set; } = true;
        public bool IsSmallTax { get; set; }
        public bool IsTaxFree { get; set; }
        public ObservableCollection<string> CompanyNameList { get; private set; }
        public ObservableCollection<string> AccountList { get; private set; }
        public ObservableCollection<string> ProductNameList { get; private set; }

        //View 표시 전용
        public double DisplayTax { get { return actualTax * 100; } set { actualTax = value / 100; } }
        
        //실제 저장될 Data 
        private double actualTax = 0.1;
        private TradingWorkBook workBook;
        private RegistMode currentMode;

        public TradingRegistryViewModel(AccountType acType) {
            CurrentAccountType = acType;
            CompanyNameList = new ObservableCollection<string>(ExcelManager.ClientWorkBook.GetAllRecords().Select(x => x.CompanyName).OrderBy(x => x));
            AccountList = new ObservableCollection<string>(ExcelManager.AccountWorkBook.GetAllRecords().Where(x => x.AccountType == CurrentAccountType).Select(x => x.Name).OrderBy(x => x));
            ProductNameList = new ObservableCollection<string>(ExcelManager.MoldWorkBook.GetAllRecords().Select(x => x.ProductName).OrderBy(x => x));

            workBook = new TradingWorkBook(DateTime.Now.Year, CurrentAccountType, DateTime.Now.Month);
        }

        public TradingRegistryViewModel(int year, int month, AccountType acType) : this(acType) {
            currentMode = RegistMode.Append;
            CurrentTrading = new Trading() {
                Date = DateTime.Now,
                TaxType = TaxType.Tax,
                AccountType = acType
            };

            workBook = new TradingWorkBook(year, CurrentAccountType, month);

        }

        public TradingRegistryViewModel(Trading tr, AccountType acType) : this(acType) {
            currentMode = RegistMode.Edit;
            CurrentTrading = tr;

            IsTax = (CurrentTrading.TaxType == TaxType.Tax) ? true : false;
            IsSmallTax = (CurrentTrading.TaxType == TaxType.TaxSmall) ? true : false;
            IsTaxFree = (CurrentTrading.TaxType == TaxType.TaxFree) ? true : false;
            actualTax = tr.Tax;
        }

        private void Save() {
            ValidateErrorHandler errChecker = CurrentTrading.ValidateRequired();

            if (IsTax) {
                CurrentTrading.TaxType = TaxType.Tax;
            } else if (IsSmallTax) {
                CurrentTrading.TaxType = TaxType.TaxSmall;
            } else {
                CurrentTrading.TaxType = TaxType.TaxFree;
            }
            
            if (errChecker.HasProblem) {
                MessageBox.Show($"{errChecker.ErrMsg}를(을) 입력해주세요!", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            CurrentTrading.Tax = actualTax;

            if (currentMode == RegistMode.Append) {
                CurrentTrading.Id = workBook.GetNewRecordId();
                workBook.AddRecord(CurrentTrading);
            }
            else {
                workBook.EditRecordById(CurrentTrading, CurrentTrading.Id);
            }

            System.Windows.MessageBox.Show("저장이 완료되었습니다!", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
            UtilCommands.CloseCommand.Execute(null);

        }

    }

}
