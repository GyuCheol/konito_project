﻿using ClosedXML.Excel;
using konito_project.Attributes;
using konito_project.Exceptions;
using konito_project.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.WorkBook {

    public abstract class WorkBookBase<T>: IWorkBookInitializer where T: class {
        public abstract string WorkBookPath { get; }
        public Type ModelType { get; } = typeof(T);
        public virtual int KeyColumn => 1;
        public string MainSheetName => "data";

        public T GetDataByIdOrNull(int id) {
            return GetDataByPredicate(r => r.Cell(KeyColumn).GetValue<int>() == id);
        }

        public T GetDataByStrKeyOrNull(string key) {
            return GetDataByPredicate(r => r.Cell(KeyColumn).GetValue<string>() == key);
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
            RemoveByPredicate(r => r.Cell(KeyColumn).GetValue<int>() == id);
        }

        public void RemoveRecordByStrKey(string key) {
            RemoveByPredicate(r => r.Cell(KeyColumn).GetValue<string>() == key);
        }

        public void EditRecordById(T data, int id) {
            EditByPredicate(r => r.Cell(KeyColumn).GetValue<int>() == id, data);
        }

        public void EditRecordByStrKey(T data, string key) {
            EditByPredicate(r => r.Cell(KeyColumn).GetValue<string>() == key, data);
        }

        public int GetNewRecordId() {
            using (var stream = new FileStream(WorkBookPath, FileMode.Open, FileAccess.Read))
            using (var workBook = new XLWorkbook(stream)) {

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

        public void AddRecord(T data) {
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

        public void AddRecords(IEnumerable<T> enumerableData) {
            using (var stream = new FileStream(WorkBookPath, FileMode.Open, FileAccess.ReadWrite))
            using (var workBook = new XLWorkbook(stream)) {

                var sheet = workBook.Worksheet(MainSheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int row = sheet.Cell("A1").CurrentRegion.RowCount() + 1;

                foreach (var data in enumerableData) {
                    InsertRow(sheet.Row(row), data);
                    row++;
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

        public void InitWorkBook() {
            
            if (File.Exists(WorkBookPath)) {

                if(CanUseWorkBook() == false) {
                    throw new CouldNotOpenWorkBookException(WorkBookPath);
                }

                return;
            }
            
            using (var workbook = new XLWorkbook()) {

                var properties = from prop in ModelType.GetProperties()
                                 orderby prop.GetCustomAttributes(typeof(ExcelColumnAttribute), false).OfType<ExcelColumnAttribute>().First().Order
                                 select prop;

                var worksheet = workbook.Worksheets.Add(MainSheetName);
                
                foreach (var prop in properties) {
                    var excelColumn = prop.GetCustomAttributes(typeof(ExcelColumnAttribute), false).OfType<ExcelColumnAttribute>().First();

                    worksheet.Cell(1, excelColumn.Order).SetValue(excelColumn.HeaderName);
                }

                workbook.SaveAs(WorkBookPath);
                InitExcel(workbook);
            }
        }

        public bool RemoveWorkBook() {
            if (File.Exists(WorkBookPath)) {
                File.Delete(WorkBookPath);
                return true;
            }

            return false;
        }

        public void ClearAllRecords() {
            using (var stream = new FileStream(WorkBookPath, FileMode.Open, FileAccess.ReadWrite))
            using (var workBook = new XLWorkbook(stream)) {

                var sheet = workBook.Worksheet(MainSheetName);

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int row = sheet.Cell("A1").CurrentRegion.RowCount();

                sheet.Rows(2, row).Clear(XLClearOptions.Contents);

                workBook.Save();
            }
        }

        protected virtual void InitExcel(XLWorkbook workbook) { }

        protected virtual T CovertToDataFromRow(IXLRow row) {
            var instance = Activator.CreateInstance(ModelType) as T;

            if (instance == null)
                throw new CouldNotCreateModelInstanceException();

            var properties = ModelType.GetProperties();

            foreach (var prop in properties) {
                var excelColumn = prop.GetCustomAttributes(typeof(ExcelColumnAttribute), false).OfType<ExcelColumnAttribute>().First();
                var value = row.Cell(excelColumn.Order).GetValue<object>();

                if (prop.PropertyType == typeof(AccountType)) {
                    var str = value.ToString();

                    Enum.TryParse(str, out AccountType accountType);

                    prop.SetValue(instance, accountType);
                } else if(prop.PropertyType == typeof(int)) {
                    prop.SetValue(instance, (int) ((double) value));
                } else {
                    prop.SetValue(instance, value);
                    break;
                }
            }

            return instance;

        }

        protected virtual void InsertRow(IXLRow row, T data) {
            var properties = ModelType.GetProperties();

            foreach (var prop in properties) {
                var excelColumn = prop.GetCustomAttributes(typeof(ExcelColumnAttribute), false).OfType<ExcelColumnAttribute>().First();
                Object value = prop.GetValue(data);

                switch (value) {
                    case string str:
                        row.Cell(excelColumn.Order).SetValue(str);
                        break;
                    case int number:
                        row.Cell(excelColumn.Order).SetValue(number);
                        break;
                    case double db:
                        row.Cell(excelColumn.Order).SetValue(db);
                        break;
                    case DateTime dt:

                        if(dt != DateTime.MinValue) {
                            row.Cell(excelColumn.Order).SetValue(dt);
                        }

                        break;
                    case AccountType accountType:
                        row.Cell(excelColumn.Order).SetValue(accountType.ToString());
                        break;
                    default:

                        if(value == null) {
                            row.Cell(excelColumn.Order).SetValue(value);
                            continue;
                        }

                        throw new UnknownColumnTypeException();
                }

            }
        }

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

        private void EditByPredicate(Func<IXLRangeRow, bool> pred, T data) {
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

                InsertRow(sheet.Row(foundRow.RowNumber()), data);

                workBook.Save();
            }
        }

    }
}
