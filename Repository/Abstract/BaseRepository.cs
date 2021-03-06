using WebApi.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class BaseRepository<T> where T : class
{
	private LogContext _context { get; set; }

	public LogContext Context
	{
		get
		{
			if (_context == null)
			{
				_context = new LogContext();
			}
			return _context;
		}
	}

	public BaseRepository()
	{
	}

	public IQueryable<T> GetAll()
	{
		return Context.Set<T>();
	}

	public virtual async Task<ICollection<T>> GetAllAsyn()
	{

		return await Context.Set<T>().ToListAsync();
	}

	public virtual T Get(int id)
	{
		return Context.Set<T>().Find(id);
	}

	public virtual async Task<T> GetAsync(int id)
	{
		return await Context.Set<T>().FindAsync(id);
	}

	public virtual T Add(T t)
	{
		Context.Set<T>().Add(t);
		Save();
		return t;
	}

	public virtual async Task<T> AddAsync(T t)
	{
		Context.Set<T>().Add(t);
		await SaveAsync();
		return t;
	}

	public virtual ICollection<T> AddRange(ICollection<T> tList)
	{
		Context.Set<T>().AddRange(tList);
		Save();
		return tList;
	}

	public virtual async Task<ICollection<T>> AddRangeAsync(ICollection<T> tList)
	{
		Context.Set<T>().AddRange(tList);
		await SaveAsync();
		return tList;
	}

	public virtual T Find(Expression<Func<T, bool>> match)
	{
		return Context.Set<T>().SingleOrDefault(match);
	}

	public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
	{
		return await Context.Set<T>().SingleOrDefaultAsync(match);
	}

	public ICollection<T> FindAll(Expression<Func<T, bool>> match)
	{
		return Context.Set<T>().Where(match).ToList();
	}

	public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
	{
		return await Context.Set<T>().Where(match).ToListAsync();
	}

	public virtual void Delete(T entity)
	{
		Context.Set<T>().Remove(entity);
		Save();
	}

	public virtual void Delete(int id)
	{
		Context.Set<T>().Remove(Get(id));
		Save();
	}

	public virtual async Task<int> DeleteAsyn(T entity)
	{
		Context.Set<T>().Remove(entity);
		return await SaveAsync();
	}

	public virtual T Update(T t, object key)
	{
		if (t == null)
			return null;
		T exist = Context.Set<T>().Find(key);
		if (exist != null)
		{
			Context.Entry(exist).CurrentValues.SetValues(t);
			Save();
		}
		return exist;
	}

	public virtual async Task<T> UpdateAsyn(T t, object key)
	{
		if (t == null)
			return null;
		T exist = await Context.Set<T>().FindAsync(key);
		if (exist != null)
		{
			Context.Entry(exist).CurrentValues.SetValues(t);
			await SaveAsync();
		}
		return exist;
	}

	public int Count()
	{
		return Context.Set<T>().Count();
	}

	public async Task<int> CountAsync()
	{
		return await Context.Set<T>().CountAsync();
	}

	public virtual void Save()
	{
		Context.SaveChanges();
	}

	public async virtual Task<int> SaveAsync()
	{
		return await Context.SaveChangesAsync();
	}

	public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
	{
		IQueryable<T> query = Context.Set<T>().Where(predicate);
		return query;
	}

	public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
	{
		return await Context.Set<T>().Where(predicate).ToListAsync();
	}

	public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
	{

		IQueryable<T> queryable = GetAll();
		foreach (Expression<Func<T, object>> includeProperty in includeProperties)
		{

			queryable = queryable.Include<T, object>(includeProperty);
		}

		return queryable;
	}

	public void RemoveAll()
	{
		Context.Set<T>().RemoveRange(GetAll());
		Save();
	}

	private bool disposed = false;
	protected virtual void Dispose(bool disposing)
	{
		if (!this.disposed)
		{
			if (disposing)
			{
				Context.Dispose();
			}
			this.disposed = true;
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

}