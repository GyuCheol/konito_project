using System;
using konito_project.WorkBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace konito_project.Tests.WorkbookAPI {
    [TestClass]
    public class ClientWorkBookTest {
        private ClientWorkBook workbook = new ClientWorkBook();

        [TestInitialize]
        public void Init() {
            workbook.RemoveWorkBook();
            workbook.InitWorkBook();
        }

        [TestMethod]
        public void Test_AddRecord() {

            workbook.AddRow(new Model.Client() {
                Id = 1,
                CompanyName = "Test",
                AccountType = Model.AccountType.Purchase
            });

            Assert.AreEqual(workbook.GetRecordCount(), 1);

            workbook.AddRow(new Model.Client() {
                Id = 2,
                CompanyName = "Test",
                AccountType = Model.AccountType.Sales
            });

            Assert.AreEqual(workbook.GetRecordCount(), 2);

            Assert.AreEqual(workbook.GetDataByIdOrNull(1).AccountType, Model.AccountType.Purchase);
            Assert.AreEqual(workbook.GetDataByIdOrNull(2).AccountType, Model.AccountType.Sales);
        }

        [TestMethod]
        public void Test_GetClientById() {

            workbook.AddRow(new Model.Client() {
                Id = 1,
                CompanyName = "Test1"
            });

            workbook.AddRow(new Model.Client() {
                Id = 2,
                CompanyName = "Test2"
            });

            Assert.IsNull(workbook.GetDataByIdOrNull(3));
            Assert.IsNotNull(workbook.GetDataByIdOrNull(1));
            Assert.IsNotNull(workbook.GetDataByIdOrNull(2));

            Assert.AreEqual(workbook.GetDataByIdOrNull(1).CompanyName, "Test1");
            Assert.AreEqual(workbook.GetDataByIdOrNull(2).CompanyName, "Test2");
        }

        [TestMethod]
        public void Test_EditClientById() {

            workbook.AddRow(new Model.Client() {
                Id = 1,
                CompanyName = "Test1"
            });

            workbook.AddRow(new Model.Client() {
                Id = 2,
                CompanyName = "Test2"
            });

            Assert.Fail("Not Improvement");
        }

        [TestMethod]
        public void Test_RemoveClientById() {

            workbook.AddRow(new Model.Client() {
                Id = 1,
                CompanyName = "Test"
            });

            workbook.AddRow(new Model.Client() {
                Id = 2,
                CompanyName = "Test"
            });

            Assert.AreEqual(workbook.GetRecordCount(), 2);

            workbook.RemoveRecordById(1);

            Assert.AreEqual(workbook.GetRecordCount(), 1);

            workbook.RemoveRecordById(2);

            Assert.AreEqual(workbook.GetRecordCount(), 0);
        }

        [TestMethod]
        public void Test_GetAllClients() {

            workbook.AddRow(new Model.Client() {
                Id = 1,
                CompanyName = "Test1"
            });

            workbook.AddRow(new Model.Client() {
                Id = 2,
                CompanyName = "Test2"
            });

            var enumerator = workbook.GetAllRecords().GetEnumerator();

            enumerator.MoveNext();
            Assert.AreEqual(enumerator.Current.CompanyName, "Test1");

            enumerator.MoveNext();
            Assert.AreEqual(enumerator.Current.CompanyName, "Test2");
        }



    }
}
