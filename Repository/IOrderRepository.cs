using ProvaPub.Models;
using System.Linq.Expressions;

namespace ProvaPub.Repository
{
    public interface IOrderRepository
    {
        Task<int> CountAsync(Expression<Func<Order, bool>> expression);
        void AddOrder(Order order);
        void SaveChanges();
    }
}
