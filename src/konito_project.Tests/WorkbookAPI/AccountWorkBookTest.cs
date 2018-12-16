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

            workbook.AddRecord(new Account() {
                AccountType = AccountType.Purchase,
                Name = "A"
            });

            Assert.AreEqual(workbook.GetRecordCount(), 9);

            workbook.AddRecord(new Account() {
                AccountType = AccountType.Sales,
                Name = "B"
            });

            Assert.AreEqual(workbook.GetRecordCount(), 10);
        }

        [TestMethod]
        public void Test_GetRecords() {
            workbook.AddRecord(new Account() {
                AccountType = AccountType.Purchase,
                Name = "A"
            });

            workbook.AddRecord(new Account() {
                AccountType = AccountType.Sales,
                Name = "B"
            });

            Assert.AreEqual(workbook.GetDataByStrKeyOrNull("A").AccountType, AccountType.Purchase);
            Assert.AreEqual(workbook.GetDataByStrKeyOrNull("B").AccountType, AccountType.Sales);

        }

        [TestMethod]
        public void Test_RemoveRecords() {
            workbook.AddRecord(new Account() {
                AccountType = AccountType.Purchase,
                Name = "A"
            });

            workbook.AddRecord(new Account() {
                AccountType = AccountType.Sales,
                Name = "B"
            });

            Assert.AreEqual(workbook.GetRecordCount(), 10);

            workbook.RemoveRecordByStrKey("A");
            Assert.AreEqual(workbook.GetRecordCount(), 9);

            workbook.RemoveRecordByStrKey("B");
            Assert.AreEqual(workbook.GetRecordCount(), 8);
        }

    }
}
