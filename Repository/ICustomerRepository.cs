using ProvaPub.Models;
using System.Linq.Expressions;

namespace ProvaPub.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer?> FindAsync(int id);
        Task<int> CountAsync(Expression<Func<Customer, bool>> expression);
    }
}
