﻿using myWeb.DataAccessLayer.Data;
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
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _context;
		public CategoryRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}
		public void Update(Category category)
		{
			_context.Categories.Update(category);
		}
	}
}
