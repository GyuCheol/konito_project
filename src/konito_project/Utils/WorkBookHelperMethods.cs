using ClosedXML.Excel;
using konito_project.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Utils {
    public static class WorkBookHelperMethods {

        public static int GetWorksheetCount(string workbookPath, string sheetName) {
            using (var workBook = new XLWorkbook(workbookPath)) {

                var sheet = workBook.Worksheet(sheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                return sheet.Cell("A1").CurrentRegion.RowCount() - 1;
            }
        }

        public static IEnumerable<T> GetConvertedWorkSheetRow<T>(string workbookPath, string sheetName, Func<IXLRow, T> converting) {
            using (var workBook = new XLWorkbook(workbookPath)) {

                var sheet = workBook.Worksheet(sheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int lastRow = sheet.Cell("A1").CurrentRegion.RowCount();

                if (lastRow == 1)
                    yield break;

                for (int row = 2; row <= lastRow; row++) {
                    yield return converting(sheet.Row(row));
                }
            }
        }

        public static void InsertRow<T>(string workbookPath, string sheetName, Action<IXLRow, T> insertAction, T data) {
            using (var workBook = new XLWorkbook(workbookPath)) {

                var sheet = workBook.Worksheet(sheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int row = sheet.Cell("A1").CurrentRegion.RowCount() + 1;
                
                insertAction(sheet.Row(row), data);
                
                workBook.Save();
            }
        }

        public static T FindConvertedWorkSheetRowOrDefault<T>(string workbookPath, string sheetName, Func<IXLRangeRow, bool> predicate, Func<IXLRow, T> converting) {
            using (var workBook = new XLWorkbook(workbookPath)) {

                var sheet = workBook.Worksheet(sheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int lastRow = sheet.Cell("A1").CurrentRegion.RowCount();

                if (lastRow == 1)
                    return default(T);

                IXLRangeRow foundRow = sheet.Range(2, 1, lastRow, 1).FindRow(predicate);

                if (foundRow == null)
                    return default(T);

                return converting(sheet.Row(foundRow.RowNumber()));
            }
        }

        public static void RemoveSheetRow(string workbookPath, string sheetName, Func<IXLRangeRow, bool> predicate) {

            using (var workBook = new XLWorkbook(workbookPath)) {

                var sheet = workBook.Worksheet(sheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int lastRow = sheet.Cell("A1").CurrentRegion.RowCount();

                if (lastRow == 1)
                    return;

                IXLRangeRow foundRow = sheet.Range(2, 1, lastRow, 1).FindRow(predicate);

                if (foundRow == null)
                    return;

                sheet.Row(foundRow.RowNumber()).Delete();

                workBook.Save();
            }
        }

    }
}
