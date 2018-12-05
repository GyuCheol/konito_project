using ClosedXML.Excel;
using konito_project.Exceptions;
using konito_project.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.WorkBook {

    public abstract class WorkBookBase<T>: IWorkBookInitializer where T: class {
        public abstract string WorkBookPath { get; }
        public abstract string[] InitColumns { get; }

        public string MainSheetName => "data";

        private T GetDataByPredicate(Func<IXLRangeRow, bool> pred) {
            using (var stream = new FileStream(WorkBookPath, FileMode.Open, FileAccess.Read))
            using (var workBook = new XLWorkbook(stream)) {

                var sheet = workBook.Worksheet(MainSheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int lastRow = sheet.Cell("A1").CurrentRegion.RowCount();

                if (lastRow == 1)
                    return default(T);

                IXLRangeRow foundRow = sheet.Range(2, 1, lastRow, 1).FindRow(pred);

                if (foundRow == null)
                    return default(T);

                return CovertToDataFromRow(sheet.Row(foundRow.RowNumber()));
            }
        }

        private void RemoveByPredicate(Func<IXLRangeRow, bool> pred) {
            using (var stream = new FileStream(WorkBookPath, FileMode.Open, FileAccess.ReadWrite))
            using (var workBook = new XLWorkbook(stream)) {

                var sheet = workBook.Worksheet(MainSheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int lastRow = sheet.Cell("A1").CurrentRegion.RowCount();

                if (lastRow == 1)
                    return;

                IXLRangeRow foundRow = sheet.Range(2, 1, lastRow, 1).FindRow(pred);

                if (foundRow == null)
                    return;

                sheet.Row(foundRow.RowNumber()).Delete();

                workBook.Save();
            }
        }

        private T FindDataByPredicate(Func<IXLRangeRow, bool> pred) {
            using (var stream = new FileStream(WorkBookPath, FileMode.Open, FileAccess.Read))
            using (var workBook = new XLWorkbook(stream)) {

                var sheet = workBook.Worksheet(MainSheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int lastRow = sheet.Cell("A1").CurrentRegion.RowCount();

                if (lastRow == 1)
                    return default(T);

                IXLRangeRow foundRow = sheet.Range(2, 1, lastRow, 1).FindRow(pred);

                if (foundRow == null)
                    return default(T);

                return CovertToDataFromRow(sheet.Row(foundRow.RowNumber()));
            }
        }

        public T GetDataByIdOrNull(int id) {
            return GetDataByPredicate(r => r.FirstCell().GetValue<int>() == id);
        }

        public T GetDataByStrKeyOrNull(string key) {
            return GetDataByPredicate(r => r.FirstCell().GetValue<string>() == key);
        }

        public int GetRecordCount() {
            using (var stream = new FileStream(WorkBookPath, FileMode.Open, FileAccess.Read))
            using (var workBook = new XLWorkbook(stream)) {

                var sheet = workBook.Worksheet(MainSheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                return sheet.Cell("A1").CurrentRegion.RowCount() - 1;
            }
        }

        public IEnumerable<T> GetAllRecords() {
            using (var stream = new FileStream(WorkBookPath, FileMode.Open, FileAccess.Read))
            using (var workBook = new XLWorkbook(stream)) {
                var sheet = workBook.Worksheet(MainSheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int lastRow = sheet.Cell("A1").CurrentRegion.RowCount();

                if (lastRow == 1)
                    yield break;

                for (int row = 2; row <= lastRow; row++) {
                    yield return CovertToDataFromRow(sheet.Row(row));
                }
            }            
        }

        public void RemoveRecordById(int id) {
            RemoveByPredicate(r => r.FirstCell().GetValue<int>() == id);
        }

        public void RemoveRecordByStrKey(string key) {
            RemoveByPredicate(r => r.FirstCell().GetValue<string>() == key);
        }

        public int GetNewRecordId() {
            using (var workBook = new XLWorkbook(WorkBookPath)) {

                var sheet = workBook.Worksheet(MainSheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int lastRow = sheet.Cell("A1").CurrentRegion.RowCount();

                if (lastRow == 1)
                    return 1;

                int maxValue = 0;

                for (int r = 2; r <= lastRow; r++) {
                    int id = sheet.Cell(r, 1).GetValue<int>();

                    maxValue = Math.Max(maxValue, id);
                }

                return maxValue + 1;

            }
        }

        public T FindDataByIdOrDefault(int id) {
            return FindDataByPredicate(r => r.FirstCell().GetValue<int>() == id);
        }

        public T FindDataByStrKeyOrDefault(string key) {
            return FindDataByPredicate(r => r.FirstCell().GetValue<string>() == key);
        }

        public void AddRow(T data) {
            using (var stream = new FileStream(WorkBookPath, FileMode.Open, FileAccess.ReadWrite))
            using (var workBook = new XLWorkbook(stream)) {

                var sheet = workBook.Worksheet(MainSheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int row = sheet.Cell("A1").CurrentRegion.RowCount() + 1;

                InsertRow(sheet.Row(row), data);

                workBook.Save();
            }
        }

        public void AddRows(IEnumerable<T> enumerableData) {
            using (var stream = new FileStream(WorkBookPath, FileMode.Open, FileAccess.ReadWrite))
            using (var workBook = new XLWorkbook(stream)) {

                var sheet = workBook.Worksheet(MainSheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int row = sheet.Cell("A1").CurrentRegion.RowCount() + 1;

                foreach (var data in enumerableData) {
                    InsertRow(sheet.Row(row), data);
                }

                workBook.Save();
            }
        }



        public bool CanUseWorkBook() {
            try {
                using (var stream = File.Open(WorkBookPath, FileMode.Open, FileAccess.Read, FileShare.None)) {
                    return true;
                }
            } catch (IOException) {
                return false;
            }
        }

        protected virtual void InitExcel(XLWorkbook workbook) {

        }
        protected abstract T CovertToDataFromRow(IXLRow row);
        protected abstract void InsertRow(IXLRow row, T data);

        public void InitWorkBook() {

            if (File.Exists(WorkBookPath))
                return;

            using (var workbook = new XLWorkbook()) {
                var worksheet = workbook.Worksheets.Add(MainSheetName);

                for (int col = 0; col < InitColumns.Length; col++) {
                    worksheet.Cell(1, col + 1).Value = InitColumns[col];
                }

                InitExcel(workbook);
                workbook.SaveAs(WorkBookPath);
                return;
            }

        }

        public bool RemoveWorkBook() {
            if (File.Exists(WorkBookPath)) {
                File.Delete(WorkBookPath);
                return true;
            }

            return false;
        }
    }
}
