
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using CodeChallenge.DTO;
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
            var expectedEmployee = new Employee()
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Department = "Engineering",
                FirstName = "John",
                LastName = "Lennon",
                Position = "Development Manager",
            };
            var expectedCompensation = new CompensationDTO()
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                EffectiveDate = new DateTime(2023, 09, 29),
                Salary = 125000.00m
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
            Assert.AreEqual(expectedCompensation.EmployeeId, newCompensation.Employee.EmployeeId);

            Assert.AreEqual(expectedEmployee.FirstName, newCompensation.Employee.FirstName);
            Assert.AreEqual(expectedEmployee.LastName, newCompensation.Employee.LastName);
            Assert.AreEqual(expectedEmployee.Department, newCompensation.Employee.Department);
            Assert.AreEqual(expectedEmployee.Position, newCompensation.Employee.Position);
            Assert.AreEqual(expectedCompensation.Salary, newCompensation.Salary);
            Assert.AreEqual(expectedCompensation.EffectiveDate, newCompensation.EffectiveDate);
        }

        [TestMethod]
        public void CreateCompensation_NonExistentEmployee_Returns_NotFound()
        {
            // Arrange
            var compensation = new CompensationDTO()
            {
                EmployeeId = "Invalid_ID",
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

        [TestMethod]
        public void GetCompensationByEmployeeId_Returns_Ok()
        {
            // Arrange
            var compensationDTO = new CompensationDTO()
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                EffectiveDate = new DateTime(2023, 9, 28),
                Salary = 123000.00m
            };
            var expectedCompensation = new Compensation()
            {
                Employee = new Employee()
                {
                    EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                    Department = "Engineering",
                    FirstName = "John",
                    LastName = "Lennon",
                    Position = "Development Manager",
                },
                Salary = 123000.00m,
                EffectiveDate = new DateTime(2023, 9, 28)

            };

            // Execute
            var requestContent = new JsonSerialization().ToJson(compensationDTO);
            _httpClient.PostAsync("api/compensation",
                new StringContent(requestContent, Encoding.UTF8, "application/json")).Wait();
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{expectedCompensation.Employee.EmployeeId}");
            var response = getRequestTask.Result;


            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(expectedCompensation.Employee.FirstName, compensation.Employee.FirstName);
            Assert.AreEqual(expectedCompensation.Employee.LastName, compensation.Employee.LastName);
            Assert.AreEqual(compensationDTO.EmployeeId, compensation.Employee.EmployeeId);
            Assert.AreEqual(compensationDTO.EffectiveDate, expectedCompensation.EffectiveDate);
            Assert.AreEqual(compensationDTO.Salary, expectedCompensation.Salary);
        }
    }
}
