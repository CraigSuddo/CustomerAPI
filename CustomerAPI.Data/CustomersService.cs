using CustomerAPI.Data.Interfaces;
using CustomerAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CustomerAPI.Data
{
    public class CustomersService : ICustomersService
    {
        private IApplicationDbContext _dbContext { get; set; }

        public CustomersService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Customer> GetCustomers()
        {
            return _dbContext.Customers.ToList();
        }

        public void AddCustomer(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }

        public void DeleteCustomerById(string id)
        {
            var customer = GetCustomerById(id);
            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
        }

        public Customer GetCustomerById(string id)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.Id == id);
        }

        public void UpdateCustomer(Customer updateCustomer)
        {
            _dbContext.Customers.Update(updateCustomer);
            _dbContext.SaveChanges();
        }
    }
}
