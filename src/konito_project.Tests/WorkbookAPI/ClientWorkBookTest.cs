using System;
using System.Collections.Generic;
using konito_project.Model;
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

            workbook.AddRecord(new Model.Client() {
                Id = 1,
                CompanyName = "Test",
                AccountType = Model.AccountType.Purchase
            });

            Assert.AreEqual(workbook.GetRecordCount(), 1);

            workbook.AddRecord(new Model.Client() {
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
            workbook.AddRecords(GetRecords());

            Assert.IsNull(workbook.GetDataByIdOrNull(3));
            Assert.IsNotNull(workbook.GetDataByIdOrNull(1));
            Assert.IsNotNull(workbook.GetDataByIdOrNull(2));

            Assert.AreEqual(workbook.GetDataByIdOrNull(1).CompanyName, "Test1");
            Assert.AreEqual(workbook.GetDataByIdOrNull(2).CompanyName, "Test2");
        }

        [TestMethod]
        public void Test_EditClientById() {

            var client1 = new Model.Client() {
                Id = 1,
                CompanyName = "Test1"
            };

            var client2 = new Model.Client() {
                Id = 2,
                CompanyName = "Test2"
            };
            
            workbook.AddRecord(client1);
            workbook.AddRecord(client2);

            client1.CompanyName = "Test Company";
            client2.CompanyName = "";

            workbook.EditRecordById(client1, client1.Id);
            workbook.EditRecordById(client2, client2.Id);

            Assert.AreEqual(workbook.GetDataByIdOrNull(1).CompanyName, client1.CompanyName);
            Assert.AreEqual(workbook.GetDataByIdOrNull(1).Id, client1.Id);

            Assert.AreEqual(workbook.GetDataByIdOrNull(2).CompanyName, client2.CompanyName);
            Assert.AreEqual(workbook.GetDataByIdOrNull(2).Id, client2.Id);
        }

        [TestMethod]
        public void Test_RemoveClientById() {
            workbook.AddRecords(GetRecords());

            Assert.AreEqual(workbook.GetRecordCount(), 2);

            workbook.RemoveRecordById(1);

            Assert.AreEqual(workbook.GetRecordCount(), 1);

            workbook.RemoveRecordById(2);

            Assert.AreEqual(workbook.GetRecordCount(), 0);
        }

        [TestMethod]
        public void Test_GetAllClients() {
            workbook.AddRecords(GetRecords());

            var enumerator = workbook.GetAllRecords().GetEnumerator();

            enumerator.MoveNext();
            Assert.AreEqual(enumerator.Current.CompanyName, "Test1");

            enumerator.MoveNext();
            Assert.AreEqual(enumerator.Current.CompanyName, "Test2");
        }

        private IEnumerable<Client> GetRecords() {
            yield return new Client() {
                Id = 1,
                CompanyName = "Test1"
            };

            yield return new Client() {
                Id = 2,
                CompanyName = "Test2"
            };
        }

        [TestMethod]
        public void Test_AddRecords() {
            workbook.AddRecords(GetRecords());
            Assert.AreEqual(workbook.GetRecordCount(), 2);
        }



    }
}
