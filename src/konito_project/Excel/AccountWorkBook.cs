using ClosedXML.Excel;
using konito_project.Exceptions;
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
            "계정명"
        };

        public static readonly string[] DEFAULT_PURCHASE = "부품비,원재료비,외주가공비,기타".Split(',');
        public static readonly string[] DEFAULT_SALES = "금형,부품,사출품,기타".Split(',');

        public static int GetRecordCount() => WorkBookHelperMethods.GetWorksheetCount(WORKBOOK_NAME, SHEET_NAME);
        public static IEnumerable<Account> GetAllRecords() => WorkBookHelperMethods.GetConvertedWorkSheetRow(WORKBOOK_NAME, SHEET_NAME, RecordToData);
        
        public static void AddAccountRecord(Account account) {
            WorkBookHelperMethods.InsertRow(WORKBOOK_NAME, SHEET_NAME, InsertRow, account);
        }
        
        public static void AddReocrds(IEnumerable<Account> accounts) {
            using (var workBook = new XLWorkbook(WORKBOOK_NAME)) {

                var sheet = workBook.Worksheet(SHEET_NAME);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int lastRow = sheet.Cell("A1").CurrentRegion.RowCount();

                sheet.Range("A2", $"Z{lastRow}").Clear(XLClearOptions.All);

                int lastRow2 = 2;

                foreach (var account in accounts) {
                    InsertRow(sheet.Row(lastRow2), account);
                    lastRow2++;
                }

                workBook.Save();
            }
        }

        private static void InsertRow(IXLRow row, Account account) {
            if (account == null)
                throw new NullReferenceException();

            row.Cell(1).Value = account.AccountType;
            row.Cell(2).Value = account.Name;
        }

        private static Account RecordToData(IXLRow row) {
            if (row == null)
                return null;
            
            return new Account() {
                AccountType = (AccountType) Enum.Parse(typeof(AccountType), row.Cell(1).GetValue<string>()),
                Name = row.Cell(2).GetValue<string>()
            };
        }

    }
}
