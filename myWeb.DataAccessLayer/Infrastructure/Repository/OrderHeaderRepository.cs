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
	public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
		private readonly ApplicationDbContext _context;
		public OrderHeaderRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(OrderHeader orderHeader)
		{
			_context.OrderHeaders.Update(orderHeader);
			//var categoryDB = _context.Categories.FirstOrDefault(x=>x.Id== category.Id);
			//if(categoryDB != null)
			//{
			//	categoryDB.Name = category.Name;
			//	categoryDB.DisplayOrder = category.DisplayOrder;
			//}
		}

        public void UpdateStatus(int id, string orderStatus, string? PaymentSatatus = null)
        {
            var order = _context.OrderHeaders.FirstOrDefault(x => x.Id == id);
			if (order != null)
			{
				order.OrderStatus = orderStatus;
			}
			if(PaymentSatatus != null)
			{
				order.PaymentStatus= PaymentSatatus;
			}
        }
    }
}
