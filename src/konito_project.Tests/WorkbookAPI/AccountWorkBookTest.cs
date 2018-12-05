using konito_project.WorkBook;
using konito_project.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Tests.WorkbookAPI {

    [TestClass]
    public class AccountWorkBookTest {
        private AccountWorkBook workbook = new AccountWorkBook();

        [TestInitialize]
        public void Init() {
            workbook.RemoveWorkBook();
            workbook.InitWorkBook();
        }

        [TestMethod]
        public void Test_AddRecords() {

            workbook.AddRow(new Account() {
                AccountType = AccountType.Purchase,
                Name = "A"
            });

            Assert.AreEqual(workbook.GetRecordCount(), 1);

            workbook.AddRow(new Account() {
                AccountType = AccountType.Sales,
                Name = "B"
            });

            Assert.AreEqual(workbook.GetRecordCount(), 2);
        }

        [TestMethod]
        public void Test_GetRecords() {
            Assert.Fail();
        }

    }
}
