using myWeb.DataAccessLayer.Data;
using myWeb.DataAccessLayer.Infrastructure.IRepository;
using myWeb.Models;
using myWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myWeb.DataAccessLayer.Infrastructure.Repository
{
	public class CartRepository : Repository<Cart>, ICartRepository
    {
		private readonly ApplicationDbContext _context;
		public CartRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

        public int IncrementCartItem(Cart cart, int count)
        {
            cart.count += count;
            return cart.count;
        }
        public int DecrementCartItem(Cart cart, int count)
        {
            cart.count -= count;
            return cart.count;
        }
    }
}
