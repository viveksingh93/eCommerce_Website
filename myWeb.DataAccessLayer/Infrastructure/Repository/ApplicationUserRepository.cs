﻿using myWeb.DataAccessLayer.Data;
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
	public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUser
    {
		private readonly ApplicationDbContext _context;
		public ApplicationUserRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		
	}
}
