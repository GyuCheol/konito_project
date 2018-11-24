using konito_project.Excel;
using konito_project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.ViewModel {
    public class AccountRegistViewModel {
        public ObservableCollection<Account> PurchaseList { get; private set; } = new ObservableCollection<Account>();
        public ObservableCollection<Account> SalesList { get; private set; } = new ObservableCollection<Account>();

        public AccountRegistViewModel() {
            foreach (var account in AccountWorkBook.GetAllRecords()) {

                switch (account.AccountType) {
                    case Account.Type.Purchase:
                        PurchaseList.Add(account);
                        break;
                    case Account.Type.Sales:
                        SalesList.Add(account);
                        break;
                }

            }
        }

    }
}
