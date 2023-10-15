using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using static ProvaPub.Services.OrderService;

namespace ProvaPub.Controllers
{
	
	/// <summary>
	/// Esse teste simula um pagamento de uma compra.
	/// O método PayOrder aceita diversas formas de pagamento. Dentro desse método é feita uma estrutura de diversos "if" para cada um deles.
	/// Sabemos, no entanto, que esse formato não é adequado, em especial para futuras inclusões de formas de pagamento.
	/// Como você reestruturaria o método PayOrder para que ele ficasse mais aderente com as boas práticas de arquitetura de sistemas?
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class Parte3Controller :  ControllerBase
	{
		[HttpGet("orders")]
		public async Task<Order> PlaceOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
			IPaymentProcessor paymentProcessor = null;

            switch (paymentMethod)
			{
                case "pix":
                    paymentProcessor = new PixPayment();
					break;
				case "creditcard":
                    paymentProcessor = new CreditcardPayment();
					break;
                case "paypal":
                    paymentProcessor = new PaypalPayment();
					break;
				default: throw new Exception("Metodo de Pagamento vazio, não é possível processar");
            }	

			var handlerPayment = new PaymentHandler(paymentProcessor);

            return await handlerPayment.PayOrder(paymentValue, customerId);
		}
	}
}
