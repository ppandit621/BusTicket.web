using BusTicket.DataAcess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.DataAcess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BusApplicationDBContext _context;
        private DbSet <T> _dbset;
        public GenericRepository(BusApplicationDBContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public void Delete(T obj)
        {
            _dbset.Remove(obj);
        }

        public void DeleteRange(IEnumerable<T> obj)
        {
            _dbset.RemoveRange(obj);
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = _dbset;
            if (includeProperties != null) 
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }

            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate, string? includeProperties = null)
        {
            IQueryable<T> query = _dbset;
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item).Where(predicate);
                }

            }
            return query.Where(predicate).FirstOrDefault();
        }

        public void Insert(T obj)
        {
            _dbset.Add(obj);
        }
    }
}
