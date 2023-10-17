using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class OrderService
	{
         TestDbContext _ctx;

        public OrderService(TestDbContext ctx)
        {
            _ctx = ctx;
        }
        public interface IPaymentProcessor
		{
            Task<Order> PayOrder(decimal paymentValue, int customerId);
		}

        public class PixPayment : IPaymentProcessor
        {
            public async Task<Order> PayOrder(decimal paymentValue, int customerId)
			{
				Console.WriteLine($"Pagamento pelo Pix de : {paymentValue}");
                return await Task.FromResult(new Order()
                {
                    Value = paymentValue,
                    OrderDate = DateTime.Now,
                });
            }
        }

        public class CreditcardPayment : IPaymentProcessor
        {
            public async Task<Order> PayOrder(decimal paymentValue, int customerId)
            {
                Console.WriteLine($"Pagamento pelo Cartão de Crédito de :{paymentValue}");
                return await Task.FromResult(new Order()
                {
                    Value = paymentValue,
                    OrderDate = DateTime.Now,
                });
            }
        }

        public class PaypalPayment : IPaymentProcessor
        {
            public async Task<Order> PayOrder(decimal paymentValue, int customerId)
            {
                Console.WriteLine($"Pagamento pelo PayPal de : {paymentValue}");
                return await Task.FromResult(new Order()
                {
                    Value = paymentValue,
                    OrderDate = DateTime.Now,
                });
            }
        }

		public class PaymentHandler
		{
            private readonly IPaymentProcessor _paymentProcessor;
            private readonly IOrderRepository _orderRepository;

            public PaymentHandler(IPaymentProcessor paymentProcessor, IOrderRepository orderRepository)
            {
                _paymentProcessor = paymentProcessor;
                _orderRepository = orderRepository;
            }

            public async Task<Order> PayOrder(decimal paymentValue, int customerId) 
            { 
                var payment = await _paymentProcessor.PayOrder(paymentValue, customerId);

                return  payment;
            }
		}

	}
}
