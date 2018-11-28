using ClosedXML.Excel;
using konito_project.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Excel {

    public static class ExcelManager {

        public static void CreateAllWorkBooks() {
            CreateClientWorkBook();
            CreateAccountWorkBook();
            CreateEmployeeWorkBook();
        }

        public static void RemoveAllWorkBooks() {
            RemoveClientWorkBook();
            RemoveAccountWorkBook();
            RemoveEmployeeWorkBook();
        }

        public static void CreateClientWorkBook() => CreateWorkBook(ClientWorkBook.WORKBOOK_NAME, ClientWorkBook.SHEET_NAME, ClientWorkBook.COLUMNS);
        public static void CreateEmployeeWorkBook() => CreateWorkBook(EmployeeWorkBook.WORKBOOK_NAME, EmployeeWorkBook.SHEET_NAME, EmployeeWorkBook.COLUMNS);
        public static void CreateAccountWorkBook() {

            if(CreateWorkBook(AccountWorkBook.WORKBOOK_NAME, AccountWorkBook.SHEET_NAME, AccountWorkBook.COLUMNS)) {
                // Add default records
                foreach (var text in AccountWorkBook.DEFAULT_PURCHASE) {
                    AccountWorkBook.AddAccountRecord(HelperMethods.CreateAccount(Model.AccountType.Purchase, text));
                }

                foreach (var text in AccountWorkBook.DEFAULT_SALES) {
                    AccountWorkBook.AddAccountRecord(HelperMethods.CreateAccount(Model.AccountType.Sales, text));
                }
            }

        }
        public static bool RemoveClientWorkBook() => RemoveWorkBook(ClientWorkBook.WORKBOOK_NAME);
        public static bool RemoveAccountWorkBook() => RemoveWorkBook(AccountWorkBook.WORKBOOK_NAME);
        public static bool RemoveEmployeeWorkBook() => RemoveWorkBook(EmployeeWorkBook.WORKBOOK_NAME);

        private static bool RemoveWorkBook(string fileName) {

            if (File.Exists(ClientWorkBook.WORKBOOK_NAME)) {
                File.Delete(ClientWorkBook.WORKBOOK_NAME);
                return true;
            }

            return false;
        }

        private static bool CreateWorkBook(string fileName, string sheetName, string[] columns) {

            if (File.Exists(fileName))
                return false;

            using (var workbook = new XLWorkbook()) {
                var worksheet = workbook.Worksheets.Add(sheetName);

                for (int col = 0; col < columns.Length; col++) {
                    worksheet.Cell(1, col + 1).Value = columns[col];
                }

                workbook.SaveAs(fileName);
                return true;
            }
        }
    }
}
