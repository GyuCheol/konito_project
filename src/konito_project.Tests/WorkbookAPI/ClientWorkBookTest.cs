using System;
using konito_project.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace konito_project.Tests.WorkbookAPI {
    [TestClass]
    public class ClientWorkBookTest {

        [TestInitialize]
        public void Init() {
            ExcelManager.RemoveClientWorkBook();
            ExcelManager.CreateWorkBooks();
        }

        [TestMethod]
        public void Test_AddRecord() {

            int row = ClientWorkBook.AddClientRecord(new Model.Client() {
                Id = 1,
                CompanyCode = "Test"
            });

            Assert.AreEqual(row, 2);

            int row2 = ClientWorkBook.AddClientRecord(new Model.Client() {
                Id = 2,
                CompanyCode = "Test"
            });

            Assert.AreEqual(row2, 3);
        }

        [TestMethod]
        public void Test_GetClientById() {

            int row = ClientWorkBook.AddClientRecord(new Model.Client() {
                Id = 1,
                CompanyCode = "Test"
            });

            int row2 = ClientWorkBook.AddClientRecord(new Model.Client() {
                Id = 2,
                CompanyCode = "Test"
            });

            Assert.IsNull(ClientWorkBook.GetClientByIdOrNull(3));
            Assert.IsNotNull(ClientWorkBook.GetClientByIdOrNull(1));
            Assert.IsNotNull(ClientWorkBook.GetClientByIdOrNull(2));
        }




    }
}
