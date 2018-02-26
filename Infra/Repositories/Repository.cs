using Domain.Abstractions.Entitys;
using Infra.Abstractions.Repositories;
using Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
	public class Repository<T,G> : IRepository<T> 
		where T : class, IBaseEntity<T>
		where G : class, T
	{
		protected readonly SAARAContext _saaraContext;
		protected readonly DbSet<G> _context;
		public Repository(SAARAContext saaraContext, DbSet<G> context)
		{
			_saaraContext = saaraContext;
			_context = context;
		}
		public virtual IList<T> Actives()
		{
			return _context.Where(e => !e.Exclusion.HasValue).ToList<T>();
		}
		public virtual T By(long id)
		{
			return _context.SingleOrDefault(e => e.ID == id);
		}
		public virtual T Delete(T exchanger)
		{
			if (exchanger.Exclusion.HasValue)
				PhysicallyDelete(exchanger);
			else
			{
				exchanger.Exclusion = DateTime.Now;
				Save(exchanger);
			}
			return exchanger;
		}
		public virtual T Delete(long id)
		{
			var exchanger = By(id);
			return Delete(exchanger);
		}
		private T PhysicallyDelete(T entity)
		{
			_context.Remove((G)entity);
			_saaraContext.SaveChanges();
			return entity;
		}
		public virtual IList<T> ListDeletedes()
		{
			return _context.Where(e => e.Exclusion.HasValue).ToList<T>();
		}

		public virtual T Save(T entity)
		{
			entity = entity.ID == entity.InstanceID ? create(entity) : edit(entity);
			_saaraContext.SaveChanges();
			return entity;
		}
		private T create(T entity)
		{
			_context.Add((G)entity);
			return entity;
		}
		private T edit(T entity)
		{
			_context.Update((G)entity);
			return entity;
		}
	}
}
