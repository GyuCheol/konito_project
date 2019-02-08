using ClosedXML.Excel;
using konito_project.Attributes;
using konito_project.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.WorkBook {
    public abstract class YearMonthlyWorkBook<T> : WorkBookManager<T> 
        where T : class {

        protected abstract string DefaultPath { get; }

        public YearMonthlyWorkBook() { }

        public YearMonthlyWorkBook(int year, int month) {
            ChangeYear(year);
            ChangeMonth(month);
        }

        public void ChangeYear(int year) {
            WorkBookPath = $"{DefaultPath}/{year}.xlsx";
            InitWorkBook();
        }

        public void ChangeMonth(int month) {
            MainSheetName = $"{month}월";
        }

        public override void InitWorkBook() {
            if (File.Exists(WorkBookPath)) {

                if (CanUseWorkBook() == false) {
                    throw new CouldNotOpenWorkBookException(WorkBookPath);
                }

                return;
            }

            using (var workbook = new XLWorkbook()) {

                var attributes = from prop in ModelType.GetProperties()
                                 select prop.GetCustomAttributes(typeof(ExcelColumnAttribute), false).OfType<ExcelColumnAttribute>().FirstOrDefault() into attr
                                 where attr != null
                                 select attr;

                for (int month = 1; month <= 12; month++) {
                    var worksheet = workbook.Worksheets.Add($"{month}월");

                    foreach (var attr in attributes) {
                        worksheet.Cell(1, attr.Order).SetValue(attr.HeaderName);
                    }
                }

                workbook.SaveAs(WorkBookPath);
                InitExcel(workbook);
            }
        }

    }
}
