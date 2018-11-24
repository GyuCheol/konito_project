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

        }

    }
}
