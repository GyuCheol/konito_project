using konito_project.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Tests.WorkbookAPI {

    [TestClass]
    public class AccountWorkBookTest {

        [TestInitialize]
        public void Init() {
            ExcelManager.RemoveAccountWorkBook();
            ExcelManager.CreateAccountWorkBook();
        }

        [TestMethod]
        public void Test_GetRecords() {

            AccountWorkBook.AddAccountRecord(new Model.Account() {
                AccountType = Model.Account.Type.Purchase,
                Name = "A"
            });

            AccountWorkBook.AddAccountRecord(new Model.Account() {
                AccountType = Model.Account.Type.Sales,
                Name = "B"
            });

            //var enumerator = AccountWorkBook
        }

    }
}
