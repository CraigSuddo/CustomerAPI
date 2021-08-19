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
    }
}
