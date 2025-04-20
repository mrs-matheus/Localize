using Localize.Company.Domain.Contracts.Repositories;
using Localize.Company.Domain.Contracts.Services;
using Localize.Company.Domain.Entities;
using System.Linq.Expressions;

namespace Localize.Company.Domain.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : EntityBase
    {
        private readonly IRepositoryBase<T> _repository;
        public ServiceBase(IRepositoryBase<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<T> Add(T entity)
        {
            return await _repository.Add(entity);
        }

        public virtual T Get(int id)
        {
            return _repository.Get(id);
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _repository.Get(predicate);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual T Update(T entity)
        {
            return _repository.Update(entity);
        }

        public virtual bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
