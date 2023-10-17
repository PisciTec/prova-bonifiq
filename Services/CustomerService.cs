using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class CustomerService
    {
        TestDbContext _ctx;
        ICustomerRepository _customerRepository;
        IOrderRepository _orderRepository;
        public CustomerService()
        {
            
        }

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public CustomerService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }
        public CustomerService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public ListResult<Customer> ListCustomers(int page)
        {
            IQueryable<Customer> customers = _ctx.Customers.OrderBy(x => x.Id);

            int TotalProdutos = customers.Count();

            if (page != 0)
                customers = customers.Skip((page - 1) * 10);

            customers = customers.Take(10);

            return new ListResult<Customer>() { HasNext = customers.Last() != _ctx.Customers.OrderBy(x => x.Id).Last(), TotalCount = TotalProdutos, Items = customers.ToList() };

        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            //Business Rule: Non registered Customers cannot purchase
            var customer = await IsRegisteredCustomer(customerId);
            
            //Business Rule: A customer can purchase only a single time per month
            var customerAlreadyMadeAPurchaseResult = await CustomerAlreadyMadeAPurchaseInThisMonth(customerId);
            if (customerAlreadyMadeAPurchaseResult)
                return false;

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var firstPurchaseAndValueGreaterThan100 = await FirstPurchaseAndValueGreaterThan100(customerId,purchaseValue);
            if (firstPurchaseAndValueGreaterThan100)
                return false;

            return true;
        }

        public async Task<Customer> IsRegisteredCustomer(int customerId)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            var customer = await _customerRepository.FindAsync(customerId);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

            return customer;
        }

        public async Task<bool> CustomerAlreadyMadeAPurchaseInThisMonth(int customerId)
        {
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            int ordersInThisMonth = await _orderRepository.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            
            return ordersInThisMonth > 0;
        }

        public async Task<bool> FirstPurchaseAndValueGreaterThan100(int customerId, decimal purchaseValue)
        {
            var haveBoughtBefore = await _customerRepository.CountAsync(s => s.Id == customerId && s.Orders.Any());
     
            return haveBoughtBefore == 0 && purchaseValue > 100;
        }

    }
}
