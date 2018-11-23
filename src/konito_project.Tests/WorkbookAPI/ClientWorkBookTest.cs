using System;
using konito_project.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace konito_project.Tests.WorkbookAPI {
    [TestClass]
    public class ClientWorkBookTest {

        [TestInitialize]
        public void Init() {
            ExcelManager.RemoveClientWorkBook();
            ExcelManager.CreateClientWorkBook();
        }

        [TestMethod]
        public void Test_AddRecord() {

            ClientWorkBook.AddClientRecord(new Model.Client() {
                Id = 1,
                CompanyName = "Test"
            });

            Assert.AreEqual(ClientWorkBook.GetRecordCount(), 1);

            ClientWorkBook.AddClientRecord(new Model.Client() {
                Id = 2,
                CompanyName = "Test"
            });

            Assert.AreEqual(ClientWorkBook.GetRecordCount(), 2);
        }

        [TestMethod]
        public void Test_GetClientById() {

            ClientWorkBook.AddClientRecord(new Model.Client() {
                Id = 1,
                CompanyName = "Test1"
            });

            ClientWorkBook.AddClientRecord(new Model.Client() {
                Id = 2,
                CompanyName = "Test2"
            });

            Assert.IsNull(ClientWorkBook.GetClientByIdOrNull(3));
            Assert.IsNotNull(ClientWorkBook.GetClientByIdOrNull(1));
            Assert.IsNotNull(ClientWorkBook.GetClientByIdOrNull(2));

            Assert.AreEqual(ClientWorkBook.GetClientByIdOrNull(1).CompanyName, "Test1");
            Assert.AreEqual(ClientWorkBook.GetClientByIdOrNull(2).CompanyName, "Test2");
        }

        [TestMethod]
        public void Test_RemoveClientById() {

            ClientWorkBook.AddClientRecord(new Model.Client() {
                Id = 1,
                CompanyName = "Test"
            });

            ClientWorkBook.AddClientRecord(new Model.Client() {
                Id = 2,
                CompanyName = "Test"
            });

            Assert.AreEqual(ClientWorkBook.GetRecordCount(), 2);

            ClientWorkBook.RemoveRecordById(1);

            Assert.AreEqual(ClientWorkBook.GetRecordCount(), 1);

            ClientWorkBook.RemoveRecordById(2);

            Assert.AreEqual(ClientWorkBook.GetRecordCount(), 0);
        }

        [TestMethod]
        public void Test_GetAllClients() {

            ClientWorkBook.AddClientRecord(new Model.Client() {
                Id = 1,
                CompanyName = "Test1"
            });

            ClientWorkBook.AddClientRecord(new Model.Client() {
                Id = 2,
                CompanyName = "Test2"
            });

            var enumerator = ClientWorkBook.GetAllRecords().GetEnumerator();

            enumerator.MoveNext();
            Assert.AreEqual(enumerator.Current.CompanyName, "Test1");

            enumerator.MoveNext();
            Assert.AreEqual(enumerator.Current.CompanyName, "Test2");
        }



    }
}
