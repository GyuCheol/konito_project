using ClosedXML.Excel;
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
        }

        public static void RemoveAllWorkBooks() {
            RemoveClientWorkBook();
            RemoveAccountWorkBook();
        }

        public static void CreateClientWorkBook() => CreateWorkBook(ClientWorkBook.WORKBOOK_NAME, ClientWorkBook.COLUMNS);
        public static void CreateAccountWorkBook() => CreateWorkBook(AccountWorkBook.WORKBOOK_NAME, AccountWorkBook.COLUMNS);
        public static bool RemoveClientWorkBook() => RemoveWorkBook(ClientWorkBook.WORKBOOK_NAME);
        public static bool RemoveAccountWorkBook() => RemoveWorkBook(AccountWorkBook.WORKBOOK_NAME);

        private static bool RemoveWorkBook(string fileName) {

            if (File.Exists(ClientWorkBook.WORKBOOK_NAME)) {
                File.Delete(ClientWorkBook.WORKBOOK_NAME);
                return true;
            }

            return false;
        }

        private static bool CreateWorkBook(string fileName, string[] columns) {

            if (File.Exists(ClientWorkBook.WORKBOOK_NAME))
                return false;

            using (var workbook = new XLWorkbook()) {
                var worksheet = workbook.Worksheets.Add("data");

                for (int col = 0; col < columns.Length; col++) {
                    worksheet.Cell(1, col + 1).Value = columns[col];
                }

                workbook.SaveAs(fileName);
                return true;
            }
        }


    }
}
