using Localize.Company.Domain.Contracts.Repositories;
using Localize.Company.Domain.Entities;
using Localize.Company.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Localize.Company.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected LocalizeCompanyContext _context;
        public RepositoryBase(LocalizeCompanyContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Add(T entity)
        {
            _context.Set<T>().Add(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual void Add(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Set<T>().Add(entity).State = EntityState.Added;
            }

            _context.SaveChangesAsync();
        }

        public virtual T Get(int id) => _context.Set<T>().FirstOrDefault(e => e.Id == id);

        public virtual IEnumerable<T> GetAll() => _context.Set<T>().ToList();

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
            => _context.Set<T>().Where(predicate);

        public virtual T Update(T entity)
        {
            _context.Set<T>().Update(entity).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return entity;
        }

        public virtual bool Delete(int id)
        {
            _context.Set<T>().Remove(Get(id)).State = EntityState.Deleted;
            _context.SaveChangesAsync();
            return true;
        }
    }
}
