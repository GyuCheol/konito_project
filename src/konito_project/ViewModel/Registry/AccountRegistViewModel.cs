using konito_project.Commands;
using konito_project.WorkBook;
using konito_project.Model;
using konito_project.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace konito_project.ViewModel.Registry {
    public class AccountRegistViewModel: ViewModelBase {
        private AccountWorkBook workBook = new AccountWorkBook();

        public ObservableCollection<NotifyWrapper<Account>> PurchaseList { get; private set; } = new ObservableCollection<NotifyWrapper<Account>>();
        public ObservableCollection<NotifyWrapper<Account>> SalesList { get; private set; } = new ObservableCollection<NotifyWrapper<Account>>();
        
        public ICommand SaveCommand => new ActionCommand(SaveAllRecords);
        public ICommand PurchaseAddCommand => new ActionCommand(() => AddAccount(AccountType.Purchase));
        public ICommand PurchaseDeleteCommand => new ActionCommand(() => RemoveAccount(AccountType.Purchase));
        public ICommand PurchaseEditCommand => new ActionCommand(() => EditRecord(AccountType.Purchase));
        public ICommand SalesAddCommand => new ActionCommand(() => AddAccount(AccountType.Sales));
        public ICommand SalesDeleteCommand => new ActionCommand(() => RemoveAccount(AccountType.Sales));
        public ICommand SalesEditCommand => new ActionCommand(() => EditRecord(AccountType.Sales));

        public string PurchaseText { get => purchaseText; set {
                purchaseText = value;
                NotifyChanged(nameof(PurchaseText));
            }
        }
        public string SalesText { get => salesText; set {
                salesText = value;
                NotifyChanged(nameof(SalesText));
            }
        }

        public NotifyWrapper<Account> SelectedPurchase { get; set; }
        public NotifyWrapper<Account> SelectedSales { get; set; }

        private string purchaseText { get; set; }
        private string salesText { get; set; }

        public AccountRegistViewModel(): base() {}

        protected override void InitWorkbook() {
            InitList();
        }

        private void SaveAllRecords() {
            workBook.ClearAllRecords();
            workBook.AddRecords(PurchaseList.Concat(SalesList).Select(x => x.Data));

            MessageBox.Show("저장되었습니다.", "성공", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddAccount(AccountType type) {
            var list = type == AccountType.Purchase ? PurchaseList : SalesList;
            var text = type == AccountType.Purchase ? PurchaseText : SalesText;

            if (list.FirstOrDefault(acc => acc.Text == text) != null) {
                MessageBox.Show("이미 존재하는 계정입니다.", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            list.Add(new NotifyWrapper<Account>(HelperMethods.CreateAccount(type, text), nameof(Account.Name)));
        }

        private void EditRecord(AccountType type) {
            var account = type == AccountType.Purchase ? SelectedPurchase : SelectedSales;
            var text = type == AccountType.Purchase ? PurchaseText : SalesText;

            if (account == null)
                return;

            account.ChangeValue(text);
       }

        private void RemoveAccount(AccountType type) {
            var list = type == AccountType.Purchase ? PurchaseList : SalesList;
            var text = type == AccountType.Purchase ? PurchaseText : SalesText;
            var account = list.FirstOrDefault(x => x.Text == text);

            if (account != null)
                list.Remove(account);
        }

        private void InitList() {
            foreach (var account in workBook.GetAllRecords()) {
                (account.AccountType == AccountType.Purchase ? PurchaseList : SalesList)
                    .Add(new NotifyWrapper<Account>(account, nameof(Account.Name)));
            }
        }

    }
}
