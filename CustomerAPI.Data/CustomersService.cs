using CustomerAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Data
{
    public class CustomersService
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

        public void AddCustomer(Customer newCustomer)
        {
            _dbContext.Customers.Add(newCustomer);
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
