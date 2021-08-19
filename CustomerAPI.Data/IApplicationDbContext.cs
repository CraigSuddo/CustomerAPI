using CustomerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

    }
}