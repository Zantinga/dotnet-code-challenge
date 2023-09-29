
using System;
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var expectedCompensation = new Compensation()
            {
                Employee = new Employee()
                {
                    EmployeeId = "1234-56890",
                    Department = "Complaints",
                    FirstName = "Debbie",
                    LastName = "Downer",
                    Position = "Receiver",
                },
                Salary = new decimal(999999.99),
                EffectiveDate = new DateTime(2023, 9, 28)

            };

            var requestContent = new JsonSerialization().ToJson(expectedCompensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.Employee.EmployeeId);
            Assert.AreEqual(expectedCompensation.Employee.EmployeeId, newCompensation.Employee.EmployeeId);
            Assert.AreEqual(expectedCompensation.Employee.FirstName, newCompensation.Employee.FirstName);
            Assert.AreEqual(expectedCompensation.Employee.LastName, newCompensation.Employee.LastName);
            Assert.AreEqual(expectedCompensation.Employee.Department, newCompensation.Employee.Department);
            Assert.AreEqual(expectedCompensation.Employee.Position, newCompensation.Employee.Position);
            Assert.AreEqual(expectedCompensation.Salary, newCompensation.Salary);
            Assert.AreEqual(expectedCompensation.EffectiveDate, newCompensation.EffectiveDate);
        }

        [TestMethod]
        public void CreateCompensation_NonExistentEmployee_Returns_NotFound()
        {
            // Arrange
            var compensation = new Compensation()
            {
                Employee = new Employee() { EmployeeId = "Invalid_ID" },
                EffectiveDate = new DateTime(),
                Salary = 75000.00m
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        //TODO: FIX THIS TEST!!!
        [TestMethod]
        public void GetCompensationByEmployeeId_Returns_Ok()
        {
            // Arrange
            var expectedCompensation = new Compensation()
            {
                Employee = new Employee()
                {
                    EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                    Department = "Complaints",
                    FirstName = "Debbie",
                    LastName = "Downer",
                    Position = "Receiver",
                },
                Salary = new decimal(999999.99),
                EffectiveDate = new DateTime(2023, 9, 28)

            };
            var requestContent = new JsonSerialization().ToJson(expectedCompensation);

            // Execute
            _httpClient.PostAsync("api/compensation",
                new StringContent(requestContent, Encoding.UTF8, "application/json")).Wait();
            
            var getRequestTask = _httpClient.GetAsync($"api/compensation/123");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(expectedCompensation.Employee.FirstName, compensation.Employee.FirstName);
            Assert.AreEqual(expectedCompensation.Employee.LastName, compensation.Employee.LastName);
        }
    }
}
