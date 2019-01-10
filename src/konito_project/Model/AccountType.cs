using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {

    public class AccountType {
        public const string PurchaseText = "매입";
        public const string SalesText = "매출";

        public static readonly AccountType Purchase = new AccountType(PurchaseText);
        public static readonly AccountType Sales = new AccountType(SalesText);

        public string Type { get; private set; }

        private AccountType(string value) {
            Type = value;
        }

        public override string ToString() => Type;
    }

}
