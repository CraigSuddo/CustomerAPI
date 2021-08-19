using CustomerAPI.Data;
using CustomerAPI.Entities;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CustomerAPI.Tests.Data
{
    public class Tests
    { 

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetCustomersFromDatabase()
        {
            // Arrange 
            var customers = new List<Customer>() {
                new Customer { Title = "Mr", Forename = "Bob", Surname = "Test", EmailAddress = "bob@testcom", MobileNo = "07112333344", Id = Guid.NewGuid().ToString() },
                new Customer { Title = "Mrs", Forename = "Bobba", Surname = "Tester", EmailAddress = "bobba@testcom", MobileNo = "07112333346", Id = Guid.NewGuid().ToString() }
            };

            var newContext = new Mock<IApplicationDbContext>();
            newContext.Setup(c => c.Customers).ReturnsDbSet(customers);

            var customersService = new CustomersService(newContext.Object);

            // Act
            var serviceCustomers = customersService.GetCustomers();

            // Assert
            Assert.AreEqual(customers, serviceCustomers);
        }

        [Test]
        public void ShouldBeAbleToAddACustomer()
        {
            var customers = new List<Customer>();
            // Arrange 
            var newContext = new Mock<IApplicationDbContext>();
            newContext.Setup(c => c.Customers).ReturnsDbSet(customers);
            newContext.Setup(c => c.Customers.Add(It.IsAny<Customer>())).Callback<Customer>(s => customers.Add(s));

            var customersService = new CustomersService(newContext.Object);
            var newCustomer = new Customer { Title = "Mr", Forename = "Mark", Surname = "Test", EmailAddress = "mark@test.com", MobileNo = "07112333341", Id = Guid.NewGuid().ToString() };

            // Act
            customersService.AddCustomer(newCustomer);
            var serviceCustomers = customersService.GetCustomers();

            // Assert
            var expected = new List<Customer>(customers);
            Assert.AreEqual(new List<Customer> { newCustomer }, serviceCustomers);
        }

        [Test]
        public void ShouldBeAbleTsoUpdateACustomer()
        {
            Assert.Pass();
        }

        [Test]
        public void ShouldBeAbleToDeleteACustomer()
        {
            var customers = new List<Customer>();
            var deleteCustomer = new Customer { Title = "Mr", Forename = "Mark", Surname = "Test", EmailAddress = "mark@test.com", MobileNo = "07112333341", Id = Guid.NewGuid().ToString() };
            customers.Add(deleteCustomer);

            // Arrange 
            var newContext = new Mock<IApplicationDbContext>();
            newContext.Setup(c => c.Customers).ReturnsDbSet(customers);
            newContext.Setup(c => c.Customers.Remove(deleteCustomer)).Callback<Customer>(s => customers.Remove(s));

            var customersService = new CustomersService(newContext.Object);

            // Act
            customersService.DeleteCustomerById(deleteCustomer.Id);
            var serviceCustomers = customersService.GetCustomers();

            // Assert
            Assert.AreEqual(serviceCustomers.Count, 0);
        }
    }
}