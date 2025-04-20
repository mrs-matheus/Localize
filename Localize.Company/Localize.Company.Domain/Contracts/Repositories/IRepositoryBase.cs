using System.Linq.Expressions;

namespace Localize.Company.Domain.Contracts.Repositories
{
    public interface IRepositoryBase<T>
    {
        Task<T> Add(T entity);

        void Add(IEnumerable<T> entities);

        T Get(int id);

        IEnumerable<T> GetAll();

        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        T Update(T entity);

        bool Delete(int id);
    }
}
