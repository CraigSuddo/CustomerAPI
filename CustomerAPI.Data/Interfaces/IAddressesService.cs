using CustomerAPI.Entities;
using System.Collections.Generic;

namespace CustomerAPI.Data.Interfaces
{
    public interface IAddressesService
    {
        public List<Address> GetAddresses();
        public void AddAddress(Address address);
        public void DeleteAddressById(string id);
        public Address GetAddressById(string id);
        public void UpdateAddress(Address address);
    }
}