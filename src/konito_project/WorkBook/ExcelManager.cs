using ClosedXML.Excel;
using konito_project.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.WorkBook {

    public static class ExcelManager {
        private static List<IWorkBookInitializer> workBookList = new List<IWorkBookInitializer>() {
            new AccountWorkBook(),
            new EmployeeWorkBook(),
            new ClientWorkBook()
        };
        
        public static void CreateAllWorkBooks() {
            workBookList.ForEach(x => x.InitWorkBook());
        }

        public static void RemoveAllWorkBooks() {
            workBookList.ForEach(x => x.RemoveWorkBook());
        }
        
    }
}
