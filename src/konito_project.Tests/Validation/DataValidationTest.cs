﻿using System;
using konito_project.Model;
using konito_project.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace konito_project.Tests.Validation {
    [TestClass]
    public class DataValidationTest {

        [TestMethod]
        public void Test_Validate_Client() {
            var client = new Client();

            Assert.AreEqual(client.ValidateRequired().HasProblem, true);

            client.CompanyCode = "1";
            Assert.AreEqual(client.ValidateRequired().HasProblem, true);

            client.CompanyName = "1";
            Assert.AreEqual(client.ValidateRequired().HasProblem, true);

            client.OwnerName = "1";
            Assert.AreEqual(client.ValidateRequired().HasProblem, true);

            client.BankAccountCode = "1";
            Assert.AreEqual(client.ValidateRequired().HasProblem, true);

            client.BankName = "1";
            Assert.AreEqual(client.ValidateRequired().HasProblem, true);

            client.BankAccountOwnerName = "1";
            Assert.AreEqual(client.ValidateRequired().HasProblem, false);
        }

        [TestMethod]
        public void Test_Validate_Employee() {
            var emp = new Employee();

            Assert.AreEqual(emp.ValidateRequired().HasProblem, true);

            emp.EmployeeCode = "1";
            Assert.AreEqual(emp.ValidateRequired().HasProblem, true);

            emp.LastName = "1";
            Assert.AreEqual(emp.ValidateRequired().HasProblem, true);

            emp.FirstName = "1";
            Assert.AreEqual(emp.ValidateRequired().HasProblem, true);

            emp.Position = "1";
            Assert.AreEqual(emp.ValidateRequired().HasProblem, true);

            emp.Salary = 1;
            Assert.AreEqual(emp.ValidateRequired().HasProblem, true);

            emp.IncomeTax = 1;
            Assert.AreEqual(emp.ValidateRequired().HasProblem, true);

            emp.EmploymentInsurance = 1;
            Assert.AreEqual(emp.ValidateRequired().HasProblem, true);

            emp.HealthInsurance = 1;
            Assert.AreEqual(emp.ValidateRequired().HasProblem, true);

            emp.ResidenceTax = 1;
            Assert.AreEqual(emp.ValidateRequired().HasProblem, true);

            emp.NationalPension = 1;
            Assert.AreEqual(emp.ValidateRequired().HasProblem, false);
        }
    }
}
