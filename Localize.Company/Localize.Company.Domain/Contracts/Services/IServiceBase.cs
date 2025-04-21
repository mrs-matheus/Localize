using Localize.Company.Domain.Notifications;
using System.Linq.Expressions;

namespace Localize.Company.Domain.Contracts.Services
{
    public interface IServiceBase<T>
    {
        Task<T> Add(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        T Update(T entity);
        bool Delete(int id);
    }
}
