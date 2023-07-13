using myWeb.DataAccessLayer.Data;
using myWeb.DataAccessLayer.Infrastructure.IRepository;
using myWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myWeb.DataAccessLayer.Infrastructure.Repository
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly ApplicationDbContext _context;
		public ProductRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(Product product)
		{
			var productDB = _context.Products.FirstOrDefault(x => x.Id == product.Id);
			if (productDB != null)
			{
				productDB.Name = product.Name;
				productDB.Description = product.Description;
				productDB.Price = product.Price;
				if(product.ImageUrl!= null)
				{
					productDB.ImageUrl = product.ImageUrl;
				}
			}
			
		}
	}
}
