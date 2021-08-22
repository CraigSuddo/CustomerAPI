using CustomerAPI.Entities;
using System.Collections.Generic;

namespace CustomerAPI.Data.Interfaces
{
    public interface ICustomersService
    {
        public List<Customer> GetCustomers();
        public void AddCustomer(Customer customer);
        public void DeleteCustomerById(string id);
        public Customer GetCustomerById(string id);
        public void UpdateCustomer(Customer customer);
    }
}