using ClosedXML.Excel;
using konito_project.Exceptions;
using konito_project.Model;
using konito_project.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.WorkBook {
    public class AccountWorkBook: WorkBookBase<Account> {
        public static readonly string[] DEFAULT_PURCHASE = "부품비,원재료비,외주가공비,기타".Split(',');
        public static readonly string[] DEFAULT_SALES = "금형,부품,사출품,기타".Split(',');
        private static readonly string[] COLUMNS = { "구분", "계정명" };

        public override string WorkBookPath => "./db/계정_정보.xlsx";
        public override string[] InitColumns => COLUMNS;
        
        protected override Account CovertToDataFromRow(IXLRow row) {
            if (row == null)
                return null;

            return new Account() {
                AccountType = (AccountType)Enum.Parse(typeof(AccountType), row.Cell(1).GetValue<string>()),
                Name = row.Cell(2).GetValue<string>()
            };
        }

        protected override void InsertRow(IXLRow row, Account account) {
            if (account == null)
                throw new NullReferenceException();

            row.Cell(1).Value = account.AccountType;
            row.Cell(2).Value = account.Name;
        }
    }
}
