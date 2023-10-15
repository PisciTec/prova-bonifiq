using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService
	{
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public ListResult<Product> ListProducts(int page)
		{
            IQueryable<Product> products = _ctx.Products.OrderBy(x=> x.Id);

            int TotalProdutos = products.Count();

            if (page != 0)
				products = products.Skip((page-1)*10);

			products = products.Take(10);

            return new ListResult<Product>() {  HasNext= products.Last() != _ctx.Products.OrderBy(x=> x.Id).Last(), TotalCount = TotalProdutos, Items = products.ToList()};
		}

	}
}
