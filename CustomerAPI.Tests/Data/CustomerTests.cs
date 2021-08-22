using CustomerAPI.Data;
using CustomerAPI.Data.Interfaces;
using CustomerAPI.Entities;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CustomerAPI.Tests.Data
{
    public class CustomerTests
    {
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
        public void GetCustomerFromDatabase()
        {
            // Arrange 
            var guidToFind = "6ade1788-1e2e-48fa-81a4-cefb04805a24";
            var customerToFind = new Customer { Title = "Mr", Forename = "Bob", Surname = "Test", EmailAddress = "bob@testcom", MobileNo = "07112333344", Id = guidToFind };
            var customers = new List<Customer>() {
                customerToFind
            };

            var newContext = new Mock<IApplicationDbContext>();
            newContext.Setup(c => c.Customers).ReturnsDbSet(customers);

            var customersService = new CustomersService(newContext.Object);

            // Act
            var serviceCustomer = customersService.GetCustomerById(guidToFind);

            // Assert
            Assert.AreEqual(customerToFind, serviceCustomer);
        }

        [Test]
        public void ShouldBeAbleToAddACustomer()
        {
            // Arrange 
            var customers = new List<Customer>();
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
            // Arrange 
            var guidToUpdate = "6ade1788-1e2e-48fa-81a4-cefb04805a24";
            var customerToBeUpdated = new Customer { Title = "Mr", Forename = "Bob", Surname = "Test", EmailAddress = "bob@testcom", MobileNo = "07112333344", Id = guidToUpdate };
            var customers = new List<Customer>() {customerToBeUpdated
            };

            var newContext = new Mock<IApplicationDbContext>();
            newContext.Setup(c => c.Customers).ReturnsDbSet(customers);

            var customersService = new CustomersService(newContext.Object);
            var updateCustomer = new Customer { Title = "Mr", Forename = "Paul", Surname = "Testing", EmailAddress = "bob@testcom", MobileNo = "07112333344", Id = guidToUpdate };

            // Act
            customersService.UpdateCustomer(updateCustomer);

            // Assert
            newContext.Verify(d => d.Customers.Update(updateCustomer), Times.Once);
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