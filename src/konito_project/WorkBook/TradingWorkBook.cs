using ClosedXML.Excel;
using konito_project.Attributes;
using konito_project.Exceptions;
using konito_project.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.WorkBook {
    public class TradingWorkBook : WorkBookManager<Trading> {
        
        public TradingWorkBook(int year, AccountType accountType, int month) {
            WorkBookPath = $"./db/{accountType}실적/{year}.xlsx";
            ChangeMonth(month);
        }

        public void ChangeMonth(int month) {
            MainSheetName = $"{month}월";
        }

        public int GetTotalyPrice() => GetAllRecords().Sum(x => x.Price);

        public override void InitWorkBook()
        {
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
