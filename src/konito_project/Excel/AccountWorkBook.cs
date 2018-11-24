using ClosedXML.Excel;
using konito_project.Model;
using konito_project.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Excel {
    public static class AccountWorkBook {
        public const string WORKBOOK_NAME = "./db/계정_정보.xlsx";
        public const string SHEET_NAME = "data";

        public static readonly string[] COLUMNS = {
            "구분",
            "계정명",
            "등록일",
            "수정일"
        };

        public static int GetRecordCount() => WorkBookHelperMethods.GetWorksheetCount(WORKBOOK_NAME, SHEET_NAME);
        public static IEnumerable<Account> GetAllRecords() => WorkBookHelperMethods.GetConvertedWorkSheetRow(WORKBOOK_NAME, SHEET_NAME, RecordToData);
        
        public static void AddAccountRecord(Account account) {
            WorkBookHelperMethods.InsertRow(WORKBOOK_NAME, SHEET_NAME, InsertRow, account);
        }
        
        private static void InsertRow(IXLRow row, Account account) {
            if (account == null)
                throw new NullReferenceException();

            var now = DateTime.Now;

            row.Cell(1).Value = account.AccountType;
            row.Cell(2).Value = account.Name;
            row.Cell(3).Value = now;
            row.Cell(4).Value = now;
        }

        private static Account RecordToData(IXLRow row) {
            if (row == null)
                return null;

            return new Account() {
                AccountType = row.Cell(1).GetValue<Account.Type>(),
                Name = row.Cell(2).GetValue<string>(),
                CreatedTime = row.Cell(3).GetValue<DateTime>(),
                LastestUpdatedTime = row.Cell(4).GetValue<DateTime>()
            };
        }

    }
}
