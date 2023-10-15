using ProvaPub.Models;

namespace ProvaPub.Services
{
	public class OrderService
	{
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
                    Value = paymentValue
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
                    Value = paymentValue
                });
            }
        }

		public class PaymentHandler
		{
            private readonly IPaymentProcessor _paymentProcessor;

            public PaymentHandler(IPaymentProcessor paymentProcessor)
            {
                _paymentProcessor = paymentProcessor;
            }

            public async Task<Order> PayOrder(decimal paymentValue, int customerId) 
            { 
                var payment = _paymentProcessor.PayOrder(paymentValue, customerId);

                return await payment;
            }
		}

	}
}
