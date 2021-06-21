using WebApi.DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using WebApi.DataAccess;
using System.Collections.Generic;

namespace Core.DataAccess
{
	public class LogRepository : BaseRepository<Log>
	{
		protected LogContext _context;
		public LogRepository(LogContext context)
		{
			_context = context;
		}

        public LogRepository()
        {
        }

        public List<Log> GetAll(Log entity)
		{
				return _context.Logs.ToList();
		}
		public void Add(Log entity)
		{
			_context.Add(entity);
		}
		public void Delete(int logId)
		{
			Log log = _context.Logs.Find(logId);
			_context.Remove(log);
		}
		public void Save()
		{
			_context.SaveChanges();
		}
	}
}