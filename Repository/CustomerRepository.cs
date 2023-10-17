using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using System.Linq.Expressions;

namespace ProvaPub.Repository
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        private TestDbContext _context;

        public CustomerRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> FindAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<int> CountAsync(Expression<Func<Customer, bool>> expression)
        {
            return await _context.Customers.CountAsync(predicate: expression);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
