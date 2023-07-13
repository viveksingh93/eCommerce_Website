﻿using Microsoft.EntityFrameworkCore;
using myWeb.DataAccessLayer.Data;
using myWeb.DataAccessLayer.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myWeb.DataAccessLayer.Infrastructure.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{

		private readonly ApplicationDbContext _context;
		private readonly IRepository<T> _repository;
		private readonly DbSet<T> _dbSet;

		public Repository(ApplicationDbContext context)
		{
			_context = context;
			//_context.Products.Include(x => x.Category);
			_dbSet = _context.Set<T>();
		}

		public void Add(T entity)
		{
			_dbSet.Add(entity);
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(entity);
		}
		public void DeleteRange(IEnumerable<T> entity)
		{
			_dbSet.RemoveRange(entity);
		}

		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			//return _dbSet.ToList();

			IQueryable<T> query = _dbSet;
			if (includeProperties != null)
			{
				foreach (var item in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)) 
				{
					query = query.Include(item);
				}
			}
			return query.ToList();
		}

		public T GetT(Expression<Func<T, bool>> predicate, string? includeProperties = null)
		{
			IQueryable<T> query = _dbSet;
            query= query.Where(predicate);
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.FirstOrDefault();
		}
	}
}
