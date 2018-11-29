using konito_project.Commands;
using konito_project.Excel;
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

namespace konito_project.ViewModel {
    public class AccountRegistViewModel: ViewModelBase {
        public ObservableCollection<NotifyWrapper<Account>> PurchaseList { get; private set; } = new ObservableCollection<NotifyWrapper<Account>>();
        public ObservableCollection<NotifyWrapper<Account>> SalesList { get; private set; } = new ObservableCollection<NotifyWrapper<Account>>();
        
        public ICommand SaveCommand { get; private set; }

        public ICommand PurchaseAddCommand { get; private set; }
        public ICommand PurchaseDeleteCommand { get; private set; }
        public ICommand PurchaseEditCommand { get; private set; }

        public ICommand SalesAddCommand { get; private set; }
        public ICommand SalesDeleteCommand { get; private set; }
        public ICommand SalesEditCommand { get; private set; }

        public string PurchaseText { get; set; }
        public string SalesText { get; set; }

        public NotifyWrapper<Account> SelectedPurchase { get; set; }
        public NotifyWrapper<Account> SelectedSales { get; set; }

        public AccountRegistViewModel(): base() {}

        private void SaveAllRecords() {
            var accounts = PurchaseList.Concat(SalesList);

            AccountWorkBook.AddReocrds(accounts.Select(x => x.Data));

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
            foreach (var account in AccountWorkBook.GetAllRecords()) {
                (account.AccountType == AccountType.Purchase ? PurchaseList : SalesList)
                    .Add(new NotifyWrapper<Account>(account, nameof(Account.Name)));
            }
        }

        protected override void InitWorkbook() {
            InitList();
        }

        protected override void InitCmd() {
            PurchaseAddCommand = new ActionCommand(() => AddAccount(AccountType.Purchase));
            PurchaseDeleteCommand = new ActionCommand(() => RemoveAccount(AccountType.Purchase));
            PurchaseEditCommand = new ActionCommand(() => EditRecord(AccountType.Purchase));

            SalesAddCommand = new ActionCommand(() => AddAccount(AccountType.Sales));
            SalesDeleteCommand = new ActionCommand(() => RemoveAccount(AccountType.Sales));
            SalesEditCommand = new ActionCommand(() => EditRecord(AccountType.Sales));

            SaveCommand = new ActionCommand(SaveAllRecords);
        }
    }
}
