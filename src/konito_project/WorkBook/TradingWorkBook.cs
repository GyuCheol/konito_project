using ClosedXML.Excel;
using konito_project.Attributes;
using konito_project.Exceptions;
using konito_project.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.WorkBook {
    public class TradingWorkBook : YearMonthlyWorkBook<Trading> {
        private AccountType accountType;

        protected override string DefaultPath => $"./db/{accountType}실적";

        public TradingWorkBook(int year, int month, AccountType accountType) {
            this.accountType = accountType;
            ChangeYear(year);
            ChangeMonth(month);
        }

        public int GetTotalyPrice() => GetAllRecords().Sum(x => x.Price);

    }
}
