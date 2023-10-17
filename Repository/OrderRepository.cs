using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using System.Linq.Expressions;

namespace ProvaPub.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbSet<Order> orders;
        private readonly TestDbContext _ctx;

        public OrderRepository(TestDbContext context)
        {
            _ctx = context;
            orders = context.Set<Order>();
        }

        public async Task<int> CountAsync(Expression<Func<Order, bool>> expression)
        {
            return await orders.CountAsync(expression);
        }
        public void AddOrder(Order order)
        {
            orders.Add(order);
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }

    }
}
