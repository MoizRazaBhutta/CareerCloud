using CareerCloud.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T>: IDataRepository<T> where T: class
    {
        private readonly CareerCloudContext _context;
        public EFGenericRepository(CareerCloudContext context) 
        {
            _context = context;
        }

        public void Add(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Added;
            }
            _context.SaveChanges();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> contextEntity = _context.Set<T>();
            foreach (var navigationProperty in navigationProperties)
            {
                contextEntity.Include(navigationProperty);
            }
            return contextEntity.ToList();
        }

        public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> contextEntity = _context.Set<T>();
            foreach (var navigationProperty in navigationProperties)
            {
                contextEntity.Include(navigationProperty);
            }
            return contextEntity.Where(where).ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> contextEntity = _context.Set<T>();
            foreach (var navigationProperty in navigationProperties)
            {
                contextEntity.Include(navigationProperty);
            }
            return contextEntity.FirstOrDefault(where);
        }

        public void Remove(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
            _context.SaveChanges();
        }

        public void Update(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }
    }
}
