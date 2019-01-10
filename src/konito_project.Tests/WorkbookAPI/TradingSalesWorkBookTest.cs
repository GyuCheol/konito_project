using konito_project.Model;
using konito_project.WorkBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Tests.WorkbookAPI {

    [TestClass]
    public class TradingSalesWorkBookTest {
        private TradingWorkBook workBook = new TradingWorkBook(2019, AccountType.Sales, 1);

        [TestInitialize]
        public void InitWorkBook() {
            workBook.RemoveWorkBook();
            workBook.InitWorkBook();
        }

        [TestMethod]
        public void Test_Purchase_AddRecords()
        {
            workBook.AddRecord(new Trading() {
                Id = 1,
                AccountType = AccountType.Sales,
                CompanyName = "거래처",
                Date = DateTime.Now,
                Price = 150_000,
                Tax = 0.1d,
                ProductName = "Test",
                TaxType = TaxType.Tax,
                Memo = "asdfasdf"
            });

            Assert.AreEqual(1, workBook.GetRecordCount());

            workBook.ChangeMonth(2);

            workBook.AddRecord(new Trading() {
                Id = 1,
                AccountType = AccountType.Sales,
                CompanyName = "거래처",
                Date = DateTime.Now,
                Price = 150_000,
                Tax = 0.1d,
                ProductName = "Test",
                TaxType = TaxType.Tax,
                Memo = "asdfasdf"
            });

            Assert.AreEqual(1, workBook.GetRecordCount());

        }
    }
}
