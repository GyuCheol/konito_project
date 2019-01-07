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
    public class AccountWorkBook: WorkBookManager<Account> {
        public static readonly string[] DEFAULT_PURCHASE = "부품비,원재료비,외주가공비,기타".Split(',');
        public static readonly string[] DEFAULT_SALES = "금형,부품,사출품,기타".Split(',');
        
        public override int KeyColumn => 2;

        public AccountWorkBook() : base("./db/계정_정보.xlsx") {

        }

        protected override void InitExcel(XLWorkbook workbook) {
            var initValues = DEFAULT_PURCHASE.Select(x => new Account() {
                AccountType = AccountType.Purchase,
                Name = x
            }).Concat(DEFAULT_SALES.Select(x => new Account() {
                AccountType = AccountType.Sales,
                Name = x
            }));

            AddRecords(initValues);
        }

    }
}
