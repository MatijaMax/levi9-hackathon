﻿using System.Linq.Expressions;

namespace FibaInfrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly FibaContext context;

        public GenericRepository(FibaContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T? GetByID(Guid id)
        {
            return context.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}
