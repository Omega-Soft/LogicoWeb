using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected LogicoWebContext LogicoWebContext { get; set; }
        public RepositoryBase(LogicoWebContext repositoryContext)
        {
            LogicoWebContext = repositoryContext;
        }

        public IQueryable<T> GetAll() => LogicoWebContext.Set<T>().AsNoTracking();

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression) =>
            LogicoWebContext.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity) => LogicoWebContext.Set<T>().Add(entity);

        public void Update(T entity) => LogicoWebContext.Set<T>().Update(entity);

        public void Delete(T entity) => LogicoWebContext.Set<T>().Remove(entity);

        public bool Exists(Expression<Func<T, bool>> expr) => LogicoWebContext.Set<T>().Any(expr);

    }
}