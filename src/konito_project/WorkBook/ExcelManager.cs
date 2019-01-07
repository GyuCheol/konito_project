using ClosedXML.Excel;
using konito_project.Model;
using konito_project.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.WorkBook {

    public static class ExcelManager {
        public static readonly WorkBookManager<Employee> EmployeeWorkBook = new WorkBookManager<Employee>("./db/임직원_정보.xlsx");
        public static readonly WorkBookManager<Client> ClientWorkBook = new WorkBookManager<Client>("./db/거래처_정보.xlsx");
        public static readonly WorkBookManager<Account> AccountWorkBook = new AccountWorkBook();
        public static readonly WorkBookManager<Mold> MoldWorkBook = new WorkBookManager<Mold>("./db/금형_관리_대장.xlsx");

        private static List<IWorkBookInitializer> workBookList = new List<IWorkBookInitializer>() {
            EmployeeWorkBook,
            ClientWorkBook,
            AccountWorkBook,
            MoldWorkBook
        };



        public static void CreateAllWorkBooks() {
            workBookList.ForEach(x => x.InitWorkBook());
        }

        public static void RemoveAllWorkBooks() {
            workBookList.ForEach(x => x.RemoveWorkBook());
        }
        
    }
}
