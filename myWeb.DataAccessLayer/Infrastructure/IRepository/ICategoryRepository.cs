using myWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeb.DataAccessLayer.Infrastructure.IRepository
{
	public interface ICategoryRepository: IRepository<Category>
	{
		void Update(Category category);
		
	}
}
