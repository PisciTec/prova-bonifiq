using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using System.Linq.Expressions;

namespace ProvaPub.Repository
{
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private TestDbContext _context;

        public OrderRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync(Expression<Func<Order, bool>> expression)
        {
            return await _context.Orders.CountAsync(expression);
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
