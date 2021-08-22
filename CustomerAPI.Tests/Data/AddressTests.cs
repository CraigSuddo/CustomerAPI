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
    public class AddressTests
    {
        [Test]
        public void ShouldBeAbleToGetAllAddresses()
        {
            var addresses = new List<Address>()
            {
                new Address() { AddressLine1 = "Test", Town = "Somewhere", Postcode = "L1 1AA", Id = "id1" },
                new Address() { AddressLine1 = "Test", Town = "Somewhere", Postcode = "L1 1AA", Id = "id2" },
            };

            var newContext = new Mock<IApplicationDbContext>();
            newContext.Setup(c => c.Addresses).ReturnsDbSet(addresses);
            var addressesService = new AddressesService(newContext.Object);

            // Act
            var serviceAddresses = addressesService.GetAddresses();

            // Assert
            Assert.AreEqual(addresses, serviceAddresses);
        }

        [Test]
        public void ShouldBeAbleToGetAnAddress()
        {
            var testAddress = new Address() { AddressLine1 = "Test", Town = "Somewhere", Postcode = "L1 1AA", Id = "id1" };
            var addresses = new List<Address>()
            {
                testAddress
            };

            var newContext = new Mock<IApplicationDbContext>();
            newContext.Setup(c => c.Addresses).ReturnsDbSet(addresses);

            var addressesService = new AddressesService(newContext.Object);

            // Act
            var serviceAddress = addressesService.GetAddressById("id1");

            // Assert
            Assert.AreEqual(testAddress, serviceAddress);
        }

        [Test]
        public void ShouldBeAbleToAddAnAddress()
        {
            var testAddress = new Address() { AddressLine1 = "Test", Town = "Somewhere", Postcode = "L1 1AA", Id = "id1" };
            var addresses = new List<Address>();

            var newContext = new Mock<IApplicationDbContext>();
            newContext.Setup(c => c.Addresses).ReturnsDbSet(addresses);
            newContext.Setup(c => c.Addresses.Add(It.IsAny<Address>())).Callback<Address>(c => addresses.Add(c));

            var addressesService = new AddressesService(newContext.Object);

            // Act
            addressesService.AddAddress(testAddress);
            var serviceAddress = addressesService.GetAddressById("id1");

            // Assert
            Assert.IsNotNull(serviceAddress);
        }

        [Test]
        public void ShouldBeAbleToUpdateAnAddress()
        {
            // Arrange 
            var guidToUpdate = "6ade1788-1e2e-48fa-81a4-cefb04805a24";
            var addressToBeUpdated = new Address { AddressLine1 = "Test", Town = "Somewhere", Postcode = "L1 1AA", Id = guidToUpdate };
            var addresses = new List<Address>() { addressToBeUpdated
            };

            var newContext = new Mock<IApplicationDbContext>();
            newContext.Setup(c => c.Addresses).ReturnsDbSet(addresses);

            var addressesService = new AddressesService(newContext.Object);
            var updateAddress = new Address { AddressLine1 = "Test", Town = "Somewhere", Postcode = "L1 1AA", Id = guidToUpdate };

            // Act
            addressesService.UpdateAddress(updateAddress);

            // Assert
            newContext.Verify(d => d.Addresses.Update(updateAddress), Times.Once);
        }

        [Test]
        public void ShouldBeAbleToDeleteAnAddress()
        {
            var addresses = new List<Address>();
            var guidToDelete = "6ade1788-1e2e-48fa-81a4-cefb04805a24";
            var addressToBeDeleted = new Address { AddressLine1 = "Test", Town = "Somewhere", Postcode = "L1 1AA", Id = guidToDelete };
            addresses.Add(addressToBeDeleted);

            // Arrange 
            var newContext = new Mock<IApplicationDbContext>();
            newContext.Setup(c => c.Addresses).ReturnsDbSet(addresses);
            newContext.Setup(c => c.Addresses.Remove(addressToBeDeleted)).Callback<Address>(s => addresses.Remove(s));

            var addressesService = new AddressesService(newContext.Object);

            // Act
            addressesService.DeleteAddressById(addressToBeDeleted.Id);
            var serviceAddresses = addressesService.GetAddresses();

            // Assert
            Assert.AreEqual(0, serviceAddresses.Count);
        }

        [Test]
        public void ShouldBeAbleToGetAllAddressesForACustomer()
        {
            var addresses = new List<Address>();
            var customerGuid = "6ade1788-1e2e-48fa-81a4-cefb04805a24";
            var address1 = new Address { AddressLine1 = "Test", Town = "Somewhere", Postcode = "L1 1AA", Id = Guid.NewGuid().ToString(), CustomerId = customerGuid };
            var address2 = new Address { AddressLine1 = "Test2", Town = "Somewhere Else", Postcode = "L6 1ZZ", Id = Guid.NewGuid().ToString(), CustomerId = customerGuid };
            addresses.Add(address1);
            addresses.Add(address2);

            // Arrange 
            var newContext = new Mock<IApplicationDbContext>();
            newContext.Setup(c => c.Addresses).ReturnsDbSet(addresses);

            var addressesService = new AddressesService(newContext.Object);

            // Act
            var serviceAddresses = addressesService.GetAddressesForCustomerId(customerGuid);

            // Assert
            Assert.AreEqual(2, serviceAddresses.Count);
        }
    }
}
