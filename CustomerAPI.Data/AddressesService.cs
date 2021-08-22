using CustomerAPI.Data.Interfaces;
using CustomerAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerAPI.Data
{
    public class AddressesService : IAddressesService
    {
        private IApplicationDbContext _dbContext { get; set; }

        public AddressesService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Address> GetAddresses()
        {
            return _dbContext.Addresses.ToList();
        }

        public void AddAddress(Address address)
        {
            _dbContext.Addresses.Add(address);
            _dbContext.SaveChanges();
        }

        public void DeleteAddressById(string id)
        {
            var address = GetAddressById(id);
            _dbContext.Addresses.Remove(address);
            _dbContext.SaveChanges();
        }

        public Address GetAddressById(string id)
        {
            return _dbContext.Addresses.FirstOrDefault(c => c.Id == id);
        }

        public void UpdateAddress(Address address)
        {
            _dbContext.Addresses.Update(address);
            _dbContext.SaveChanges();
        }

        public List<Address> GetAddressesForCustomerId(string customerGuid)
        {
            return _dbContext.Addresses.Where(a => a.CustomerId == customerGuid).ToList();
        }
    }
}
