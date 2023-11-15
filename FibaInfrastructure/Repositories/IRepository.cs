using System.Linq.Expressions;

namespace FibaInfrastructure.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetByID(Guid id);
        void Insert(T entity);

        void InsertRange(IEnumerable<T> entities);

        void Delete(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Save();
    }
}
